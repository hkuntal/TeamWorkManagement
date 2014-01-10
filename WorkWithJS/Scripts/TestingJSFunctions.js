//Trying to test this statment from the JS Good Parts BookmarkCollection
//5. A function always returns a value. If the return value is not specified, then undefined
//is returned.
//If the function was invoked with the new prefix and the return value is not an object,
//then this (the new object) is returned instead.

var A  = function()
{
    alert("Testing some stuff dude");
    var i = 5;
    return 2;
}

//Now call the above function using constructor call
var c = new A();
alert(c);
