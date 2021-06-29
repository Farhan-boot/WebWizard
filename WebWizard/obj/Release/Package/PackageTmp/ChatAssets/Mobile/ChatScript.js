function getData() {
    $("#notCount").empty();
    $.ajax({
        url: '/WebWizardDashboard/GetNotificationForWebWizard',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $("#tbl").empty();
                // $tbl.append(' <tr><th>ID</th><th>Name</th><th>Last Executed Date</th><th>Status</th></tr>');
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    // rows.push('<li style="padding:5px">' + data[i].Title + '</li>');
                    rows.push('<li>' +
                        '<a href="/NewsFeed/ClientNewsFeedDetails/' + data[i].Id + ' " >' +
                        '<div class="pull-left" style="padding: 6px;">' +

                        '<img style="height: 60px; width: 60px;border-radius: 50%;" src=' + data[i].WebWizardProfileImageUrl + '>' +
                        '</div>' +
                        '<h4>' +
                        data[i].Email
                        + '<small style="float: right;">' + '<i class="fa fa-money" style="padding-right: 5px;">' + '</i>' + data[i].Amount + '</small>' +
                        '</h4>' +
                        '<p>' + data[i].Title + '</p>' +
                        '</a>' +
                        '</li>');
                }
                $("#tbl").append(rows.join(''));
            }
        }
    });
}

function getData2() {
    //GetWebWizardNotification
    $("#notCount2").empty();
    $.ajax({
        url: '/WebWizardDashboard/GetWebWizardNotification',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $("#tbl2").empty();
                // $tbl.append(' <tr><th>ID</th><th>Name</th><th>Last Executed Date</th><th>Status</th></tr>');
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    // rows.push('<li style="padding:5px">' + data[i].Title + '</li>');
                    rows.push('<li>' +
                        '<a href="/WebWizardDashboard/WorkOrder/' + data[i].NewsfeedId + ' " >' +
                        '<div class="pull-left" style="padding: 6px;">' +

                        '<img style="height: 60px; width: 60px;border-radius: 50%;" src=' + "/ClientAssets/ClientDashboard/ProfileImage/" + data[i].ClientImagePath + '>' +


                        '</div>' +
                        '<h4>' +
                        data[i].FullName.substring(0, 8)
                        + '<small style="float: right;">' + '<i class="fa fa-bell-o" style="padding-right:5px">' + '</i>' + "Accepeted" + '</small>' +
                        '</h4>' +
                        '<p>' + data[i].BidContent.substring(0, 8) + '</p>' +
                        '</a>' +
                        '</li>');
                }
                $("#tbl2").append(rows.join(''));
            }
        }
    });

}

var rowsMsgForWz = [];
var isNullgetData3 = false;
var isFirstTimegetData3 = false;
var currentPageNumber = 1;
function getData3(currentPage) {
    //GetWebWizardMessageList
    isNullgetData3 = false;
    if (currentPage === 1) {
        isFirstTimegetData3 = true;
        currentPageNumber = 1;
        $("#tbl3").empty();

        var myDbId = $('#userDbId').val();
        var myUserType = $('#hdUserUserType').val();

    }

    $("#notCount3").empty();
    if (currentPage >= 1) {
        if (currentPage === 1) {
            $("#tbl3").empty();
            rowsMsgForWz = [];
        }

        $.ajax({
            url: '/WebWizardDashboard/GetWebWizardMessageList',
            type: 'POST',
            data: { pageNumber: currentPage, pageSize: 8 },
            datatype: 'json',
            success: function (data) {
                if (data.length > 0) {
                    // $("#tbl3").empty();
                    // $tbl.append(' <tr><th>ID</th><th>Name</th><th>Last Executed Date</th><th>Status</th></tr>');

                    var tbl3 = $("#tbl3");
                    for (var i = 0; i < data.length; i++) {
                        var userDbId = $('#userDbId').val();


                        rowsMsgForWz.push({
                            senderId: data[i].SenderId,
                            userType: data[i].UserType
                        })


                        //tbl3.append('<li class="user-list-item">' +
                        //    '<a href="#" onClick="messageBox(' + data[i].SenderId + ')">' +
                        //    '<div class="pull-left" style="padding: 6px;">' +
                        //    '<input hidden id ="offlineUserType" type="text" value="' + data[i].UserType + '">' +
                        //    '<img style="height: 60px; width: 60px;border-radius: 50%;" src=' + data[i].UserUrl + '>' +
                        //    '</div>' +
                        //    '<h4>' +
                        //    data[i].UserName.substring(0, 38) +
                        //    '</h4>' +
                        //    '<small>' + '<i class="fa fa-envelope" style="padding-right:5px">' + '</i>' + data[i].UserEmail.substring(0, 80) + '</small>' +
                        //    '</a>' +
                        //    '</li>');



                        tbl3.append('<li class="user-list-item">' + '<a href="#" onclick="messageBox(' + data[i].SenderId + ')">' +
                            '<img src=' + data[i].UserUrl + ' class="rounded-circle" alt="image" style="margin: 3px;cursor:pointer;height: 50px;width: 50px;border-radius: 50%;margin-right: 8px;">' +
                            '<input hidden id ="offlineUserType" type="text" value="' + data[i].UserType + '">' +
                            '<div class="users-list-body">' +
                            '<div>' +
                            '<h5>' + data[i].UserName.substring(0, 38) + '</h5>' +
                            '<p>' + data[i].UserEmail.substring(0, 80) + '</p>' +
                            '</div>' +
                            '<div class="last-chat-time">' +
                            '<small class="text-muted">' + "14:45 pm" + '</small>' +
                            '<div class="chat-toggle mt-1">' +
                            '<div class="dropdown">' +
                            '<a data-toggle="dropdown" href="#">' +
                            '<i class="fas fa-ellipsis-h ellipse_header">' + '</i>' +
                            '</a>' +
                            '<div class="dropdown-menu dropdown-menu-right">' +
                            '<a href="#" class="dropdown-item">' + "Open" + '</a>' +
                            '<a href="#" class="dropdown-item dream_profile_menu">' + "Profile" + '</a>' +
                            '<a href="#" class="dropdown-item">' + "Add to archive" + '</a>' +
                            '<div class="dropdown-divider">' + '</div>' +
                            '<a href="#" class="dropdown-item">' + "Delete" + '</a>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</a>' + '</li >');


                    }
                    //tbl3.append(rows.join(''));

                }
                else {
                    isNullgetData3 = true;
                    isFirstTimegetData3 = true;
                }
            }
        });

    }

}

