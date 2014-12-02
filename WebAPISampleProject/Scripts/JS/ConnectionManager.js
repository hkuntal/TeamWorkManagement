ConnectionManager = function (serviceUrl, webSocketOpenCallback) {
    this.serviceUrl = serviceUrl;
    var socket;
    var protocol = "dakota-protocol-1.0";
    var binaryType = "arraybuffer";
    
    this.init = function() {
        socket = this.createWebSocketInstance();
        socket.binaryType = binaryType;
        socket.onopen = onSocketOpen;
        socket.onmessage = onMessage;
        socket.onerror = onError;
        socket.onclose = onSocketClose;
    };

    this.createWebSocketInstance = function() {
        return new WebSocket(serviceUrl, protocol);
    };
    
    function onSocketOpen() {
        //Check if the socket ready state changed or not
        console.log('[WebSocket] connection opened at ' + new Date());
        webSocketOpenCallback();
    }
    function onMessage(e) {
        var currentData = e.data;

        if (typeof currentData === "string") {
            if (currentData.indexOf("CMD") == 0) {
                if (currentData.indexOf("CMDCACHESTUDY") == 0) {
                    // Study layout is to follow
                }
                else if (currentData.indexOf("CMDGETSERIESMETADATA") == 0) {
                    hasStudyLayout = false;
                }
                else if (currentData.indexOf("CMDGETOBJ") == 0) {
                    var headerData = currentData.split(" ");
                    currentTokenId = headerData[1];
                    isThumbnail = false;
                    isExpectingHeaderData = true;
                    isPng = false;
                    if (headerData[2] != "IT_RAW") {
                        isPng = true;
                    }
                }
                else if (currentData.indexOf("CMDGETTHUMBNAIL") == 0) {
                    currentTokenId = currentData.split(" ")[1];
                    isThumbnail = true;
                }
                else if (currentData.indexOf("CMDGETNIOOBJ") == 0) {
                    var token = currentData.split(" ")[1];
                    if (token.indexOf("#-") != -1) {
                        token = token.split("#")[0];
                        currentTokenId = token + "#0";
                        nioDataCallback[token](currentTokenId);
                    } else {
                        currentTokenId = currentData.split(" ")[1];
                        hasNio = true;
                    }
                }
                else if (currentData.indexOf("CMDERROR") == 0) {
                    if (currentData.indexOf("FetchTimeoutError") != -1) {
                        console.log('[WebSocket] close socket on error!');
                        socket.close(1000, "NORMAL_CLOSE");
                    }
                    window.parseAndDisplayErrorMessage(currentData);
                }
                else if (currentData.indexOf("CMDGETPATIENTHISTORYFORNAVIGATOR") == 0) {
                    var navgData = currentData.replace("CMDGETPATIENTHISTORYFORNAVIGATOR ", "");
                    navStudyListCallback[0](navgData);
                }
                else if (currentData.indexOf("CMDGETPATIENTHISTORY") == 0) {
                    var studyData = currentData.replace("CMDGETPATIENTHISTORY ", "");
                    studyListCallback[0](studyData);
                }
                else if (currentData.indexOf("CMDSESSIONINFO") == 0) {
                    var sessionInfo = jQuery.parseJSON(currentData.replace("CMDSESSIONINFO ", ""));
                    console.log("[DEBUG] No of logged in users - " + sessionInfo.SessionCount);
                    console.log("[DEBUG] Current Websocket Session Id - " + sessionInfo.SessionId);
                    GE.ZFP.PerformanceLog.Logger.getInstance().log({
                        transaction: "ZeroFootPrint, number of users logged in to the ZFP client " + sessionInfo.SessionCount
                    });
                }
                else if (currentData.indexOf("CMDGETEXAMNOTES") == 0) {
                    var examNotesData = currentData.split(" ");
                    var examNoteHeader = examNotesData[0];

                    var examToken = examNoteHeader.split("#")[1];

                    var examNotes = currentData.replace(examNoteHeader + " ", "");
                    examNotesCallback[examToken](examNotes);
                }
                return;

            } else if (isExpectingHeaderData) {
                header = currentData;
                isExpectingHeaderData = false;
                isExpectingPixelData = true;
            } else if (isExpectingPixelData) {
                // NOTE: We get pixel data as a base64 string for Safari 5.1
                var jsonHeader = jQuery.parseJSON(header);
                var pixelLength = getImageLengthBasedOnHeader(header);
                var tmpBuffer = new window.ArrayBuffer(pixelLength);
                var arraybuffer;
                if (jsonHeader.SopClassUid.indexOf("1.2.840.10008.5.1.4.1.1.88.") != -1 && jsonHeader.SopClassUid != "1.2.840.10008.5.1.4.1.1.88.59") {
                    arraybuffer = _base64Binary.decode(currentData).buffer;
                } else {
                    if (!isPng)
                        arraybuffer = _base64Binary.decode(currentData, tmpBuffer).buffer;
                    else
                        arraybuffer = currentData;
                }
                var imageInfo = {};
                imageInfo.ImageHeader = header;
                imageInfo.ImageData = arraybuffer;
                if (lossyImageDataCallback[currentTokenId] != undefined && isPng) {
                    isPng = false;
                    lossyImageDataCallback[currentTokenId](currentTokenId, imageInfo);
                    console.log("LossyImage pixel data received for token ID " + currentTokenId);
                    delete lossyImageDataCallback[currentTokenId];
                }
                if (imageDataCallback[currentTokenId] != undefined) {
                    imageDataCallback[currentTokenId](currentTokenId, imageInfo);
                    delete imageDataCallback[currentTokenId];
                } else {
                    // This means we don't have a callback defined for the token!!
                    console.log('No callback defined for token: ' + currentTokenId);
                }
                isExpectingPixelData = false;
                return;
            }

            if (!hasStudyLayout) {
                // First message is the study layout
                hasStudyLayout = true;
                //isWebSocketReconnected ? webSocketErrorMobile.studyLayoutCallback(currentData) :
                if (!isWebSocketReconnected) {
                    var studyLayoutTime = new Date(),
                        timeTaken = (studyLayoutTime - GE.ZFP.Globals.requestInitiationTime) / 1000;
                    console.log("[!!DEBUG!!!] Time taken to fetch Study Layout"
                        + " - " + timeTaken + " secs");

                    GE.ZFP.PerformanceLog.Logger.getInstance().log({
                        transaction: "Full Study Load",
                        timeInMilliSec: timeTaken * 1000
                    });
                    studyLayoutCallback(currentData);
                }
                return;
            }

            if (hasNio) {
                if (nioDataCallback[currentTokenId] != undefined) {
                    var data1 = {};
                    data1.niometadata = currentData;
                    nioDataCallback[currentTokenId](currentTokenId, data1);
                    delete nioDataCallback[currentTokenId];
                }
                if (gspsDataCallback[currentTokenId] != undefined) {
                    var data2 = {};
                    data2.gspsMetaData = currentData;
                    gspsDataCallback[currentTokenId](currentTokenId, data2);
                    delete gspsDataCallback[currentTokenId];
                }
                hasNio = false;
            }
        }
        else if (currentData instanceof window.ArrayBuffer) {
            // set the flag to false before callback, 
            // otherwise it might lead to mismatch in callbacks (gets into safari code when header is recieved)
            isExpectingPixelData = false;
            var data = {};
            data.ImageHeader = header;
            data.ImageData = currentData;
            if (thumbnailDataCallback[currentTokenId] != undefined && isThumbnail) {
                // this is thumbnail data
                isThumbnail = false;
                _.each(thumbnailDataCallback[currentTokenId], function (thumbCallback) {
                    thumbCallback(currentTokenId, currentData);
                });
                // NOTE: This is for a peculiar race condition that started occuring when showing Series Navigator by default. This occurs when two thumbnails are showing the same image
                // It might happen for e.g. when there is a KIN series that is displaying the same thumbnail as displayed by the actual series. In that case the callback of the second
                // thumbnail gets deleted before its execution because the callback for the second thumbnail gets inserted between the execution of the first thumbnail callback and its deletion

                //Before deletion check if any new callback got inserted
                if (thumbnailDataCallback[currentTokenId].length > 1) {
                    //delete only the first callback
                    thumbnailDataCallback[currentTokenId].shift();
                } else {
                    delete thumbnailDataCallback[currentTokenId];
                }
            }
            else if (lossyImageDataCallback[currentTokenId] != undefined && isPng) {
                var callback = lossyImageDataCallback[currentTokenId];
                delete lossyImageDataCallback[currentTokenId];
                console.log("Lossy Image pixel data received for tokenId:  " + currentTokenId);
                callback(currentTokenId, data);
            }
            else if (imageDataCallback[currentTokenId] != undefined && !isPng && !isThumbnail) {
                var func = imageDataCallback[currentTokenId];
                console.log("Lossless Image pixel data received for tokenId:  " + currentTokenId);
                delete imageDataCallback[currentTokenId];
                func(currentTokenId, data);
            } else {
                // TODO: This means we don't have a callback defined for the token!!
                console.log('No callback defined for token: ' + currentTokenId + " isThumbnail: " + isThumbnail + " isLossy: " + isPng);
            }
        }
    }

    function onError(e) {
        console.log('Error: ' + e);
        for (var x in e) {
            console.log('Error attr: ' + x + ':' + e[x]);
        }
    }

    function onSocketClose(e) {
        console.log('[WebSocket] connection closed at ' + new Date());
        if (e.code == 1006 && e.wasClean == false) { //1006:CLOSE_ABNORMAL
           
        }
    }
    
    this.getStudyLayout = function (ncd, accNbr, patientRef, patHistorySuis, archiveId, localWorkListType, getStudyLayoutCallback, fetchExam, isComparisonStudy) {
        studyNcd = ncd;
        accessionNumber = accNbr;
        patRef = patientRef;
        patientHistorySuis = patHistorySuis;
        studyLayoutCallback = getStudyLayoutCallback;
        fetchOfflineExam = fetchExam ? fetchExam : false;
        starttime = new Date();
        this.workListType = localWorkListType;

        buildCacheStudyQuery(command.CacheOptimisedStudy, null, archiveId, this.workListType, isComparisonStudy);
        socket.send(JSON.stringify(queryCmd));
    };
}

