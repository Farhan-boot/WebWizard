var app = angular.module('SearchProjectsApp', []);
app.controller('SearchProjectsCtrl', function ($scope, $http) {
    $scope.WebWizardLogInModel = {
        Id: "",
        Email: "",
        Password: ""
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
        $http.get('/WebWizardPortfolio/GetRunOnServer').then(function (myRunOnServer) {
            $scope.runOnServer = myRunOnServer.data;
        });
    };
    $scope.portfolioAllListLoad();



    // Initialize variable
    $scope.pageno = 1;
    $scope.PageNumber = 1;
    // This would fetch the data on page change.
    $scope.getData = function (pageno) {
        // Proceed to search function once validation success
        if ($scope.PageNumber <= 0) {
            $scope.pageno = 1;
            $scope.PageNumber = 1;
        }
        else {
            // Assign page number
            $scope.pageno = pageno;
            $scope.SearchController = {
                Page: pageno
            }
            $http.post('/Search/GetProjectsListForSearch', $scope.SearchController).then(function (getProjectList) {
                if (getProjectList.data.length == 0) {
                    $scope.pageno = pageno;
                    $scope.PageNumber = pageno - 1;
                    toastr.info("End of the list", "Info");
                }
                else {
                    $scope.projectList = getProjectList.data;
                }
            });
        }
    };
    // Initial load set to page 1
    $scope.getData(1);

   


});