//let videoIdForUser;
function messageBox(sId) {
    //videoIdForUser = sId;

    var senderId;
    var userType;

    var offlineUser = rowsMsgForWz.find(x => x.senderId == sId);
    if (offlineUser === undefined) {
        senderId = sId;
        $("#senderId").val(senderId);
        userType = $("#onlineSenderType").val();
    }
    else {
        senderId = offlineUser.senderId
        $("#senderId").val(offlineUser.senderId);
        userType = offlineUser.userType;
    }


    //my identificetion
    var myDbId = parseInt($('#userDbId').val());
    var myUserType = $('#hdUserUserType').val();
    //Call Method
    var chatHub = $.connection.chatHub;

    chatHub.server.openMessageBox(senderId, userType, myDbId, myUserType);

    $('.left-sidebar').removeClass('show-left-sidebar');
    $('.left-sidebar').addClass('hide-left-sidebar');
    $('.chat').addClass('show-chatbar');
     
}

$(function () {

    setScreen(false);
    // Declare a proxy to reference the hub.
    var chatHub = $.connection.chatHub;
    registerClientMethods(chatHub);

    chatHub.client.myMessageBox = function (sender, senderType, Receiver, ReceiverType) {
        // messageBox(sender);
        var myDbId = $('#userDbId').val();
        if (myDbId == Receiver) {
            chatHub.server.getPrivateMessage(Receiver, ReceiverType, sender, senderType);


        }
    };


    // Start Hub
    let c = 0;
    let c2 = 0;
    let c3 = 0;
    chatHub.client.displayStatus = function () {
        getData();
        c = c + 1;
        $("#notCount").empty();
        $("#notCount").append(c);
    };
    chatHub.client.displayMessage = function () {
        //for message
        getData3(currentPageNumber);
        c3 = c3 + 1;
        $("#notCount3").empty();
        $("#notCount3").append(c3);
    }

    chatHub.client.displayStatusForWizard = function (webWizard) {
        $.ajax({
            url: '/WebWizardDashboard/GetWebWizardRegistrationDetail',
            type: 'GET',
            datatype: 'json',
            success: function (data) {
                if (data.WebWizardId == webWizard) {
                    getData2();
                    c2 = c2 + 1;
                    $("#notCount2").empty();
                    $("#notCount2").append(c2);
                }
            }
        });

    };

    $.connection.hub.start().done(function () {
        registerEvents(chatHub);
    });

    getData();
    getData2();
    //getData3();






    jQuery(function ($) {

        //$("#tbl3").scroll(function () {
        //    $("#tbl3").height() - $("#tbl3").height()
        //    if ($("#tbl3").scrollTop() == 0) {
        //        currentPageNumber--;
        //            getData3(currentPageNumber);
        //    }
        //})

        $('#tbl3').on('scroll', function () {

            if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {

                if (currentPageNumber === 1 && isFirstTimegetData3 === true) {
                    isFirstTimegetData3 = true;
                    currentPageNumber++;
                    getData3(currentPageNumber);
                }
                else {
                    isFirstTimegetData3 = false;

                    if (isFirstTimegetData3 == false && isNullgetData3 == false) {
                        currentPageNumber++;
                        getData3(currentPageNumber);
                    }
                }

            }

        });
    });


});

function setScreen(isLogin) {

    if (!isLogin) {

        $("#divChat").hide();
        $("#divLogin").show();
    }
    else {

        $("#divChat").show();
        $("#divLogin").hide();
    }

}

function registerEvents(chatHub) {

    $.ajax({
        url: '/ClientDashboard/GetClientInfoForChat',
        type: 'GET',
        success: function (response) {

            if (response.ClientFullName.length > 0) {
                chatHub.server.connect(response.ClientId, response.ClientFullName, response.ClientEmail, response.ClientProfileImageUrl, response.UserType);
            }
            else {
                alert("Please enter name");
            }
        },
        error: function (error) {
            $(this).remove();
            DisplayError(error.statusText);
        }
    });




    $('#btnSendMsg').click(function () {

        var msg = $("#txtMessage").val();
        if (msg.length > 0) {

            var userName = $('#hdUserName').val();
            var userEmail = $('#hdUserEmail').val();
            var userUrl = $('#hdUserImageUrl').val();
            var userType = $('#hdUserUserType').val();

            chatHub.server.sendMessageToAll(userName, userEmail, userUrl, userType, msg);
            $("#txtMessage").val('');
        }
    });


    //$("#txtNickName").keypress(function (e) {
    //    if (e.which == 13) {
    //        $("#btnStartChat").click();
    //    }
    //});

    //$("#txtMessage").keypress(function (e) {
    //    if (e.which == 13) {
    //        $('#btnSendMsg').click();
    //    }
    //});


}

