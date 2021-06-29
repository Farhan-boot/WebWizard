var app = angular.module('WebWizardMemberApp', ['ui.bootstrap', 'ngImgCrop']);

app.controller('WebWizardMemberCtrl', function ($scope, $http, $filter, $uibModal, $log) {
    // alert("test");
    //Get Project Type
    
    $http.get('/WebWizardPortfolio/GetProjectType').then(function (myProjectType) {
        $scope.projectType = myProjectType.data;
    });
    $scope.type = {
        NameOfProject: ''
    };
    $scope.myProjectType = function () {
        $scope.type;
        $scope.getData1(1, $scope.type.NameOfProject);
    };


    // Initialize variable
    $scope.itemsPerPage2 = 3;
    $scope.pageno2 = 1;
    $scope.total_count2 = 0;
    $scope.dishesPageList2 = [];
    $scope.PageNumber2 = 1;
    // This would fetch the data on page change.
    $scope.getData2 = function (pageno2) {
        // Proceed to search function once validation success
        if ($scope.PageNumber2 <= 0) {
            $scope.pageno2 = 1;
            $scope.PageNumber2 = 1;
        }
        else {
            $scope.LoadingText2 = "Loading Dishes...";
            $scope.showLoading2 = true;
            // Resets page list and total count on each page change
            $scope.DishesPageList2 = {
                ClientId : '',
                FirstName : '',
                LastName : '',
                ClientEmail : '',
                ClientProfileImageUrl : '',
                ClientMobileNo : ''
            };


            $scope.total_count2 = 0;
            // Assign new page number
            $scope.pageno2 = pageno2;
            $scope.WebWizardMemberController = {
                Page2: pageno2
            }
            $http.post('/WebWizardMember/GetAllClientExceptMe', $scope.WebWizardMemberController).then(function (AllClientExceptMeDishesPageList) {
                if (AllClientExceptMeDishesPageList.data.length == 0) {
                    $scope.pageno2 = pageno2;
                    $scope.PageNumber2 = pageno2 - 1;
                    toastr.info("End of the list", "Info");
                }
                else {
                    $scope.allClientExceptMe = AllClientExceptMeDishesPageList.data;
                }
            });


        }
    };
    // Initial load set to page 1
    $scope.getData2(1);
    // For ClientExceptMe end






    // Initialize variable
    $scope.itemsPerPage1 = 3;
    $scope.pageno1 = 1;
    $scope.total_count1 = 0;
    $scope.dishesPageList1 = [];
    $scope.PageNumber1 = 1;
    // This would fetch the data on page change.
    $scope.getData1 = function (pageno1, type) {
        // Proceed to search function once validation success
        if ($scope.PageNumber1 <= 0) {
            $scope.pageno1 = 1;
            $scope.PageNumber1 = 1;
        }
        else {
            $scope.LoadingText1 = "Loading Dishes...";
            $scope.showLoading1 = true;
            // Resets page list and total count on each page change
            $scope.DishesPageList1 = {
                Id: '',
                FirstName: '',
                LastName: '',
                Email: '',
                WebWizardProfileImageUrl: '',
                WebWizardId: ''
            };
           

            $scope.total_count1 = 0;
            // Assign new page number
            $scope.pageno1 = pageno1;
            $scope.type = $scope.type;
            $scope.WebWizardProjectForClientController = {
                Page1: pageno1,
                Type: $scope.type.NameOfProject
            }
            $http.post('/WebWizardProjectForClient/GetAllProject', $scope.WebWizardProjectForClientController).then(function (allProject) {
                if (allProject.data.length == 0) {
                    $scope.pageno1 = pageno1;
                    $scope.PageNumber1 = pageno1 - 1;
                    toastr.info("End of the list", "Info");
                }
                else {
                    $scope.allProject = allProject.data;
                }
            });
        }
    };
    // Initial load set to page 1
    $scope.getData1(1);
    // For newsfeed end
    
     //$http.get('/WebWizardProjectForClient/GetAllProject').then(function (allProject) {
     //    $scope.allProject = allProject.data;
     //});


    

    // Initialize variable
    $scope.itemsPerPage = 3;
    $scope.pageno = 1;
    $scope.total_count = 0;
    $scope.dishesPageList = [];
    $scope.PageNumber = 1;
    // This would fetch the data on page change.
    $scope.getData = function (pageno) {
        // Proceed to search function once validation success
        if ($scope.PageNumber <= 0) {
            $scope.pageno = 1;
            $scope.PageNumber = 1;
        }
        else {
            $scope.LoadingText = "Loading Dishes...";
            $scope.showLoading = true;
            // Resets page list and total count on each page change
            $scope.DishesPageList = {
                Id: '',
                FirstName: '',
                LastName: '',
                Email: '',
                WebWizardProfileImageUrl: '',
                WebWizardId: ''
            };
            $scope.total_count = 0;
            // Assign new page number
            $scope.pageno = pageno;
            $scope.WebWizardMemberController = {
                Page: pageno
            }
            $http.post('/WebWizardMember/GetWebWizardList', $scope.WebWizardMemberController).then(function (DishesPageList) {
                if (DishesPageList.data.length == 0) {
                    $scope.pageno = pageno;
                    $scope.PageNumber = pageno - 1;
                    toastr.info("End of the list", "Info");
                }
                else {
                    $scope.dishesPageList = DishesPageList.data;
                }
            });
        }
    };
    // Initial load set to page 1
    $scope.getData(1);
    // For newsfeed end


    //Profile crop
    $scope.name = 'World';
    $scope.items = ['item1', 'item2', 'item3'];
    $scope.animationsEnabled = true;
    $scope.open = function (size) {
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: size,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });
        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };

    // add Portfolio
    $scope.WebWizardPortfolioModel = {
        Id: "",
        WebWizardId: "",
        ProjectTitle: "",
        ProjectDescription: "",
        ProjectSize: "",
        ProjectZipFilePath: "",
        ProjectImagePath: "",
        IsPublishNow: "",
        IsFreeDownload: "",
        IsOnlyRegisteredUserCanSee: "",
        TechnologyId: "",
        ProjectTypeId: "",
        BackendLanguageId: "",
        RunOnServerId: "",
        LiveDemoLink: "",
        Status: "",
        CreateDate: "",
        UpdateDate: "",
        CreateBy: "",
        UpdateBy: "",
        File: []
    };


    $scope.openMessageBox = function (sId) {
        var senderId;
        var userType;
        var offlineUser = rowsMsgForWz.find(x => x.senderId == sId);
        if (offlineUser === undefined) {
            senderId = sId;
            $("#senderId").val(senderId);
            userType = $("#onlineSenderType").val();
        }
        else {
            senderId = offlineUser.senderId
            $("#senderId").val(offlineUser.senderId);
            userType = offlineUser.userType;
        }

        //my identificetion
        var myDbId = parseInt($('#userDbId').val());
        var myUserType = $('#hdUserUserType').val();

        //Call Method
        var chatHub = $.connection.chatHub;
        chatHub.server.openMessageBox(senderId, userType, myDbId, myUserType);

    }

})

