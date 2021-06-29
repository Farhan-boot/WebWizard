var app = angular.module('SubmittedProjectApp', []);
app.controller('SubmittedProjectCtrl', function ($scope, $http) {
    $scope.newsFeedModel = {
        Id: "",
        Title: "",
        Amount: ""
    };
    $scope.workingProcess = ["Pending", "Review", "Accepted"];

    $scope.loadData = function () {
        $http.get('/SubmittedProject/GetNewsFeedList').then(function (newsFeedList) {
            $scope.myNewsFeedList = newsFeedList.data;
        });
    }
    $scope.loadData();

    $scope.submitNewsfeedId = function (id) {
        $scope.newsFeedModel.Id = id;
        $http.post('/SubmittedProject/GetActiveBidWizardList', $scope.newsFeedModel).then(function (activeWizard) {
            $scope.activeWizardList = activeWizard.data;
        });
    }

    $scope.IdModel = {
        NewsFeedId: "",
        WebWizardId: ""
    };
    $scope.submittedProjectList = function (newsfeedId, webWizardId) {
        $scope.IdModel.NewsFeedId = newsfeedId;
        $scope.IdModel.WebWizardId = webWizardId;

        $http.post('/SubmittedProject/SubmittedProjectByClient', $scope.IdModel).then(function (submittedProjectByClient) {
            $scope.submittedProjectByClient = submittedProjectByClient.data;
        });
    }

    $scope.submittedProjects = {
        Id: "",
        NewsFeedId: "",
        PostDate: "",
        Comment: "",
        ProjectUrl: "",
        WebWizardId: "",
        SubmitWorkStatus: ""
    };
    $scope.submitWorkStatus = function (submittedProjects) {
        $scope.submittedProjects = submittedProjects;

        $http.post('/SubmittedProject/SubmitWorkStatusByClient', $scope.submittedProjects).then(function (submitWorkStatus) {
            $scope.workStatus = submitWorkStatus.data;
            toastr.info("Work Status Is " + $scope.workStatus.SubmitWorkStatus, "Info");
           
        });
    }



});
