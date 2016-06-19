// Check for the various File API support.
if (window.File && window.FileReader && window.FileList && window.Blob) {
    // Great success! All the File APIs are supported.
} else {
    alert('The File APIs are not fully supported in this browser.');
}

// declare a global variable to hold the raw data
var losslessRawImageData = null;
var image = {
    // Header for the Africa image
     header:{"NumberOfFrames":0,"ImageType":"ORIGINAL\\PRIMARY","BitsAllocated":"16","BitsStored":"12","HighBit":"11","Columns":"2320","Rows":"2828","SamplesPerPixel":"1","PhotometricInterpretation":"MONOCHROME2","PixelRepresentation":"0","WindowCenter":2048,"WindowWidth":4096,"AutoWindowCenter":"2270","AutoWindowWidth":"3470","RescaleIntercept":"0","RescaleSlope":"1","LossyRescaleIntercept":"0","LossyRescaleSlope":"16.0627450980392","RescaleType":"OD REL","PixelSpacing":"1.50000006E-001\\1.50000006E-001","PatientOrientation":"L\\F","ImageNumber":"1","StudyNumber":"481086","SeriesTime":"082237","SeriesDate":"20131002","AcquisitionTime":"082237","Manufacturer":"Agfa","ManufacturerModelName":"CR 85","ImagerPixelSpacing":"0.15\\0.15","SeriesDescription":"LOWER LEG AP","SeriesNumber":"1","IsMeasurementSafe":true,"CanImageBeManuallyCalibrated":true,"PixelDataValueRepresentation":"OB","MaxPixelValue":"4095","MinPixelValue":"0","ImageScaleFactorWidth":0.0,"ImageScaleFactorHeight":0.0,"IsOriginalImageLossyCompressed":false,"InstitutionName":"Drs Lamprecht & Partners","OverlayPixelMap":[],"SopClassUid":"1.2.840.10008.5.1.4.1.1.1","SopInstanceUid":"1.2.840.113745.39738.9167.9682.101911.20131025.1","AreReferencesValid":false,"EaArchiveId":"ARCH1~scgUmtJEG9YQ2TSHvodxK7oFTLNuyFwgPVZVFrTmRuI=","ModalityType":"CR"}
    //header: { "NumberOfFrames": 0, "ImageType": "ORIGINAL\\PRIMARY", "BitsAllocated": "16", "BitsStored": "16", "HighBit": "15", "Columns": "128", "Rows": "128", "SamplesPerPixel": "1", "PhotometricInterpretation": "MONOCHROME2", "PixelRepresentation": "1", "AutoWindowCenter": "0.000490500571", "AutoWindowWidth": "0.0009808264312", "RescaleIntercept": "0", "RescaleSlope": "8.73554E-08", "LossyRescaleIntercept": "-0.0014311871959", "LossyRescaleSlope": "1.1224997614902E-05", "ImageOrientation": "1\\0\\0\\0\\1\\-0", "ImagePosition": "-337.34375000000\\-337.34375000000\\454.930023193359", "FrameOfReferenceUid": "1.2.840.113619.2.55.3.2416578628.929.1345197942.908.8715.5", "PixelSpacing": "5.3125\\5.3125", "SliceThickness": "3.2700", "ImageNumber": "134", "ImageDate": "20120817", "ImageTime": "151447", "StudyNumber": "14209", "SeriesTime": "150618", "SeriesDate": "20120817", "AcquisitionTime": "151224", "Manufacturer": "GE MEDICAL SYSTEMS", "ManufacturerModelName": "Discovery STE", "ProtocolName": "2D WBCT/PET LEGS", "SeriesDescription": "WB_2D NAC", "SeriesNumber": "4", "IsMeasurementSafe": true, "CanImageBeManuallyCalibrated": true, "PixelDataValueRepresentation": "OB", "MaxPixelValue": "32767", "MinPixelValue": "0", "ImageScaleFactorWidth": 0.0, "ImageScaleFactorHeight": 0.0, "IsOriginalImageLossyCompressed": false, "InstitutionName": "UPMC Hillman Cancer Center", "OverlayPixelMap": [], "SopClassUid": "1.2.840.10008.5.1.4.1.1.128", "SopInstanceUid": "1.2.840.113619.2.131.2416578628.1345230887.898602", "AreReferencesValid": false, "EaArchiveId": "ARCH1~scgUmtJEG9YQ2TSHvodxK7oFTLNuyFwgPVZVFrTmRuI=", "ModalityType": "PT" }
};
var originalWc = image.header.WindowCenter;
var originalWw = image.header.WindowWidth;
var huLookup = [];
var ylookup = [];
var pixelBuffer;
var tmpcanvas = document.createElement("canvas"),
           tempContext = tmpcanvas.getContext("2d");
var logDiv = document.getElementById("outputLog");

