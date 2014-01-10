//$(rcDiv).contextMenu({
//    menu: 'rcMenu',
//    inSpeed: 0,
//    outSpeed: 0
//},
//   function (action) {
//       if (action === "img-operation-snapshot") {
//           var saveImage = new SaveImage();
//           saveImage.saveImageByCanvg();
//           return null;
//       }

//       if (action === "img-operation-calibrate") {
//           viewmodel.selectDrawOption('calibrate');
//           return null;
//       }

//       $('#' + action).click();
//       return false;
//   });
function onDivShow() {
    alert("the div has been displayed");
}


$(document).ready(function () {
    //set a function that will be fired each time the div becomes visible/invisible
    
    $('#rcDiv').contextMenu({
        selector: '#rcDiv',
        menu: 'rcMenu'
        // Randomly enable or disable each option
        
    });
    
    //$('#rcMenu').show(0, onDivShow);
});

//$(document).ready(function () {
//    $.contextMenu({
//        selector: '#rcDiv',
//        menu: 'rcMenu'
//        // Randomly enable or disable each option

//    });
//});
//$(document).ready(function () {
//    $.contextMenu({
//        selector: '.context-menu-one',
//        callback: function (key, options) {
//            var m = "clicked: " + key;
//            window.console && console.log(m) || alert(m);
//        },
//        items: {
//            "edit": { name: "Edit", icon: "edit" },
//            "cut": { name: "Cut", icon: "cut" },
//            "copy": { name: "Copy", icon: "copy" },
//            "paste": { name: "Paste", icon: "paste" },
//            "delete": { name: "Delete", icon: "delete" },
//            "sep1": "---------",
//            "quit": { name: "Quit", icon: "quit" }
//        }
//    });

//    $('.context-menu-one').on('click', function (e) {
//        console.log('clicked', this);
//    })
//});