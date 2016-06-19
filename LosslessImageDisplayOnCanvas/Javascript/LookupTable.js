var hsk = hsk || {};
hsk.LookupTable = hsk.LookupTable || {};

(function (ns) {
    //hsk.LookupTable = hsk.LookupTable || {};
    var huLookUpTables = {};
    var yLookUpTables = {};

    var getHuLookupTable = function(bits, rescaleSlope, rescaleIntercept) {
        var totalBits = Math.pow(2, bits);
        var huLookup = getHuLookupIfExist(bits, rescaleSlope, rescaleIntercept);
        var stringBits = bits.toString() + rescaleSlope.toString() + rescaleIntercept.toString();
        if (!huLookup) {
            huLookup = [];
            var minPixel = 0;
            for (var inputValue = 0; inputValue <= totalBits - 1; inputValue++) {
                huLookup[inputValue] = minPixel * rescaleSlope + rescaleIntercept;
                minPixel++;
            }

            huLookUpTables[stringBits] = huLookup;
        }
        return huLookUpTables[stringBits];
    };
    
    var getHuLookupIfExist = function (imageStoredbits, rescaleSlope, rescaleIntercept) {
        var stringBits = imageStoredbits.toString() + rescaleSlope.toString() + rescaleIntercept.toString();
        return huLookUpTables[stringBits] ? huLookUpTables[stringBits] : null;
    };

    var getYLookupIfExist = function (imageStoredbits, windowCenter, windowWidth) {
        var stringBits = imageStoredbits.toString() + windowCenter.toString() + windowWidth.toString();
        return yLookUpTables[stringBits] ? yLookUpTables[stringBits] : null;
    };

    var allocatePixelArray = function (len) {
        if (window.Uint8Array) {
            return new window.Uint8Array(len);
        } else {
            return [];
            //If typed arrays are supported then even they can be returned        
        }
    };

    ns.getyLookupTable = function (bits, rescaleSlope, rescaleIntercept, windowCenter, windowWidth) {
        var ylookup = getYLookupIfExist(bits, windowCenter, windowWidth);
        if (!ylookup) {
            var stringBits = bits.toString() + windowCenter.toString() + windowWidth.toString();
            var totalBits = Math.pow(2, bits);
            var yMax = 255;
            var yMin = 0;
            var lookup = getHuLookupTable(bits, rescaleSlope, rescaleIntercept); 
            ylookup = allocatePixelArray(totalBits);
            for (var inputValue = 0; inputValue < totalBits; inputValue++) {

                if (lookup[inputValue] <= windowCenter - windowWidth / 2) {
                    ylookup[inputValue] = yMin;
                } else if (lookup[inputValue] > windowCenter + windowWidth / 2) {
                    ylookup[inputValue] = yMax;
                } else {
                    ylookup[inputValue] = (lookup[inputValue] - windowCenter) / windowWidth * yMax;
                }

            }
            yLookUpTables[stringBits] = ylookup;
        }
        return ylookup;
    };

    
}(hsk.LookupTable));


