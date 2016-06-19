var hsk = hsk || {};
hsk.Dicom = hsk.Dicom || {};
// Check for the various File API support.
if (window.File && window.FileReader && window.FileList && window.Blob) {
    // Great success! All the File APIs are supported.
} else {
    alert('The File APIs are not fully supported in this browser.');
}
var logDiv = document.getElementById("outputLog");
hsk.image = {};

(function (ns) {
    var image, canvas;
    var tmpcanvas = document.createElement("canvas"),
        tempContext = tmpcanvas.getContext("2d");
    
    ns.RenderImage = function (canvasToDrawImage) {
        canvas = canvasToDrawImage;
    };

    ns.RenderImage.prototype.drawImageOnCanvas = function (imageToBeDrawn) {
        image = imageToBeDrawn;
        
        var losslessImageHeader = image.header;
        
        //var bits = image.header.BitsStored;
        var bitsStored = parseInt(image.header.BitsStored), highBit = parseInt(image.header.HighBit), bitsAllocated = parseInt(image.header.BitsAllocated), pixelRepresentation = parseInt(image.header.PixelRepresentation);
        
        var rescaleSlope = parseFloat(losslessImageHeader.RescaleSlope), rescaleIntercept = parseFloat(losslessImageHeader.RescaleIntercept);
        var windowWidth = losslessImageHeader.WindowWidth ? parseFloat(losslessImageHeader.WindowWidth) : parseFloat(losslessImageHeader.AutoWindowWidth),
            windowCenter = losslessImageHeader.WindowCenter ? parseFloat(losslessImageHeader.WindowCenter) : parseFloat(losslessImageHeader.AutoWindowCenter);

        var rows = losslessImageHeader.Rows, cols = losslessImageHeader.Columns;
        var ylookup = hsk.LookupTable.getyLookupTable(bitsStored, rescaleSlope, rescaleIntercept, windowCenter, windowWidth);
        //calculateLookup(image.header.BitsStored);

        var pixelBuffer = hsk.ImageUtilities.processPixels(bitsStored, highBit, bitsAllocated, pixelRepresentation, image.rawImageData);
        //pixelBuffer = processPixels(image);
        
        tmpcanvas.height = rows;
        tmpcanvas.width = cols;
        tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);

        var imageData = tempContext.getImageData(0, 0, cols, rows);
        
        var data = imageData.data;
        if (losslessImageHeader.PhotometricInterpretation == "MONOCHROME2") {
            hsk.ImageUtilities.iteratorfillImageData(data, pixelBuffer, ylookup, rows, cols);
        } else if (losslessImageHeader.PhotometricInterpretation == "MONOCHROME1") {
            hsk.ImageUtilities.iteratorfillInvertImageData(data, pixelBuffer, ylookup, rows, cols);
        }

        imageData.data = data;

        tempContext.putImageData(imageData, 0, 0);

        renderImageonCanvas();
    };
    
    function renderImageonCanvas() {
        var context = canvas.getContext("2d");
        context.drawImage(tmpcanvas, 0, 0, canvas.width, canvas.height);
    }
    
})(hsk.Dicom);


function handleFileSelect(evt) {
    var files = evt.target.files; // FileList object

    // Loop through the FileList and render image files as thumbnails.
    for (var i = 0, f; f = files[i]; i++) {
        var reader = new FileReader();
        // Closure to capture the file information.
        reader.onload = (function (theFile) {
            return function (e) {
                // Render thumbnail.
                var span = document.createElement('span');
                span.innerHTML = ['<img class="thumb" src="', e.target.result,
                    '" title="', escape(theFile.name), '"/>'].join('');
                document.getElementById('list').insertBefore(span, null);
                //losslessRawImageData = reader.result;
                hsk.image.rawImageData = reader.result;
            };
        })(f);

        // Read in the image file as a data URL.
        //reader.readAsDataURL(f);
        reader.readAsArrayBuffer(f);
    }
}

document.getElementById('files').addEventListener('change', handleFileSelect, false);

function drawImage() {
    var counter = 1;
    var canvas = document.getElementById("canvas1");

    var loopMax = $("#noOfTimes").val() ? parseInt($("#noOfTimes").val()) : 1;
    var frequency = $("#frequency").val() ? parseInt($("#frequency").val()) : 1000;
    var interval = setInterval(function () {
        if (counter <= loopMax) {
            
            var renderImage = new hsk.Dicom.RenderImage(canvas);
            renderImage.drawImageOnCanvas(hsk.image);
            //Update the counter
            $("#counter").text("Counter: " + counter);
            counter++;
            // clear the canvas before redrawing
            // canvas.getContext("2d").clearRect(0, 0, canvas.width, canvas.height);
            
        } else {
            clearInterval(interval);
        }
    }, parseInt(1000 / frequency));
    
}

