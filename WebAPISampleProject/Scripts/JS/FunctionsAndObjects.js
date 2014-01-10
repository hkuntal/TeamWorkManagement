//This is to test the Functions and Objects as sprcified in JavaScript


// Create a constructor function called Quo.
// It makes an object with a status property.
var Quo = function (string) {
    this.status = string;
    //This to check if global functions can exist in a class

    function checkAlert() {
        document.writeln("Hariom");
    }
    var chekcAlert1 = function() {
        document.writeln("Kuntal");
        };
    checkAlert2 = function() {
        document.writeln("singh");
    };
    this.checkAlert3 = function() {
        document.writeln("\n this is specific to object");
    };

};
// Give all instances of Quo a public method
// called get_status.
Quo.prototype.get_status = function () {
    return this.status;
};

//Adding more functions the way its been done in the ZFP project
Quo.prototype = function() {
    var protoAlert1 = function() {
        document.writeln("protoAlert1");
    };
    var protoAlert2 = function() {
        document.writeln("protoAlert2");
    };
    //Trying a global function
    protoAlert3 = function() {
        document.writeln("protoAlert3");
    };
    return {
        protoAlert1: protoAlert1,
        protoAlert2: protoAlert2,
        protoAlert3: protoAlert3
    };
}();


//checkAlert2(); //Since this is global but is defined with in the constructor of the class, it cannot be called until an object of it has been created once and this function has been parsed


// Make an instance of Quo.
var myQuo = new Quo("confused");
//Trying to call the multiple functions provided thorugh prototype as GE example
myQuo.protoAlert3(); /* Calling these methods */ 
myQuo.protoAlert1();
protoAlert3();
myQuo.protoAlert2();

document.writeln(myQuo.get_status()); // confused



//Check out the different calls
//checkAlert(); //This is a local function and cannot be invoked from outside
//checkAlert1(); //This is a local function and cannot be invoked from outside
checkAlert2(); //This acts as a global function and can be invoked directly as well
//checkAlert3(); //This can be called only on an object

//myQuo.checkAlert();//Cannot be called on an object. It is local
//myQuo.checkAlert1();//Cannot be called on an object. It is local
//myQuo.checkAlert2();//It is a global function and cannot be called on an object
myQuo.checkAlert3();

//Assigning functions through prototype in a different way, the ZFP way, assigning multiple functions
//Quo.prototype = function() {
    
//}
var Quo1 = function (string) {
    this.status = string;
    alert(string);//This is called even when the constructor is invoked and also when it is called as a function. It is common to both kind of calls
};
//var myQuo1 = new Quo1('kuntal');
//document.writeln(myQuo1.status);
//Quo1('Hariom'); //Quo1 is called as a function and the alert is displayed
//alert(myQuo1.status); //Thus line alerts the object status

/* Lesson:
1. Objects can be created through Constructor calls. Such functions can be called directly without even creating an object, in that case anything that depends on the the object like "this" and all will not be
considered but rest of the code which is not "this" specific will be executed.
*/