var currentPrivateMessagePageNumber = 0;
function registerClientMethods(chatHub) {

    // Calls when user successfully logged in
    chatHub.client.onConnected = function (userDbId, id, userName, userEmail, userUrl, userType, allUsers, messages) {

        setScreen(true);

        $('#userDbId').val(userDbId);
        $('#hdId').val(id);
        $('#hdUserName').val(userName);
        $('#spanUser').html(userName);
        $('#spanEmail').html(userEmail);

        $('#spanUserType').html(userType);
        $('#hdUserUserType').val(userType);

        $('#spanImage').attr('src', userUrl);

        $('#hdUserEmail').val(userEmail);
        $('#hdUserImageUrl').val(userUrl);

        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            AddUser(chatHub, allUsers[i].UserDbId, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].UserEmail, allUsers[i].UserUrl, allUsers[i].UserType);
        }

        // Add Existing Messages
        for (i = 0; i < messages.length; i++) {

            AddMessage(messages[i].UserName, messages[i].UserEmail, messages[i].UserUrl, messages[i].UserType, messages[i].Message);
        }


    }

    // On New User Connected
    chatHub.client.onNewUserConnected = function (userDbId, id, name, email, path, type) {

        AddUser(chatHub, userDbId, id, name, email, path, type);
    }


    // On User Disconnected
    chatHub.client.onUserDisconnected = function (id, userName, userEmail, userUrl, userType) {

        $('#' + id).remove();

        var ctrId = 'private_' + id;
        $('#' + ctrId).remove();


        var disc = $('<div class="disconnect">"' + userName + userEmail + userUrl + userType + '" logged off.</div>');

        $(disc).hide();
        $('#divusers').prepend(disc);
        $(disc).fadeIn(200).delay(2000).fadeOut(200);

    }

    chatHub.client.messageReceived = function (userName, userEmail, userUrl, userType, message) {

        AddMessage(userName, userEmail, userUrl, userType, message);
    }


    chatHub.client.sendPrivateMessage = function (windowId, fromUserName, userEmail, userUrl, userType, message) {

        var ctrId = 'private_' + windowId;
        if ($('#' + ctrId).length == 0) {
            createPrivateChatWindow(chatHub, userDbId, windowId, ctrId, fromUserName, userEmail, userUrl, userType);
            currentPrivateMessagePageNumber = 0;
        }


        //$('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + '<img id="" style="cursor:pointer;height:30px;width:30px" src='+ userUrl +' />' + fromUserName +'<br/>'+ userEmail+ '</span>: ' + message + '</div>');
        $('#' + ctrId).find('#divMessage').append('<div class="direct-chat-msg" style="background-color: gainsboro;padding-top: 2px;">' +
            '<div class= "direct-chat-info clearfix" style="margin: 5px;">' +
            '<div style="float:left">'+
            '<img class="direct-chat-img" src=' + userUrl + ' alt="message user image" style="height: 50px;width: 50px;border-radius: 50%;">' +
            '</div>' +

            '<div style="float:left">' +
            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
            + '</span>' +
            '<span class="direct-chat-timestamp pull-right" style="padding-left: 10px;font-weight: bold;">' +
            userEmail
            + '</span>' +
            '<p class="direct-chat-text" style="padding-left: 15px;">' +
            message +
            '</p>' +
            '</div>' +


            '</div >' +
            '</span>' + '</div>');

        // set scrollbar
        var height = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
        $('#' + ctrId).find('#divMessage').scrollTop(height);

        var userIdChat = '#' + ctrId;


        $(userIdChat).find("#divMessage").scroll(function () {

            if ($(userIdChat).find("#divMessage").scrollTop() == 0) {
                var chatHub = $.connection.chatHub;
                //chatHub.server.getPrivateMessage(myDbId, myUserType, userDbId, userType);
                if (currentPrivateMessagePageNumber === 0) {
                    $(userIdChat).find("#divMessage").empty();
                }

                currentPrivateMessagePageNumber++;
                privateMessageReLoad(currentPrivateMessagePageNumber);

                // alert("id" + userIdChat);

                $(userIdChat).find("#divMessage").scrollIntoView({ block: "center" });


                // $(userIdChat).find("#divMessage").animate({ scrollTop: $(userIdChat).find("#divMessage").height() }, 'easeInExpo');



            };




            //Privet message reload 
            function privateMessageReLoad(currentPage) {

                var userDbId = $("#senderId").val();

                var userType;
                var offlineUser = rowsMsgForWz.find(x => x.senderId == userDbId);



                if (offlineUser === undefined) {
                    userDbId = userDbId;
                    userType = $("#onlineSenderType").val();
                }
                else {
                    userDbId = offlineUser.senderId;
                    userType = offlineUser.userType;
                }



                var myDbId = $('#userDbId').val();
                var myUserType = $('#hdUserUserType').val();
                var myEmail = $('#hdUserEmail').val();
                var myUrl = $('#hdUserImageUrl').val();
                //get online
                var onlineSenderId = $("#onlineSenderId").val();
                var onlineSenderType = $("#onlineSenderType").val();

                if (userDbId == "" || userDbId == undefined && userType == "" || userType == undefined) {
                    userDbId = onlineSenderId;
                    userType = onlineSenderType;
                }

                $.ajax({
                    url: '/WebWizardDashboard/LoadMessageList',
                    type: 'POST',
                    data: { sender: myDbId, senderType: myUserType, Receiver: userDbId, ReceiverType: userType, pageNumber: currentPage, pageSize: 15 },
                    datatype: 'json',
                    success: function (data) {

                        if (data.length > 0) {
                            // chatHub.client.sendPrivateMessage(userIdChat, "xx", "xx", "xx", "xx", "xx");

                            $(data).each(function (index, txt) {

                                if (currentPrivateMessagePageNumber === 1) {

                                    if (txt.Sender === parseInt(myDbId) && txt.Receiver === parseInt(userDbId)) {

                                        $('#' + ctrId).find('#divMessage').prepend('<div class="direct-chat-msg" style="background-color: gainsboro;padding-top: 2px;">' +


                                            '<div class= "direct-chat-info clearfix" style="margin: 5px;">' +
                                            '<div style="float:left">' +
                                            '<img class="direct-chat-img" src=' + myUrl + ' alt="message user image" style="height: 50px;width: 50px;border-radius: 50%;">' +
                                            '</div>' +
                                            '<div style="float:left">' +
                                            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
                                            + '</span>' +
                                            '<span class="direct-chat-timestamp pull-right" style="padding-left: 10px;font-weight: bold;">' +
                                            myEmail
                                            + '</span>' +
                                            '<p class="direct-chat-text" style="padding-left: 15px;">' +
                                            txt.Body +
                                            '</p>' +
                                            '</div>' +


                                            '</div >' +
                                            '</span>' + '</div>');
                                    }
                                    else {
                                        $('#' + ctrId).find('#divMessage').prepend('<div class="direct-chat-msg" style="background-color: gainsboro;padding-top: 2px;">' +

                                            '<div class= "direct-chat-info clearfix" style="margin: 5px;">' +
                                            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">' +

                                            '<div style="float:left">' +
                                            '<img class="direct-chat-img" src=' + userUrl + ' alt="message user image" style="height: 50px;width: 50px;border-radius: 50%;">' +
                                            '</div>' +
                                            '</span>' +

                                            '<div style="float:left">' +
                                            '<span class="direct-chat-timestamp pull-right" style="padding-left: 5px;">' +
                                            userEmail
                                            + '</span>' +
                                            '<p class="direct-chat-text" style="padding-left: 15px;">' +
                                            txt.Body +
                                            '</p>' +
                                            '</div>' +

                                            '</div >' +     
                                            '</span>' + '</div>');
                                    }
                                }
                                else {

                                    if (txt.Sender === parseInt(myDbId) && txt.Receiver === parseInt(userDbId)) {

                                        $('#' + ctrId).find('#divMessage').prepend('<div class="direct-chat-msg" style="background-color: gainsboro;padding-top: 2px;">' +
                                            '<div class= "direct-chat-info clearfix" style="margin: 5px;">' +
                                            '<div style="float:left">' +
                                            '<img class="direct-chat-img" src=' + myUrl + ' alt="message user image" style="height: 50px;width: 50px;border-radius: 50%;">' +
                                            '</div>'+

                                            '<div style="float:left">' +
                                            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
                                            + '</span>' +
                                            '<span class="direct-chat-timestamp pull-right" style="padding-left: 10px;font-weight: bold;">' +
                                            myEmail
                                            + '</span>' +
                                            '<p class="direct-chat-text" style="padding-left: 15px;">' +
                                            txt.Body +
                                            '</p>' +
                                            '</div>' +

                                            '</div >' +
                                            '</span>' + '</div>');

                                    }
                                    else {
                                        $('#' + ctrId).find('#divMessage').prepend('<div class="direct-chat-msg" style="background-color: gainsboro;padding-top: 2px;">' +

                                            '<div class= "direct-chat-info clearfix" style="margin: 5px;">' +
                                            '<div style="float:left">' +
                                            '<img class="direct-chat-img" src=' + userUrl + ' alt="message user image" style="height: 50px;width: 50px;border-radius: 50%;">' +
                                            '</div>' +

                                            '<div style="float:left">' +
                                            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
                                            + '</span>' +
                                            '<span class="direct-chat-timestamp pull-right" style="padding-left: 10px;font-weight: bold;">' +
                                            userEmail
                                            + '</span>' +
                                            '<p class="direct-chat-text" style="padding-left: 15px;">' +
                                            txt.Body +
                                            '</p>' +
                                            '</div>' +

                                            '</div >' +
                                            '</span>' + '</div>');

                                    }


                                }


                            })



                        }
                    }
                });


            }

        });
    }

    var setMsgBoxId;
    chatHub.client.typingPrivateMessage = function (cnnectionId, userName) {
        setMsgBoxId = 'private_' + cnnectionId;
        $("#" + setMsgBoxId).find("#typing").html(userName + " is typing...");


    }
    setInterval(function () { $("#" + setMsgBoxId).find("#typing").html("Can not typing") }, 3000);




    var ClickImageId;
    if (ClickImageId === undefined) {

        $("#divContainer").on("click", "div", function () {
            ClickImageId = $(this).attr('id');
            if (ClickImageId != undefined) {
                var trm = ClickImageId.replace('private_', '');
                $("#typeId").val(trm);
            }

        });
        $("#divContainer").click();
    }



}

