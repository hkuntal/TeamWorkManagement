hsk.ImageUtilities = hsk.ImageUtilities || {};

(function (ns) {

    ns.processPixels = function (bitsStored, highBit, bitsAllocated, pixelRepresentation, rawImageData) {
        //var bitsStored = parseInt(image.header.BitsStored), highBit = parseInt(image.header.HighBit), bitsAllocated = parseInt(image.header.BitsAllocated), pixelRepresentation = parseInt(image.header.PixelRepresentation);

        var signBit = (1 << (bitsStored - 1));
        var absmask = signBit - 1; // mask off absolute value for signed values.
        var datamask = (1 << bitsStored) - 1;
        var shiftBits = highBit + 1 - bitsStored;

        //var func = viewport.getReadLossy() ? readLossy : readLossless;
        //var pBuf = func(pixData);
        //var pBuf = readLossless(pixData);
        var pixData = new Uint16Array(rawImageData);
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
    };

    ns.iteratorfillImageData = function (data, pixeldata, ylookup, dcmRows, dcmCols) {
        //var dcmRows = 512, dcmCols = 512, pixelArrayIndex = 0, pixelOffset =0;
        var pixelArrayIndex = 0, pixelOffset = 0;
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
    };

    ns.iteratorfillInvertImageData = function (data, pixeldata, ylookup, dcmRows, dcmCols) {
        var pixelArrayIndex = 0, pixelOffset = 0;
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
    };
})(hsk.ImageUtilities)