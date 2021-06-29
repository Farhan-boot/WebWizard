function getData() {
    $("#notCount").empty();
    $.ajax({
        url: '/ClientDashboard/GetClientNotificationForNewsFeedBid',
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
                        '<a href="/ClientDashboard/ViewDetailsByBidId/' + data[i].Id + ' " style="padding-top: 0px;padding-bottom: 0px;">' +
                        '<div class="pull-left" style="">' +
                       

                        '</div>' +
                        '<h4>' +
                        data[i].UserName + '</br>' +
                        '<h3 style="margin-bottom: 1px;">'+
                        data[i].InformetionTitle.substring(0, 10)
                        +'</h3>'
                        + '<small style="float: right;" class="label label-primary">' + '<i class="fa fa-money" style="padding-right: 5px;">' + '</i>' + data[i].InformetionOption + '</small>' +
                        '</h4>' +
                        '<p style="color:black">' + data[i].InformetionDescription.substring(0, 30) + '</p>' +
                        '</a>' +
                        '</li>');
                }
                $("#tbl").append(rows.join(''));
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

    //$("#notCount3").empty();
    $.ajax({
        url: '/ClientDashboard/GetClientMessageList',
        type: 'GET',
        data: { pageNumber: currentPage, pageSize: 3 },
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
               // $("#tbl3").empty();
                // $tbl.append(' <tr><th>ID</th><th>Name</th><th>Last Executed Date</th><th>Status</th></tr>');
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    var userDbId = $('#userDbId').val();

                    rowsMsgForWz.push({
                        senderId: data[i].SenderId,
                        userType: data[i].UserType
                    })


                    var tbl3 = $("#tbl3");
                    

                        tbl3.append('<li>' +
                        '<a href="#" onClick="messageBox(' + data[i].SenderId + ')">' +
                        '<div class="pull-left" style="padding: 6px;">' +

                        '<input hidden id ="userType" type="text" value="' + data[i].UserType + '">' +


                        '<img style="height: 60px; width: 60px;border-radius: 50%;" src=' + "/Assets/WebWizardDashboard/ProfileImage/" + data[i].UserUrl + '>' +

                        '</div>' +
                        '<h4>' +
                        data[i].UserName.substring(0, 8) +
                        '</h4>' +
                        '<small>' + '<i class="fa fa-envelope" style="padding-right:5px">' + '</i>' + data[i].UserEmail.substring(0, 10) + '</small>' +
                        '</a>' +
                        '</li>');
                }

               // $("#tbl3").append(rows.join(''));
            }
            else {
                isNullgetData3 = true;
                isFirstTimegetData3 = true;
            }


           
        }
    });

}

