function Dog(name, breed, weight) {
    this.name = name;
    this.breed = breed;
    this.weight = weight;
    var height = 5;
    this.bark = function () {
        if (this.weight > 25) {
            alert(this.name + " says Woof!");
        } else {
            alert(this.name + " says Yip!");
        }
    };
}
//Create the Dog object
//alert(height);
var fido = new Dog("Fido", "Mixed", 38);
alert(fido.name);
alert(height);