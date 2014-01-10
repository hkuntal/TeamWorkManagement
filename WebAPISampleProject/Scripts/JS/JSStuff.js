function Add(a, b, c) {
    var sum = a + b + c;
    return sum;
}

function AddString(a, b, c) {
    var sum = a + b + c;
    return sum;
}

var add = function(a, b) {
    if (typeof a !== 'number' || typeof b !== 'number') {
        throw {
            name: 'TypeError',
            message: 'add needs numbers'
        };
    }
    return a + b;
};
//Check out try catch work
var try_it = function() {
    try {
        add("seven");
    } catch(e) {
        document.writeln(e.name + ': ' + e.message);
        //throw e;
    }
};
var try_itAgain = function () {
    try {
        try_it();
    } catch (e) {
        alert(e.name + ': ' + e.message);
        //throw e;
    }
};
try_itAgain();

