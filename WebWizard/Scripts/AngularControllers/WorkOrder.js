var app = angular.module('WorkOrderApp', []);
app.controller('WorkOrderCtrl', function ($scope, $http) {

    $scope.submitProject = function () {
        var SubmitProjectModel = {
            Id: "",
            NewsFeedId: $scope.SubmitProjectModel.NewsFeedId,
            ClientId: $scope.SubmitProjectModel.ClientId,
            WizardId: "",
            Comment: $scope.SubmitProjectModel.Comment,
            ProjectUrl: "",
            PostDate: "",
            SubmitWorkStatus: "",
            File: $('#myFile').prop('files')[0]
        };
        var configs = {
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                angular.forEach(data, function (value, key) {
                    formData.append(key, value);
                });
                return formData;
            }
        };
        $http.post('/WebWizardDashboard/SubmitProject', SubmitProjectModel, configs).then(function (response) {
            toastr.info("Successfully Submitted !", "Info");
            SubmitProjectModel = null;
                $scope.SubmitProjectModel.NewsFeedId = null,
                $scope.SubmitProjectModel.ClientId = null,
                $scope.SubmitProjectModel.Comment = null,
                $("#myFile").val(null);
                $scope.getSubmitProject();
            return response.data;
        })
    };
  
    $scope.getSubmitProjectModel = {
        Id: "",
        NewsFeedId: $("#myNewsFeedId").val(),
        ClientId: "",
        WizardId: "",
        Comment: "",
        ProjectUrl: "",
        PostDate: "",
        SubmitWorkStatus: ""
    };
    $scope.getSubmitProject = function () {
        $http.post('/WebWizardDashboard/GetSubmittedProjectByNewsFeedId', $scope.getSubmitProjectModel).then(function (getSubmitProjectModel) {
            $scope.getSubmitProjectList = getSubmitProjectModel.data;
        });
    }
    $scope.getSubmitProject();

    


});
