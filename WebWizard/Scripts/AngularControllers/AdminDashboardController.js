var app = angular.module('AdminDashboardApp', ['ui.bootstrap', 'ngImgCrop', 'ngSanitize']);
app.controller('AdminDashboardCtrl', function ($scope, $http, $filter, $uibModal, $log) {
    $scope.Forgot = {
        Id: "",
        Email: "", 
        Password: ""
    };

    //Get Wizard Portfolio List
    $scope.LoadData = function () {
        $http.get('/Admin/GetWizardPortfolioListbyStatus').then(function (WizardPortfolioList) {
            $scope.portfolioList = WizardPortfolioList.data;
        });
    }
    $scope.LoadData();

    $scope.acceptedRequest = {
        Id: "",
        ProjectDescription: "",
        ProjectTitle: "",
        Status: ""
    };
    $scope.accepted = function (Id) {
        $scope.acceptedRequest.Id = Id;
        $http.post('/Admin/AcceptedRequest', $scope.acceptedRequest).then(function (Request) {
            $scope.LoadData();
        });
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

   // alert("test");

});

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
        //AdminDetails Model

        $scope.AdminDetailsModel = {
            adminProfile: "",
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

        $scope.AdminDetailsModel.adminProfile = file.name;
        var Data = new FormData();
        Data.append("file", file);

        $http.post('/Admin/SaveProfilePicture', Data,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
            .then(function (adminLog) {
                window.location.reload();
            });
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});
