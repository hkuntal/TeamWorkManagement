// Create myObject. It has a value and an increment
// method. The increment method takes an optional
// parameter. If the argument is not a number, then 1
// is used as the default.
var add = function (i, j) {
    return i + j;
}

//Please note that the above method add is a global function as it is not present inside any class or other function
var myObject = {
    value: 0,
    increment: function (inc) {
        this.value += typeof inc === 'number' ? inc : 1;
        //Not you can include this parameter here also, as it is a part of this object
    }
};
myObject.increment();
document.writeln(myObject.value); // 1
myObject.increment(4);
document.writeln(myObject.value); // 3

alert("Testing Nested Function functionality");

// Augment myObject with a double method.
myObject.double = function () {
    var that = this; // Workaround.
    var helper = function () {//NOTE: that this helper function will not have access to the "this" which refers to the object myObject because it is a nested function
        that.value = add(that.value, that.value);
    };
    helper(); // Invoke helper as a function.
};
// Invoke double as a method. Note Double is a method and not a function where as helper is a function.
myObject.double();
document.writeln(myObject.value); // 6