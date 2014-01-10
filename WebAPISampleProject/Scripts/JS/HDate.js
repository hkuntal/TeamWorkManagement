Date.prototype.getHariomFormat = function() {
    var d = this;
    var da = d.getDate();

    var mo = d.getMonth() + 1; //Months are zero based
    var y = d.getFullYear();
    var h = d.getHours();
    var m = d.getMinutes();
    var s = d.getSeconds();
    var mm = d.getMilliseconds();
    var launchdate = y + "-" + mo + "-" + da + " " + h + ":" + m + ":" + s + "." + mm;
    return launchdate;

};

var a = new Date();
alert(a.getHariomFormat());