function buildCacheStudyQuery(cmd, seriesUid, archiveId, workListType, isComparisonStudy) {
    // Construct a msg object containing the data the server needs to process.
    queryCmd = {
        CommandName: cmd,
        StudyUID: studyNcd,
        AccessionNumber: accessionNumber,
        AssigningAuthorityId: viewmodel.patientIdDomain(),
        PatRef: patRef,
        IsPatientHistoryLoaded: isPatientHistoryLoaded,
        LoadSinglePatientHistory: GE.ZFP.Globals.loadSinglePatientHistory,
        CurrentUser: currentUser,
        AuditUser: auditUser,
        ClientIp: clientIp,
        HostName: hostName,
        ApplicationMode: applicationMode,
        SeriesInstanceUID: seriesUid,
        PatientHistorySuis: patientHistorySuis,
        OpenApiCmd: window.openApiCmd,
        LoadFromWorkList: window.loadfromWorkList,
        WorkListType: workListType,
        StudyFetchSetupCmd: {
            ClientDeviceInfo: {
                IsTouchDevice: browserConfiguration.isTouchDevice
            },
            SetSeriesPriorityOnStudyLoad: !isComparisonStudy
        },
        EaArchiveId: archiveId,
        FetchOfflineExam: fetchOfflineExam
    };
}
/*
{"CommandName":"CMDGETOPTIMIZEDSTUDY","StudyUID":"1.2.528.1.1001.1.960113006.200.2.19960306.125615749","AccessionNumber":"A091","AssigningAuthorityId":"1","PatRef":{"PatientName":"Ill%5EVery","PatientId":"1000026000","AuthorityShortCode":""},"IsPatientHistoryLoaded":true,"LoadSinglePatientHistory":false,"CurrentUser":"502230035","AuditUser":"GEMEDAMERICA\\502230035","ClientIp":"::1","HostName":"::1","ApplicationMode":"StandAloneLaunch","SeriesInstanceUID":null,"PatientHistorySuis":[],"OpenApiCmd":"Default","LoadFromWorkList":true,"WorkListType":"All","StudyFetchSetupCmd":{"ClientDeviceInfo":{"IsTouchDevice":false},"SetSeriesPriorityOnStudyLoad":true},"EaArchiveId":"AE_ARCH1~0","FetchOfflineExam":false}
*/