var app = angular.module('RegisterClientApp', ['ngCookies']);
app.controller('RegisterClientCtrl', function ($scope, $http, $window, $cookies) {
    //alert("jesy");
    $scope.ClientLogInModel = {
        Id: "",
        Email: "",
        Password: ""
    };

    $http.get('/ClientRegistration/GetCountry').then(function (GetCountryList) {
        $scope.CountryList = GetCountryList.data;
    });


    $scope.ClientRegisterModel = {
        NameTitle: "",
        FirstName: "",
        LastName: "",
        Email: "",
        StateId: "",
        Password: "",
        VerificationCode: ""
    };

   

    $scope.stepOne = function (stepOne) {

        this.ClientRegisterModel = stepOne;

        if ($scope.ClientRegisterModel.NameTitle == "" || $scope.ClientRegisterModel.NameTitle == undefined || $scope.ClientRegisterModel.FirstName == "" || $scope.ClientRegisterModel.FirstName == undefined || $scope.ClientRegisterModel.LastName == "" || $scope.ClientRegisterModel.LastName == undefined || $scope.ClientRegisterModel.Email == "" || $scope.ClientRegisterModel.Email == undefined || $scope.ClientRegisterModel.StateId == "" || $scope.ClientRegisterModel.StateId == undefined || $scope.ClientRegisterModel.Password == "" || $scope.ClientRegisterModel.Password == undefined) {
            $scope.ClientRegisterModel = null;
            //  window.location.pathname = 'WebWizardRegister/Register';
            toastr.error("Invalid Field !", "Error");
        }
        else {
            $http.post('/ClientRegistration/Register', $scope.ClientRegisterModel).then(function (ClientRegisterModel) {
               // $scope.ClientRegisterModel = null;
                $cookies.put("FirstName", ClientRegisterModel.data.FirstName);
                $cookies.put("LastName", ClientRegisterModel.data.LastName);
                $cookies.put("RegEmail", ClientRegisterModel.data.Email);
                $cookies.put("NameTitle", ClientRegisterModel.data.NameTitle);
                $cookies.put("StateId", ClientRegisterModel.data.StateId);
                $cookies.put("Password", ClientRegisterModel.data.Password);

                if (ClientRegisterModel.data.Status == true) {
                    window.location.pathname = 'ClientRegistration/Register2';
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
        $cookies.put("VerificationCode", stepTwo.VerificationCode);


        //stepOne
        $scope.ClientRegisterModel.NameTitle = $cookies.get('NameTitle');
        $scope.ClientRegisterModel.FirstName = $cookies.get('FirstName');
        $scope.ClientRegisterModel.LastName = $cookies.get('LastName');
        $scope.ClientRegisterModel.Email = $cookies.get('RegEmail');
        $scope.ClientRegisterModel.StateId = $cookies.get('StateId');
        $scope.ClientRegisterModel.Password = $cookies.get('Password');
        $scope.ClientRegisterModel.VerificationCode = $cookies.get('VerificationCode');

        if (stepTwo.VerificationCode == ""||stepTwo.VerificationCode == undefined) {
           
            toastr.error("Please Type Your Verification Code !", "Information");  
        }
        else {
            $http.post('/ClientRegistration/AddClient', $scope.ClientRegisterModel).then(function (ClientRegisterModel) {

                if (ClientRegisterModel.data.VerificationCode==null) {
                    toastr.info("Please type correct verification code!", "Information");
                }
                else {
                    toastr.info("Registetion Complite!", "Information");  
                    //auto login
                    $scope.ClientLogInModel.Email = $scope.ClientRegisterModel.Email;
                    $scope.ClientLogInModel.Password = $scope.ClientRegisterModel.Password;
                    $http.post('/ClientLogIn/Index', $scope.ClientLogInModel).then(function (ClientLogInModel) {
                        // Set a cookie email and password
                        $.cookie('clientEmail', ClientLogInModel.data.Email);
                        $.cookie('clientPassword', ClientLogInModel.data.Password);
                        window.location.pathname = 'ClientDashboard/Profile';
                    });
                }

            });
        }
    }


   

  });
