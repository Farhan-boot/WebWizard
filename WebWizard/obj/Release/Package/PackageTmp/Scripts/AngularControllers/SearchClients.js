var app = angular.module('SearchClientsApp', []);
app.controller('SearchClientsCtrl', function ($scope, $http) {
    $scope.WebWizardLogInModel = {
        Id: "",
        Email: "",
        Password: ""
    };
    $http.get('/WebWizardRegister/GetCountry').then(function (GetCountryList) {
        $scope.CountryList = GetCountryList.data;
    });

    //$http.get('/Search/GetWebWizardListForSearch').then(function (getWebWizardList) {
    //    $scope.webWizardList = getWebWizardList.data;
    //});


    // Initialize variable
    $scope.pageno = 1;
    $scope.PageNumber = 1;
    // This would fetch the data on page change.
    $scope.getData = function (pageno, Name) {
        // Proceed to search function once validation success
        if ($scope.PageNumber <= 0) {
            $scope.pageno = 1;
            $scope.PageNumber = 1;
        }
        else {
            // Assign page number
            $scope.pageno = pageno;
            $scope.Name = Name;
            $scope.SearchController = {
                Page: pageno,
                Name: Name
            }
            $http.post('/Search/GetClientsListForSearch', $scope.SearchController).then(function (getWebWizardList) {
                if (getWebWizardList.data.length == 0) {
                    $scope.pageno = pageno;
                    $scope.PageNumber = pageno - 1;
                    toastr.info("End of the list", "Info");
                }
                else {
                    $scope.webWizardList = getWebWizardList.data;
                }
            });
        }
    };
    // Initial load set to page 1
    $scope.getData(1);


   
});