function AddUser(chatHub, userDbId, id, name, email, path, type) {

    var userId = $('#hdId').val();

    var code = "";

    if (userId == id) {

        //  code = $('<div class="loginUser">' + '<img id="" style="cursor:pointer;height: 50px;width: 50px;border-radius: 50%;margin-right: 8px;" src=' + path + ' />' + name + '<br/>' + email + "</div>");


        //       code = $('<div class="loginUser" style="padding:0px">' + '<div class="widget-user-header bg-yellow" style="border-bottom-right-radius: 50%;">'+
        //           '<div class= "widget-user-image" style="float: left;">'+
        //           '<img class="img-circle user-image profile-user-img img-responsive img-circle" src='+path+' style="margin: 3px;cursor:pointer;height: 50px;width: 50px;border-radius: 50%;margin-right: 8px;" alt="User Avatar">'+
        //           '</div>' +
        //           '<div class= "">' +
        //           '<h3 class="widget-user-username" style="margin-top: 5px; font-size:18px;font-weight: bold;">' + name + ' ['+type+']' +'</h3>'+
        //           '<h5 class="widget-user-desc" style="margin-top: 0px;margin-bottom: 0px;padding-bottom: 9px;">' + email +'</h5>' +
        //           '</div>' +
        //'</div >' + "</div>");

    }
    else {

        //   code = $('<a id="' + id + '" class="user" >' + '<img id="" style="cursor:pointer;height: 50px;width: 50px;border-radius: 50%;margin-right: 8px;" src=' + path + ' />' + name + '<br/>' + email+ '<a>');

        code = $('<a href="#"  onclick="messageBox(' + userDbId + ')">' + '<li class="user-list-item" id="' + id + '">' + 
            '<img src=' + path + ' class="rounded-circle" alt="image" style="margin: 3px;cursor:pointer;height: 50px;width: 50px;border-radius: 50%;margin-right: 8px;">' +
            '<div class="users-list-body">' +
            '<div>' +
            '<h5>' + name + ' [' + type + ']' + '</h5>' +
            '<p>' + email + '</p>' +
            '</div>' +
            '<div class="last-chat-time">' +
            '<small class="text-muted">' + "14:45 pm" + '</small>' +
            '<div class="chat-toggle mt-1">' +
            '<div class="dropdown">' +
            '<a data-toggle="dropdown" href="#">' +
            '<i class="fas fa-ellipsis-h ellipse_header">' + '</i>' +
            '</a>' +
            '<div class="dropdown-menu dropdown-menu-right">' +
            '<a href="#" class="dropdown-item">' + "Open" + '</a>' +
            '<a href="#" class="dropdown-item dream_profile_menu">' + "Profile" + '</a>' +
            '<a href="#" class="dropdown-item">' + "Add to archive" + '</a>' +
            '<div class="dropdown-divider">' + '</div>' +
            '<a href="#" class="dropdown-item">' + "Delete" + '</a>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</li >' + '</a>');









        //code = $('<a id="' + id +'" class="user" style="padding:0px" onclick="messageBox('+userDbId+')">' + '<div class="widget-user-header" style="color:white">' +
        //    '<div class= "widget-user-image" style="float: left;">' +
        //    '<img class="img-circle user-image profile-user-img img-responsive img-circle" src=' + path + ' style="margin: 3px;cursor:pointer;height: 50px;width: 50px;border-radius: 50%;margin-right: 8px;" alt="User Avatar">' +
        //    '</div>' +
        //    '<div class= "">' +
        //    '<h4 class="widget-user-username" style="margin-top: 5px; font-size:15px;font-weight: bold;padding-top: 10px;margin-bottom: 2px;">' + name + ' [' + type + ']' + '</h4>' +
        //    '<h5 class="widget-user-desc" style="font-size:12px;margin-top: 0px;margin-bottom: 0px;padding-bottom: 9px;">' + email + '</h5>' +
        //    '</div>' +
        //    '</div >' + '</a>');



        //$(code).click(function () {

        var id = $(this).attr('id');

        if (userId != id)
            //  OpenPrivateChatWindow(chatHub, userDbId, id, name, email, path, type);
            currentPrivateMessagePageNumber = 1;


        $("#onlineSenderId").val(userDbId);
        $("#onlineSenderType").val(type);


        //});

    }


    $("#divusers").append(code);


}

