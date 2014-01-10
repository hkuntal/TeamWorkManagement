function MakeAjaxCallToServer() {
    var url = window.document.getElementById("txtUrl").value;
    var data = window.document.getElementById("txtData").value;
    var type = window.document.getElementById("txtType").value;
    
    //call the function
    CallTheServer(data, url, type);
}

function CallTheServer(dataSent, url, type) {
    $.ajax({
        type: type,
        url: url,
        crossDomain: true,
        data: dataSent,
        contentType: 'text/plain',
        success: function (serverData) {
            console.log("Data Sent successfully to the server. The server returned" + serverData);
            window.document.getElementById("ResultfromServer").value = serverData;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("error: " + errorThrown);
        },
        });
}