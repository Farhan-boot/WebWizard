using Microsoft.AspNet.SignalR;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebWizard.Controllers;
using WebWizard.Helper;
using WebWizard.Models;

namespace WebWizard
{
    public class ChatHub : Hub
    {
        #region Data Members
        ServiceAccess Access = new ServiceAccess();

        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        //for call
       

        #endregion

        #region Methods

        public void Connect(string userDbId, string userName, string userEmail, string userUrl, string userType)
        {
            var id = Context.ConnectionId;
            var check = ConnectedUsers.SingleOrDefault(x => x.UserDbId == userDbId);

            if (check != null)
            {
                Clients.All.onUserDisconnected(check.ConnectionId, check.UserName, check.UserEmail, check.UserUrl, check.UserType);
                ConnectedUsers.Remove(check);

                //ConnectedUsers.Remove(check);
                if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
                {
                    ConnectedUsers.Add(new UserDetail { UserDbId = userDbId, ConnectionId = id, UserName = userName, UserEmail = userEmail, UserUrl = userUrl, UserType = userType });
                    // send to caller
                    Clients.Caller.onConnected(userDbId, id, userName, userEmail, userUrl, userType, ConnectedUsers, CurrentMessage);
                    // send to all except caller client
                    Clients.AllExcept(id).onNewUserConnected(userDbId, id, userName, userEmail, userUrl, userType);
                }
            }
            else
            {
                if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
                {
                    ConnectedUsers.Add(new UserDetail { UserDbId = userDbId, ConnectionId = id, UserName = userName, UserEmail = userEmail, UserUrl = userUrl, UserType = userType });
                    // send to caller
                    Clients.Caller.onConnected(userDbId, id, userName, userEmail, userUrl, userType, ConnectedUsers, CurrentMessage);
                    // send to all except caller client
                    Clients.AllExcept(id).onNewUserConnected(userDbId, id, userName, userEmail, userUrl, userType);
                }
            }

        }

        public void SendMessageToAll(string userName, string userEmail, string userUrl, string userType, string message)
        {
            // store last 100 messages in cache
            AddMessageinCache(userName, userEmail, userUrl, userType, message);
            // Broad cast message
            Clients.All.messageReceived(userName, userEmail, userUrl, userType, message);
        }