function AddMessage(userName, userEmail, userPath, userType, message) {
    //$('#divChatWindow').append('<div class="message"><span class="userName">' + '<img id="" style="cursor:pointer;height:50px;width:50px;border-radius: 50%;" src='+userPath+'>' + userName + userEmail + userType+'</span>: ' + message + '</div>');


    $('#divChatWindow').append('<div class="message"><span class="userName">' + '<div class="direct-chat-msg">' + '<div class="direct-chat-info clearfix">' + userEmail + '<span class="direct-chat-name pull-left" style="padding-left: 5px;">' + '</span>' + '<span class="direct-chat-timestamp pull-right">' + userName + ' [' + userType + ']' + '</span>' + '</div><img class="direct-chat-img" src=' + userPath + ' alt="message user image">' + '<div class="direct-chat-text" style="position: inherit;">' + message + '</div></div>' + '</div>');



    var height = $('#divChatWindow')[0].scrollHeight;
    $('#divChatWindow').scrollTop(height);
}

function OpenPrivateChatWindow(chatHub, userDbId, id, userName, userEmail, userUrl, userType) {

    currentPrivateMessagePageNumber = 1;
    $("#onlineSenderId").val(userDbId);
    $("#onlineSenderType").val(userType);

    var ctrId = 'private_' + id;


    if ($('#' + ctrId).length > 0) return;

    createPrivateChatWindow(chatHub, userDbId, id, ctrId, userName, userEmail, userUrl, userType);

}

