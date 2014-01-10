(function () {
    var foo = 'Hello';
    var bar = 'World!'
    alert(foo + ' ' + bar);
    b = function baz() {
       return foo + ' ' + bar;
    }
})();
console.log(b());