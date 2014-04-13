var gl;
function initGL(canvas) {
    try {
        gl = canvas.getContext("experimental-webgl");
        gl.viewportWidth = canvas.width;
        gl.viewportHeight = canvas.height;
    } catch (e) {
    }
    if (!gl) {
        alert("Could not initialise WebGL, sorry :-(");
    }
}

function getShader(gl, id) {
    var shaderScript = document.getElementById(id);
    if (!shaderScript) {
        return null;
    }

    var str = "";
    var k = shaderScript.firstChild;
    while (k) {
        if (k.nodeType == 3) {
            str += k.textContent;
        }
        k = k.nextSibling;
    }

    var shader;
    if (shaderScript.type == "x-shader/x-fragment") {
        shader = gl.createShader(gl.FRAGMENT_SHADER);
    } else if (shaderScript.type == "x-shader/x-vertex") {
        shader = gl.createShader(gl.VERTEX_SHADER);
    } else {
        return null;
    }

    gl.shaderSource(shader, str);
    gl.compileShader(shader);

    if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
        alert(gl.getShaderInfoLog(shader));
        return null;
    }

    return shader;
}

var shaderProgram;

function initShaders() {
    var fragmentShader = getShader(gl, "shader-fs");
    var vertexShader = getShader(gl, "shader-vs");

    shaderProgram = gl.createProgram();
    gl.attachShader(shaderProgram, vertexShader);
    gl.attachShader(shaderProgram, fragmentShader);
    gl.linkProgram(shaderProgram);

    if (!gl.getProgramParameter(shaderProgram, gl.LINK_STATUS)) {
        alert("Could not initialise shaders");
    }

    gl.useProgram(shaderProgram);

    shaderProgram.vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
    gl.enableVertexAttribArray(shaderProgram.vertexPositionAttribute);

    shaderProgram.pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
    shaderProgram.mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
}

var mvMatrix = mat4.create();
var pMatrix = mat4.create();

function setMatrixUniforms() {
    gl.uniformMatrix4fv(shaderProgram.pMatrixUniform, false, pMatrix);
    gl.uniformMatrix4fv(shaderProgram.mvMatrixUniform, false, mvMatrix);
}

var triangleVertexPositionBuffer;
var squareVertexPositionBuffer;

function initBuffers() {
    triangleVertexPositionBuffer = gl.createBuffer();
    /* We create a buffer for the triangle’s vertex positions. Vertices (don’t you just love irregular plurals?) are the points in 3D space that define the shapes we’re drawing. For our triangle, we will have three of them (which we’ll set up in a minute). This buffer is actually a bit of memory on the graphics card; by putting the vertex positions on the card once in our initialisation code and then, when we come to draw the scene, essentially just telling WebGL to “draw those things I told you about earlier”,*/
    
    gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
    
    /* This line tells WebGL that any following operations that act on buffers should use the one we specify. There’s always this concept of a “current array buffer”, and functions act on that rather than letting you specify which array buffer you want to work with. Odd, but I’m sure there a good performance reasons behind it…
    */
    var vertices = [
         0.0, 1.0, 0.0,
        -1.0, -1.0, 0.0,
         1.0, -1.0, 0.0
    ];
    
    /* Next, we define our vertex positions as a JavaScript list. You can see that they’re at the points of an isosceles triangle with its centre at (0, 0, 0).*/
    
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

    /* Now, we create a Float32Array object based on our JavaScript list, and tell WebGL to use it to fill the current buffer, which is of course our triangleVertexPositionBuffer. We’ll talk more about Float32Arrays in a future lesson, but for now all you need to know is that they’re a way of turning a JavaScript list into something we can pass over to WebGL for filling its buffers.*/
    
    triangleVertexPositionBuffer.itemSize = 3;
    triangleVertexPositionBuffer.numItems = 3;
    
    /*The last thing we do with the buffer is to set two new properties on it. These are not something that’s built into WebGL, but they will be very useful later on. One nice thing (some would say, bad thing) about JavaScript is that an object doesn’t have to explicitly support a particular property for you to set it on it. So although the buffer object didn’t previously have itemSize and numItems properties, now it does. We’re using them to say that this 9-element buffer actually represents three separate vertex positions (numItems), each of which is made up of three numbers (itemSize). */
    
    // SIMILARLY WE DO FOR SQUARE ARRAY BUFFER'
    squareVertexPositionBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
    
    vertices = [
         1.0, 1.0, 0.0,
        -1.0, 1.0, 0.0,
         1.0, -1.0, 0.0,
        -1.0, -1.0, 0.0
    ];

    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
    squareVertexPositionBuffer.itemSize = 3;
    squareVertexPositionBuffer.numItems = 4;
}

