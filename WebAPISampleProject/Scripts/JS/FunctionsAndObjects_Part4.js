//Directly from the Javascript the good parts
/*
Functions in JavaScript are objects. Objects are collections of name/value pairs having a hidden link to a prototype object. Objects produced from object literals are
linked to Object.prototype. Function objects are linked to Function.prototype (which is itself linked to Object.prototype). Every function is also created with two
additional hidden properties: the function’s context and the code that implements the function’s behavior.
Every function object is also created with a prototype property. Its value is an object with a constructor property whose value is the function. This is distinct from the
hidden link to Function.prototype. The meaning of this convoluted construction will
be revealed in the next chapter.

If a function is invoked with the new prefix, then a new object will be created with a
hidden link to the value of the function’s prototype member, and this will be bound
to that new object.
*/


//Create a literal object
var student1 = {
    Name: "Hariom",
    Age:30
};

document.writeln(student1.Name);
//A lierral object has the prototype property set to undefined
//student1.Prototype = undefined

//In the case of a class as below the prototype property is set to Object
//Student.prototype = object
//Student.__proto__ =  function Empty(){}


var Student = function(name) {
    this.Name = name;
    console.log(name);
    this.Age = 0;
    var School = "NPS";
    return this; //console.log(Student("sd") === window); // true In case when we are calling the Student as a function this is pointing to the Window object.
};
// When we first created the Student class we actually created an object of type function. Again in Javascript everything is an object
//typeof Student = "function"
//For those unaware, Function is a predefined object in JavaScript, and, as a result, has its own properties (e.g. length and arguments) and methods (e.g. call and apply). \
//And yes, it, too, has its own prototype object, as well as the secret __proto__ link. This means that, somewhere within the JavaScript engine, there is a bit of code that could be 
//similar to the following:

//Function.prototype = {
//    arguments: null,
//    length: 0,
//    call: function(){
//        // secret code
//    },
//    apply: function(){
//        // secret code
//    }
//    ...
//}
//Now,the above code suggests that Function.prototype suggests that Function.prototype is an object but it is not. It is a function. For the sake of brevity I think it will be safe to assume
//that it is an Object
//typeof Function = "function"
//typeof Object = "function"
//typeof Function.prototype = "function"
//typeof Object.prototype = "object"
//All objects inherit from Object.prototype
//All Function objects inherit from Function.prototype which in turn inherit from Object.prototype
//Function.prototype has properties/methods like 
//arguments,
//caller,
//constructor, 
//lenght,
//name and methods like 
//apply,
//bind,
//call,
//isGenerator,
//toSource,
//toString
//Object.prototype provides properties like 
//__parent__, 
//__proto__ and methods like 
//__defineGetter__, 
//__defineSetter__, 
//constructor 
//hasOwnProperty, 
//isPrototypeOf, 
//__lookupGetter__, 
//__lookUpSetter__, 
//__noSuchMethod__,
//propertyIsEnumerable,
//toLocaleString, 
//unwatch, 
//valueOf, 
//watch

//To create a Function object
//var Student = new Function ([arg1, arg2, ... argn], functionBody); We can also define functions using the function literal

//Every function object is also created with a prototype property. Its value is an object with a constructor property whose value is the function. This is distinct from the
//hidden link to Function.prototype

//typeof Student.prototype = "object"
//typeof Student.__proto__ = "function"
//Student.constructor = function Function() { [native code] }
//Student instanceof Function //true

//In my words - A class is an an instance of Function and as Function is a object with its own properties, the class gets all those properties like arguments, call, length etc..
//When an instance of an object is created, the __proto__ property is updated to point to the constructor’s prototype, which, in this case, is Function.
//Student.__proto__ === Function.prototype //true    -- note that Student is an instance of Function. Student is an instance of Function, so it will be wise to say that the __proto__ points to the 
//Constructors prototype. The constructor here is the Function.

//NOTE: typeof Function.prototype == function. And we type Function.prototype property directly in the console log, we get function Empty{}

//Properties of Function:  caller, constructor, length, name METHODS: Apply, Call, toSource, toString

//Properties Methods of Object:

var student2 = new Student("Ruby"); 
student2.Age = 25;

//student2.__proto__ == Student.Prototype //true
//student2.constructor = the constructor function of the Student class
//student2.prototype //undefined

//Now here the __proto__ property is pointing to the constructors prototype, because the constructor of student2 is Student

document.writeln(student2.Age);

//Using the apply method invocation pattern
