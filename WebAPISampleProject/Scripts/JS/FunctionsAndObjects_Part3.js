var flight = {
    airline: "Oceanic",
    number: 815,
    departure: {
        IATA: "SYD",
        time: "2004-09-22 14:55",
        city: "Sydney"
    },
    arrival: {
        IATA: "LAX",
        time: "2004-09-23 10:42",
        city: "Los Angeles"
    }
};

var stooge = {
    "firstname": "Jerome",
    "last-name": "Howard"
};

document.writeln(stooge["firstname"]); // "Joe"
document.writeln(flight.departure.IATA); // "SYD"
//The undefined value is produced if an attempt is made to retrieve a nonexistent member:
document.writeln(stooge["middle-name"]); // undefined
document.writeln(flight.status); // undefined
document.writeln(stooge["FIRST-NAME"]); // undefined
//The || operator can be used to fill in default values:
var middle = stooge["middle-name"] || "(none)";
var status = flight.status || "unknown";


var another_stooge = Object.create(stooge);
document.writeln(another_stooge["firstname"]);

//Augmenting Types
//Number.prototype
var Quo = function(string) {
    this.status = string;
    var _name = "a"; //This is a public property here
    //Defining public properties to access the private properties of an object.
    Object.defineProperty(this, "Name", {
        get: function() {
            return _name;
        },
            set: function(value){
            _name=value;
    }
});
};
//Quo.prototype.method('Name', function () {
//    return this.status + 'Hariom';
//});
//document.writeln(a.Name);
var a = new Quo("Kuntal");
a.Name = "Testing Public Property";
document.writeln(a.Name);

Function.prototype.method = function (name, func) {
    this.prototype[name] = func;
    return this;
};
Number.method('integer', function () {
    return Math[this < 0 ? 'ceiling' : 'floor'](this);
});
document.writeln((-10 / 3).integer()); // -3