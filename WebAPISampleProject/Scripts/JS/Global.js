var HSK = HSK || {};
HSK.Globals = HSK.Globals || {};
HSK.Globals.IpAddress = "localhost";
HSK.Globals.canvasIndex = -1;

HSK.Globals.getUrlTOLaunchStudy = function (url) {
    return "http://" + HSK.Globals.IpAddress + "/" + url;
}