function handleFileSelect(evt) {
    var files = evt.target.files; // FileList object

    // Loop through the FileList and render image files as thumbnails.
    for (var i = 0, f; f = files[i]; i++) {

        // Only process image files.
        //if (!f.type.match('image.*')) {
        //    continue;
        //}

        var reader = new FileReader();
        

        // Closure to capture the file information.
        reader.onload = (function (theFile) {
            return function (e) {
                // Render thumbnail.
                var span = document.createElement('span');
                span.innerHTML = ['<img class="thumb" src="', e.target.result,
                                  '" title="', escape(theFile.name), '"/>'].join('');
                document.getElementById('list').insertBefore(span, null);
                losslessRawImageData = reader.result;
                image.rawImageData = reader.result;
            };
        })(f);

        // Read in the image file as a data URL.
        //reader.readAsDataURL(f);
        reader.readAsArrayBuffer(f);
        
    }
}

document.getElementById('files').addEventListener('change', handleFileSelect, false);

function drawImage() {

    calculateHULookUpAndShiftBits();

    applyWindowLevelAndDrawImage();
}

function calculateHULookUpAndShiftBits() {
    calculateHULookUp(image.header.BitsStored);
    pixelBuffer = processPixels(image);
    tmpcanvas.height = image.header.Rows;
    tmpcanvas.width = image.header.Columns;
    tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);
}

function applyWindowLevelAndDrawImage() {
    var arrayBuffer = image.rawImageData;
    var losslessImageHeader = image.header;
    
    //var rows = 512, cols = 512;
    var rows = losslessImageHeader.Rows, cols = losslessImageHeader.Columns;
    calculateLookup(image.header.BitsStored);
    //tmpcanvas.height = rows;
    //tmpcanvas.width = cols;
    //tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);
    var imageData = tempContext.getImageData(0, 0, cols, rows);
    var pixelArrayIndex = 0;
    var pixelOffset = 0;

    var data = imageData.data;
    if (losslessImageHeader.PhotometricInterpretation == "MONOCHROME2") {
        iteratorfillImageData(data, pixelBuffer);
    }
    else if (losslessImageHeader.PhotometricInterpretation == "MONOCHROME1") {
        iteratorfillInvertImageData(data, pixelBuffer);
    }

    imageData.data = data;

    tempContext.putImageData(imageData, 0, 0);

    renderImageonCanvas(tmpcanvas);
}
function calculateHULookUp(bits) {
    var losslessImageHeader = image.header;
    var totalBits = Math.pow(2, bits);
    //var minPixel = 0, rescaleSlope = 1, rescaleIntercept = -1024;
    var minPixel = 0, rescaleSlope = parseFloat(losslessImageHeader.RescaleSlope), rescaleIntercept = parseFloat(losslessImageHeader.RescaleIntercept);
    for (var inputValue = 0; inputValue <= totalBits - 1; inputValue++) {
        //huLookup[inputValue] = Math.round(minPixel * rescaleSlope + rescaleIntercept);
        huLookup[inputValue] = minPixel * rescaleSlope + rescaleIntercept;
        minPixel++;
    }

}
function calculateLookup(bits) {
    var losslessImageHeader = image.header;
    var windowWidth = losslessImageHeader.WindowWidth ? parseFloat(losslessImageHeader.WindowWidth) : parseFloat(losslessImageHeader.AutoWindowWidth),
        windowCenter = losslessImageHeader.WindowCenter ? parseFloat(losslessImageHeader.WindowCenter) : parseFloat(losslessImageHeader.AutoWindowCenter);
    //var windowWidth = parseInt(losslessImageHeader.WindowWidth.split("\\")[0]), windowCenter = parseInt(losslessImageHeader.WindowCenter.split("\\")[0]);
    var totalBits = Math.pow(2, bits);
    var xMin = windowCenter - 0.5 - (windowWidth - 1) / 2.0;
    var xMax = windowCenter - 0.5 + (windowWidth - 1) / 2.0; //xMin + (windowWidth - 1);
    var yMax = 255;
    var yMin = 0;
    var lookup = huLookup;
    ylookup = allocatePixelArray(totalBits);
    for (var inputValue = 0; inputValue < totalBits; inputValue++) {

        if (lookup[inputValue] <= windowCenter - windowWidth/2) {
            ylookup[inputValue] = yMin;
            //y = ymin
        } else if (lookup[inputValue] > windowCenter + windowWidth/2) {
            ylookup[inputValue] = yMax;
            //y = ymax
        } else {
            //y = ((x - (c - 0.5)) / (w - 1) + 0.5) * (ymax - ymin) + ymin
            //ylookup[inputValue] = ((huLookup[inputValue] - (windowCenter - 0.5)) / (windowWidth - 1) + 0.5) * yMax;
            //(x - c) / w * (ymax- ymin) + ymin
            ylookup[inputValue] = (lookup[inputValue] - windowCenter) / windowWidth  * yMax ;
        }

        //if (lookup[inputValue] <= xMin) {
        //    ylookup[inputValue] = yMin;
        //    //y = ymin
        //} else if (lookup[inputValue] >= xMax) {
        //    ylookup[inputValue] = yMax;
        //    //y = ymax
        //} else {
        //    //y = ((x - (c - 0.5)) / (w - 1) + 0.5) * (ymax - ymin) + ymin
        //    //ylookup[inputValue] = ((huLookup[inputValue] - (windowCenter - 0.5)) / (windowWidth - 1) + 0.5) * yMax;
        //    ylookup[inputValue] = ((lookup[inputValue] - xMin) / (windowWidth - 1)) * (yMax - yMin) + yMin;
        //}

        //if (lookup[inputValue] <= xMin) {
        //    ylookup[inputValue] = yMin;
        //} else if (lookup[inputValue] >= xMax) {
        //    ylookup[inputValue] = yMax;
        //} else {
        //    ylookup[inputValue] = ((huLookup[inputValue] - (windowCenter - 0.5)) / (windowWidth - 1) + 0.5) * yMax;
            
        //    //var y = ((lookup[inputValue] - xMin) / (windowWidth - 1)) * (yMax - yMin) + yMin;
        //    //ylookup[inputValue] = Math.round(y);
        //}
    }
}
function processPixels(image) {
    var bitsStored = parseInt(image.header.BitsStored), highBit = parseInt(image.header.HighBit), bitsAllocated = parseInt(image.header.BitsAllocated), pixelRepresentation = parseInt(image.header.PixelRepresentation);
    
    var signBit = (1 << (bitsStored - 1));
    var absmask = signBit - 1; // mask off absolute value for signed values.
    var datamask = (1 << bitsStored) - 1;
    var shiftBits = highBit + 1 - bitsStored;

    //var func = viewport.getReadLossy() ? readLossy : readLossless;
    //var pBuf = func(pixData);
    //var pBuf = readLossless(pixData);
    var pixData = new Uint16Array(image.rawImageData);
    var pBuf = pixData;
    var p, j;
    //viewport.getReadLossy() is false, may be it is false when we are trying to read the images
    if ((shiftBits != 0 || bitsStored != bitsAllocated)) {
        var bufLen;
        if (pixelRepresentation == 1) {
            for (j = 0, bufLen = pBuf.length; j < bufLen; j++) {
                p = pBuf[j] >> shiftBits;
                if ((p & signBit) !== 0) {
                    pBuf[j] = (p & absmask) - signBit;
                } else {
                    pBuf[j] = (p & absmask);
                }
            }
        } else {
            for (j = 0, bufLen = pBuf.length; j < bufLen; j++) {
                pBuf[j] = (pBuf[j] >> shiftBits) & datamask;
            }
        }
    }
    return pBuf;
}