hsk.eventManager = function() {


    // -----------------------------------------------------------------------------MOUSE EVENTS-------------------------------------------------------------------------------------------------
    var lastMouseX, lastMouseY;

    function mouseDownEvent(event) {
        //alert(evt);
        document.getElementById("canvas1").addEventListener("mousemove", mouseMoveonCanvas);
        lastMouseX = event.x;
        lastMouseY = event.y;

    }

    function mouseUpEvent() {
        //alert("mouseDownEventcalled");
        document.getElementById("canvas1").removeEventListener("mousemove", mouseMoveonCanvas);
    }

    function mouseMoveonCanvas(event) {

        //var msg = "Handler for .mousemove() called at ";
        //msg += event.x + ", " + event.y;
        //console.log(msg);

        var currentX = event.x,
            currentY = event.y,
            diffX = currentX - lastMouseX,
            diffY = currentY - lastMouseY;

        // Calculate the equivalent difference between the window levelling values
        calculateWindowLevel(diffX, diffY);

        lastMouseX = event.x;
        lastMouseY = event.y;

        applyWindowLevelAndDrawImage();

        //console.log(msg);
    }

    //function mouseMoveEvent() {
    //    alert("mouseDownEventcalled");
    //}

    function getPosition(event) {
        var x = new Number(),
            y = new Number(),
            canvas = document.getElementById("canvas1");

        if (event.x != undefined && event.y != undefined) {
            x = event.x;
            y = event.y;
        } else {
            x = event.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
            y = event.clientY + document.body.scrollTop + document.documentElement.scrollTop;
        }

        x -= canvas.offset().left;
        y -= canvas.offset().top;

        return { x: x, y: y };
    }

    var calculateWindowLevel = function (windowLevelX, windowLevelY) {
        // self.setWindowLevelXFactor(windowLevelXFactor);
        var canvas = document.getElementById("canvas1");
        var header = image.header;

        // use window-based weighting
        header.WindowWidth = header.WindowWidth + windowLevelX * (originalWw / canvas.width);
        header.WindowCenter = header.WindowCenter + windowLevelY * (originalWc / canvas.height);
        //image.setWindowLevelValues(header.winWidth, header.winCenter);
        console.log("Window evel X,Y" + " = " + image.header.WindowCenter + "," + image.header.WindowWidth);
        // TODO: add check to run code only if photometric inter is RGB
        // set values for RGB W/L
        //image.windowLevelX = image.windowLevelX < 259 ? image.windowLevelX + windowLevelX : 258;
        //image.windowLevelX = image.windowLevelX > -255 ? image.windowLevelX + windowLevelX : -254;
        //image.contrastFactor = (image.windowLevelX > -255 && image.windowLevelX < 259) ? (259 * (image.windowLevelX + 255)) / (255 * (259 - image.windowLevelX)) : image.contrastFactor;
        //image.brightnessFactor = image.brightnessFactor + 256.0 * (windowLevelY / 512.0);
    };
};

