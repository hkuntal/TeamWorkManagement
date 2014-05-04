

//craete a new class
var Cow = function() {
    //Add some properties to class
    var kgOfFodderNeeded = 100;
    this.timeOfDay = "noon";
    var timeOfDayPrivate = "noon";
    var self = this;
    //Add an instance method
    this.getFodderRequired = function(time) {
        //set both the instance variable and the private variable
        this.timeOfDay = time;
        var timeOfDayPrivate = time;
        //to return the fodder required simulate an ajax call
        this.getFodderFromServer(time);

        //DisplayFodder3("2500");

    };
    this.getFodderFromServer = function(time) {
        //Make an ajax call to the server and pass the time
        var xmlhttp;
        var url = "http://localhost:50968/JS/GetFodderRequired?time="+time;
        if (window.XMLHttpRequest) { // code for IE7+, Firefox, Chrome, Opera, Safari
            xmlhttp = new XMLHttpRequest();
        } else { // code for IE6, IE5
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        //xmlhttp.responseType = "arraybuffer";
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                //We will receive the image byte array here. Bind it to the Canvas directly
                var data = xmlhttp.response;
                //document.getElementById("myDiv").innerHTML = a.ImageData;
                //Display this data on the canvas
                if (data) {
                    //Checkinf of this callback can access the function objects or not
                    console.log("this:" + this);//this here refers to the XMLHttpRequest object
                    console.log("self:" + self);//self can be accessed here as such. Contains the property "timeOfDay"
                    console.log("this.timeOfDay:" + this.timeOfDay);//xmlhttpRequest does not contain timeOfDay property
                    console.log("timeOfDayPrivate:" + timeOfDayPrivate);//timeOfDayPrivate = undefined and self.timeOfDayPrivate is also undefined as timeOfDayPrivate is not a part of this
                    //1. Pass the data to the callback function. This callback function is a part of the Cow class itself declared as a private function in the class.
                    //the results of this call are identified as below in the callback function itself
                    DisplayFodder(data);
                    
                    //2. Next let us try to call an instance method on it and see how it works
                    //this.DisplayFodder1(data);//it is not going to work here as this refers to the xmlhttp context
                    
                    //3. Lets try to pass the context explicitly. See the result in the function call itself
                    //DisplayFodder2.call(self, data);

                }
            }
        };
        xmlhttp.open("GET", url, true);
        //xmlhttp.responseType = "arraybuffer";
        xmlhttp.send();
    };
    function DisplayFodder(fodder) {
        var t = document.getElementById("output");
        t.innerHTML = timeOfDayPrivate + ":" + fodder;
        //In the above line the timeOfDayPrivate variable is accessoble as it becomes a closure.

        //t.innerHTML = this.timeOfDay + ":" + fodder;
        //In the above line this refers to the window object hence cannot access the timeOfDay variable.
    }

    this.DisplayFodder1 = function(fodder) {
        var t = document.getElementById("output");
        t.innerHTML = timeOfDayPrivate + ":" + fodder;
        //In the above line the timeOfDayPrivate variable cannot be accessed in the callback function

        //t.innerHTML = this.timeOfDay + ":" + fodder;
        //In the above line this is an undefined variable as it does not recognize the variable.
    };
    
    DisplayFodder2 = function(fodder) {
        var t = document.getElementById("output");
        //t.innerHTML = timeOfDayPrivate + ":" + fodder;//timeOfDayPrivate is accessible here which is the private variable of the class
        t.innerHTML = this.timeOfDay + ":" + fodder; //this gives proper results here. this was passed in teh context when the call was made as DisplayFodder2.call(self, data);

    };

    DisplayFodder3 = function (fodder) {
        var t = document.getElementById("outputDirect");
        t.innerHTML = t.innerHTML + ":" + timeOfDayPrivate + ":" + fodder;//timeOfDayPrivate provides the proper value here as it accessible here
        t.innerHTML = t.innerHTML + ":" + this.timeOfDay + ":" + fodder; //this here refers to the window object and hence it does not work here

    };
};

function initialize() {
    //create an instance of Cow
    var objCow = new Cow();
    //call its instance method
    objCow.getFodderRequired("evening");
}

