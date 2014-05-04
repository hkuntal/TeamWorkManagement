function parentFunction() {
    var a = "parent";
    document.writeln("Calling from parent function, value =  " + a); //Prints "parent"
    nestedFunction();
    
    function nestedFunction() {
        document.writeln("Calling from nested function, value =  " + a); //Prints "parent"
    }
}

parentFunction(); 

//Test using classes and Objects

function ClassParentFunction() {

    var b = "private ObjectParameter - b";
    this.c = "this ObjectParameter - c";
    var self = this;
    this.parentFunction = function() {
        var a = "parentClass";
        document.writeln("Calling from parent function, value =  " + a); //prints "parentClass"
        nestedFunction();

        function nestedFunction() {
            //in this function "this" refers to Window object
            document.write("\n");
            document.writeln("Calling from nested function, value =  " + a); //prints "parentClass"
            document.write("\n");
            document.writeln("Calling from nested function, value =  " + b); //prints "private ObjectParameter - b""
            document.write("\n");
            document.writeln("Calling from nested function, value =  " + self.c); // prints this ObjectParameter - c
            document.write("\n");
            document.writeln("Calling from nested function, value =  " + this.c); // c is undefined for this
            document.write("\n");
            document.writeln("Calling from nested function, value =  " + c); // c is undefined for this
            document.write("\n");
        }
    };
}
//Create parent class object
var objParent = new ClassParentFunction();
objParent.parentFunction();