var allocatePixelArray = function (len) {
    if (window.Uint8Array) {
        return new window.Uint8Array(len);
    } else {
        return [];
        //If typed arrays are supported then even they can be returned        
    }
};

function iteratorfillImageData(data, pixeldata) {
    //var dcmRows = 512, dcmCols = 512, pixelArrayIndex = 0, pixelOffset =0;
    var dcmRows = image.header.Rows, dcmCols = image.header.Columns, pixelArrayIndex = 0, pixelOffset = 0;
    for (var yPix = 0; yPix < dcmRows; yPix++) {
        var ydcm = yPix * dcmCols;
        for (var xPix = 0; xPix < dcmCols; xPix++) {
            var offset = (ydcm + xPix) * 4;
            var pxValue = ylookup[pixeldata[pixelArrayIndex] + pixelOffset];
            //var pxValue = pixeldata[pixelArrayIndex];
            data[offset] = (pxValue);
            data[offset + 1] = (pxValue);
            data[offset + 2] = (pxValue);
            pixelArrayIndex++;
        }
    }
}
function iteratorfillInvertImageData(data, pixeldata) {
    var dcmRows = image.header.Rows, dcmCols = image.header.Columns, pixelArrayIndex = 0, pixelOffset = 0;
    for (var yPix = 0; yPix < dcmRows; yPix++) {
        var ydcm = yPix * dcmCols;
        for (var xPix = 0; xPix < dcmCols; xPix++) {
            var offset = (ydcm + xPix) * 4;
            //var pxValue = lookUpTable[pixeldata[pixelArrayIndex] + pixelOffset];
            var pxValue = ylookup[pixeldata[pixelArrayIndex] + pixelOffset];
            data[offset] = 255 - (pxValue);
            data[offset + 1] = 255 - (pxValue);
            data[offset + 2] = 255 - (pxValue);
            pixelArrayIndex++;
        }
    }
}

function renderImageonCanvas(tmpcanvas) {
    var canvasId = "canvas1";
    var canvas = document.getElementById(canvasId);
    var context = canvas.getContext("2d");
    //context.drawImage(tmpcanvas, 0, 0, tmpcanvas.width, tmpcanvas.height);
    context.drawImage(tmpcanvas, 0, 0, canvas.width, canvas.height);
    
}

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
        x = event.x; y = event.y;
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