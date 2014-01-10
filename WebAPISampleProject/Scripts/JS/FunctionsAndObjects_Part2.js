var Quo = function (string) {
    this.status = string;
    //This to check if global functions can exist in a class. YES global function can exist in a class which will not be called as a part of the object. But for it to be called,
    //the object should be instantiated alteast once

    //function checkAlert() {
    //    document.writeln("Hariom");
    //}
    //var chekcAlert1 = function () {
    //    document.writeln("Kuntal");
    //};
    //checkAlert2 = function () {
    //    document.writeln("singh");
    //};
    //this.checkAlert3 = function () {
    //    document.writeln("\n this is specific to object");
    //};

};
//If we want to set multiple functions with in a prototype then we will have to use the below method where in it has to be self executing function
//and we will need to return an object that will have properties mapped to the new functions that were created as a part of the prototype. When an object will be created
//the prototype functions will be available as a part of the object. PLEASE NOTE that at one time you can do it only one way, either through Type1 or Type 2 but not both.
//If you try both one of them overrides the other

// Give all instances of Quo a public method
// called get_status.
//**************************TYPE 1*****************************
//Quo.prototype = function() {
//    var get_status = function() {
//        return this.status;
//    };
//    return { get_status: get_status };
//}();

//Quo.prototype = function () {
//    var get_status1 = function () {
//        return this.status;
//    };
//    return { get_status1: get_status1 };
//}();
//**************************TYPE 2*****************************
Quo.prototype.get_status = function() {
    return this.status;
};
Quo.prototype.get_status1 = function () {
    return this.status;
};
var myQuo = new Quo("confused");
document.writeln(myQuo.get_status1()); // confused
document.writeln(myQuo.get_status()); // confused
