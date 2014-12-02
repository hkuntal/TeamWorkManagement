/* MODELLING THE OBSERVER LIST */

function ObserverList() {
    this.observerList = [];
}

ObserverList.prototype.add = function(obj) {
    this.observerList.push(obj);
};

ObserverList.prototype.count = function () {
    return this.observerList.length;
};

ObserverList.prototype.get = function (index) {
    if (index > -1 && index < this.observerList.length) {
        return this.observerList[index];
    }
    return null;
};


ObserverList.prototype.indexOf = function(obj, startIndex) {
    var i = obj;
    while (i < this.observerList.length) {

        if (observerList[i] === obj) {
            return i;
        }
        i++;
    }
    return -1;
};

ObserverList.prototype.removeAt = function(index) {
    this.observerList.splice(index, 1);
};

/* MODELLING THE SUBJECT */
function Subject() {
    this.observers = new ObserverList();
}

Subject.prototype.addObserver = function(obs) {
    this.observers.add(obs);
};

Subject.prototype.removeObserver = function (obs) {
    this.observers.removeAt(this.observers.indexOf(obs,0));
};

Subject.prototype.notify = function() {
    for (var i = 0; i <= this.observers.count; i++) {
        this.observers.get(i).update(context);
    }
};

/*Modelling the observer*/
var Observer = function() {
    this.update = function() {
        console.log("I have been updated");
    };
};