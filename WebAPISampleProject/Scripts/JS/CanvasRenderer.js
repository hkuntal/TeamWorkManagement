function renderImageonCanvas(tmpcanvas) {
    var canvasId = "dcmCanvasLossless" + (HSK.Globals.canvasIndex + 1);
    var canvas = document.getElementById(canvasId);
    var context = canvas.getContext("2d");
    //context.drawImage(tmpcanvas, 0, 0, tmpcanvas.width, tmpcanvas.height);
    context.drawImage(tmpcanvas, 0, 0, canvas.width, canvas.height);
    HSK.Globals.canvasIndex++;
}
