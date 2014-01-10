var main = function() {
    //alert("main function has been called");

    var canvas = document.getElementById("myCanvas");
    try {
        var GL = canvas.getContext("experimental-webgl", { antialias: false });
        if (!GL) {
            alert("aah !! u r not web gl compliant");
            //Set the background color. White background with 100% visibility
            GL.clearColor(1.0, 1.0, 1.0, 1.0); // There are the RGBA colors - red, Green, Blue, Alpha. They are in the range 0.0 - 1.0, hence ideally they would need to be divided by 255
            this.GL.enable(this.GL.DEPTH_TEST); //Enable Depth Testing  
            this.GL.depthFunc(this.GL.LEQUAL); //Set Perspective View 
            
        }
    } catch(e) {
        alert("u r not web gl compliant");
        return false;
    }
};
function webGLStart() {
    var canvas = document.getElementById("myCanvas");
    initGL(canvas);
    initShaders();
    initBuffers();

    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.enable(gl.DEPTH_TEST);

    drawScene();
}