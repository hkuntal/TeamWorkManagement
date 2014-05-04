var HSK = HSK || {};
HSK.Globals = HSK.Globals || {};
HSK.Globals.IpAddress = "169.254.141.124";
HSK.Globals.canvasIndex = -1;

HSK.Globals.getUrlTOLaunchStudy = function (url) {
    return "http://" + HSK.Globals.IpAddress + "/" + url;
}