function messageBox(sId) {

    //$("#senderId").val(senderId);

    //var userType = $("#userType").val();
    ////my identificetion
    //var myDbId = parseInt($('#userDbId').val());
    //var myUserType = $('#hdUserUserType').val();

    //
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
            chatHub.server.getPrivateMessageForClient(Receiver, ReceiverType, sender, senderType);
        }
    };


    let c = 0;
    chatHub.client.clientNotificationForNewsFeedBid = function (clientId) {
        $.ajax({
            url: '/ClientDashboard/GetClientInfo',
            type: 'GET',
            datatype: 'json',
            success: function (data) {
                if (data.ClientRegistration.ClientId == clientId) {
                    getData();
                    c = c + 1;
                    $("#notCount").empty();
                    $("#notCount").append(c);
                }
            }
        });
    };

    // Start Hub
    $.connection.hub.start().done(function () {
        registerEvents(chatHub)
    });
    getData();
  //  getData3();




    jQuery(function ($) {


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
                chatHub.server.connect(response.ClientId,response.ClientFullName, response.ClientEmail, response.ClientProfileImageUrl, response.UserType);
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


    $("#txtNickName").keypress(function (e) {
        if (e.which == 13) {
            $("#btnStartChat").click();
        }
    });

    $("#txtMessage").keypress(function (e) {
        if (e.which == 13) {
            $('#btnSendMsg').click();
        }
    });


}


var currentPrivateMessagePageNumber = 0;
function registerClientMethods(chatHub) {

    // Calls when user successfully logged in
    chatHub.client.onConnected = function (userDbId,id, userName, userEmail, userUrl, userType, allUsers, messages) {

        setScreen(true);

        $('#userDbId').val(userDbId);

        $('#hdId').val(id);
        $('#hdUserName').val(userName);
        $('#spanUser').html(userName);
        $('#spanEmail').html(userEmail);

        $('#spanUserType').html(userType);
        $('#hdUserUserType').val(userType);

        $('#spanImage').attr('src',userUrl);

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
    chatHub.client.onNewUserConnected = function (userDbId,id, name,email,path,type) {

        AddUser(chatHub, userDbId, id, name, email, path, type);
    }


    // On User Disconnected
    chatHub.client.onUserDisconnected = function (id, userName, userEmail, userUrl, userType) {

        $('#' + id).remove();

        var ctrId = 'private_' + id;
        $('#' + ctrId).remove();


        var disc = $('<div class="disconnect">"' + userName + userEmail + userUrl + userType +'" logged off.</div>');

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
            createPrivateChatWindow(chatHub,userDbId, windowId, ctrId, fromUserName, userEmail, userUrl, userType);
            currentPrivateMessagePageNumber = 0;
        }

        //$('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + '<img id="" style="cursor:pointer;height:30px;width:30px" src='+ userUrl +' />' + fromUserName +'<br/>'+ userEmail+ '</span>: ' + message + '</div>');

        $('#' + ctrId).find('#divMessage').append('<div class="direct-chat-msg">'+
           '<div class= "direct-chat-info clearfix">'+
            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
            
            +'</span>'+
            '<span class="direct-chat-timestamp pull-right">'+
            userEmail
            +'</span>'+
                    '</div >'+
            '<img class="direct-chat-img" src='+userUrl+' alt="message user image">'+
                '<div class="direct-chat-text">'+
             message +
                '</div>'+    
            '</span>' +  '</div>');


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


            }




            //Privet message reload 
            function privateMessageReLoad(currentPage) {

                //var userDbId = $("#senderId").val();
                //var userType = $("#userType").val();
                //var myDbId = $('#userDbId').val();
                //var myUserType = $('#hdUserUserType').val();
                //var myEmail = $('#hdUserEmail').val();
                //var myUrl = $('#hdUserImageUrl').val();
                ////get online
                //var onlineSenderId = $("#onlineSenderId").val();
                //var onlineSenderType = $("#onlineSenderType").val();

                //if (userDbId == "" || userDbId == undefined && userType == "" || userType == undefined) {
                //    userDbId = onlineSenderId;
                //    userType = onlineSenderType;
                //}



                //
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
                    url: '/ClientDashboard/LoadMessageList',
                    type: 'POST',
                    data: { sender: myDbId, senderType: myUserType, Receiver: userDbId, ReceiverType: userType, pageNumber: currentPage, pageSize: 15 },
                    datatype: 'json',
                    success: function (data) {

                        if (data.length > 0) {
                            // chatHub.client.sendPrivateMessage(userIdChat, "xx", "xx", "xx", "xx", "xx");

                            $(data).each(function (index, txt) {

                                if (currentPrivateMessagePageNumber === 1) {

                                    if (txt.Sender === parseInt(myDbId) && txt.Receiver === parseInt(userDbId)) {

                                        $('#' + ctrId).find('#divMessage').prepend('<div class="direct-chat-msg">' +
                                            '<div class= "direct-chat-info clearfix">' +
                                            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
                                            + '</span>' +
                                            '<span class="direct-chat-timestamp pull-right">' +
                                            myEmail
                                            + '</span>' +
                                            '</div >' +
                                            '<img class="direct-chat-img" src=' + myUrl + ' alt="message user image">' +
                                            '<div class="direct-chat-text">' +
                                            txt.Body +
                                            '</div>' +
                                            '</span>' + '</div>');

                                    }
                                    else {
                                        $('#' + ctrId).find('#divMessage').prepend('<div class="direct-chat-msg">' +
                                            '<div class= "direct-chat-info clearfix">' +
                                            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
                                            + '</span>' +
                                            '<span class="direct-chat-timestamp pull-right">' +
                                            userEmail
                                            + '</span>' +
                                            '</div >' +
                                            '<img class="direct-chat-img" src=' + userUrl + ' alt="message user image">' +
                                            '<div class="direct-chat-text">' +
                                            txt.Body +
                                            '</div>' +
                                            '</span>' + '</div>');

                                    }
                                }
                                else {

                                    if (txt.Sender === parseInt(myDbId) && txt.Receiver === parseInt(userDbId)) {

                                        $('#' + ctrId).find('#divMessage').prepend('<div class="direct-chat-msg">' +
                                            '<div class= "direct-chat-info clearfix">' +
                                            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
                                            + '</span>' +
                                            '<span class="direct-chat-timestamp pull-right">' +
                                            myEmail
                                            + '</span>' +
                                            '</div >' +
                                            '<img class="direct-chat-img" src=' + myUrl + ' alt="message user image">' +
                                            '<div class="direct-chat-text">' +
                                            txt.Body +
                                            '</div>' +
                                            '</span>' + '</div>');

                                    }
                                    else {
                                        $('#' + ctrId).find('#divMessage').prepend('<div class="direct-chat-msg">' +
                                            '<div class= "direct-chat-info clearfix">' +
                                            '<span class="direct-chat-name pull-left" style="padding-left: 5px;">'
                                            + '</span>' +
                                            '<span class="direct-chat-timestamp pull-right">' +
                                            userEmail
                                            + '</span>' +
                                            '</div >' +
                                            '<img class="direct-chat-img" src=' + userUrl + ' alt="message user image">' +
                                            '<div class="direct-chat-text">' +
                                            txt.Body +
                                            '</div>' +
                                            '</span>' + '</div>');

                                    }


                                }


                            })



                        }
                    }
                });




            }







        })


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

