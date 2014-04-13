var model = {};
var webGlRenderer;
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