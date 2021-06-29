var app = angular.module('WebWizardProfileApp', ['ui.bootstrap', 'ngImgCrop', 'ngSanitize']);
app.controller('WebWizardProfileCtrl', function ($scope, $http, $filter, $uibModal, $log) {
    //alert("jesy");

    $scope.reloadWebWizardDetail = function () {
        $http.get('/WebWizardDashboard/MyBidList').then(function (myBid) {
            $scope.myBidList = myBid.data;
        });

        $http.get('/WebWizardDashboard/GetWebWizardDetail').then(function (myWebWizardDetail) {
            $scope.myWebWizardDetail = myWebWizardDetail.data;
          

            $scope.myWebWizardDetail.ExperienceYearFrom = myWebWizardDetail.data.ExperienceYearFrom.toString().replace("/", "").replace(/.$/, "");



            var s = Date($scope.myWebWizardDetail.ExperienceYearFrom);
            $scope.myWebWizardDetail.ExperienceYearFrom = s;


            //show into text box
            $scope.WebWizardDetailsModel.About = $scope.myWebWizardDetail.AboutWebWizard;
            $scope.WebWizardDetailsModel.DateOfBarth = $scope.myWebWizardDetail.DateOfBarth;
            $scope.WebWizardDetailsModel.MobileNo = $scope.myWebWizardDetail.WebWizardMobileNo;
            $scope.WebWizardDetailsModel.Education = $scope.myWebWizardDetail.EducationId.toString();
            $scope.WebWizardDetailsModel.Designation = $scope.myWebWizardDetail.DesignationId.toString();
           // $scope.WebWizardDetailsModel.Status = $scope.myWebWizardDetail.Status;
            $scope.WebWizardDetailsModel.ExperienceFrom = $scope.myWebWizardDetail.ExperienceYearFrom;
            $scope.WebWizardDetailsModel.ExperienceTo = $scope.myWebWizardDetail.ExperienceYearTo;
         //  $scope.myWebWizardDetail.ExperienceYearFrom = $filter('date')(new Date(1585677600000), 'yyyy-MM-dd');
        });
        // all service call
        $http.get('/WebWizardDashboard/GetWebWizardSkills').then(function (WebWizardSkills) {
            $scope.webWizardSkillList = WebWizardSkills.data;
        });

        $http.get('/WebWizardDashboard/GetWebWizardLocationDetail').then(function (WebWizardLocationDetail) {
            $scope.webWizardLocationDetail = WebWizardLocationDetail.data;
        });

        $http.get('/WebWizardDashboard/GetWebWizardEducation').then(function (WebWizardEducation) {
            $scope.webWizardEducation = WebWizardEducation.data;
        });

        $http.get('/WebWizardDashboard/GetWebWizardDesignation').then(function (WebWizardDesignation) {
            $scope.webWizardDesignation = WebWizardDesignation.data;
        });

        $http.get('/WebWizardDashboard/GetEducationList').then(function (EducationList) {
            $scope.educationList = EducationList.data;
        });

        $http.get('/WebWizardDashboard/GetDesignationList').then(function (DesignationList) {
            $scope.designationList = DesignationList.data;
        });

        $http.get('/WebWizardDashboard/GetSkillList').then(function (SkillList) {
            $scope.skillList = SkillList.data;
        });

        // get my newsfeed item call
        $http.get('/WebWizardDashboard/MyNewsFeedList').then(function (myNewsFeedList) {
            $scope.newsFeedList = myNewsFeedList.data;
        });

    }
    $scope.reloadWebWizardDetail();

    $scope.reloadWebWizardRegistrationDetail = function () {
        $http.get('/WebWizardDashboard/GetWebWizardRegistrationDetail').then(function (myWebWizardRegistrationDetail) {
            $scope.myWebWizardRegistrationDetail = myWebWizardRegistrationDetail.data;
        });
    }
    $scope.reloadWebWizardRegistrationDetail();
    
    //get Web Wizard skills
    $scope.SkillItemId = [];
    $scope.SkillItem = [];
    $scope.addSkill = function () {
        $scope.SkillItem.push($("#itemValue").val());
        $scope.removeItem = null;
        for (var i = 0; i < $scope.skillList.length; i++) {
            if ($scope.skillList[i].NameOfSkill == $("#itemValue").val()) {
                $scope.removeItem = $scope.skillList[i];
                $scope.SkillItemId.push($scope.removeItem.Id);
                $scope.skillList.splice($scope.removeItem, $scope.skillList.indexOf($scope.removeItem));
            }
        }
    }

    //get Web Wizard locetion
    $scope.latitude;
    $scope.longitude;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            $scope.$apply(function () {
                $scope.position = position;
                // alert($scope.position);
                $scope.latitude = position.coords.latitude;
                $scope.longitude = position.coords.longitude;
            });
        });
    }

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
        LocationId:"",
        Skills: []
    };
    //set defolt value into object
    $scope.WebWizardDetailsModel.DateOfBarth = new Date();
    $scope.WebWizardDetailsModel.ExperienceFrom = new Date();
    $scope.WebWizardDetailsModel.ExperienceTo = new Date();

    //post Web Wizard Details
    $scope.WizardDetails = function () {
        $scope.WebWizardDetailsModel.Latitude = $scope.latitude;
        $scope.WebWizardDetailsModel.Longitude = $scope.longitude;


        for (var i = 0; i < $scope.SkillItemId.length; i++) {
            $scope.WebWizardDetailsModel.Skills.push($scope.SkillItemId[i])
        }
        $scope.WebWizardDetails = $scope.WebWizardDetailsModel;

        if ($scope.WebWizardDetails.ExperienceFrom < $scope.WebWizardDetails.ExperienceTo) {

            $http.post('/WebWizardDashboard/WebWizardDetails', $scope.WebWizardDetails).then(function (WebWizardDetails) {
                $scope.reloadWebWizardDetail();
                $scope.reloadWebWizardRegistrationDetail();
            });


        }
        else {
            $scope.SkillItemId = null;
            $scope.SkillItem = null;
            toastr.error("Invalid Your Experience Date !", "Error");
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


    //Start WebWizard Portfolio

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

        // Web Wizard Portfolio List
        $http.get('/WebWizardPortfolio/GetWebWizardPortfolioListByWizardId').then(function (WebWizardPortfolioList) {
            $scope.webWizardPortfolioList = WebWizardPortfolioList.data;
        });

    };
    $scope.portfolioAllListLoad();


    $scope.projectDetals = function (Id) {
        $scope.singleprojectDetals = $scope.webWizardPortfolioList.filter(x => x.Id === Id)[0];
        $scope.singleprojectDetals.ProjectSize = Math.round($scope.singleprojectDetals.ProjectSize/1024);
    }

    //update advanced settings
    $scope.WebWizardRegisterModel = {
        WebWizardId: "",
        FirstName: "",
        LastName: "",
        Email: "",
        NameTitle: "",
        StateId: "",
        Password: "",
        StartAsCompany: "",
        NoOfEmployees: "",
        StartAsFreelancer: "",
        TermsAndConditionsId: "",
        VerificationCode: "",
        Status: ""
    };
    $scope.SubmitAndChange = function () {
        $scope.WebWizardRegisterModel.FirstName = $scope.myWebWizardRegistrationDetail.FirstName;
        $scope.WebWizardRegisterModel.LastName = $scope.myWebWizardRegistrationDetail.LastName;
        $scope.WebWizardRegisterModel.Email = $scope.myWebWizardRegistrationDetail.Email;
        $scope.WebWizardRegisterModel.Password = $scope.myWebWizardRegistrationDetail.Password;

        if ($scope.WebWizardRegisterModel.FirstName == undefined || $scope.WebWizardRegisterModel.LastName == undefined || $scope.WebWizardRegisterModel.Email == undefined || $scope.WebWizardRegisterModel.Password == undefined) {
            toastr.error("Invalid Your Advanced Settings", "Error");
        }
        else {
            $http.post('/WebWizardRegister/UpdateAdvancedSettings', $scope.WebWizardRegisterModel).then(function (registerModel) {
              
                toastr.info("Update Your Advanced Settings", "Info");
                setTimeout(function () {
                    $scope.$apply(function () {
                        window.location.reload();
                    });
                }, 2000); 
            });
        }
    }


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
        File:[]
    };

    $scope.filterCatagory = function (id) {
        $scope.webWizardPortfolioList = $scope.webWizardPortfolioList.filter(x => x.ProjectTypeId === id);
    }
    $scope.reloadWebWizardProtfulio = function () {
        // Web Wizard Portfolio List
        $http.get('/WebWizardPortfolio/GetWebWizardPortfolioListByWizardId').then(function (WebWizardPortfolioList) {
            $scope.webWizardPortfolioList = WebWizardPortfolioList.data;
        });

        $("input[type='checkbox']").prop({
            checked: false
        });
    }

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
                        $http.post('/WebWizardDashboard/DeleteWebWizardNewsFeed', $scope.obj).then(function (deleteWebWizard) {
                            $scope.reloadWebWizardDetail();
                            toastr.info("Delete the item", "Info");
                        });
                    }
                }
            });
    }

    //$scope.WebWizardPortfolioUpload = function () {
    //    $scope.webWizardPortfolio = $scope.WebWizardPortfolioModel;
    //    $scope.fileZip = document.getElementById('myFile').files[0],
    //        $scope.webWizardPortfolio.File = $scope.fileZip;
    //   // $http.post('/WebWizardPortfolio/AddWebWizardPortfolio', $scope.webWizardPortfolio).then(function (webWizardPortfolio) {
    //   // });
    //   var Data = new FormData();
    //   Data.append("file", $scope.webWizardPortfolio.File);
      
    //    $http.post('/WebWizardPortfolio/AddWebWizardPortfolio', Data,
    //        {
    //            withCredentials: true,
    //            headers: { 'Content-Type': undefined },
    //            transformRequest: angular.identity
    //        })
    //        .then(function (webWizardDetail) {

    //        });
    //};

    // add NewsFeed
    //$scope.NewsFeedModel = {
    //    Id: "",
    //    UserId: "",
    //    Title: "",
    //    Content: "",
    //    IsWebWizard: ""
    //};
    //$scope.addNewsFeed = function () {
    //    var element = angular.element('#post');
    //    // $scope.NewsFeedModel.Content = JSON.stringify(element);
    //    var ff = $('#post').val();

    //    $http.post('/NewsFeed/SaveNewsFeed', $scope.NewsFeedModel).then(function (newsFeed) {
    //        toastr.info("Add Your NewsFeed", "Info");
    //    });
    //}


})

app.controller('ModalInstanceCtrl', function ($http,$scope, $timeout, $uibModalInstance, items) {
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

        $http.post('/WebWizardDashboard/SaveProfilePicture', Data,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
            .then(function (webWizardDetail) {
                $http.get('/WebWizardDashboard/GetWebWizardDetail').then(function (myWebWizardDetail) {
                    window.location.reload();
                });
                window.location.reload();
               // $scope.profilePicture = res.data;
            });
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});








 