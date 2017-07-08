var module = angular.module('app', []);

module.controller('KycUpload', function ($scope, $http, $sce) {
    $scope.document_attachments.uploadresults = [];
    $scope.isAttachmentUploadingA = false;

    //Fetch the library documents of KYC Attachments Tab Attachments
    $http.get('/api/Library/Index?module=KYC&moduleId=' + $scope.currentRecordId + "&subModule=Attachments&subModuleId=0").then(function (data4) {
        if (data4 != "") {
            $scope.document_attachments = data4.data;
        } else {
            $scope.document_attachments = [];
        }
    })

    //upload KYC document
    $scope.uploadLibraryDocumentA = function (files) {
        debugger;
        if (files[0].size > 4000000) {
            alert("Maximum 4MB document size allowed!");
            $("#documentA").val("");
            return false;
        }
        var filename = files[0].name;
        filename = filename.toLowerCase();
        if (filename.includes(".jpg") || filename.includes(".jpeg") || filename.includes(".bmp") || filename.includes(".png") ||
            filename.includes(".pdf") || filename.includes(".txt") || filename.includes(".doc") || filename.includes(".docx") || filename.includes(".xls") ||
            filename.includes(".xlsx") || filename.includes(".ppt") || filename.includes(".pptx")) {
            $scope.isAttachmentUploadingA = true;
            var fd = new FormData();
            //Take the first selected file
            fd.append("file", files[0]);

            $http.post("/api/Library/Resource?module=Resume&moduleId=" + $scope.currentRecordId + "&subModule=Attachments&subModuleId=0", fd, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).success(function (data) {
                $("#documentA").val("");
                $scope.isAttachmentUploadingA = false;
                if (!$scope.document_attachments.uploadresults) {
                    $scope.document_attachments["uploadresults"] = [];
                    $scope.document_attachments.uploadresults.push(data[0]);

                }
                else {
                    $scope.document_attachments.uploadresults.push(data[0]);
                }
                alert("Attachment successfully uploaded!");
                if ($scope.saveArray.Tab7Save == false) {
                    $scope.saveArray.Tab7Save = true;
                    countSaveArray();
                }
                saveResumeProgress();
            }
            ).error(function (data) {
                alert("Error occured while uploading!");
            }
            )
        } else {
            alert("Valid file types are: jpg/jpeg/bmp/png/pdf/txt/doc/docx/xls/xlsx/ppt/pptx");
            $("#documentA").val("");
        };

    };

    //delete KYC attachment
    $scope.deleteAttachmentA = function (id) {
        var ans = confirm("Are you sure you want to delete?");
        if (ans) {
            $http.delete("/api/Library/Resource/" + id).success(function (data) {
                var indx = 0;
                angular.forEach($scope.document_attachments.uploadresults, function (value, index) {
                    if (value.fileId == id) { indx = index; }
                })
                $scope.document_attachments.uploadresults.splice(indx, 1);
                toaster.pop('success', 'Success', "Attachment successfully deleted!");
            });
        }
    }
  
    //view document
    $scope.showimage = function (uniquename, filename, filetype, filepath) {
        $scope.documentID = uniquename;
        $scope.documentNAME = filename;
        $scope.documentFILETYPE = filetype;
        $scope.fullFilePath = filepath + $scope.documentID;
        $scope.viewerFullFilePath = "<iframe src='https://docs.google.com/viewer?url=" + filepath + $scope.documentID + "&embedded=true&chrome=false&dov=1' style='width:100%;height:750px' frameborder='0'></iframe>";
        $scope.TrustedviewerFullFilePath = $sce.trustAsHtml($scope.viewerFullFilePath);
        $scope.isIframe = true;
    }

})