function AddUser(chatHub, userDbId, id, name,email,path,type) {

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
        if (type =="Wizard") {


            code = $('<a id="' + id + '" class="user" style="padding:0px" onclick="messageBox('+userDbId+')">' + '<div class="widget-user-header" style="border-bottom-right-radius: 50%;color:white">' +
            '<div class= "widget-user-image" style="float: left;">' +
            '<img class="img-circle user-image profile-user-img img-responsive img-circle" src='+path+' style="margin: 3px;cursor:pointer;height: 50px;width: 50px;border-radius: 50%;margin-right: 8px;" alt="User Avatar">' +
            '</div>' +
            '<div class= "" style="padding-top: 5px;">' +
            '<h3 class="widget-user-username" style="margin-top: 5px; font-size:15px;font-weight: bold;margin-bottom: 3px;">' + name + ' [' + type + ']'+'</h3>' +
            '<h5 class="widget-user-desc" style="margin-top: 0px;margin-bottom: 0px;padding-bottom: 9px;">' + email +'</h5>' +
            '</div>' +
            '</div >' + '</a>');
            //$(code).click(function () {

            var id = $(this).attr('id');
            if (userId != id)
                $("#onlineSenderId").val(userDbId);
                $("#onlineSenderType").val(type);

             //   OpenPrivateChatWindow(chatHub, userDbId, id, name, email, path, type);
                currentPrivateMessagePageNumber=1;


            //});

        }


    }

    $("#divusers").append(code);

}

