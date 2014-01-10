function Animal(foodtype) {
    this.foodtype = foodtype;
}
function Cow(color)
{
    this.color = color;
}
//Inheritance Magic
Cow.prototype = new Animal("Hay"); // Over here the Animal becomes the base class of Cow. The constructor function is called for the Animal
//Cow.prototype = Animal;
var c = new Cow("White"); //When we create an object of Cow, it is now going to first call the constructor function of Animal and then the constructor function of Cow
document.writeln(c.foodtype); //Hay
document.writeln(c.color);//white
var test = c instanceof Animal;  //true
var test2 = c instanceof Cow; //true
var b = new Animal("Meat");
var literObject = {
    name: "hariom",
    age: "28"
};
