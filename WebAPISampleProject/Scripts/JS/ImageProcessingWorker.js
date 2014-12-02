var queue = [];
var ImageProcessingWorker = ImageProcessingWorker || {};
var isWorkerBusy = false;
ImageProcessingWorker.CreateImageObject = function () {
    var invertImage,
        dcmRows,
        dcmCols,
        pixelArrayIndex = 0,
        pixelOffset,
        lookUpTable,
        frameNo,
        doWindowingOnRgb,
        brightnessFactor,
        contrastFactor,
        dicomImgType,
        image,
        imgPixelData;

    this.init = function (workerMsg) {
        invertImage = workerMsg.invertImage;
        dcmRows = workerMsg.dcmRows;
        dcmCols = workerMsg.dcmCols;
        pixelOffset = workerMsg.pixelOffset;
        lookUpTable = workerMsg.lookUpTable;
        brightnessFactor = workerMsg.brightnessFactor;
        contrastFactor = workerMsg.contrastFactor;

        doWindowingOnRgb = ((contrastFactor !== 0 && contrastFactor !== 1) ||
                             brightnessFactor !== 0);
    };
    this.fillData = function (workerMsg, callback) {
        image = workerMsg.image;
        imgPixelData = workerMsg.pixelData;
        dicomImgType = workerMsg.imageType;
        frameNo = workerMsg.frameNo;
        pixelArrayIndex = 0;
        var data = image.data;
        switch (dicomImgType) {
            case "RGB":
            case "YBR_RCT":
            case "PALETTE COLOR":
                if (invertImage) {
                    iterator(fillRgbInvertImageData, data, imgPixelData);
                } else {
                    iterator(fillRgbImageData, data, imgPixelData);
                }
                break;
            case "MONOCHROME1":
                if (invertImage) {
                    iterator(fillImageData, data, imgPixelData);
                } else {
                    iterator(fillInvertImageData, data, imgPixelData);
                }
                break;
            case "MONOCHROME2":
                if (invertImage) {
                    iterator(fillInvertImageData, data, imgPixelData);
                } else {
                    iterator(fillImageData, data, imgPixelData);
                }
                break;
            default:
                console.log("Unknown photometric interpretation:  " + dicomImgType + "!");
                return;
        }
        imgPixelData = null; // Added by Hariom
        image.data = data;
        callback(frameNo, image, {
            Height: dcmRows, Width: dcmCols, WindowWidth: brightnessFactor, WindowCenter: contrastFactor,
            PhotometricInterpretation: dicomImgType
        });
    };

    function iterator(func, data, pixeldata) {
        for (var yPix = 0; yPix < dcmRows; yPix++) {
            for (var xPix = 0; xPix < dcmCols; xPix++) {
                var offset = (yPix * dcmCols + xPix) * 4;
                func(data, pixeldata, offset);
            }
        }
    }

    function fillImageData(data, pixelData, offset) {
        var pxValue = lookUpTable[pixelData[pixelArrayIndex] + pixelOffset];
        data[offset] = (pxValue);
        data[offset + 1] = (pxValue);
        data[offset + 2] = (pxValue);
        pixelArrayIndex++;
    }

    function fillInvertImageData(data, pixelData, offset) {
        var pxValue = lookUpTable[pixelData[pixelArrayIndex] + pixelOffset];
        data[offset] = 255 - (pxValue);
        data[offset + 1] = 255 - (pxValue);
        data[offset + 2] = 255 - (pxValue);
        pixelArrayIndex++;
    }

    function fillRgbImageData(data, pixelData, offset) {
        if (doWindowingOnRgb) {
            //For Changing Brightness
            data[offset] = convertToRange((pixelData[pixelArrayIndex]) + brightnessFactor);
            data[offset + 1] = convertToRange((pixelData[pixelArrayIndex + 1]) + brightnessFactor);
            data[offset + 2] = convertToRange((pixelData[pixelArrayIndex + 2]) + brightnessFactor);

            //For Changing Contrast
            data[offset] = convertToRange(((data[offset] - 128) * contrastFactor) + 128);
            data[offset + 1] = convertToRange(((data[offset + 1] - 128) * contrastFactor) + 128);
            data[offset + 2] = convertToRange(((data[offset + 2] - 128) * contrastFactor) + 128);
        } else {
            data[offset] = (pixelData[pixelArrayIndex]);
            data[offset + 1] = (pixelData[pixelArrayIndex + 1]);
            data[offset + 2] = (pixelData[pixelArrayIndex + 2]);
        }
        pixelArrayIndex += 3;
    }

    function fillRgbInvertImageData(data, pixelData, offset) {
        if (doWindowingOnRgb) {
            //For Changing Brightness
            data[offset] = 255 - convertToRange((pixelData[pixelArrayIndex]) + brightnessFactor);
            data[offset + 1] = 255 - convertToRange((pixelData[pixelArrayIndex + 1]) + brightnessFactor);
            data[offset + 2] = 255 - convertToRange((pixelData[pixelArrayIndex + 2]) + brightnessFactor);

            //For Changing Contrast
            data[offset] = 255 - convertToRange(((data[offset] - 128) * contrastFactor) + 128);
            data[offset + 1] = 255 - convertToRange(((data[offset + 1] - 128) * contrastFactor) + 128);
            data[offset + 2] = 255 - convertToRange(((data[offset + 2] - 128) * contrastFactor) + 128);
        } else {
            data[offset] = 255 - (pixelData[pixelArrayIndex]);
            data[offset + 1] = 255 - (pixelData[pixelArrayIndex + 1]);
            data[offset + 2] = 255 - (pixelData[pixelArrayIndex + 2]);
        }
        pixelArrayIndex += 3;
    }
};
self.addEventListener('message', function (e) {
    var workerMessage = {
        image: e.data.image,
        invertImage: e.data.invertImage,
        dcmRows: e.data.rows,
        dcmCols: e.data.cols,
        pixelOffset: e.data.offset,
        lookUpTable: e.data.lookup,
        imageType: e.data.imageType,
        pixelData: e.data.pixeldata,
        frameNo: e.data.frameNo,
        brightnessFactor: e.data.brightnessFactor,
        contrastFactor: e.data.contrastFactor
    };

    objWorker.init(workerMessage);

    if (isWorkerBusy) {
        queue[workerMessage.frameNo] = workerMessage;
    } else {
        isWorkerBusy = true;
        objWorker.fillData(workerMessage, postData);
    }
    workerMessage = null;
});
var objWorker = new ImageProcessingWorker.CreateImageObject();
function postData(frameNo,image, headerData) {
    isWorkerBusy = false;
    var message = {
        imageData: image,
        frameNo: frameNo,
        headerInfo : {
            Height: headerData.Height,
            Width: headerData.Width,
            WindowWidth: headerData.WindowWidth,
            WindowCenter: headerData.WindowCenter,
            PhotometricInterpretation: headerData.PhotometricInterpretation
        }
    };
    self.postMessage(message);
    var item = getNextItemFromQueue();
    if(item) {
        objWorker.fillData(item, this);
    }
}

function getNextItemFromQueue() {
    if (queue.length != 0) {
        return queue.shift();
    }
    return false;
}

function convertToRange(pixelValue) {
    if (pixelValue < 0) {
        pixelValue = 0;
    }

    if (pixelValue > 255) {
        pixelValue = 255;
    }

    return pixelValue;
}