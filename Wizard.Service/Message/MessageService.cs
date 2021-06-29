using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.CustomModel;
using Wizard.Data.Data.Entities;
using Wizard.Data.Repository;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Service.Message
{
    public interface IMessageService
    {
        Wizard.Data.Data.Entities.Message AddMessage(Wizard.Data.Data.Entities.Message message);
        IEnumerable<Wizard.Data.Data.Entities.Message> GetUserMessageList(string sender, string senderType, string Receiver, string ReceiverType);
        IEnumerable<Wizard.Data.CustomModel.MessageDetailModel> GetWebWizardMessageList(int webWizardId);
        UserDetailModel GetOffLineUserInformetionByDbId(int ReceiverId,string ReceiverType);
        IEnumerable<Wizard.Data.CustomModel.MessageDetailModel> GetClientMessageList(int clientId);
        UserDetailModel GetOffLineWebWizardInformetionByDbId(int ReceiverId);
    }
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;
        public MessageService(MessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }

        public Data.Data.Entities.Message AddMessage(Data.Data.Entities.Message message)
        {
            return _messageRepository.AddMessage(message);
        }

        public IEnumerable<MessageDetailModel> GetClientMessageList(int clientId)
        {
            return _messageRepository.GetClientMessageList(clientId);
        }

        public UserDetailModel GetOffLineUserInformetionByDbId(int ReceiverId,string ReceiverType)
        {
            return _messageRepository.GetOffLineUserInformetionByDbId(ReceiverId, ReceiverType);
        }

        public UserDetailModel GetOffLineWebWizardInformetionByDbId(int ReceiverId)
        {
            return _messageRepository.GetOffLineWebWizardInformetionByDbId(ReceiverId);
        }

        public IEnumerable<Data.Data.Entities.Message> GetUserMessageList(string sender, string senderType, string Receiver, string ReceiverType)
        {
            return _messageRepository.GetUserMessageList(sender, senderType, Receiver, ReceiverType);
        }

        public IEnumerable<MessageDetailModel> GetWebWizardMessageList(int webWizardId)
        {
            return _messageRepository.GetWebWizardMessageList(webWizardId);
        }
    }
}
