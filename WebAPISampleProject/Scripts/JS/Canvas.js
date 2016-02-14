//Javascript file for rendering images in canvas using javascript
var huLookup = [];
var ylookup = [];
var losslessImageHeader;
var DrawImage = function(imageData) {
    var canvas = document.getElementById("forCanvas");
    var context = canvas.getContext("2d");
    var imageObject = new Image();
    //var imageObject = document.getElementById("forCanvas");
    imageObject.onload = function() {
        context.drawImage(imageObject, 0, 0, 512, 512);
    };
    imageObject.height = 512;
    imageObject.width = 512;
    if (imageData) {
        imageObject.src = "data:image/png;base64, " + imageData;
    } else {
        imageObject.src = "http://3.20.165.88/WebAPISampleProject/Images/Car.JPG";
    }
};
var DrawLossyImage = function (imageData) {
    var canvas = document.getElementById("dcmCanvasLossy");
    var context = canvas.getContext("2d");
    var imageObject = new Image();
    //var imageObject = document.getElementById("forCanvas");
    imageObject.onload = function () {
        context.drawImage(imageObject, 0, 0, 512, 512);
    };
    imageObject.height = 512;
    imageObject.width = 512;
    if (imageData) {
        imageObject.src = "data:image/png;base64, " + imageData;
    } else {
        imageObject.src = "http://3.20.165.88/WebAPISampleProject/Images/Car.JPG";
    }
};
var DrawImageFromZFP = function() {
    
    //Authenticate the URL before launching the application
    var authenticationQuery = 'mode=StandAloneLaunch';

    $.ajax({
        type: 'POST',
        async: true,
        url: "http://3.20.165.88/zfp/Dicom/UrlAuthentication?" + authenticationQuery,
        //contentType: "application/json; charset=utf-8",
        //HAD to modify the content type below as it was a cross domain request. And it was not accepting a POST request
        //For details abt cross domain requests see this article https://developer.mozilla.org/en-US/docs/HTTP/Access_control_CORS?redirectlocale=en-US&redirectslug=HTTP_access_control
        
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        cache: false
    })
        .done(function (data) {
            //Don't do anything/ Just log the results
            console.log(data);
            launchStudyInZFPAndDisplayImagesReceivedFromZfpApi();
        });
};

