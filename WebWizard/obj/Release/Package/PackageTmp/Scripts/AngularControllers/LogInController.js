var app = angular.module('LogInrApp', []);
app.controller('LogInCtrl', function ($scope, $http) {
    $scope.Forgot = {
        Id: "",
        Email: "",
        Password: ""
    };
    $scope.SetPassword = {
        Id: "",
        Code: "",
        Password: ""
    };
    $scope.logIn = function (WebWizardLogInModel) {
        $http.post('/LogIn/Index', $scope.WebWizardLogInModel).then(function (WebWizardLogInModel) {

        });
    }

    //client
    $scope.clientForgotOption = function () {
        if ($scope.Forgot.Email == "" || $scope.Forgot.Email == undefined) {
            toastr.info("Incorrect Email or Gmail", "Info");
        }
        else {
            $http.post('/ClientRegistration/ForgotPasswordForClientCode', $scope.Forgot).then(function (Forgot) {
                if (Forgot.data.Email == null) {
                    toastr.info("This Email or Gmail Not Exists", "Info");
                }
                else {
                    window.location.pathname = 'ClientRegistration/ForgotPasswordForClientCodeAndPassword';
                }
            });
            toastr.info("Please wait", "Info");
        }
    }

    $scope.changePassword = function () {

        $http.post('/ClientRegistration/ChangeClientPassword', $scope.SetPassword).then(function (ChangeClientPassword) {
            if (ChangeClientPassword.data.Code == null) {
                toastr.info("Incorrect Code", "Info");
            }
            else {
                toastr.info("Successfuly Update Your Password", "Info"); 
                window.location.pathname = 'LogIn/Index';
            }

        });
    }


    //webWizard
    $scope.webWizardForgotOption = function () {
        if ($scope.Forgot.Email == "" || $scope.Forgot.Email == undefined) {
            toastr.info("Incorrect Email or Gmail", "Info");
        }
        else {
            $http.post('/WebWizardRegister/ForgotPasswordForWizardCode', $scope.Forgot).then(function (Forgot) {
                if (Forgot.data.Email == null) {
                    toastr.info("This Email or Gmail Not Exists", "Info");
                }
                else {
                    window.location.pathname = 'WebWizardRegister/ForgotPasswordForWebWizardCodeAndPassword';
                }
            });
            toastr.info("Please wait", "Info");
        }
    }

    $scope.changePasswordbyWizard = function () {

        $http.post('/WebWizardRegister/ChangeWizardPassword', $scope.SetPassword).then(function (ChangeClientPassword) {
            if (ChangeClientPassword.data.Code == null) {
                toastr.info("Incorrect Code", "Info");
            }
            else {
                toastr.info("Successfuly Update Your Password", "Info");
                window.location.pathname = 'LogIn/Index';
            }

        });
    }






});
