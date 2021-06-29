var app = angular.module('RegisterApp', ['ngCookies']);
app.controller('RegisterCtrl', function ($scope, $http, $window, $cookies) {
    //var WebWizardRegisterModel = new Object();
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

    $http.get('/WebWizardRegister/GetCountry').then(function (GetCountryList) {
        $scope.CountryList = GetCountryList.data;
    });

    $http.get('/WebWizardRegister/GetTermAndCondition').then(function (GetTermAndConditionList) {
        $scope.TermAndConditionList = GetTermAndConditionList.data;
    });

    $(".selectUI").select2({
        theme: "classic"
    });

    $scope.stepOne = function (stepOne) {

        this.WebWizardRegisterModel = stepOne;

        if ($scope.WebWizardRegisterModel.NameTitle == "" || $scope.WebWizardRegisterModel.NameTitle == undefined || $scope.WebWizardRegisterModel.FirstName == "" || $scope.WebWizardRegisterModel.FirstName == undefined || $scope.WebWizardRegisterModel.LastName == "" || $scope.WebWizardRegisterModel.LastName == undefined || $scope.WebWizardRegisterModel.Email == "" || $scope.WebWizardRegisterModel.Email == undefined) {
            $scope.WebWizardRegisterModel = null;
            //  window.location.pathname = 'WebWizardRegister/Register';
            toastr.error("Invalid Field !", "Error");
        }
        else {
            $http.post('/WebWizardRegister/Register', $scope.WebWizardRegisterModel).then(function (WebWizardRegisterModel) {
               // $scope.WebWizardRegisterModel = null;
                $cookies.put("FirstName", WebWizardRegisterModel.data.FirstName);
                $cookies.put("LastName", WebWizardRegisterModel.data.LastName);
                $cookies.put("Email", WebWizardRegisterModel.data.Email);
                $cookies.put("NameTitle", WebWizardRegisterModel.data.NameTitle);

                if (WebWizardRegisterModel.data.Status == true) {
                    window.location.pathname = 'WebWizardRegister/Register_02';
                    toastr.info("Congratulations step one complete", "Information");
                }
                else {
                   // window.location.pathname = 'WebWizardRegister/Register';
                    toastr.info("This email already exists", "Information");
                }

            });

        }
    }

    $scope.stepTwo = function (stepTwo) {
        if (stepTwo.TermsAndConditionsId == true) {
            $scope.WebWizardRegisterModel = stepTwo;
            $scope.WebWizardRegisterModel.Email = $cookies.get('Email');

            if (angular.equals($scope.WebWizardRegisterModel.Password, $scope.WebWizardRegisterModel.RetypePassword)) {
                $http.post('/WebWizardRegister/Register_02', $scope.WebWizardRegisterModel).then(function (WebWizardRegisterModel) {
                    $cookies.put("Password", WebWizardRegisterModel.data.Password);
                    $cookies.put("StateId", WebWizardRegisterModel.data.StateId);
                    $cookies.put("StartAsFreelancer", WebWizardRegisterModel.data.StartAsFreelancer);
                   // $cookies.put("Email", WebWizardRegisterModel.data.Email);
                    $cookies.put("TermsAndConditionsId", WebWizardRegisterModel.data.TermsAndConditionsId);
                    window.location.pathname = 'WebWizardRegister/Register_03';
                   // $scope.WebWizardRegisterModel = null;
                });

            }
            else {
                alert("Password are not match !");
            }
               
           // $scope.WebWizardRegisterModel = null;
        }
        else {
           // $scope.WebWizardRegisterModel = null;
            toastr.info("Please Select Terms And Conditions !", "Information");
        }
    }


    $scope.stepThree = function (stepThree) {

        if (stepThree.VerificationCode == "" || stepThree.VerificationCode == undefined) {
            toastr.info("Please put the Verification Code from your email !", "Information");
        }
        else {
            //stepOne
            $scope.WebWizardRegisterModel.FirstName = $cookies.get('FirstName');
            $scope.WebWizardRegisterModel.LastName = $cookies.get('LastName');
            $scope.WebWizardRegisterModel.Email = $cookies.get('Email');
            $scope.WebWizardRegisterModel.NameTitle = $cookies.get('NameTitle');
            //stepTwo
            $scope.WebWizardRegisterModel.StateId = $cookies.get('StateId');
            $scope.WebWizardRegisterModel.Password = $cookies.get('Password');
            $scope.WebWizardRegisterModel.StartAsFreelancer = $cookies.get('StartAsFreelancer');
            $scope.WebWizardRegisterModel.TermsAndConditionsId = $cookies.get('TermsAndConditionsId');
            $scope.WebWizardRegisterModel.VerificationCode = stepThree.VerificationCode;

            $http.post('/WebWizardRegister/Register_03', $scope.WebWizardRegisterModel).then(function (WebWizardRegisterModel) {

                if (WebWizardRegisterModel.data.VerificationCode == null) {
                    toastr.info("Please enter correct Verification Code !", "Information");
                }
                else {
                    if (WebWizardRegisterModel.data.Status == false) {
                        toastr.info("Right now your email already used !", "Information");

                    }
                    else {
                        toastr.info("Verification your email !", "Information");
                        window.location.pathname = 'WebWizardRegister/Register_04';
                    }
                }
               
            });
           
        }
    }

  });
