var app = angular.module('NewsFeedApp', ['ngSanitize']);

app.controller('NewsFeedCtrl', function ($scope, $http) {
    //alert("test");
    //$scope.WebWizardLogInModel = {
    //    Id: "",
    //    Email: "",
    //    Password: ""
    //};
    $scope.numLimit = 500;
    $scope.readMore = function () {
        $scope.numLimit = 100000;
    };

    $scope.loadNewsFeed = function () {
        // For newsfeed start
        // Initialize variable
        $scope.itemsPerPage = 10;
        $scope.pageno = 1;
        $scope.total_count = 0;
        $scope.dishesPageList = [];
        $scope.newPageNumber = 1;
        // This would fetch the data on page change.
        $scope.getData = function (pageno) {
            // Proceed to search function once validation success
            if ($scope.newPageNumber <= 0) {
                $scope.pageno = 1;
                $scope.newPageNumber = 1;
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
                $scope.NewsFeedController = {
                    Page: pageno
                }
                $http.post('/NewsFeed/GetMyNewsFeed', $scope.NewsFeedController).then(function (DishesPageList) {
                    if (DishesPageList.data.length == 0) {
                        $scope.pageno = pageno;
                        $scope.newPageNumber = pageno - 1;
                        toastr.info("End of the list", "Info");
                    }
                    else {
                        $scope.dishesPageList = DishesPageList.data;
                    }
                });
            }
        };
        // Initial load set to page 1
        $scope.getData(1);
        // For newsfeed end


        // Initialize variable
        $scope.itemsPerPageForClient = 10;
        $scope.pagenoForClient = 1;
        $scope.total_countForClient = 0;
        $scope.dishesPageListForClient = [];
        $scope.newPageNumberForClient = 1;
        // This would fetch the data on page change.
        $scope.getDataForClient = function (pagenoForClient) {
            // Proceed to search function once validation success
            if ($scope.newPageNumberForClient <= 0) {
                $scope.pagenoForClient = 1;
                $scope.newPageNumberForClient = 1;
            }
            else {
                $scope.LoadingTextForClient = "Loading Dishes...";
                $scope.showLoadingForClient = true;
                // Resets page list and total count on each page change
                $scope.DishesPageListForClient = {
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
                $scope.total_countForClient = 0;
                // Assign new page number
                $scope.pagenoForClient = pagenoForClient;
                $scope.NewsFeedController = {
                    PageForClient: pagenoForClient
                }
                $http.post('/NewsFeed/GetMyNewsFeedForClient', $scope.NewsFeedController).then(function (DishesPageListForClient) {
                    if (DishesPageListForClient.data.length == 0) {
                        $scope.pagenoForClient = pagenoForClient;
                        $scope.newPageNumberForClient = pagenoForClient - 1;
                        toastr.info("End of the list", "Info");
                    }
                    else {
                        $scope.dishesPageListForClient = DishesPageListForClient.data;
                    }
                });
            }
        };
        // Initial load set to page 1
        $scope.getDataForClient(1);
        // For newsfeed end


        $http.get('/NewsFeed/GetNewsFeedCounter').then(function (NewsFeedCounter) {
            $scope.newsFeedCounter = NewsFeedCounter.data;
        });

       

        //GetAllClient
        // Initialize variable
        $scope.itemsPerPage4 = 10;
        $scope.pageno4 = 1;
        $scope.total_count4 = 0;
        $scope.dishesPageList4 = [];
        $scope.newPageNumber4 = 1;
        // This would fetch the data on page change.
        $scope.getData4 = function (pageno4) {
            // Proceed to search function once validation success
            if ($scope.newPageNumber4 <= 0) {
                $scope.pageno4 = 1;
                $scope.newPageNumber4 = 1;
            }
            else {
                $scope.LoadingText4 = "Loading Dishes...";
                $scope.showLoading4 = true;
                // Resets page list and total count on each page change
                $scope.AllClientDishesPageList = {
                    ClientId: '',
                    FirstName: '',
                    LastName: '',
                    ClientProfileImageUrl: '',
                    AboutClient: '',
                    ClientEmail: '',
                    ClientMobileNo: ''
                };
                $scope.total_count4 = 0;
                // Assign new page number
                $scope.pageno4 = pageno4;
                $scope.NewsFeedController = {
                    Page4: pageno4
                }
                $http.post('/NewsFeed/GetAllClient', $scope.NewsFeedController).then(function (AllClientDishesPageList) {
                    if (AllClientDishesPageList.data.length == 0) {
                        $scope.pageno4 = pageno4;
                        $scope.newPageNumber4 = pageno4 - 1;
                        toastr.info("End of the list", "Info");
                    }
                    else {
                        $scope.allClientDishesPageList = AllClientDishesPageList.data;
                    }
                });
            }
        };
        // Initial load set to page 1
        $scope.getData4(1);
        // For newsfeed end

        
       


    }
    $scope.loadNewsFeed();

    $scope.newsfeedBid = {
        Id: '',
        NewsfeedId: '',
        WebWizardId: '',
        BidAmount: '',
        BidContent: '',
        PostDate: '',
        Status: ''
    };

    //modalBid popUp
    $scope.modalBid = function (id) {
        $scope.newsfeedBid.NewsfeedId = id;
        $http.post('/NewsFeed/UpdateNewsFeedBidForClient', $scope.newsfeedBid).then(function (newsfeedBid) {
            $scope.myNewsfeedForUpdate = newsfeedBid.data;
            if ($scope.myNewsfeedForUpdate.Status == true) {
                toastr.info("You Can Change Becouse Client is Accepeted.", "Info"); 
            }
            else {
                if ($scope.myNewsfeedForUpdate == "" || $scope.myNewsfeedForUpdate == null || $scope.myNewsfeedForUpdate == undefined) {
                    toastr.info("You Can Proposal Your Bid.", "Info");

                }
                else {
                    $scope.newsfeedBid.Id = $scope.myNewsfeedForUpdate.Id;
                    $scope.newsfeedBid.BidAmount = $scope.myNewsfeedForUpdate.BidAmount;
                    $scope.newsfeedBid.BidContent = $scope.myNewsfeedForUpdate.BidContent;
                    $scope.newsfeedBid.NewsfeedId = $scope.myNewsfeedForUpdate.NewsfeedId;
                    $scope.newsfeedBid.Status = $scope.myNewsfeedForUpdate.Status;
                    $scope.newsfeedBid.WebWizardId = $scope.myNewsfeedForUpdate.WebWizardId;
                    toastr.info("You Can Update Your Bid until client are not Accepeted.", "Info");
                }     
            }

        });

    }
    $scope.newsFeedBidForClient = function (newsfeedBid) {
        $http.post('/NewsFeed/AddNewsFeedBidForClient', $scope.newsfeedBid).then(function (newsfeedBid) {
            $scope.check = newsfeedBid.data;
            if ($scope.check.Status == true) {
                toastr.info("You Can't Change Becouse Client is Accepeted.", "Info"); 
            }
            else {
                toastr.info("Your Bid is Submit please wait for confirmation.", "Info");
                $scope.newsfeedBid.BidAmount = "";
                $scope.newsfeedBid.BidContent = "";    
            }

        });
    }




   












});


