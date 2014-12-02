var pixelArrayIndex = 0;
var pixelOffset = 0;
var worker = null;

function drawColorImage(arrayBuffer) {
    //Color images are 8 bit, hence convert it into it.
    var pixeldata = new Uint8Array(arrayBuffer);

    //var rows = 512, cols = 512;
    var rows = losslessImageHeader.Rows, cols = losslessImageHeader.Columns;
    var tmpcanvas = document.createElement("canvas"),
           tempContext = tmpcanvas.getContext("2d");
    tmpcanvas.height = rows;
    tmpcanvas.width = cols;
    tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);
    var imageData = tempContext.getImageData(0, 0, cols, rows);

    //calculateHULookUp(losslessImageHeader.BitsStored);
    var pixelBuffer = processPixels1(pixeldata);
    //calculateLookup(losslessImageHeader.BitsStored);

    var data = imageData.data;
    if (losslessImageHeader.PhotometricInterpretation == "RGB") {
        iteratorcolor(fillRgbImageData, data, pixelBuffer);
    }
    imageData.data = data;

    tempContext.putImageData(imageData, 0, 0);

    //var canvas1 = document.getElementById("dcmCanvasLossy");
    //var tempContext1 = canvas1.getContext("2d");
    //tempContext1.putImageData(imageData, 0, 0);
    var canvasId = "dcmCanvasLossless" + (HSK.Globals.canvasIndex + 1);
    var canvas = document.getElementById(canvasId);
    var context = canvas.getContext("2d");
    //context.drawImage(tmpcanvas, 0, 0, tmpcanvas.width, tmpcanvas.height);
    //Flip the canvas before drawing
    context.rotate(Math.PI / 4);
    context.drawImage(tmpcanvas, 0, 0, canvas.width, canvas.height);
}

function drawColorImageUsingWebworker(arrayBuffer) {
    //Color images are 8 bit, hence convert it into it.
    var pixeldata = new Uint8Array(arrayBuffer);

    //var rows = 512, cols = 512;
    var rows = losslessImageHeader.Rows, cols = losslessImageHeader.Columns;
    var tmpcanvas = document.createElement("canvas"),
           tempContext = tmpcanvas.getContext("2d");
    tmpcanvas.height = rows;
    tmpcanvas.width = cols;
    tempContext.fillRect(0, 0, tmpcanvas.width, tmpcanvas.height);
    var imageData = tempContext.getImageData(0, 0, cols, rows);

    //calculateHULookUp(losslessImageHeader.BitsStored);
    var pixelBuffer = processPixels1(pixeldata);
    //calculateLookup(losslessImageHeader.BitsStored);

    var data = imageData.data;
    if (losslessImageHeader.PhotometricInterpretation == "RGB") {
        iteratorcolor(fillRgbImageData, data, pixelBuffer);
    }
    imageData.data = data;

    tempContext.putImageData(imageData, 0, 0);

    //var canvas1 = document.getElementById("dcmCanvasLossy");
    //var tempContext1 = canvas1.getContext("2d");
    //tempContext1.putImageData(imageData, 0, 0);
    var canvasId = "dcmCanvasLossless" + (HSK.Globals.canvasIndex + 1);
    var canvas = document.getElementById(canvasId);
    var context = canvas.getContext("2d");
    //context.drawImage(tmpcanvas, 0, 0, tmpcanvas.width, tmpcanvas.height);
    context.drawImage(tmpcanvas, 0, 0, canvas.width, canvas.height);
}

function iteratorcolor(func, data, pixeldata) {
    for (var yPix = 0; yPix < losslessImageHeader.Rows; yPix++) {
        for (var xPix = 0; xPix < losslessImageHeader.Columns; xPix++) {
            var offset = (yPix * losslessImageHeader.Columns + xPix) * 4;
            func(data, pixeldata, offset);
        }
    }
}

function fillRgbImageData(data, pixelData, offset) {
        data[offset] = (pixelData[pixelArrayIndex]);
        data[offset + 1] = (pixelData[pixelArrayIndex + 1]);
        data[offset + 2] = (pixelData[pixelArrayIndex + 2]);
    
        pixelArrayIndex += 3;
}

var processPixels1 = function (pixData) {
    //var bitsStored = 12, highBit = 11, bitsAllocated = 16, pixelRepresentation = 0;
    var bitsStored = parseInt(losslessImageHeader.BitsStored), highBit = parseInt(losslessImageHeader.HighBit), bitsAllocated = parseInt(losslessImageHeader.BitsAllocated), pixelRepresentation = parseInt(losslessImageHeader.PixelRepresentation);
    var signBit = (1 << (bitsStored - 1));
    var absmask = signBit - 1; // mask off absolute value for signed values.
    var datamask = (1 << bitsStored) - 1;
    var shiftBits = highBit + 1 - bitsStored;

    //var func = viewport.getReadLossy() ? readLossy : readLossless;
    //var pBuf = func(pixData);
    //var pBuf = readLossless(pixData);
    var pBuf = getTypedArray(pixData, pixelRepresentation, parseInt(bitsAllocated), losslessImageHeader.PhotometricInterpretation);
    //var pBuf = pixData;
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
};

