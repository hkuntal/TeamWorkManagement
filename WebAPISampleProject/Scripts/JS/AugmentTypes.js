Function.prototype.method = function (name, func) {
    this.prototype[name] = func; // This works
    //this.prototype.name = func; ==> For some reason this does not work
    return this;
};
// Add a method conditionally, that is only if it is not present else it may be overridden by methods from other libraries
Function.prototype.method = function (name, func) {
    if (!this.prototype[name]) {
        this.prototype[name] = func;
    }
};

Function.prototype.integer = function () {
    return Math[this < 0 ? 'ceiling' : 'floor'](this);
};

//The below code works
//Object.prototype.integer = function () {
//    return Math[this < 0 ? 'ceiling' : 'floor'](this);
//};

//The below code works
//Number.prototype.integer = function () {
//    return Math[this < 0 ? 'ceiling' : 'floor'](this);
//};

Number.method('integer', function () {
    return Math[this < 0 ? 'ceiling' : 'floor'](this);
});

//Call the integer method to get integer part from the decimal
document.writeln((3.16).integer()); //This works with the Object prototype method, the Number prototype method but not with the function prototype directly method way

String.method('trim', function () {
    return this.replace(/^\s+|\s+$/g, '');
});
document.writeln('"' + " neat ".trim() + '"');

//Memorization
var fibonacci = function ( ) {
    var memo = [0, 1];
    var fib = function (n) {
        var result = memo[n];
        if (typeof result !== 'number') {
            result = fib(n - 1) + fib(n - 2);
            memo[n] = result;
        }
        return result;
    };
    return fib;
}();
document.writeln(fibonacci(10));

//Here is an example function of returning another function
//var getName = function() {
//    var prefix = "kk";
//    var gName = function(name) {
//        return prefix + name;
//    };
//    return gName;
//}();
//The above function can also be written in the following way

var getName = function () {
    var prefix = "kk";
    return function (name) {
        return prefix + name;
    };
    //return gName;
}();
//Please note that prefix here is defined as a closure and cannot be accessed globally
document.writeln(getName("Hariom"));