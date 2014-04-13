var Test;
Test = Test || {};
Test.IndexedDatabase = Test.IndexedDatabase || function () { };

Test.IndexedDatabase.prototype = function () {
    var db = null;
    var initializeIndexedDatabase = function () {
        var request = window.indexedDB.open("MyDataBase2", 1);
        console.log("Initializing indexed database");
        request.onsuccess = function (event) {
            db = event.target.result;
            console.log("DB Created: " + db);
            //alert("DB Created");
        };

        request.onupgradeneeded = function (event) {
            db = event.target.result;
            if (!db.objectStoreNames.contains("LossLessObjectStore")) {
                db.createObjectStore("LossLessObjectStore");
                db.createObjectStore("LossyObjectStore");
                alert("OS Created");
            }
        };
    },
        saveArrayBufferIntoIndexDatabase = function (key, isLossy, value) {
            //If the IndexedDB has not been intialized, don't do anything, just RETURN
            if (!db) {
                return;
            }
            var selectedStore = null;
            if (isLossy) {
                selectedStore = db.transaction(["LossyObjectStore"], "readwrite").objectStore("LossyObjectStore");
            } else {
                selectedStore = db.transaction(["LossLessObjectStore"], "readwrite").objectStore("LossLessObjectStore");
            }
            console.log("Adding byte array for the key: " + key);
            var saveRequest = selectedStore.put(value, key);

            saveRequest.onsuccess = function (e) {
                console.log("OBJECT ADDED INTO INDEXED DATABASE WITH KEY: " + key);
            };

            saveRequest.onerror = function (e) {
                alert("ERROR ADDING OBJECT WITH KEY: " + key);
            };
        },
        getArrayBufferFromIndexedDatabase = function (key, isLossy, callback) {
            //If the database has not been initialized, GO BACK !!
            if (!db) {
                callback(false);
            }
            var objectStore = null;
            if (isLossy) {
                objectStore = db.transaction(["LossyObjectStore"]).objectStore("LossyObjectStore");
            } else {
                objectStore = db.transaction(["LossLessObjectStore"]).objectStore("LossLessObjectStore");
            }
            
            //console.log("OBJECT RETRIVED FROM INDEXED DATABASE WITH KEY: " + new Date);
            var getRequest = objectStore.get(key);
            
            getRequest.onsuccess = function (e) {
                //console.log("OBJECT RETRIVED FROM INDEXED DATABASE WITH KEY: " + new Date);
                console.log("Received Success:getArrayBufferFromIndexedDatabase");
                var value = e.target.result;
                //if (value)
                callback(value);
            };

            getRequest.onerror = function (e) {
                console.log("Received Error:getArrayBufferFromIndexedDatabase");
                //alert("Error in getting object");
            };
        },
        deleteIndexedDatabase = function () {
            var request = window.indexedDB.deleteDatabase("MyDataBase");

            request.onsuccess = function (event) {
                console.log("INDEX DB DELETED");
            };
        };


    return {
        initializeIndexedDatabase: initializeIndexedDatabase,
        saveArrayBufferIntoIndexDatabase: saveArrayBufferIntoIndexDatabase,
        getArrayBufferFromIndexedDatabase: getArrayBufferFromIndexedDatabase,
        deleteIndexedDatabase: deleteIndexedDatabase
    };
} ();