app.controller('ModalInstanceCtrl', function ($http, $scope, $timeout, $uibModalInstance, items) {
    $scope.myCroppedImage = '';
    $scope.myImage = '';
    $scope.rectangleWidth = 100;
    $scope.rectangleHeight = 100;
    $scope.cropper = {
        cropWidth: $scope.rectangleWidth,
        cropHeight: $scope.rectangleHeight
    };
    var handleFileSelect = function (evt) {
        var file = evt.currentTarget.files[0];
        var reader = new FileReader();
        reader.onload = function (evt) {
            $scope.$apply(function ($scope) {
                $scope.myImage = evt.target.result;
            });
        };
        reader.readAsDataURL(file);
    };
    $timeout(function () { angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect); }, 1000, false);

    $scope.ok = function () {
        $uibModalInstance.close($scope.myCroppedImage);
        //WebWizardDetails Model
        $scope.WebWizardDetailsModel = {
            WebWizardId: "",
            About: "",
            DateOfBarth: "",
            Education: "",
            Latitude: "",
            Longitude: "",
            MobileNo: "",
            ProfilePicture: "",
            ExperienceFrom: "",
            ExperienceTo: "",
            Designation: "",
            Status: "",
            LocationId: "",
            Skills: []
        }
        var blob = dataURItoBlob($scope.myCroppedImage);
        function dataURItoBlob(dataURI) {
            // convert base64/URLEncoded data component to raw binary data held in a string
            var byteString;
            if (dataURI.split(',')[0].indexOf('base64') >= 0)
                byteString = atob(dataURI.split(',')[1]);
            else
                byteString = unescape(dataURI.split(',')[1]);
            // separate out the mime component
            var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
            // write the bytes of the string to a typed array
            var ia = new Uint8Array(byteString.length);
            for (var i = 0; i < byteString.length; i++) {
                ia[i] = byteString.charCodeAt(i);
            }
            return new Blob([ia], { type: mimeString });
        }
        var file = new File([blob], "fileName.jpeg", {
            type: "'image/jpeg'"
        });

        $scope.WebWizardDetailsModel.ProfilePicture = file.name;
        var Data = new FormData();
        Data.append("file", file);

        $http.post('/ClientDashboard/SaveProfilePicture', Data,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
            .then(function (webWizardDetail) {
                //$http.get('/WebWizardDashboard/GetWebWizardDetail').then(function (myWebWizardDetail) {
                //    window.location.reload();
                //});
                window.location.reload();
                // $scope.profilePicture = res.data;
            });
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});



