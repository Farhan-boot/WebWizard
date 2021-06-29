using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.CustomModel;
using Wizard.Data.Data;
using Wizard.Data.Data.Entities;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Data.Repository
{
    public interface IMessageRepository
    {
        Wizard.Data.Data.Entities.Message AddMessage(Wizard.Data.Data.Entities.Message message);
        IEnumerable<Wizard.Data.Data.Entities.Message> GetUserMessageList(string sender, string senderType, string Receiver, string ReceiverType);
        IEnumerable<Wizard.Data.CustomModel.MessageDetailModel> GetWebWizardMessageList(int webWizardId);
        UserDetailModel GetOffLineUserInformetionByDbId(int ReceiverId,string ReceiverType);
        IEnumerable<Wizard.Data.CustomModel.MessageDetailModel> GetClientMessageList(int clientId);
        UserDetailModel GetOffLineWebWizardInformetionByDbId(int ReceiverId);
    }

    public class MessageRepository : IMessageRepository
    {
        public Message AddMessage(Message message)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                db.Message.Add(message);
                db.SaveChanges();
                return message;
            }
        }

        public IEnumerable<MessageDetailModel> GetClientMessageList(int clientId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var msgList = db.Message.Distinct().Where(x => x.Receiver == clientId||x.Sender==clientId).ToList();
                List<MessageDetailModel> messageDetail = new List<MessageDetailModel>();

                foreach (var userMsg in msgList.OrderByDescending(x => x.Id).Distinct())
                {
                    if (userMsg.Receiver != clientId || userMsg.Sender != clientId)
                    {
                        var userList = db.WebWizardRegistration.Where(x => x.WebWizardId == userMsg.Sender || x.WebWizardId == userMsg.Receiver).ToList();

                        var userListExceptMe = userList.Join(db.WebWizardDetails,
                 x => x.WebWizardId,
                 y => y.WebWizardId,
                (x, y) => new { y.Id, x.FirstName, x.LastName, x.Email, y.WebWizardProfileImageUrl, y.WebWizardMobileNo, y.WebWizardId }).ToList();

                        foreach (var user in userListExceptMe)
                        {
                            MessageDetailModel obj = new MessageDetailModel();
                            obj.UserId = user.WebWizardId;
                            obj.UserName = user.FirstName + " " + user.LastName;
                            obj.UserEmail = user.Email;
                            obj.MessageId = userMsg.Id;

                            if (user.WebWizardProfileImageUrl == null)
                            {
                                obj.UserUrl = "user-demo.png";
                            }
                            else
                            {
                                obj.UserUrl = user.WebWizardProfileImageUrl;
                            }


                            if (obj.UserId != userMsg.Sender)
                            {
                                obj.UserType = "Wizard";
                                obj.SenderId = obj.UserId;
                            }
                            else
                            {
                                obj.UserType = userMsg.UserType;
                                obj.SenderId = userMsg.Sender;
                            }

                            var check = messageDetail.Where(x => x.UserEmail == obj.UserEmail).ToList();
                            if (check.Count == 0)
                            {
                                messageDetail.Add(obj);
                            }

                        }


                    }
                }


                return messageDetail;
            }
        }

        public UserDetailModel GetOffLineUserInformetionByDbId(int ReceiverId,string ReceiverType)
        {
           
            using (WebWizardConnection db = new WebWizardConnection())
            {
                try
                {
                    UserDetailModel userObject = new UserDetailModel();
                    if (ReceiverType== "User")
                    {
                        var userRge = db.ClientRegistration.SingleOrDefault(x => x.ClientId == ReceiverId);
                        var userDetls = db.ClientDetails.SingleOrDefault(x => x.ClientId == userRge.ClientId);
                        userObject.UserDbId = userRge.ClientId.ToString();
                        userObject.UserEmail = userRge.Email;
                        userObject.UserName = userRge.FirstName + " " + userRge.LastName;
                        userObject.UserType = "User";
                        if (userDetls.ClientProfileImageUrl == null)
                        {
                            userObject.UserUrl = "/ClientAssets/ClientDashboard/ProfileImage/user-demo.png";
                        }
                        else
                        {
                            userObject.UserUrl = "/ClientAssets/ClientDashboard/ProfileImage/" + userDetls.ClientProfileImageUrl;
                        }

                        //Auto genarete ConnectedId
                        var AgId = Guid.NewGuid();
                        userObject.ConnectionId = AgId.ToString();

                    }
                    else
                    {
                        var userRge = db.WebWizardRegistration.SingleOrDefault(x => x.WebWizardId == ReceiverId);
                        var userDetls = db.WebWizardDetails.SingleOrDefault(x => x.WebWizardId == userRge.WebWizardId);
                        userObject.UserDbId = userRge.WebWizardId.ToString();
                        userObject.UserEmail = userRge.Email;
                        userObject.UserName = userRge.FirstName + " " + userRge.LastName;
                        userObject.UserType = "Wizard";
                        if (userDetls.WebWizardProfileImageUrl == null)
                        {
                            userObject.UserUrl = "/Assets/WebWizardDashboard/ProfileImage/user-demo.png";
                        }
                        else
                        {
                            userObject.UserUrl = "/Assets/WebWizardDashboard/ProfileImage/" + userDetls.WebWizardProfileImageUrl;
                        }

                        //Auto genarete ConnectedId
                        var AgId = Guid.NewGuid();
                        userObject.ConnectionId = AgId.ToString();
                    }



                    return userObject;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }



        }

        public UserDetailModel GetOffLineWebWizardInformetionByDbId(int ReceiverId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                try
                {

                var userRge = db.WebWizardRegistration.SingleOrDefault(x => x.WebWizardId == ReceiverId);
                var userDetls = db.WebWizardDetails.SingleOrDefault(x => x.WebWizardId == userRge.WebWizardId);

                UserDetailModel userObject = new UserDetailModel();
                userObject.UserDbId = userRge.WebWizardId.ToString();
                userObject.UserEmail = userRge.Email;
                userObject.UserName = userRge.FirstName + " " + userRge.LastName;
                userObject.UserType = "Wizard";


                if (userDetls.WebWizardProfileImageUrl == null)
                {
                    userObject.UserUrl = "/Assets/WebWizardDashboard/ProfileImage/user-demo.png";
                }
                else
                {
                    userObject.UserUrl = "/Assets/WebWizardDashboard/ProfileImage/" + userDetls.WebWizardProfileImageUrl;
                }

                //Auto genarete ConnectedId
                var AgId = Guid.NewGuid();
                userObject.ConnectionId = AgId.ToString();

                return userObject;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public IEnumerable<Message> GetUserMessageList(string sender, string senderType, string Receiver, string ReceiverType)
        {
            int sId = Convert.ToInt32(sender);
            int rId = Convert.ToInt32(Receiver);
            using (WebWizardConnection db = new WebWizardConnection())
            {
                if (sId == rId)
                {
                    var msgListEqul = db.Message.Where(x => x.Receiver == rId && x.UserType == ReceiverType && x.Sender == rId || x.Sender == sId && x.UserType == senderType && x.Receiver == sId).ToList();
                    return msgListEqul;
                }
                else
                {
                    var msgList = db.Message.Where(x => x.Sender == sId && x.Receiver == rId || x.Receiver == rId && x.Receiver == sId || x.Sender == rId && x.Receiver == sId || x.Receiver == sId && x.Receiver == rId).ToList();
                    return msgList;
                }

            }
        }

        public IEnumerable<MessageDetailModel> GetWebWizardMessageList(int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var msgList = db.Message.Distinct().Where(x => x.Receiver == webWizardId || x.Sender == webWizardId).ToList();
                List<MessageDetailModel> messageDetail = new List<MessageDetailModel>();

                foreach (var userMsg in msgList.OrderByDescending(x => x.Id).Distinct())
                {
                    if (userMsg.Receiver != webWizardId || userMsg.Sender != webWizardId)
                    {
                        var userList = db.ClientRegistration.Where(x => x.ClientId == userMsg.Sender || x.ClientId == userMsg.Receiver).ToList();
                        var userList2 = db.WebWizardRegistration.Where(x => x.WebWizardId == userMsg.Sender || x.WebWizardId == userMsg.Receiver).ToList();


                        var userListExceptMe = userList.Join(db.ClientDetails,
                 x => x.ClientId,
                 y => y.ClientId,
                (x, y) => new { y.Id, x.FirstName, x.LastName, x.Email, y.ClientProfileImageUrl, y.ClientMobileNo, y.ClientId }).ToList();


                        var userListExceptMe2 = userList2.Join(db.WebWizardDetails,
                x => x.WebWizardId,
                y => y.WebWizardId,
               (x, y) => new { y.Id, x.FirstName, x.LastName, x.Email, y.WebWizardProfileImageUrl, y.WebWizardMobileNo, y.WebWizardId }).ToList();



                        foreach (var user in userListExceptMe)
                        {
                            MessageDetailModel obj = new MessageDetailModel();
                            obj.UserId = user.ClientId;
                            obj.UserName = user.FirstName + " " + user.LastName;
                            obj.UserEmail = user.Email;
                            obj.MessageId = userMsg.Id;

                            if (user.ClientProfileImageUrl == null)
                            {
                                obj.UserUrl = "/ClientAssets/ClientDashboard/ProfileImage/user-demo.png";
                            }
                            else
                            {
                                obj.UserUrl = "/ClientAssets/ClientDashboard/ProfileImage/"+ user.ClientProfileImageUrl;
                            }


                            if (obj.UserId != userMsg.Sender)
                            {
                                obj.UserType = "User";
                                obj.SenderId = obj.UserId;
                            }
                            else
                            {
                                obj.UserType = userMsg.UserType;
                                obj.SenderId = userMsg.Sender;
                            }

                            var check = messageDetail.Where(x => x.UserEmail == obj.UserEmail).ToList();
                            if (check.Count == 0)
                            {
                                messageDetail.Add(obj);
                            }

                        }


                        foreach (var user2 in userListExceptMe2)
                        {
                            MessageDetailModel obj2 = new MessageDetailModel();
                            obj2.UserId = user2.WebWizardId;
                            obj2.UserName = user2.FirstName + " " + user2.LastName;
                            obj2.UserEmail = user2.Email;
                            obj2.MessageId = userMsg.Id;

                            if (user2.WebWizardProfileImageUrl == null)
                            {
                                obj2.UserUrl = "/ClientAssets/ClientDashboard/ProfileImage/user-demo.png";
                            }
                            else
                            {
                                obj2.UserUrl = "/Assets/WebWizardDashboard/ProfileImage/" + user2.WebWizardProfileImageUrl;
                            }


                            if (obj2.UserId != userMsg.Sender)
                            {
                                obj2.UserType = "Wizard";
                                obj2.SenderId = obj2.UserId;
                            }
                            else
                            {
                                obj2.UserType = userMsg.UserType;
                                obj2.SenderId = userMsg.Sender;
                            }

                            var check = messageDetail.Where(x => x.UserEmail == obj2.UserEmail).ToList();
                            if (check.Count == 0)
                            {
                                messageDetail.Add(obj2);
                            }

                        }


                    }
                }


                return messageDetail;
            }



        }
    }
}
