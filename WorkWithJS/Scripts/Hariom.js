//Trying to test if the variables defined within a function without this become global
/*Yes that is right. Parameters without "this" kind of become Global or say static parameters*/


//The statement below defines empty objects which will act as namespaces

//Pleas note that declaring a variable as 'var' inside a function will not make it a global variable, but it will remain a local variable to
//the class. If declared outside the function than yes it will be a global variable
GE = {};
GE.ZFP = {};
var k = 5000;
GE.ZFP.ImageDataManager = function () {
    j = 10;
    this.i = 5;
    this.ReturnAnInt = function () {
        return k;
    }
}

function sayHello2(name) {
    this.text = 'Hello ' + name; // local variable
    sayAlert = function () { alert(text); }
    //return sayAlert;
}
//sayHello2('Hariom');
var im = new sayHello2('Kuntal');
//im();
alert(im.text);
alert(sayAlert());