function AddMessage(userName, userEmail, userPath, userType, message) {
    //$('#divChatWindow').append('<div class="message"><span class="userName">' + '<img id="" style="cursor:pointer;height:50px;width:50px;border-radius: 50%;" src='+userPath+'>' + userName + userEmail + userType+'</span>: ' + message + '</div>');


    $('#divChatWindow').append('<div class="message"><span class="userName">' + '<div class="direct-chat-msg">' + '<div class="direct-chat-info clearfix">' + userEmail + '<span class="direct-chat-name pull-left" style="padding-left: 5px;">' + '</span>' + '<span class="direct-chat-timestamp pull-right">' + userName + ' [' + userType + ']' + '</span>' + '</div><img class="direct-chat-img" src=' + userPath + ' alt="message user image">' +'<div class="direct-chat-text" style="position: inherit;">'+ message +'</div></div>'+'</div>');



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

function createPrivateChatWindow(chatHub, userDbId, userId, ctrId, userName, userEmail, userUrl,userType) {
    var myDbId = $('#userDbId').val();
    var myUserType = $('#hdUserUserType').val();


    chatHub.server.getPrivateMessageForClient(myDbId, myUserType, userDbId, userType);


    var div = '<div id="' + ctrId + '" class="ui-widget-content draggable" rel="0">' +
        '<div class="header box-header with-border" style="padding-left:0px;">' +
        
        '<div class="col-md-12">' +
        '<div style="float:right;">' +
        '<a href="#" onClick="openPopup()" class="open-popup"><i class="fa fa-phone" aria-hidden="true" style="padding-right: 10px; font-weight: bold;">' +'</i></a>' +
        '<i id="imgDelete" style="cursor:pointer;" class="fa fa-times" aria-hidden="true"></i>'+
        '</div>' +
        '<div class="col-md-4 col-sm-4" style="float:left;margin-right: -12px;">' +
        '<img id="" class="user-image profile-user-img img-responsive img-circle" style="cursor:pointer;height:50px;width:50px;border-radius:50%;" src='+userUrl+' />' +
        '</div>' +
        '<div class="selText box-title col-md-6 col-sm-6" style="font-size: 10px;font-weight: bold;" rel="0">' + userName + ' ['+userType +']'+ '</div>' +
        '<div class="selText box-title col-md-6 col-sm-6" rel="0" style="font-size:15px;font-style: italic;font-size: 10px;">' + userEmail +'</div>' +
        '</div>'+
        '</div>' +
        '<div id="divMessage" class="messageArea" style="padding:5px;background-color: white;">' +
        '</div>' +
        '<p id="typing" style="padding: 0;margin: 0;text-align: center;font-weight: bold;">' + '</p>' +


        '<div class="buttonBar input-group" style="background-color: white;">' +
        '<input id="txtPrivateMessage" oninput="onTypePrivateMessage()" class="msgText form-control" type="text" style="width:142px" placeholder="Type Message ..."/>' +
        '<input style="width: 42px;height: 42px;" type="button" value="😀" readonly="" onclick="mySymbol()">' +
        '<input id="btnSendMessage" class="button btn-flat" style="margin-left: 1px;height:42px;color:black" type="button" value="Send" />' +



        '<div id="symbolBox" hidden  class="" style="background-color: rgba(42, 123, 72, 0.27);height: 121px;margin-top:12px;overflow: scroll;overflow-x: auto;">' +
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
        '<input id="31" onClick="setSymbol(31)" type ="button" value="🤐" readonly="" >' 







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
    $div.find("#txtPrivateMessage").keypress(function (e) {
        if (e.which == 13) {
            $div.find("#btnSendMessage").click();
        }
    });

    AddDivToContainer($div);

}

function AddDivToContainer($div) {
    $('#divContainer').prepend($div);

    $div.draggable({

        handle: ".header",
        stop: function () {

        }
    });

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


function openPopup(){
   // e.preventDefault();

    var x = screen.width / 2 - 900 / 2;
    var y = screen.height / 2 - 500 / 2;
    //window.open(meh.href, 'sharegplus', 'height=485,width=700,left=' + x + ',top=' + y);

    window.open("/Online/ClientVideoCall", 'sharegplus', 'width=1000,height=485, left = ' + x + ', top =' + y);

}



//on type event
function onTypePrivateMessage() {
    var onlineSenderId = $("#typeId").val();
    var chatHub = $.connection.chatHub;
    chatHub.server.privateMessageTyping(onlineSenderId);
}