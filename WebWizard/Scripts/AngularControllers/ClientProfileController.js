var app = angular.module('ClientProfileApp', ['ui.bootstrap', 'ngImgCrop', 'ngSanitize']);
app.controller('ClientProfileCtrl', function ($scope, $http, $filter, $uibModal, $log) {
    $scope.NewsFeed = {
        Id: "",
    };
    $scope.ViewProposal = function (newsFeedId) {
      //  window.location.href = '/ClientDashboard/MyViewProposalList';
        $scope.NewsFeed.Id = parseInt(newsFeedId);
        $http.post('/ClientDashboard/GetProposalList', $scope.NewsFeed).then(function (ProposalList) {
            $scope.proposalList = ProposalList.data;
        });
    }

    $scope.deleteBid = function (Id) {
        $scope.NewsFeed.Id = parseInt(Id);
        $http.get('/ClientDashboard/DeleteBid', $scope.NewsFeed).then(function (deleteBidByClient) {
            toastr.info("Delete the Bid !", "Info");
        });
    }

  

    $scope.ApprovedByClient = function (Id) {
        $scope.NewsFeed.Id = parseInt(Id);
        $http.post('/ClientDashboard/ApprovedByClient', $scope.NewsFeed).then(function (approve) {
            toastr.info("Approved By Client !", "Info");
            $("#ViewProposalId").click();
        });
    }

   

  //NewsFeed Counter;
    $http.get('/NewsFeed/GetNewsFeedCounter').then(function (NewsFeedCounter) {
        $scope.newsFeedCounter = NewsFeedCounter.data;
    });
    //EightWebWizardList;
    $http.get('/ClientDashboard/GetEightWebWizardList').then(function (EightWebWizardList) {
        $scope.eightWebWizardList = EightWebWizardList.data;
        angular.forEach($scope.eightWebWizardList, function (value, key) {
            if (value.WebWizardProfileImageUrl == null)
                value.WebWizardProfileImageUrl = "user-demo.png"; 
        });
    });

    //GetClientInfo
    $http.get('/ClientDashboard/GetClientInfo').then(function (ClientInfo) {
        $scope.clientInfo = ClientInfo.data;
    });

    //Education List
    $http.get('/WebWizardDashboard/GetEducationList').then(function (ClientEducation) {
        $scope.clientEducation = ClientEducation.data;
    });
     //clientInformetionModel
    $scope.ClientDetailsModel = {
        ClientId: '',
        AboutClient: '',
        DateOfBarth: '',
        ClientMobileNo: '',
        EducationId: ''
    };
    //clientInformetion
    $scope.clientInformetion = function () {
        $scope.ClientDetailsModel.DateOfBarth = new Date($scope.ClientDetailsModel.DateOfBarth);

        if ($scope.ClientDetailsModel.AboutClient == "" || $scope.ClientDetailsModel.AboutClient == undefined || $scope.ClientDetailsModel.DateOfBarth == "" || $scope.ClientDetailsModel.DateOfBarth == undefined || $scope.ClientDetailsModel.ClientMobileNo == "" || $scope.ClientDetailsModel.ClientMobileNo == undefined || $scope.ClientDetailsModel.EducationId == "" || $scope.ClientDetailsModel.EducationId == undefined) {
            toastr.info("Every Filed are requerd", "Info");
        }
        else
        {
            $http.post('/ClientDashboard/ClientDetails', $scope.ClientDetailsModel).then(function (clientDetails) {

                toastr.info("Save", "Info");
            });
           
        }
    }

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


    $scope.portfolioAllListLoad = function () {
        //Get Project Type
        $http.get('/WebWizardPortfolio/GetProjectType').then(function (myProjectType) {
            $scope.projectType = myProjectType.data;
        });
        //Get Technology
        $http.get('/WebWizardPortfolio/GetTechnology').then(function (myTechnology) {
            $scope.technology = myTechnology.data;
        });
        //Get Backend language
        $http.get('/WebWizardPortfolio/GetBackendlanguage').then(function (myBackendlanguage) {
            $scope.backendlanguage = myBackendlanguage.data;
        });
        //Get Backend language
        $http.get('/WebWizardPortfolio/GetRunOnServer').then(function (myRunOnServer) {
            $scope.runOnServer = myRunOnServer.data;
        });

        $scope.ProjectGroup = ['Service', 'Development'];

    };
    $scope.portfolioAllListLoad();


    // Initialize variable
    $scope.itemsPerPage = 10;
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
                WebWizardId: '',
                Title: '',
                PostDate: '',
                PostContent: '',
                IsWebWizard: ''
            };
            $scope.total_count = 0;
            // Assign new page number
            $scope.pageno = pageno;
            $scope.ClientDashboardController = {
                Page: pageno
            }
            $http.post('/ClientDashboard/GetMyNewsFeedItems', $scope.ClientDashboardController).then(function (DishesPageList) {
                if (DishesPageList.data.length == 0) {
                    $scope.pageno = pageno;
                    $scope.PageNumber = pageno - 1;
                    toastr.info("End of the list", "Info");
                }
                else {
                    $scope.newsFeedList = DishesPageList.data;
                }
            });
        }
    };
    // Initial load set to page 1
    $scope.getData(1);
        // get my newsfeed item call


    $scope.obj = new Object();
    $scope.deleteNewsFeedItem = function (id) {
        $scope.obj.Id = id;
        toastr.warning("<button type='button' class='btn btn-info btn-sm' value='yes'>Yes</button>", 'Are you sure you want to delete ?',
            {
                allowHtml: true,
                onclick: function (toast) {
                    value = toast.target.value
                    if (value == 'yes') {
                        //alert(obj.Id);
                        $http.post('/ClientDashboard/DeleteClientNewsFeed', $scope.obj).then(function (deleteWebWizard) {
                            $http.get('/ClientDashboard/GetMyNewsFeedItems').then(function (myNewsFeedList) {
                                $scope.newsFeedList = myNewsFeedList.data;
                            });
                            toastr.info("Delete the item", "Info");
                        });
                    }
                }
            });
    }



    $scope.openMessageBox = function(sId) {
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















 