// GLOBAL OBJECTS
hsk.image = {
// Header for the Africa image
    // header:{"NumberOfFrames":0,"ImageType":"ORIGINAL\\PRIMARY","BitsAllocated":"16","BitsStored":"12","HighBit":"11","Columns":"2320","Rows":"2828","SamplesPerPixel":"1","PhotometricInterpretation":"MONOCHROME2","PixelRepresentation":"0","WindowCenter":2048,"WindowWidth":4096,"AutoWindowCenter":"2270","AutoWindowWidth":"3470","RescaleIntercept":"0","RescaleSlope":"1","LossyRescaleIntercept":"0","LossyRescaleSlope":"16.0627450980392","RescaleType":"OD REL","PixelSpacing":"1.50000006E-001\\1.50000006E-001","PatientOrientation":"L\\F","ImageNumber":"1","StudyNumber":"481086","SeriesTime":"082237","SeriesDate":"20131002","AcquisitionTime":"082237","Manufacturer":"Agfa","ManufacturerModelName":"CR 85","ImagerPixelSpacing":"0.15\\0.15","SeriesDescription":"LOWER LEG AP","SeriesNumber":"1","IsMeasurementSafe":true,"CanImageBeManuallyCalibrated":true,"PixelDataValueRepresentation":"OB","MaxPixelValue":"4095","MinPixelValue":"0","ImageScaleFactorWidth":0.0,"ImageScaleFactorHeight":0.0,"IsOriginalImageLossyCompressed":false,"InstitutionName":"Drs Lamprecht & Partners","OverlayPixelMap":[],"SopClassUid":"1.2.840.10008.5.1.4.1.1.1","SopInstanceUid":"1.2.840.113745.39738.9167.9682.101911.20131025.1","AreReferencesValid":false,"EaArchiveId":"ARCH1~scgUmtJEG9YQ2TSHvodxK7oFTLNuyFwgPVZVFrTmRuI=","ModalityType":"CR"}
    //UPMC Image header
    //header: { "NumberOfFrames": 0, "ImageType": "ORIGINAL\\PRIMARY", "BitsAllocated": "16", "BitsStored": "16", "HighBit": "15", "Columns": "128", "Rows": "128", "SamplesPerPixel": "1", "PhotometricInterpretation": "MONOCHROME2", "PixelRepresentation": "1", "AutoWindowCenter": "0.000490500571", "AutoWindowWidth": "0.0009808264312", "RescaleIntercept": "0", "RescaleSlope": "8.73554E-08", "LossyRescaleIntercept": "-0.0014311871959", "LossyRescaleSlope": "1.1224997614902E-05", "ImageOrientation": "1\\0\\0\\0\\1\\-0", "ImagePosition": "-337.34375000000\\-337.34375000000\\454.930023193359", "FrameOfReferenceUid": "1.2.840.113619.2.55.3.2416578628.929.1345197942.908.8715.5", "PixelSpacing": "5.3125\\5.3125", "SliceThickness": "3.2700", "ImageNumber": "134", "ImageDate": "20120817", "ImageTime": "151447", "StudyNumber": "14209", "SeriesTime": "150618", "SeriesDate": "20120817", "AcquisitionTime": "151224", "Manufacturer": "GE MEDICAL SYSTEMS", "ManufacturerModelName": "Discovery STE", "ProtocolName": "2D WBCT/PET LEGS", "SeriesDescription": "WB_2D NAC", "SeriesNumber": "4", "IsMeasurementSafe": true, "CanImageBeManuallyCalibrated": true, "PixelDataValueRepresentation": "OB", "MaxPixelValue": "32767", "MinPixelValue": "0", "ImageScaleFactorWidth": 0.0, "ImageScaleFactorHeight": 0.0, "IsOriginalImageLossyCompressed": false, "InstitutionName": "UPMC Hillman Cancer Center", "OverlayPixelMap": [], "SopClassUid": "1.2.840.10008.5.1.4.1.1.128", "SopInstanceUid": "1.2.840.113619.2.131.2416578628.1345230887.898602", "AreReferencesValid": false, "EaArchiveId": "ARCH1~scgUmtJEG9YQ2TSHvodxK7oFTLNuyFwgPVZVFrTmRuI=", "ModalityType": "PT" }
    //512*512 UZA CT Image header 
    header: {"NumberOfFrames":0,"ImageType":"DERIVED\\SECONDARY\\REFORMATTED\\AVERAGE","BitsAllocated":"16","BitsStored":"16","HighBit":"15","Columns":"512","Rows":"512","SamplesPerPixel":"1","PhotometricInterpretation":"MONOCHROME2","PixelRepresentation":"1","WindowCenter":"40.0","WindowWidth":"400.0","AutoWindowCenter":"-430.5","AutoWindowWidth":"1187","RescaleIntercept":"-1024","RescaleSlope":"1","LossyRescaleIntercept":"-160","LossyRescaleSlope":"1.56862745098039","PixelPaddingValue":"-2000","ImageOrientation":"0.0\\1.0\\0.0\\0.0\\0.0\\-1.0","ImagePosition":"41.61383\\-186.12878\\-9.952515","FrameOfReferenceUid":"1.2.840.113619.2.55.3.185255957.691.1221132133.602.15550.5","PixelSpacing":"0.78125\\0.78125","SliceThickness":"3.0","ImageNumber":"74","ImageDate":"20080911","ImageTime":"105251.000","StudyNumber":"21413","SeriesTime":"105243.000","SeriesDate":"20080911","AcquisitionTime":"104227.000","Manufacturer":"GE MEDICAL SYSTEMS","ManufacturerModelName":"LightSpeed16","ProtocolName":"5.4 CHEST ABD PELVIS SMART PREP LIVER","SeriesDescription":"Reformatted","SeriesNumber":"103","RecommendedDisplayFrameRate":0,"PixelDataValueRepresentation":"OB","MaxPixelValue":"2144","MinPixelValue":"-4","ImageScaleFactorWidth":0.0,"ImageScaleFactorHeight":0.0,"IsOriginalImageLossyCompressed":false,"OverlayPixelMap":[],"MinModalityPixelValue":0,"MaxModalityPixelValue":0,"CineRate":0,"FrameTime":0.0,"PixelSpacingFactor":1.0,"SopClassUid":"1.2.840.10008.5.1.4.1.1.2","SopInstanceUid":"1.2.840.113619.2.80.3996740016.27213.1221148371.75","AreReferencesValid":false,"EaArchiveId":"ARCH11~scgUmtJEG9YQ2TSHvodxK7oFTLNuyFwgPVZVFrTmRuI=","ModalityType":"CT","StudyInstanceUid":"1.2.840.113619.2.55.3.185255957.691.1221132133.601"}
};


/***********************************************************************CHECKING MEMORY LEAKS BECAUSE OF CLOSURES IMPLEMENTATION*********************************************************/

//var theThing = null;
//var replaceThing = function () {
//    var originalThing = theThing;
//    theThing = {
//        longStr: new Array(1000000).join('*')
//    };
//};
//alert("Hariom");
//setInterval(replaceThing, 1000);