var getPixelDataFromFileSystemIfNotFoundInIndexedDB = function (patientName) {
    var xmlhttp;
    //var url = "http://3.20.165.88/WebAPISampleProject/HTML5/GetLosslessImage?PatientName=" + patientName;
    var url = HSK.Globals.getUrlTOLaunchStudy("WebAPISampleProject/HTML5/GetLosslessImage?PatientName=" + patientName);
    if (window.XMLHttpRequest) { // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else { // code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    //xmlhttp.responseType = "arraybuffer";
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            //We will receive the image byte array here. Bind it to the Canvas directly
            var arrayBuffer = xmlhttp.response;
            //document.getElementById("myDiv").innerHTML = a.ImageData;
            //Display this data on the canvas
            if (arrayBuffer) {
                //*********************************Try to save the data in the IndexedDB********************************//
                //SavetheDataInIndexedDB(patientName, false, arrayBuffer);
                //*********************************Adding Completed ********************************//
                //Remove the extra 2656 bytes from the arrayBuffer
                //byteArray.splice(-2655, 2656);
                DrawLosslessImage(arrayBuffer, patientName);
                //DrawLosslessImageWithoutTempCanvas(byteArray);
            }
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.responseType = "arraybuffer";
    xmlhttp.send();
};

var GetPixelDataFromFileSystem = function (patientName) {
    //Check if the image is there in the IndexedDB database, and uncomment DrawLosslessImage method if u comment the below line
    //gettheDataFromIndexedDB(patientName, false, DrawLosslessImage);

    DrawLosslessImage(null, patientName);
};

var DrawImageByReadingImageDataFromFileSystem = function(patientName) {
    //First get the image header
    var url = HSK.Globals.getUrlTOLaunchStudy("WebAPISampleProject/HTML5/GetPaitentImageHederInfo?patientName=" + patientName);
    $.getJSON(url, function (data) {
        if (data == 'Image header not present') {
            alert("Image Header is not present");
        } else {
            console.log("ImageHeaderReceived: " + JSON.stringify(data));
            losslessImageHeader = data;
            //Check if the image is a color image or a grayscale image

            GetPixelDataFromFileSystem(patientName);
        }
    });
};

function launchStudyInZFPAndDisplayImagesReceivedFromZfpApi() {

    var sui = "1.2.840.113619.2.55.3.2831216740.515.1357125441.10";
    var sop = "1.2.840.113619.2.55.3.2831216740.515.1357125441.19.5";
    //This will create the zfpService object for that sui
    
    $.getJSON(getUrlTOLaunchStudy(sui), function (data) {
        if (data.indexOf('CMDERROR') == 0) {
            console.log("An error has occured when trying to launch a study");
        } else {
            console.log("Study function successfully called");
            DisplayLosslessImage(sui, sop);
            DisplayLossyImage(sui, sop);
        }
    });
}

function DisplayLossyImage(sui,sop) {
    var index = 0;
    var options = JSON.stringify({ OutputFormat: "IT_JPEG", QualityLevel: "92" });
    
    $.getJSON(getImageDataUrl(sui, sop, index, null, null, options), function(data) {
        if (data.ImageHeader.indexOf('CMDERROR') == 0) {
            console.log("$$ Lossy Image Error for " + sop + "#" + index + " at " + new Date() + " Message = " + data.ImageHeader);
            console.log("Error Occurred :" + data.ImageHeader);
        } else {
            //console.log("$$ Received lossy image - token " + token);
            //console.log("$$ Response received from server for lossy image token " + sop + "#" + index + " at " + (new Date()).getFormatedDate());
            //console.log("$$ Time For downloading lossy image token " + sop + "#" + index + " is " + (new Date() - requestSent) + " ms");
            //Pass the 64 bit encoded string
            DrawLossyImage(data.ImageData);
        }
    });
}

function DisplayLosslessImage(sui, sop) {
    var index = 0;
    var options = JSON.stringify({ OutputFormat: "IT_RAW", QualityLevel: 100 });
    var scale = 1.0;
    
    $.getJSON(getImageDataUrl(sui, sop, index, "RAW", 1, options), function (data) {
        if (data.ImageHeader.indexOf('CMDERROR') == 0) {
            console.log("$$ LosslessImage Error for " + sop + "#" + index);

        } else {
            //var dataReceived = new Date();
            //console.log("$$ Time For downloading lossless image token " + sop + "#" + index + " is " + (new Date() - requestSent) + " ms");
            //console.log("Image Size " + (data.ImageData.length * 2) + " Bytes");
            //console.log("Received lossless image - token " + token);
            DrawLosslessImage(data.ImageData);
            
        }
    });
}

function DrawLosslessImage(arrayBuffer, patientName) {

    //First the pixel data will be returned from the IndexedDB. If found continue, else get it from the server
    if (!arrayBuffer) {
        getPixelDataFromFileSystemIfNotFoundInIndexedDB(patientName);
        return;
    }

    if (losslessImageHeader.PhotometricInterpretation == "RGB") {
        drawColorImage(arrayBuffer);
        //drawColorImageUsingWebworker(arrayBuffer);
    } else {
        drawGrayScaleImage(arrayBuffer);
    }
    
}
function drawGrayScaleImage(arrayBuffer) {
    //Convert the blob object into UInt8 array because that is what it is
    var pixeldata = new Uint16Array(arrayBuffer);

    //var rows = 512, cols = 512;
    var rows = losslessImageHeader.Rows, cols = losslessImageHeader.Columns;
    var tmpcanvas = document.createElement("canvas"),
           tempContext = tmpcanvas.getContext("2d");
    tmpcanvas.height = rows;
    tmpcanvas.width = cols;
    tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);
    var imageData = tempContext.getImageData(0, 0, cols, rows);
    var pixelArrayIndex = 0;
    var pixelOffset = 0;
    calculateHULookUp(losslessImageHeader.BitsStored);
    var pixelBuffer = processPixels(pixeldata);
    calculateLookup(losslessImageHeader.BitsStored);

    var data = imageData.data;
    if (losslessImageHeader.PhotometricInterpretation == "MONOCHROME2") {
        iteratorfillImageData(data, pixelBuffer);
    }
    else if (losslessImageHeader.PhotometricInterpretation == "MONOCHROME1") {
        iteratorfillInvertImageData(data, pixelBuffer);
    }

    imageData.data = data;

    tempContext.putImageData(imageData, 0, 0);

    //var canvas1 = document.getElementById("dcmCanvasLossy");
    //var tempContext1 = canvas1.getContext("2d");
    //tempContext1.putImageData(imageData, 0, 0);
    //var canvasId = "dcmCanvasLossless" + (HSK.Globals.canvasIndex + 1);
    //var canvas = document.getElementById(canvasId);
    //var context = canvas.getContext("2d");
    ////context.drawImage(tmpcanvas, 0, 0, tmpcanvas.width, tmpcanvas.height);
    //context.drawImage(tmpcanvas, 0, 0, canvas.width, canvas.height);

    renderImageonCanvas(tmpcanvas);
}
//This functionality does not work
function DrawLosslessImageWithoutTempCanvas(pixeldata) {
    //It does not work without temp canvas, as the imageData.Data property is read only and cannot be edited
    var rows = 512, cols = 512;
    var tmpcanvas = document.createElement("canvas"),
           tempContext = tmpcanvas.getContext("2d");
    tmpcanvas.height = rows;
    tmpcanvas.width = cols;
    tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);
    var imageData = tempContext.getImageData(0, 0, rows, cols);
    var pixelArrayIndex = 0;
    var pixelOffset = 0;
    calculateHULookUp(12);
    var pixelBuffer = processPixels(pixeldata);
    calculateLookup(12);

    //var data = imageData.data;
    var data = new Uint8ClampedArray(262144 * 4);
    //set the pixel values as 0,0,0,255
    for (var i = 3; i < data.length; i=i+4) {
        data[i] = 255;
    }
    iteratorfillImageData(data, pixelBuffer);
    var imgData = new ImageData(512, 512, data);

    //imageData.data = data;

    //tempContext.putImageData(imageData, 0, 0);

    var canvas = document.getElementById("dcmCanvasLossless");
    var context = canvas.getContext("2d");
    //context.drawImage(tmpcanvas, 0, 0,tmpcanvas.width, tmpcanvas.height);
    //context.drawImage(imageData, 0, 0, 512, 512);
    context.putImageData(imgData, 0, 0);
}