        public void SendPrivateMessage(string toUserId, string message, string sender, string senderType, string Receiver, string ReceiverType)
        {
            string fromUserId = Context.ConnectionId;
            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            Wizard.Data.Data.Entities.Message msgObj = new Wizard.Data.Data.Entities.Message();
            if (toUser == null)
            {
                msgObj.Sender = Convert.ToInt32(fromUser.UserDbId);
                msgObj.UserType = fromUser.UserType;
                msgObj.Receiver = Convert.ToInt32(sender);
                msgObj.Body = message;
                msgObj.IsRead = false;
                msgObj.IsDelete = false;
                msgObj.SendDate = DateTime.Now;
                var msgObjs = Access.MessageService.AddMessage(msgObj);
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, fromUser.UserType, message);
            }
            else
            {
                msgObj.Sender = Convert.ToInt32(fromUser.UserDbId);
                msgObj.UserType = fromUser.UserType;
                msgObj.Receiver = Convert.ToInt32(toUser.UserDbId);
                msgObj.Body = message;
                msgObj.IsRead = false;
                msgObj.IsDelete = false;
                msgObj.SendDate = DateTime.Now;
                //Call repository
                var msgObjs = Access.MessageService.AddMessage(msgObj);

                if (toUser != null && fromUser != null)
                {
                    //connect user
                    var isOnlineUser = ConnectedUsers.SingleOrDefault(x => x.UserDbId == toUser.UserDbId && x.UserType == toUser.UserType);
                    if (isOnlineUser != null)
                    {
                        var msgList = Access.MessageService.GetUserMessageList(isOnlineUser.UserDbId, isOnlineUser.UserType, fromUser.UserDbId, fromUser.UserType);

                        Clients.Client(isOnlineUser.ConnectionId).sendPrivateMessage(fromUser.ConnectionId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, fromUser.UserType, fromUser.ConnectionId);

                        foreach (var msg in msgList)
                        {
                            if (msg.Sender.ToString() == isOnlineUser.UserDbId && msg.Receiver.ToString() == fromUser.UserDbId)
                            {
                                Clients.Client(isOnlineUser.ConnectionId).sendPrivateMessage(fromUser.ConnectionId, toUser.UserName, toUser.UserEmail, toUser.UserUrl, toUser.UserType, msg.Body);
                            }
                            else
                            {
                                Clients.Client(isOnlineUser.ConnectionId).sendPrivateMessage(fromUser.ConnectionId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, fromUser.UserType, msg.Body);
                            }

                        }
                        Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, fromUser.UserType, message);
                    }

                    // send to 
                    // Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, fromUser.UserType, message);
                    // send to caller user
                    // Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, fromUser.UserType, message);

                    if (msgObjs.UserType == "User")
                    {
                        ShowMessage();
                    }
                }

            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName, item.UserEmail, item.UserUrl, item.UserType);
            }
            return base.OnDisconnected(stopCalled);
        }




        



        #endregion

        #region private Messages

        private void AddMessageinCache(string userName, string userEmail, string userUrl, string userType, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, UserEmail = userEmail, UserUrl = userUrl, UserType = userType, Message = message });
            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        #endregion


        public List<Wizard.Data.Data.Entities.Message> GetPrivateMessage(string sender, string senderType, string Receiver, string ReceiverType)
        {
            var msgList = Access.MessageService.GetUserMessageList(sender, senderType, Receiver, ReceiverType).OrderBy(x => x.Id).Reverse().Take(10).Reverse();
            var toUser = ConnectedUsers.FirstOrDefault(x => x.UserDbId == sender && x.UserType == senderType);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.UserDbId == Receiver && x.UserType == ReceiverType);

            if (fromUser == null)
            {
                var userInfoFromDb = Access.MessageService.GetOffLineUserInformetionByDbId(Convert.ToInt32(Receiver), ReceiverType);

                UserDetail OffLineUser = new UserDetail();
                OffLineUser.ConnectionId = userInfoFromDb.ConnectionId;
                OffLineUser.UserDbId = userInfoFromDb.UserDbId;
                OffLineUser.UserEmail = userInfoFromDb.UserEmail;
                OffLineUser.UserName = userInfoFromDb.UserName;
                OffLineUser.UserType = userInfoFromDb.UserType;
                OffLineUser.UserUrl = userInfoFromDb.UserUrl;
                fromUser = OffLineUser;
            }

            Clients.Caller.sendPrivateMessage(fromUser.ConnectionId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, fromUser.UserType, fromUser.ConnectionId);
            foreach (var msg in msgList)
            {
                if (Convert.ToInt32(toUser.UserDbId) == msg.Sender && Convert.ToInt32(fromUser.UserDbId) == msg.Receiver && toUser.UserType == msg.UserType)
                {
                    Clients.Client(toUser.ConnectionId).sendPrivateMessage(fromUser.ConnectionId, toUser.UserName, toUser.UserEmail, toUser.UserUrl, msg.UserType, msg.Body);
                }
                else
                {
                    Clients.Caller.sendPrivateMessage(fromUser.ConnectionId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, msg.UserType, msg.Body);
                }
            }
            return msgList.ToList();
        }

        public List<Wizard.Data.Data.Entities.Message> GetPrivateMessageForClient(string sender, string senderType, string Receiver, string ReceiverType)
        {
            var msgList = Access.MessageService.GetUserMessageList(sender, senderType, Receiver, ReceiverType).OrderBy(x => x.Id).Reverse().Take(10).Reverse(); ;
            var toUser = ConnectedUsers.FirstOrDefault(x => x.UserDbId == sender && x.UserType == senderType);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.UserDbId == Receiver && x.UserType == ReceiverType);

            if (fromUser == null)
            {
                var userInfoFromDb = Access.MessageService.GetOffLineWebWizardInformetionByDbId(Convert.ToInt32(Receiver));
                UserDetail OffLineUser = new UserDetail();
                OffLineUser.ConnectionId = userInfoFromDb.ConnectionId;
                OffLineUser.UserDbId = userInfoFromDb.UserDbId;
                OffLineUser.UserEmail = userInfoFromDb.UserEmail;
                OffLineUser.UserName = userInfoFromDb.UserName;
                OffLineUser.UserType = userInfoFromDb.UserType;
                OffLineUser.UserUrl = userInfoFromDb.UserUrl;
                fromUser = OffLineUser;
            }

            Clients.Caller.sendPrivateMessage(fromUser.ConnectionId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, fromUser.UserType, fromUser.ConnectionId);
            foreach (var msg in msgList)
            {
                if (Convert.ToInt32(toUser.UserDbId) == msg.Sender && Convert.ToInt32(fromUser.UserDbId) == msg.Receiver)
                {
                    Clients.Client(toUser.ConnectionId).sendPrivateMessage(fromUser.ConnectionId, toUser.UserName, toUser.UserEmail, toUser.UserUrl, msg.UserType, msg.Body);
                }
                else
                {
                    Clients.Caller.sendPrivateMessage(fromUser.ConnectionId, fromUser.UserName, fromUser.UserEmail, fromUser.UserUrl, msg.UserType, msg.Body);
                }
            }
            return msgList.ToList();
        }

        //for Message
        public static void ShowMessage()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.displayMessage();
        }

        public void OpenMessageBox(int sender, string senderType, int Receiver, string ReceiverType)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.myMessageBox(sender, senderType, Receiver, ReceiverType);
        }

        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.displayStatus();
        }

        public static void ShowWizardNotification(int WebWizard)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.displayStatusForWizard(WebWizard);
        }

        public static void NotificationForNewsFeedBid()
        {
            int clientId = ClientDashboardController.setClientId;
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.clientNotificationForNewsFeedBid(clientId);
        }


        public void PrivateMessageTyping(string onlineSenderId)
        {
            var formUserInfo = ConnectedUsers.SingleOrDefault(x => x.ConnectionId == Context.ConnectionId);
            var toUserInfo = ConnectedUsers.SingleOrDefault(x => x.ConnectionId == onlineSenderId.ToString());
            if (toUserInfo!=null)
            {
                Clients.Client(toUserInfo.ConnectionId).typingPrivateMessage(formUserInfo.ConnectionId, formUserInfo.UserName);
            }
            

        }


        // for video call
        public void PrivateVideoCall(string targetUserId)
        {
            var callingUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = ConnectedUsers.SingleOrDefault(u => u.UserDbId == targetUserId);

            // Make sure the person we are trying to call is still here
            if (targetUser == null)
            {
                // If not, let the caller know
                Clients.Caller.callDeclined(targetUser.ConnectionId, "The user you called has left.");
                return;
            }

            // They are here, so tell them someone wants to talk
            Clients.Client(targetUser.ConnectionId).incomingCall(callingUser);
        }


        public void AnswerCall(bool acceptCall, string targetConnectionId)
        {
            var callingUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // This can only happen if the server-side came down and clients were cleared, while the user
            // still held their browser session.
            if (callingUser == null)
            {
                return;
            }

            // Make sure the original caller has not left the page yet
            if (targetUser == null)
            {
                Clients.Caller.callEnded(targetConnectionId, "The other user in your call has left.");
                return;
            }

            // Send a decline message if the callee said no
            if (acceptCall == false)
            {
                Clients.Client(targetConnectionId).callDeclined(callingUser, string.Format("{0} did not accept your call.", callingUser.UserName));
                return;
            }

            // Tell the original caller that the call was accepted
            Clients.Client(targetConnectionId).callAccepted(callingUser);


        }



    }
}