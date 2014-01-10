//Create a view model object. 
//EmployeeModel is an already created object
//ko.applyBindings(EmployeeModel);

// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
function AppViewModel() {
    this.firstName = ko.observable("Bert");
    this.lastName = ko.observable("Bertington");
}

// Activates knockout.js
ko.applyBindings(new AppViewModel());