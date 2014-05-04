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
    var pixelArrayIndex = 0;
    var pixelOffset = 0;
    //calculateHULookUp(losslessImageHeader.BitsStored);
    //var pixelBuffer = processPixels(pixeldata);
    //calculateLookup(losslessImageHeader.BitsStored);

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
    var canvasId = "dcmCanvasLossless" + (HSK.Globals.canvasIndex + 1);
    var canvas = document.getElementById(canvasId);
    var context = canvas.getContext("2d");
    //context.drawImage(tmpcanvas, 0, 0, tmpcanvas.width, tmpcanvas.height);
    context.drawImage(tmpcanvas, 0, 0, canvas.width, canvas.height);
}
