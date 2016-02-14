var model = {};
var webGlRenderer;


    window.onload = function () {
        var fileInput = document.getElementById('fileInput');
        var fileDisplayArea = document.getElementById('fileDisplayArea');

        fileInput.addEventListener('change', function (e) {
            var file = fileInput.files[0];
            var textType = /text.*/;
            var imageType = /image.*/;
            //var blobType = /text.*/;

            if (file.type.match(textType)) {
                var reader = new FileReader();

                reader.onload = function(e) {
                    fileDisplayArea.innerText = reader.result;
                };

                reader.readAsText(file);
            } else if (file.type.match(imageType)) {
                var reader = new FileReader();

                reader.onload = function(e) {
                    fileDisplayArea.innerHTML = "";

                    var img = new Image();
                    img.src = reader.result;

                    fileDisplayArea.appendChild(img);
                };
                reader.readAsDataURL(file);
            }
            else if (file.type == "") { // Means it is a blob file
                var reader = new FileReader();

                reader.onload = function(e) {
                    var arrayBuffer = reader.result;
                    var rows = 512, cols = 512;
                    // Display image on canvas
                    var tmpcanvas = document.createElement("canvas"),
                    tempContext = tmpcanvas.getContext("2d");
                    tmpcanvas.height = rows;
                    tmpcanvas.width = cols;
                    tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);
                    var imageData = tempContext.getImageData(0, 0, cols, rows);
                    var data = imageData.data;
                        
                    // Convert the array buffer into UInt8 array
                    var actualImageData = new window.Uint8Array(arrayBuffer);
                    var pixelArrayIndex = 0, pixelOffset = 0;
                    // Fill the imageData from the pixel data
                    for (var yPix = 0; yPix < rows; yPix++) {
                        var ydcm = yPix * cols;
                        for (var xPix = 0; xPix < cols; xPix++) {
                            var offset = (ydcm + xPix) * 4;
                            //var pxValue = ylookup[pixeldata[pixelArrayIndex] + pixelOffset];
                            var pxValue = actualImageData[pixelArrayIndex];
                            data[offset] = (pxValue);
                            data[offset + 1] = (pxValue);
                            data[offset + 2] = (pxValue);
                            pixelArrayIndex++;
                        }
                    }
                        
                    // draw the data back on the temporary canvas
                    tempContext.putImageData(imageData, 0, 0);
                        
                    // paste it on the actual canvas
                    var canvasId = "canvas1";
                    var canvas = document.getElementById(canvasId);
                    var context = canvas.getContext("2d");
                    //context.drawImage(tmpcanvas, 0, 0, tmpcanvas.width, tmpcanvas.height);
                    context.drawImage(tmpcanvas, 0, 0, canvas.width, canvas.height);
                        
                };

                reader.readAsArrayBuffer(file);
  
            }
            else {
                fileDisplayArea.innerText = "File not supported!";
            }
        });
    }

    
var initWebGlCanvas = function () {
    // init the 'webgl' renderer by passing the ImageCanvas
    var imageCanvas = document.getElementById("canvasLossyImage");
        //headerInfoFromImage = fetchHeaderInfoFromImage();

    if (webGlRenderer == undefined) {
        webGlRenderer = new WebGlRenderer(imageCanvas, model.headerData, model.pixelData);
        //webGlRenderer.init();
        webGlRenderer.setLossyImageInfo();
    } else {
        webGlRenderer.resetVariables();
        webGlRenderer.setImageCanvas(imageCanvas);
        webGlRenderer.setHeaderInfo(model.headerData);
        webGlRenderer.setImage(self.getImage());
    }
};

function DisplayLossyImage() {
    getLossyImageFromServer();
    getImageHeaderInfo();
    //var obj = new WebGlRenderer(model.headerData, model.pixelData);
    //obj.setLossyImageInfo();
}
function startDisplayLossyImage() {
    var obj = new WebGlRenderer(model.headerData, model.pixelData);
    obj.setLossyImageInfo();
}
var getLossyImageFromServer = function () {

    var xmlhttp;
    var url = "http://localhost:50968/HTML5/GetLossyImageForWebGL";
    if (window.XMLHttpRequest) { // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else { // code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.responseType = "arraybuffer";
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            //We will receive the image byte array here. Bind it to the Canvas directly
            var arrayBuffer = xmlhttp.response;
            //document.getElementById("myDiv").innerHTML = a.ImageData;
            //Display this data on the canvas
            if (arrayBuffer) {
                //Convert the blob object into UInt8 array because that is what it is
                //var byteArray = new Uint16Array(arrayBuffer);
                model.pixelData = arrayBuffer;
                //DrawLosslessImage(byteArray);
                startDisplayLossyImage();
            }
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
};

function getImageHeaderInfo() {
    var xmlhttp;
    var url = "http://localhost:50968/HTML5/GetImageHeaderInfoForWebGL";
    if (window.XMLHttpRequest) { // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else { // code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.responseType = "text";
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            //We will receive the image byte array here. Bind it to the Canvas directly
            var data = xmlhttp.responseText;
            //document.getElementById("myDiv").innerHTML = a.ImageData;
            //Display this data on the canvas
            if (data) {
                //Convert the blob object into UInt8 array because that is what it is
                //var byteArray = new Uint16Array(arrayBuffer);
                var d = jQuery.parseJSON(data);
                model.headerData = d.Root;
                getLossyImageFromServer();
                //DrawLosslessImage(byteArray);
            }
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

var getLosslessImageFromServer = function () {

    var xmlhttp;
    var url = "http://localhost:50968/HTML5/GetLosslessImageForWebGL";
    if (window.XMLHttpRequest) { // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else { // code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.responseType = "arraybuffer";
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            //We will receive the image byte array here. Bind it to the Canvas directly
            var arrayBuffer = xmlhttp.response;
            //document.getElementById("myDiv").innerHTML = a.ImageData;
            //Display this data on the canvas
            if (arrayBuffer) {
                //Convert the blob object into UInt8 array because that is what it is
                //var byteArray = new Uint16Array(arrayBuffer);
                model.pixelData = arrayBuffer;
                //DrawLosslessImage(byteArray);
            }
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
};