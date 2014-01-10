//Javascript file for rendering images in canvas using javascript

var DrawImage = function(imageData) {
    var canvas = document.getElementById("dcmCanvas");
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
        imageObject.src = "http://localhost:50968/Images/Car.JPG";
    }
};
var DrawImageFromZFP = function() {
    var sui = "1.2.840.113619.2.55.3.2831216740.515.1357125441.10";
    var sop = "1.2.840.113619.2.55.3.2831216740.515.1357125441.19.5";
   //This will create the zfpService object for that sui
    
    $.getJSON(getUrlTOLaunchStudy(sui), function (data) {
               if (data.indexOf('CMDERROR') == 0) {
                    console.log("An error has occured when trying to launch a study");
                } else {
                   console.log("Study function successfully called");
                   getLosslessImage(sui, sop);
                }
            });

    //getLossyImage(sui,sop);

    
};

function getLossyImage(sui,sop) {
    var index = 0;
    var options = JSON.stringify({ OutputFormat: "IT_JPEG", QualityLevel: "92" });
    
    $.getJSON(getImageDataUrl(sui, sop, index, null, null, options), function(data) {
        if (data.ImageHeader.indexOf('CMDERROR') == 0) {
            console.log("$$ Lossy Image Error for " + sop + "#" + index + " at " + (new Date()).getFormatedDate() + " Message = " + data.ImageHeader);
            console.log("Error Occurred :" + data.ImageHeader);
        } else {
            //console.log("$$ Received lossy image - token " + token);
            //console.log("$$ Response received from server for lossy image token " + sop + "#" + index + " at " + (new Date()).getFormatedDate());
            //console.log("$$ Time For downloading lossy image token " + sop + "#" + index + " is " + (new Date() - requestSent) + " ms");
            //Pass the 64 bit encoded string
            DrawImage(data.ImageData);
        }
    });
}

function getLosslessImage(sui, sop) {
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

function DrawLosslessImage(pixeldata) {
    var tmpcanvas = document.createElement("canvas"),
           tempContext = tmpcanvas.getContext("2d");
    tmpcanvas.height = 512;
    tmpcanvas.width = 512;
    tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);
    var imageData = tempContext.getImageData(0, 0, 512, 512);
    var pixelArrayIndex = 0;
    var pixelOffset = 0;
    
    //function iterator(func, data, pixeldata) {
        for (var yPix = 0; yPix < 512; yPix++) {
            for (var xPix = 0; xPix < 512; xPix++) {
                var offset = (yPix * 512 + xPix) * 4;
                fillImageData(imageData.data, pixeldata, offset);
            }
        }
    //}

    function fillImageData(data, imgPixelData, off) {
        //var pxValue = lookUpTable[imgPixelData[pixelArrayIndex] + pixelOffset];
        var pxValue = imgPixelData[pixelArrayIndex] + pixelOffset;
        data[off] = (pxValue);
        data[off + 1] = (pxValue);
        data[off + 2] = (pxValue);
        pixelArrayIndex++;
    }



    imageData.data = pixeldata;
    
    tempContext.putImageData(imageData, 0, 0);
    
    var canvas = document.getElementById("dcmCanvas");
    var context = canvas.getContext("2d");
    context.drawImage(tmpcanvas, 0, 0,
                tmpcanvas.width, tmpcanvas.height);
}


function getImageDataUrl(sui, sop, index, comp, scale, options) {
    var url = "http://3.20.165.14/ZFP/Dicom/ImageData?";
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

    var url = "http://3.20.165.14/ZFP/Dicom/ImageData?sui=" + sui + "&patHistoryLoaded=true&assigningAuthorityId=1&applicationMode=StandAloneLaunch&getOptimizedStudy=true" +
        "&patientHistoryStudyUids=[]&patRef=" + options + "&openApiCmd=Default";

    return url;
}

    