function drawScene() {
    gl.viewport(0, 0, gl.viewportWidth, gl.viewportHeight);
    
    /*The first step is to tell WebGL a little bit about the size of the canvas using the viewport function; we’ll come back to why that’s important in a (much!) later lesson; for now, you just need to know that the function needs calling with the size of the canvas before you start drawing. Next, we clear the canvas in preparation for drawing on it:
    */
    
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
    
    mat4.perspective(45, gl.viewportWidth / gl.viewportHeight, 0.1, 100.0, pMatrix);
    
    /* Here we’re setting up the perspective with which we want to view the scene.  By default, WebGL will draw things that are close by the same size as things that are far away (a style of 3D known as orthographic projection).  In order to make things that are further away look smaller, we need to tell it a little about the perspective we’re using.  For this scene, we’re saying that our (vertical) field of view is 45°, we’re telling it about the width-to-height ratio of our canvas, and saying that we don’t want to see things that are closer than 0.1 units to our viewpoint, and that we don’t want to see things that are further away than 100 units.*/
    
    /*As you can see, this perspective stuff is using a function from a module called mat4, and involves an intriguingly-named variable called pMatrix. */
    
    mat4.identity(mvMatrix);
    
    /*The first step is to “move” to the centre of the 3D scene.  In OpenGL, when you’re drawing a scene, you tell it to draw each thing you draw at a “current” position with a “current” rotation — so, for example, you say “move 20 units forward, rotate 32 degrees, then draw the robot”, the last bit being some complex set of “move this much, rotate a bit, draw that” instructions in itself.  This is useful because you can encapsulate the “draw the robot” code in one function, and then easily move said robot around just by changing the move/rotate stuff you do before calling that function.

The current position and current rotation are both held in a matrix; as you probably learned at school, matrices can represent translations (moves from place to place), rotations, and other geometrical transformations.  For reasons I won’t go into right now, you can use a single 4×4 matrix (not 3×3) to represent any number of transformations in 3D space; you start with the identity matrix — that is, the matrix that represents a transformation that does nothing at all — then multiply it by the matrix that represents your first transformation, then by the one that represents your second transformation, and so on.   The combined matrix represents all of your transformations in one. The matrix we use to represent this current move/rotate state is called the model-view matrix, and by now you have probably worked out that the variable mvMatrix holds our model-view matrix, and the mat4.identity function that we just called sets it to the identity matrix so that we’re ready to start multiplying translations and rotations into it.  Or, in other words, it’s moved us to an origin point from which we can move to start drawing our 3D world.

Sharp-eyed readers will have noticed that at the start of this discussion of matrices I said “in OpenGL”, not “in WebGL”.  This is because WebGL doesn’t have this stuff built in to the graphics library. Instead, we use a third-party matrix library — Brandon Jones’s excellent glMatrix — plus some nifty WebGL tricks to get the same effect. More about that niftiness later.

Right, let’s move on to the code that draws the triangle on the left-hand side of our canvas.*/
    
    mat4.translate(mvMatrix, [-0.0, -1.0, -6.0]);
    
    /*Having moved to the centre of our 3D space with by setting mvMatrix to the identity matrix, we start the triangle  by moving 1.5 units to the left (that is, in the negative sense along the X axis), and seven units into the scene (that is, away from the viewer; the negative sense along the Z axis).  (mat4.translate, as you might guess, means “multiply the given matrix by a translation matrix with the following parameters”.)*/
    
    //WE WANT TO DRAW THE TRIANGLE SO WE NEED TO BRING THAT INTO THE CURRENT ARRAY BUFFER
    
    gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
    gl.vertexAttribPointer(shaderProgram.vertexPositionAttribute, triangleVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);
    
    setMatrixUniforms();
    
    /*This tells WebGL to take account of our current model-view matrix (and also the projection matrix, about which more later).   This is required because all of this matrix stuff isn’t built in to WebGL.  The way to look at it is that you can do all of the moving around by changing the mvMatrix variable you want, but this all happens in JavaScript’s private space.  setMatrixUniforms, a function that’s defined further up in this file, moves it over to the graphics card. */
    
    gl.drawArrays(gl.TRIANGLES, 0, triangleVertexPositionBuffer.numItems);
    
    /* Or, put another way, “draw the array of vertices I gave you earlier as triangles, starting with item 0 in the array and going up to the numItemsth element”.*/
    
    //DRAW THE SQUARE NOW
    mat4.translate(mvMatrix, [3.0, 0.0, -10.0]);
    
    /*We start by moving our model-view matrix three units to the right.  Remember, we’re currently already 1.5 to the left and 7 away from the screen, so this leaves us 1.5 to the right and 7 away.  Next: */
    
    gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
    gl.vertexAttribPointer(shaderProgram.vertexPositionAttribute, squareVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);
    
    //So, we tell WebGL to use our square’s buffer for its vertex positions…

    setMatrixUniforms();
    
    //…we push over the model-view and projection matrices again
    
    gl.drawArrays(gl.TRIANGLE_STRIP, 0, squareVertexPositionBuffer.numItems);
    
    /* Draw the points.  What, you may ask, is a triangle strip?  Well, it’s a strip of triangles :-)   More usefully, it’s a strip of triangles where the first three vertices you give specify the first triangle, then the last two of those vertices plus the next one specify the next triangle, and so on.  In this case, it’s a quick-and-dirty way of specifying a square.  In more complex cases, it can be a really useful way of specifying a complex surface in terms of the triangles that approximate it.*/
    

    

}

function webGLStart() {
    var canvas = document.getElementById("myCanvas");
    initGL(canvas);
    initShaders();
    initBuffers();

    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.enable(gl.DEPTH_TEST);

    drawScene();
}