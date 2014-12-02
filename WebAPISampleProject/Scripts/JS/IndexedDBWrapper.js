//Create an IndexedDB object
var indb = new Test.IndexedDatabase();
//Initialize the Indexed database
indb.initializeIndexedDatabase();

function gettheDataFromIndexedDB(key, isLossy, callback) {
    indb.getArrayBufferFromIndexedDatabase(key, isLossy, callback);
}

function SavetheDataInIndexedDB(key, islossy, data) {
    window.indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB;
    if (!window.indexedDB) {
        window.alert("Your browser doesn't support a stable version of IndexedDB. Such and such feature will not be available.");
    }

    else  //Call the IndexedDB functions
    {
        //alert("Your browser supports IndexedDB");
        //indb.initializeIndexedDatabase();  //openIndexedDatabase();
        indb.saveArrayBufferIntoIndexDatabase(key, islossy, data);
    }
}

function openIndexedDatabase() {
    var db;

    // Let us open our database
    var request = window.indexedDB.open("toDoList", 4);

    // these two event handlers act on the database being opened successfully, or not
    request.onerror = function (event) {
        //note.innerHTML += '<li>Error loading database.</li>';
        alert("Database creation error !!");
    };

    request.onsuccess = function (event) {
        //note.innerHTML += '<li>Database initialised.</li>';


        alert("Database creation successfully !!");    // store the result of opening the database in the db variable. This is used a lot below
        db = request.result;

        // Run the displayData() function to populate the task list with all the to-do list data already in the IDB
        //displayData();
    };

    // This event handles the event whereby a new version of the database needs to be created
    // Either one has not been created before, or a new version number has been submitted via the
    // window.indexedDB.open line above
    //it is only implemented in recent browsers
    request.onupgradeneeded = function (event) {
        var db = event.target.result;

        db.onerror = function (event) {
            //note.innerHTML += '<li>Error loading database.</li>';
            alert("Error loading database");
        };

        // Create an objectStore for this database   
        var objectStore = db.createObjectStore("toDoList", { keyPath: "taskTitle" });
    };
}