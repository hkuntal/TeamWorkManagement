WebGlRenderer = function (htmlImageCanvas, headerInformation, imgData) {
    var image = imgData,//this is the Image Object in ZFP
    headerInfo = headerInformation, //This is a custom object in ZFP
    imageCanvas = htmlImageCanvas, //The one below and this are the same
    canvas = imageCanvas,
    self = this,
    FRAG_SHADER_8 = 0,
    FRAG_SHADER_16 = 1,
    FRAG_SHADER_RGB_8 = 2,
    FRAG_SHADER_INVERT_8 = 3,
    FRAG_SHADER_INVERT_16 = 4,
    FRAG_SHADER_RGB_INVERT_8 = 5,
    FRAG_SHADER_SIGNED_8 = 6,
    FRAG_SHADER_SIGNED_INVERT_8 = 7,
    FRAG_SHADER_LOSSY_8 = 8,
    FRAG_SHADER_INVERT_LOSSY_8 = 9;
    /**************************************************************GLOBAL FUNCTIONS*******************************************************/

    function setMatrixUniforms() {
        gl.uniformMatrix4fv(shaderProgram.pMatrixUniform, false, pMatrix);
        gl.uniformMatrix4fv(shaderProgram.mvMatrixUniform, false, mvMatrix);
    }

    var model = {};
    // global data for image display
    var gl,
        shaderPrograms = {},
        mvMatrix = mat4.create(),
        pMatrix = mat4.create(),
        squareVertexPositionBuffer,
        vertexIndexBuffer,
        zoomLevel,
        viewportChanged,
        imageWidthAfterZoom,
        imageHeightAfterZoom,
        zoomChangedManually,
        THE_TEXTURE,
        CLUT_TEXTURE;

    // global data for image manipulation
    var pan = [0, 0],
        prevPanValue = [0, 0],
        flipXValues = [0, 180],
        flipYValues = [0, 180],
        flipXValueToUse = 0,
        flipYValueToUse = 0,
        rotateValues = [0, 90, 180, 270],
        rotateValueToUse = 0,
        displayedImageWidth,
        displayedImageHeight;

    var dcmFrame,
        dcmFrameType = headerInfo.PhotometricInterpretation,
        dcmFrameBits = headerInfo.BitsAllocated,
        dcmWindowCenter = headerInfo.WindowCenter,
        dcmWindowWidth = headerInfo.WindowWidth,
        dcmContrastFactor = 1,
        dcmBrightnessFactor = 0,
        dcmRescaleSlope = headerInfo.RescaleSlope,
        dcmRescaleIntercept = headerInfo.RescaleIntercept,
        dcmHeight = headerInfo.Height,
        dcmWidth = headerInfo.Width,
        dcmPixelRepresentation = headerInfo.PixelPrepresentation,
        textureCoordBuffer,
        invertImage = false,
        ratioX,
        ratioY,
        defaultRatioX = -1,
        defaultRatioY = -1;

    function setMatrixUniforms() {
        gl.uniformMatrix4fv(shaderProgram.pMatrixUniform, false, pMatrix);
        gl.uniformMatrix4fv(shaderProgram.mvMatrixUniform, false, mvMatrix);
    }
  
    // ZFP LOSSY LOSSLESS DOWNLOAD //
    this.resetVariables = function () {
        // Init. the canvas for web-gl
        try {
            gl = canvas.getContext("experimental-webgl", { preserveDrawingBuffer: true }); // canvas.getContext("experimental-webgl");
            gl.viewportWidth = canvas.width;
            gl.viewportHeight = canvas.height;
        } catch (e) {
        }

        gl.clearColor(0.0, 0.0, 0.0, 1.0);
        gl.enable(gl.DEPTH_TEST);

        //pan = [0, 0];
        //prevPanValue = [0, 0];
        //flipXValues = [0, 180];
        //flipYValues = [0, 180];
        //flipXValueToUse = 0;
        //flipYValueToUse = 0;
        //rotateValues = [0, 90, 180, 270];
        //rotateValueToUse = 0;
    };
    
    this.setLossyImageInfo = function() {
        renderFrame();
    };

    function renderFrame() {
        initBuffers();
        renderJpegOrCanvas(imgData,true);
    }

    function renderJpegOrCanvas(inputValue, convertToBase64) {
        var textureData = inputValue;
        // need to 'set/reset' the frametype to original
        dcmFrameType = model.headerInfo.PhotometricInterpretation;
        //if (image.getIsLossy()) {
        if (true) {
            if (convertToBase64) {
                textureData = "data:image/jpeg;base64," + byteStreamToBase64(inputValue);
            }

            // add the '_RGB' tag for 'lossy image'
            dcmFrameType += "_RGB";
            loadLossy(textureData);
        } else {
            // when rendering 'canvas'
            drawLossyOrCanvas(textureData, postRenderCallback);
        }
    }

    function byteStreamToBase64(byteArray) {
        var base64;
        var uInt8Array = new window.Uint8Array(byteArray);
        var stringData = "";
        while (uInt8Array.length > 65000) {
            var chunk = uInt8Array.subarray(0, 65000);
            var tempArray = uInt8Array.subarray(65000);
            uInt8Array = tempArray;
            stringData += String.fromCharCode.apply(null, chunk);
        }
        stringData += String.fromCharCode.apply(null, uInt8Array);
        base64 = window.btoa(stringData);
        return base64;
    }

    ;

    function loadLossy(data) {
        //data here is the bas64 encoded string
        // load the data onto img and then render
        THE_TEXTURE.img = new Image();
        THE_TEXTURE.img.onload = function() {
            drawLossyOrCanvas(this, renderJpegOrCanvasCallback);
            this.onload = null;
            this.src = "";
        };
        THE_TEXTURE.img.src = data;
    }

    function drawLossyOnCanvas(textureData) {

    }

    function initTextureFromJpegOrCanvas(textureData) {
        gl.bindTexture(gl.TEXTURE_2D, THE_TEXTURE);
        gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, true);
        gl.pixelStorei(gl.UNPACK_ALIGNMENT, 1);
        gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, textureData);

        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);

        gl.bindTexture(gl.TEXTURE_2D, null);
    }

    var initBuffers = function() {
        squareVertexPositionBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);

        var x,
            y,
            dimensions;

        if (gl.viewportWidth < gl.viewportHeight) {
            dimensions = getDimensionsToFitHorizontally(gl.viewportWidth, gl.viewportHeight, dcmWidth, dcmHeight);
        } else {
            dimensions = getDimensionsToFitVertically(gl.viewportWidth, gl.viewportHeight, dcmWidth, dcmHeight);
        }

        // set the width and height of an image after resizing
        displayedImageWidth = dimensions.width;
        displayedImageHeight = dimensions.height;

        // always using gl.viewportHeight because webgl considers Y axis as '1' by default always
        x = displayedImageWidth / gl.viewportHeight;
        y = displayedImageHeight / gl.viewportHeight;

        var vertices = [
            -x, -y, 0.0,
            x, -y, 0.0,
            x, y, 0.0,
            -x, y, 0.0
        ];

        var retainPreviousZoom = false;
        //If viewport is changed the check if previous zoom is to be retained or not
        if (zoomChangedManually) {
            if (viewportChanged) {
                var fitToViewportHeight;
                var fitToViewportWidth;

                if (gl.viewportWidth < gl.viewportHeight) {
                    fitToViewportHeight = y * 1 * gl.viewportWidth;
                    fitToViewportWidth = x * 1 * gl.viewportWidth;
                } else {
                    fitToViewportHeight = y * 1 * gl.viewportHeight;
                    fitToViewportWidth = x * 1 * gl.viewportHeight;
                }

                //Check whether the image needs to be scaled to a higher zoom factor
                if (fitToViewportWidth < imageWidthAfterZoom || fitToViewportHeight < imageHeightAfterZoom) {
                    //retain previous zoom level
                    retainPreviousZoom = true;
                } else {
                    //set fit to window zoom level
                    zoomLevel = 1;
                }
            }
        } else {
            // TODO: confirm the behavior. In else do nothing, values will already be there
            //set fit to window zoom level
            //zoomLevel = 1;
        }

        var corners = [];
        if (!retainPreviousZoom) {
            // do the "Zoom"
            var zoom = 1.0 * zoomLevel;

            for (var cornerIx in vertices) {
                var v = vertices[cornerIx];
                corners.push(v * zoom);
            }

            //Calculate image dimension after zoom
            if (gl.viewportWidth < gl.viewportHeight) {
                imageWidthAfterZoom = x * zoom * gl.viewportWidth;
                imageHeightAfterZoom = y * zoom * gl.viewportWidth;
            } else {
                imageWidthAfterZoom = x * zoom * gl.viewportHeight;
                imageHeightAfterZoom = y * zoom * gl.viewportHeight;
            }
        } else {
            //Calculate zoom for previous image width and height
            //Calculate image dimension after zoom
            if (gl.viewportWidth < gl.viewportHeight) {
                //Since imageWidthAfterZoom = x * zoom * gl.viewportWidth;
                zoomLevel = imageWidthAfterZoom / (x * gl.viewportWidth);
            } else {
                //Since imageWidthAfterZoom = x * zoom * gl.viewportHeight;
                zoomLevel = imageWidthAfterZoom / (x * gl.viewportHeight);
            }

            //Save new zoom level
            var zoom = 1.0 * zoomLevel;

            for (var cornerIx in vertices) {
                var v = vertices[cornerIx];
                corners.push(v * zoom);
            }
        }

        ratioX = Math.abs(corners[0]);
        ratioY = Math.abs(corners[1]);

        if (defaultRatioX === -1 || defaultRatioY === -1) {
            // if ratio is set as '-1' means its the first time display
            // save the ratio values as default
            defaultRatioX = ratioX;
            defaultRatioY = ratioY;
        }

        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(corners), gl.STATIC_DRAW);
        squareVertexPositionBuffer.itemSize = 3;
        squareVertexPositionBuffer.numItems = 4;

        // Texture coords
        textureCoordBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, textureCoordBuffer);

        var textureCoordinates = [
            0.0, 0.0,
            1.0, 0.0,
            1.0, 1.0,
            0.0, 1.0
        ];
        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoordinates), gl.STATIC_DRAW);
        textureCoordBuffer.itemSize = 2;
        textureCoordBuffer.numItems = 4;

        vertexIndexBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, vertexIndexBuffer);
        var vertexIndices = [
            0, 1, 2, 0, 2, 3
        ];
        gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(vertexIndices), gl.STATIC_DRAW);
        vertexIndexBuffer.itemSize = 1;
        vertexIndexBuffer.numItems = 6;
    };

    function getDimensionsToFitVertically(containerWidth, containerHeight, originalWidth, originalHeight) {
        var height = containerHeight,
            width = (originalWidth / originalHeight) * height;

        if (width > containerWidth) {
            height = (height / width) * containerWidth;
            width = containerWidth;
        }

        return {
            width: width,
            height: height
        };
    }

    ;

    function getDimensionsToFitHorizontally(containerWidth, containerHeight, originalWidth, originalHeight) {
        var width = containerWidth,
            height = (originalHeight / originalWidth) * width;

        if (height > containerHeight) {
            width = (width / height) * containerHeight;
            height = containerHeight;
        }

        return {
            width: width,
            height: height
        };
    }
}