function iteratorfillImageData(data, pixeldata) {
    //var dcmRows = 512, dcmCols = 512, pixelArrayIndex = 0, pixelOffset =0;
    var dcmRows = losslessImageHeader.Rows, dcmCols = losslessImageHeader.Columns, pixelArrayIndex = 0, pixelOffset = 0;
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
    var dcmRows = losslessImageHeader.Rows, dcmCols = losslessImageHeader.Columns, pixelArrayIndex = 0, pixelOffset = 0;
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

function calculateHULookUp(bits) {
    var totalBits = Math.pow(2, bits);
    //var minPixel = 0, rescaleSlope = 1, rescaleIntercept = -1024;
    var minPixel = 0, rescaleSlope = parseInt(losslessImageHeader.RescaleSlope), rescaleIntercept = parseInt(losslessImageHeader.RescaleIntercept);
    for (var inputValue = 0; inputValue <= totalBits - 1; inputValue++) {
        huLookup[inputValue] = Math.round(minPixel * rescaleSlope + rescaleIntercept);
        minPixel++;
    }

}
function calculateLookup(bits) {
    
    //var windowWidth = 241, windowCenter = 116;
    var windowWidth = parseInt(losslessImageHeader.WindowWidth.split("\\")[0]), windowCenter = parseInt(losslessImageHeader.WindowCenter.split("\\")[0]);
    var totalBits = Math.pow(2, bits);
    var xMin = windowCenter - 0.5 - (windowWidth - 1) / 2.0;
    var xMax = xMin + (windowWidth - 1);
    var yMax = 255;
    var yMin = 0;
    var lookup = huLookup;
    this.ylookup = allocatePixelArray(totalBits);
    for (var inputValue = 0; inputValue < totalBits; inputValue++) {
        if (lookup[inputValue] <= xMin) {
            ylookup[inputValue] = yMin;
        } else if (lookup[inputValue] >= xMax) {
            ylookup[inputValue] = yMax;
        } else {
            var y = ((lookup[inputValue] - xMin) / (windowWidth - 1)) * (yMax - yMin) + yMin;
            ylookup[inputValue] = Math.round(y);
        }
    }
}
//function readImg() {
//    pixelBuffer = processPixels(pixelData);
//}
allocatePixelArray = function (len) {
    if (window.Uint8Array) {
        return new window.Uint8Array(len);
    } else {
        return [];
        //If typed arrays are supported then even they can be returned        
    }
};

function calculateHULookup(bits, minPixel) {
    if (this.pixelRepresentation == 0 || minPixel >= 0)
        minPixel = 0;
    this.huPixelOffset = -minPixel;
    if (this.LUTType === "M") {
        //this.huLookup = this.calculateNonlinearModalityLookup(bits, minPixel);
    } else {
        var totalBits = Math.pow(2, bits);
        this.huLookup = [];

        for (var inputValue = 0; inputValue <= totalBits - 1; inputValue++) {
            this.huLookup[inputValue] = Math.round(minPixel * this.rescaleSlope + this.rescaleIntercept);
            minPixel++;
        }
    }
}

function processPixels(pixData) {
    var bitsStored = 12, highBit = 11, bitsAllocated = 16, pixelRepresentation =0;
    var signBit = (1 << (bitsStored - 1));
    var absmask = signBit - 1; // mask off absolute value for signed values.
    var datamask = (1 << bitsStored) - 1;
    var shiftBits = highBit + 1 - bitsStored;

    //var func = viewport.getReadLossy() ? readLossy : readLossless;
    //var pBuf = func(pixData);
    //var pBuf = readLossless(pixData);
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

function readLossless(losslessData) {
    var lossLessBuffer = GE.ZFP.image.getDataArray(losslessData, pixelRepresentation, bitsAllocated, dicomImageType);
    return lossLessBuffer;
}

function getImageDataUrl(sui, sop, index, comp, scale, options) {
    var url = "http://3.20.165.88/ZFP/Dicom/ImageData?";
    if (comp == null && scale == null) {
        
            if (sui)
                url += 'sui=' + sui + '&';

            if (sop)
                url += 'sop=' + sop + '&';

            url += 'index=' + index + '&';

            if (options)
                url += 'jpegOptions=' + options;

            if (url[url.length - 1] === '&')
                url = url.substring(0, url.length - 2);

            return url;
        }
        url = url + 'sui=' + sui + '&sop=' + sop + '&index=' + index + '&comp=' + comp + '&options=' + options;
        return url;
    }

function getUrlTOLaunchStudy(sui) {
    var options = JSON.stringify({ PatientId: "S3PID_9_11", PatientName: "S3_CT_HEADANGIO_PRIOR_911" });
    var studyFetchSetupCmd = {
        ClientDeviceInfo: {
            IsTouchDevice: false
        },
        SetSeriesPriorityOnStudyLoad: false
    };


    var url = "http://3.20.165.88/ZFP/Dicom/ImageData?sui=" + sui + "&patHistoryLoaded=true&assigningAuthorityId=1&applicationMode=StandAloneLaunch&getOptimizedStudy=true" +
        "&patientHistoryStudyUids=[]&patRef=" + options + "&openApiCmd=Default" + "&studyFetchSetupCmd=" + JSON.stringify(studyFetchSetupCmd);;

    return url;
}

    