function createPrivateChatWindow(chatHub, userDbId, userId, ctrId, userName, userEmail, userUrl, userType) {
    var myDbId = $('#userDbId').val();
    var myUserType = $('#hdUserUserType').val();

    chatHub.server.getPrivateMessage(myDbId, myUserType, userDbId, userType);


    //var div = '<div id="' + ctrId + '" class="ui-widget-content draggable" rel="0">' +
    //    '<div class="header box-header with-border" style="padding-left:0px;">' +

    //    '<div class="col-md-12">' +
    //    '<div style="float:right;">' +
    //    //'<img id="imgDelete" style="cursor:pointer;" src="/ChatAssets/delete.png" />' +
    //    '<i id="imgDelete" style="cursor:pointer;" class="fa fa-times" aria-hidden="true"></i>' +
    //    '</div>' +
    //    '<div class="col-md-4 col-sm-4" style="float:left;margin-right: -12px;">' +
    //    '<img id="" class="user-image profile-user-img img-responsive img-circle" style="cursor:pointer;height:50px;width:50px;border-radius:50%;" src=' + userUrl + ' />' +
    //    '</div>' +
    //    '<div class="selText box-title col-md-6 col-sm-6" style="font-size: 10px;font-weight: bold;" rel="0">' + userName + ' [' + userType + ']' + '</div>' +
    //    '<div class="selText box-title col-md-6 col-sm-6" rel="0" style="font-size:15px;font-style: italic;font-size: 10px;">' + userEmail + '</div>' +
    //    '</div>' +
    //    '</div>' +



    //    '<div id="divMessage" class="messageArea" style="padding:5px;background-color: white;">' +
    //    '</div>' +
    //    '<p id="typing" style="padding: 0;margin: 0;text-align: center;font-weight: bold;">' +'</p>'+

    //    '<div class="buttonBar input-group" style="background-color: white;">' +
    //    '<input id="txtPrivateMessage" style="width:142px" class="msgText form-control" type="text" oninput="onTypePrivateMessage()" placeholder="Type Message ..."/>' +

    //    '<input style="width: 42px;height: 42px;" type="button" id="btnSymbol" value="😀" readonly="" onclick="mySymbol()">' +
    //    '<input id="btnSendMessage" class="button btn-flat" style="margin-left: 1px;height: 42px;" type="button" value="Send" />' +

    //    '<div id="symbolBox" hidden  class="" style="background-color: rgba(42, 123, 72, 0.27);height: 121px;margin-top:12px;overflow: scroll;overflow-x: auto;">' +
    //    //symbole set
    //    '<input id="1" onClick="setSymbol(1)" type ="button" value="😀" readonly="" >' +
    //    '<input id="2" onClick="setSymbol(2)" type ="button" value="😁" readonly="" >' +
    //    '<input id="3" onClick="setSymbol(3)" type ="button" value="😂" readonly="" >' +
    //    '<input id="4" onClick="setSymbol(4)" type ="button" value="🤣" readonly="" >' +
    //    '<input id="5" onClick="setSymbol(5)" type ="button" value="😃" readonly="" >' +
    //    '<input id="6" onClick="setSymbol(6)" type ="button" value="😄" readonly="" >' +
    //    '<input id="7" onClick="setSymbol(7)" type ="button" value="😅" readonly="" >' +
    //    '<input id="8" onClick="setSymbol(8)" type ="button" value="😆" readonly="" >' +
    //    '<input id="9" onClick="setSymbol(9)" type ="button" value="😉" readonly="" >' +
    //    '<input id="10" onClick="setSymbol(10)" type ="button" value="😊" readonly="" >' +
    //    '<input id="11" onClick="setSymbol(11)" type ="button" value="😋" readonly="" >' +
    //    '<input id="12" onClick="setSymbol(12)" type ="button" value="😎" readonly="" >' +
    //    '<input id="13" onClick="setSymbol(13)" type ="button" value="😍" readonly="" >' +
    //    '<input id="14" onClick="setSymbol(14)" type ="button" value="😘" readonly="" >' +
    //    '<input id="15" onClick="setSymbol(15)" type ="button" value="😗" readonly="" >' +
    //    '<input id="16" onClick="setSymbol(16)" type ="button" value="😙" readonly="" >' +
    //    '<input id="17" onClick="setSymbol(17)" type ="button" value="😚" readonly="" >' +
    //    '<input id="18" onClick="setSymbol(18)" type ="button" value="🙂" readonly="" >' +
    //    '<input id="19" onClick="setSymbol(19)" type ="button" value="🤗" readonly="" >' +
    //    '<input id="20" onClick="setSymbol(20)" type ="button" value="🤩" readonly="" >' +
    //    '<input id="21" onClick="setSymbol(21)" type ="button" value="🤔" readonly="" >' +
    //    '<input id="22" onClick="setSymbol(22)" type ="button" value="🤨" readonly="" >' +
    //    '<input id="23" onClick="setSymbol(23)" type ="button" value="😐" readonly="" >' +
    //    '<input id="24" onClick="setSymbol(24)" type ="button" value="😑" readonly="" >' +
    //    '<input id="25" onClick="setSymbol(25)" type ="button" value="😶" readonly="" >' +
    //    '<input id="26" onClick="setSymbol(26)" type ="button" value="🙄" readonly="" >' +
    //    '<input id="27" onClick="setSymbol(27)" type ="button" value="😏" readonly="" >' +
    //    '<input id="28" onClick="setSymbol(28" type ="button" value="😣" readonly="" >' +
    //    '<input id="29" onClick="setSymbol(29)" type ="button" value="😥" readonly="" >' +
    //    '<input id="30" onClick="setSymbol(30)" type ="button" value="😮" readonly="" >' +
    //    '<input id="31" onClick="setSymbol(31)" type ="button" value="🤐" readonly="" >'



    //    + '</div>' +
    //    '</div>' +
    //    '</div>';


    var div = '<div class="draggable" id="' + ctrId + '">'+ '<div class="chat-header">' +
        '<div class="user-details" >' +
        '<i id="imgDelete" style="cursor:pointer;" class="fa fa-times" aria-hidden="true" hidden></i>' +
        '<div class="d-lg-none ml-2">' +

        '<ul class="list-inline mt-2 mr-2">' +
        '<li class="list-inline-item">' +
        '<a onclick="backToClien()" class="text-muted px-0 left_side" href="#" data-chat="open">' +
        '<i class="fas fa-arrow-left">' + '</i>' +
        '</a>' +
        '</li>' +
        '</ul>' +
        '</div>' +


        '<figure class="avatar ml-1">' +
        '<img src=' + userUrl + ' class="rounded-circle" alt="image">' +
        '</figure>' +
        '<div class="mt-1">' +
        '<h5 class="mb-1">' + userName + ' [' + userType + ']' + '</h5>' +
        '<small class="text-muted mb-2" id="typing">' +
        
        '</small>' +
        '</div>' +
        '</div>' +
        '<div class="chat-options">' +
        '<!--<ul class="list-inline">' +
        '<li class="list-inline-item" data-toggle="tooltip" title="" data-original-title="Voice call">' +
        '<a href="javascript:void(0)" class="btn btn-outline-light" data-toggle="modal" data-target="#voice_call">' +
        '<i class="fas fa-phone-alt voice_chat_phone">' + '</i>' +
        '</a>' +
        '</li>' +
        '<li class="list-inline-item" data-toggle="tooltip" title="" data-original-title="Video call">' +
        '<a href="javascript:void(0)" onclick="captureVideoUser()" class="btn btn-outline-light capture-button" data-toggle="modal" data-target="#video_call">' +
        '<i class="fas fa-video">' + '</i>' +
        '</a>' +
        '</li>' +
        '<li class="list-inline-item">' +
        '<a class="btn btn-outline-light" href="#" data-toggle="dropdown">' +
        '<i class="fas fa-ellipsis-h">' + '</i>' +
        '</a>' +
        '<div class="dropdown-menu dropdown-menu-right">' +
        '<a href="#" class="dropdown-item dream_profile_menu">' + "Profile" + '</a>' +
        '<a href="#" class="dropdown-item">' + "Delete" + '</a>' +
        '</div>' +
        '</li>' +
        '</ul>-->' +
        '</div>' +
        '</div >' +

       '<div id="divMessage" class="messageArea" style="padding:5px;background-color: white;padding-bottom: 50px;">' +
       '</div>' +
       '<p id="typing" style="padding: 0;margin: 0;text-align: center;font-weight: bold;">' +'</p>'+
        '<div class="chat-footer">' +
        '<form action="#">' +
        '<input autocomplete="off" id="txtPrivateMessage"  type="text" class="form-control chat_form" oninput="onTypePrivateMessage()" placeholder="Write a message.">' +
        '<div class="form-buttons">' +
        //'<button class="btn" type="button">' +
        //'<i class="far fa-smile">' + '</i>' +
        //'</button>' +
        '<div class="dropdown-menu dropdown-menu-right header_drop_icon hide" style="position: relative;width: 230px;height: 150px;overflow: scroll;overflow-x: hidden; transform: translate3d(816px, 53px, 0px); top: 0px; left: 0px; will-change: transform;" x-placement="bottom-end">' +
        //symbole set
        '<input id="1" onClick="setSymbol(1)" type ="button" value="😀" readonly="" >' +
        '<input id="2" onClick="setSymbol(2)" type ="button" value="😁" readonly="" >' +
        '<input id="3" onClick="setSymbol(3)" type ="button" value="😂" readonly="" >' +
        '<input id="4" onClick="setSymbol(4)" type ="button" value="🤣" readonly="" >' +
        '<input id="5" onClick="setSymbol(5)" type ="button" value="😃" readonly="" >' +
        '<input id="6" onClick="setSymbol(6)" type ="button" value="😄" readonly="" >' +
        '<input id="7" onClick="setSymbol(7)" type ="button" value="😅" readonly="" >' +
        '<input id="8" onClick="setSymbol(8)" type ="button" value="😆" readonly="" >' +
        '<input id="9" onClick="setSymbol(9)" type ="button" value="😉" readonly="" >' +
        '<input id="10" onClick="setSymbol(10)" type ="button" value="😊" readonly="" >' +
        '<input id="11" onClick="setSymbol(11)" type ="button" value="😋" readonly="" >' +
        '<input id="12" onClick="setSymbol(12)" type ="button" value="😎" readonly="" >' +
        '<input id="13" onClick="setSymbol(13)" type ="button" value="😍" readonly="" >' +
        '<input id="14" onClick="setSymbol(14)" type ="button" value="😘" readonly="" >' +
        '<input id="15" onClick="setSymbol(15)" type ="button" value="😗" readonly="" >' +
        '<input id="16" onClick="setSymbol(16)" type ="button" value="😙" readonly="" >' +
        '<input id="17" onClick="setSymbol(17)" type ="button" value="😚" readonly="" >' +
        '<input id="18" onClick="setSymbol(18)" type ="button" value="🙂" readonly="" >' +
        '<input id="19" onClick="setSymbol(19)" type ="button" value="🤗" readonly="" >' +
        '<input id="20" onClick="setSymbol(20)" type ="button" value="🤩" readonly="" >' +
        '<input id="21" onClick="setSymbol(21)" type ="button" value="🤔" readonly="" >' +
        '<input id="22" onClick="setSymbol(22)" type ="button" value="🤨" readonly="" >' +
        '<input id="23" onClick="setSymbol(23)" type ="button" value="😐" readonly="" >' +
        '<input id="24" onClick="setSymbol(24)" type ="button" value="😑" readonly="" >' +
        '<input id="25" onClick="setSymbol(25)" type ="button" value="😶" readonly="" >' +
        '<input id="26" onClick="setSymbol(26)" type ="button" value="🙄" readonly="" >' +
        '<input id="27" onClick="setSymbol(27)" type ="button" value="😏" readonly="" >' +
        '<input id="28" onClick="setSymbol(28" type ="button" value="😣" readonly="" >' +
        '<input id="29" onClick="setSymbol(29)" type ="button" value="😥" readonly="" >' +
        '<input id="30" onClick="setSymbol(30)" type ="button" value="😮" readonly="" >' +
        '<input id="31" onClick="setSymbol(31)" type ="button" value="🤐" readonly="" >'+

        '</div>' +

        '<a href="#" data-toggle="dropdown" aria-expanded="true" class="btn">' +
        '<i class="far fa-smile">' + '</i>' +
        '</a>' +


        '<!--<button class="btn" type="button" data-toggle="modal" data-target="#drag_files">' +
        '<i class="fas fa-paperclip">' + '</i>' +
        '</button>' +
        '<button class="btn" type="button">' +
        '<i class="fas fa-microphone-alt">' + '</i>' +
        '</button>-->' +
        '<button class="btn send-btn" type="button" id="btnSendMessage">' +
        '<i class="fab fa-telegram-plane">' + '</i>' +
        '</button>' +
        '</div>' +
        '</form>' +
        '</div>' +
        '</div>';















    var $div = $(div);

    // DELETE BUTTON IMAGE
    $div.find('#imgDelete').click(function () {
        $('#' + ctrId).remove();
    });

    // Send Button event
    $div.find("#btnSendMessage").click(function () {
        var senderId = $("#senderId").val();
        var senderType = $("#userType").val();
        var receiverId = parseInt($('#userDbId').val());
        var receiverType = $('#hdUserUserType').val();

        $textBox = $div.find("#txtPrivateMessage");
        var msg = $textBox.val();
        if (msg.length > 0) {

            chatHub.server.sendPrivateMessage(userId, msg, senderId, senderType, receiverId, receiverType);
            $textBox.val('');
        }
    });

    // Text Box event
    //$div.find("#txtPrivateMessage").keypress(function (e) {
    //    if (e.which == 13) {
    //        $div.find("#btnSendMessage").click();
    //    }
    //});

   
    AddDivToContainer($div);

}