var getTypedArray = function (lenOrData, sign, bitsAllocated, dcmImgType) {
    var typedArray;
    if (dcmImgType == "PALETTE COLOR") {
        typedArray = new window.Uint8Array(lenOrData);
    } else if (bitsAllocated > 8) {
        if (sign == 1) {
            typedArray = new window.Int16Array(lenOrData);
        } else
            typedArray = new window.Uint16Array(lenOrData);
    } else {
        if (sign == 1) {
            typedArray = new window.Int8Array(lenOrData);
        } else
            typedArray = new window.Uint8Array(lenOrData);
    }
    return typedArray;
};

/*FUNCTIONS FOR USING WEBWORKERS FOR DISPLAYING THE COLOR IMAGE DATA*/
var createMessageForWorker = function (frameNo, pixelData) {
    
    if (!worker) {
        initialiseWorker();
    }
    
    //var bitsStored = image.getBitsStored();
    //    lookupObj = image.getLookUpObj();
    //if (image.getHeaderInfo().LUT) {
    //    lookupObj.calculateVOILookup(bitsStored);
    //} else {
    //lookupObj.calculateLookup(bitsStored);
    //calculateLookup(lossyImageHeader.BitsStored);

    var pixelBuffer = processPixels1(pixelData);
    pixelData = null;
    //}
    var pixelBuffer = image.pixelBuffer,
        dcmCols = losslessImageHeader.Rows,
        dcmRows = losslessImageHeader.Columns,
        dicomImageType = losslessImageHeader.PhotometricInterpretation,
        lookUpTable = ylookup,
        winWidth, winCenter,
        pixelOffset = image.getPixelOffset();

    //if (viewPortToUse.series().isMixedSeries
    //    && GE.ZFP.Globals.CommonFunctions.isRgbImage(image.getDicomImageType())) {
    //    winWidth = image.getWindowWidth();
    //    winCenter = image.getWindowCenter();
    //} else {
    //    winWidth = viewPortToUse.series().windowWidth();
    //    winCenter = viewPortToUse.series().windowCenter();
    //}


    //if (frameNo == 0) {
    //    var tempCanvas = getCanvasElement(dcmRows, dcmCols);
    //    var tempContext = tempCanvas.getContext("2d");
    //    tempContext.fillRect(0, 0, tempCanvas.width, tempCanvas.height);
    //    imageData = tempContext.getImageData(0, 0, dcmCols, dcmRows);
    //}
    var workerMessage = {
        frameNo: frameNo,
        rows: dcmRows,
        cols: dcmCols,
        invertImage: false,
        pixeldata: pixelBuffer,
        image: imageData,
        lookup: lookUpTable,
        imageType: dicomImageType,
        offset: pixelOffset,
        brightnessFactor: image.winWidth(),
        contrastFactor: image.winCenter()
    };

    if (isBrowserIE11) {
        workerExecutionQueue.push(workerMessage);
        manageWorkerQueue();
    } else {
        worker.postMessage(workerMessage);
    }
};

var initialiseWorker = function () {
    worker = null;
    if (typeof (Worker) !== "undefined") {
        //worker = new Worker(window.appName + '/ImageProcessingWorker.js');
        worker = new Worker('Scripts/ImageProcessingWorker.js');
        worker.onmessage = workerEventHandler;
    }
};

var workerEventHandler = function (e) {

    //Created canvas instance for IE 10 working.
    var canvastemp = document.createElement("canvas");
    canvastemp.height = e.data.headerInfo.Width;
    canvastemp.width = e.data.headerInfo.Height;
    
    var contexttemp = canvastemp.getContext("2d");
    contexttemp.fillRect(0, 0, canvastemp.width, canvastemp.height);
    contexttemp.putImageData(e.data.imageData, 0, 0);
    
    //var frameNo = e.data.frameNo;
    //canvasArray[frameNo] = {
    //    canvas: canvastemp,
    //    headerInfo: e.data.headerInfo,
    //    isCanvasLossy: false
    //};
    //e.data.imageData = null;
    //image.pixelBuffer = null; // clear the previous buffer once the canvas is created UInt8ClampedArray

    var canvasId = "dcmCanvasLossless" + (HSK.Globals.canvasIndex + 1);
    var canvas = document.getElementById(canvasId);
    var context = canvas.getContext("2d");
    //context.drawImage(tmpcanvas, 0, 0, tmpcanvas.width, tmpcanvas.height);
    context.drawImage(canvastemp, 0, 0, canvas.width, canvas.height);
    
    //Check if all the frames have been added in the array
    //if (frameNo == self.totalFramesCount - 1) {
        worker.terminate();
        worker = null;
    //}
    //if (isBrowserIE11) {
    //    isWorkerFree = true;
    //    manageWorkerQueue();
    //}
};

/*FUNCTIONS FOR USING WEBWORKERS FOR DISPLAYING THE COLOR IMAGE DATA*/