function AddDivToContainer($div) {
    $("#divContainer").empty();
    $('#divContainer').prepend($div);

    //$div.draggable({

    //    handle: ".header",
    //    stop: function () {

    //    }
    //});

    ////$div.resizable({
    ////    stop: function () {

    ////    }
    ////});

}

//symbolBox
function mySymbol() {

    var isHidden = $('#symbolBox').is(':hidden');
    if (isHidden == true) {
        $("#symbolBox").show();
    }
    else {
        $("#symbolBox").hide();
    }

}
function setSymbol(sb) {
    var myId = "#" + sb;
    var elmId = $(myId).val();
    var txt = $('#txtPrivateMessage').val();
    $('#txtPrivateMessage').val(txt + elmId);
}

//on type event
function onTypePrivateMessage() {
    var onlineSenderId = $("#typeId").val();
    var chatHub = $.connection.chatHub;
    chatHub.server.privateMessageTyping(onlineSenderId);
}

function backToClien() {
    $("#imgDelete").click();
    $("#divMessages").show();
    $('.sidebar-group ').removeClass('hide-left-sidebar');
    $("#divOnlineUsers").show();
    $("#divMessages").hide();
   // window.location.href = "WizerdDashboard"; 
}






////for video call
//function captureVideoUser() {
//    var targetId = videoIdForUser;// $("#typeId").val();
//    var chatHub = $.connection.chatHub;
//    $.support.cors = true;
//    $.connection.chatHub.url = '/signalr/hubs';


//    chatHub.server.privateVideoCall(targetId);
//    captureVideo();


//}


//$(document).ready(function () {
//    var _mediaStream;
//    var _signaler;
//    var _connections = {};
//    var _iceServers = [{ url: 'stun:74.125.142.127:19302' }];


//    var chatHub = $.connection.chatHub;
//    chatHub.client.incomingCall = function (callingUser) {
//        // audio.play();
//        console.log('incoming call from: ' + JSON.stringify(callingUser));
//        // Ask if we want to talk
//        alertify.confirm(callingUser.UserName + ' is calling.', function (e) {
//            if (e) {
//                // I want to chat
//                chatHub.server.answerCall(true, callingUser.ConnectionId);

//                // So lets go into call mode on the UI
//                //viewModel.Mode('incall');
//            } else {
//                // Go away, I don't want to chat with you
//                chatHub.server.answerCall(false, callingUser.ConnectionId);
//            }
//        });
//    };



//    // Hub Callback: Call Accepted
//    chatHub.client.callAccepted = function (acceptingUser) {
//        console.log('call accepted from: ' + JSON.stringify(acceptingUser) + '.  Initiating WebRTC call and offering my stream up...');

//        // audio.pause();
//        // Callee accepted our call, let's send them an offer with our video stream
//         _initiateOffer(acceptingUser.ConnectionId, _mediaStream);

//        // Set UI into call mode
//       // viewModel.Mode('incall');
//    };


//    // Send an offer for audio/video
//    _initiateOffer = function (partnerClientId, stream) {
//        // Get a connection for the given partner
//        var connection = _getConnection(partnerClientId);

//        // Add our audio/video stream
//        connection.addStream(stream);

//        console.log('stream added on my end');

//        // Send an offer for a connection
//        connection.createOffer(function (desc) {
//            connection.setLocalDescription(desc, function () {
//                _signaler.sendSignal(JSON.stringify({ "sdp": connection.localDescription }), partnerClientId);
//            });
//        }, function (error) { console.log('Error creating session description: ' + error); });
//    };

//    // Hand off a new signal from the signaler to the connection
//    _newSignal = function (partnerClientId, data) {
//        var signal = JSON.parse(data),
//            connection = _getConnection(partnerClientId);

//        console.log('WebWizard: received signal');

//        // Route signal based on type
//        if (signal.sdp) {
//            _receivedSdpSignal(connection, partnerClientId, signal.sdp);
//        } else if (signal.candidate) {
//            _receivedCandidateSignal(connection, partnerClientId, signal.candidate);
//        }
//    },
//        // Retreive an existing or new connection for a given partner
//        _getConnection = function (partnerClientId) {
//            var connection = _connections[partnerClientId] || _createConnection(partnerClientId);
//            return connection;
//        },

//        _startSession();
//        _startSession = function () {
//            //viewModel.Username(username); // Set the selected username in the UI
//            //viewModel.Loading(true); // Turn on the loading indicator

//            // Ask the user for permissions to access the webcam and mic
//            getUserMedia(
//                {
//                    // Permissions to request
//                    video: true,
//                    audio: true
//                },
//                function (stream) { // succcess callback gives us a media stream
//                    $('.instructions').hide();

//                    // Now we have everything we need for interaction, so fire up SignalR
//                    _connect(username, function (chatHub) {
//                        // tell the viewmodel our conn id, so we can be treated like the special person we are.
//                        //viewModel.MyConnectionId(hub.connection.id);

//                        // Initialize our client signal manager, giving it a signaler (the SignalR hub) and some callbacks
//                        console.log('initializing connection manager');
//                        connectionManager.initialize(chatHub.server, _callbacks.onReadyForStream, _callbacks.onStreamAdded, _callbacks.onStreamRemoved);

//                        // Store off the stream reference so we can share it later
//                        _mediaStream = stream;

//                        // Load the stream into a video element so it starts playing in the UI
//                        console.log('playing my local video feed');
//                        var videoElement = document.querySelector('.video.mine');
//                        attachMediaStream(videoElement, _mediaStream);

//                        // Hook up the UI
//                        _attachUiHandlers();

//                        // viewModel.Loading(false);
//                    }, function (event) {
//                        alertify.alert('<h4>Failed SignalR Connection</h4> We were not able to connect you to the signaling server.<br/><br/>Error: ' + JSON.stringify(event));
//                        // viewModel.Loading(false);
//                    });
//                },
//                function (error) { // error callback
//                    alertify.alert('<h4>Failed to get hardware access!</h4> Do you have another browser type open and using your cam/mic?<br/><br/>You were not connected to the server, because I didn\'t code to make browsers without media access work well. <br/><br/>Actual Error: ' + JSON.stringify(error));
//                    // viewModel.Loading(false);
//                }
//            );
//        },
        

//        // Create a new WebWizard Peer Connection with the given partner
//        _createConnection = function (partnerClientId) {
//            console.log('WebWizard: creating connection...');

//            // Create a new PeerConnection
//            var connection = new RTCPeerConnection({ iceServers: _iceServers });

//            // ICE Candidate Callback
//            connection.onicecandidate = function (event) {
//                if (event.candidate) {
//                    // Found a new candidate
//                    console.log('WebWizard: new ICE candidate');
//                    _signaler.sendSignal(JSON.stringify({ "candidate": event.candidate }), partnerClientId);
//                } else {
//                    // Null candidate means we are done collecting candidates.
//                    console.log('WebWizard: ICE candidate gathering complete');
//                }
//            };

//            // State changing
//            connection.onstatechange = function () {
//                // Not doing anything here, but interesting to see the state transitions
//                var states = {
//                    'iceConnectionState': connection.iceConnectionState,
//                    'iceGatheringState': connection.iceGatheringState,
//                    'readyState': connection.readyState,
//                    'signalingState': connection.signalingState
//                };

//                console.log(JSON.stringify(states));
//            };

//            // Stream handlers
//            connection.onaddstream = function (event) {
//                console.log('WebWizard: adding stream');
//                // A stream was added, so surface it up to our UI via callback
//                _onStreamAddedCallback(connection, event);
//            };

//            connection.onremovestream = function (event) {
//                console.log('WebWizard: removing stream');
//                // A stream was removed
//                _onStreamRemovedCallback(connection, event.stream.id);
//            };

//            // Store away the connection
//            _connections[partnerClientId] = connection;

//            // And return it
//            return connection;
//        },

//        // Connection Manager Callbacks
//        _callbacks = {
//            onReadyForStream: function (connection) {
//                // The connection manager needs our stream
//                // todo: not sure I like this
//                connection.addStream(_mediaStream);
//            },
//            onStreamAdded: function (connection, event) {
//                console.log('binding remote stream to the partner window');

//                // Bind the remote stream to the partner window
//                var otherVideo = document.querySelector('.video.partner');
//                attachMediaStream(otherVideo, event.stream); // from adapter.js
//            },
//            onStreamRemoved: function (connection, streamId) {
//                // todo: proper stream removal.  right now we are only set up for one-on-one which is why this works.
//                console.log('removing remote stream from partner window');

//                // Clear out the partner window
//                var otherVideo = document.querySelector('.video.partner');
//                otherVideo.src = '';
//            }
//        };

//    // Process a newly received SDP signal
//    _receivedSdpSignal = function (connection, partnerClientId, sdp) {
//        console.log('WebRTC: processing sdp signal');
//        connection.setRemoteDescription(new RTCSessionDescription(sdp), function () {
//            if (connection.remoteDescription.type == "offer") {
//                console.log('WebRTC: received offer, sending response...');
//                _onReadyForStreamCallback(connection);
//                connection.createAnswer(function (desc) {
//                    connection.setLocalDescription(desc, function () {
//                        _signaler.sendSignal(JSON.stringify({ "sdp": connection.localDescription }), partnerClientId);
//                    });
//                },
//                    function (error) { console.log('Error creating session description: ' + error); });
//            } else if (connection.remoteDescription.type == "answer") {
//                console.log('WebRTC: received answer');
//            }
//        });
//    },










//    // Hub Callback: Call Declined
//    chatHub.client.callDeclined = function (decliningConnectionId, reason) {
//        console.log('call declined from: ' + decliningConnectionId);

//        // Let the user know that the callee declined to talk
//        alertify.error(reason);

//        // Back to an idle UI
//        //viewModel.Mode('idle');
//    };


//});

