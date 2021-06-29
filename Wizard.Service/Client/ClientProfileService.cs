using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.CustomModel;
using Wizard.Data.Data.Entities;
using Wizard.Data.Repository;
using Wizard.Model.Client;
using Wizard.Model.Clients;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Service.Search
{
    public interface IClientProfileService
    {
        ClientDetails GetClientDetailsByClientId(int clientId);
        ClientDetailsModel AddClientDetails(ClientDetailsModel clientProfilePicture);
        ClientDetails UpdateClientProfilePicture(ClientDetails oldClientDetails,int clientId);
        Wizard.Data.Data.Entities.ClientDetails UpdateClientDetails(Wizard.Data.Data.Entities.ClientDetails oldClientDetail, ClientDetailsModel clientDetails);
        Wizard.Data.Data.Entities.ClientDetails AddClientDetailInfo(ClientDetailsModel clientDetails, int clientId);
        object GetEightWebWizardList();
        object GetClientInformetionByClientId(int clientId);
        List<ShowAllWebWizardModel> GetAllWebWizardList();
        List<ShowAllProjectForClientModel> GetAllProjectList();
        Wizard.Data.Data.Entities.WebWizardPortfolio GetSingleProjectById(int id);
        List<ShowAllClientModel> GetAllClientExceptMe(int clientId);
        List<NewsFeed> GetMyNewsFeedListByClientId(int clientId);
        Wizard.Data.Data.Entities.NewsFeed DeleteClientNewsFeed(int id);
        object GetProposalList(int newsFeedId);
        Wizard.Data.Data.Entities.WebWizardBid DeleteBidByClient(int Id);
        Wizard.Data.Data.Entities.WebWizardBid ApprovedByClient(int Id);
        IEnumerable<WebWizardNotificationModel> GetClientNotificationForAcceptNewsFeedBid(int clientId);
        Wizard.Data.CustomModel.ViewDetailsByBidIdModel GetViewDetailsByBidId(int Id);
    }
    public class ClientProfileService : IClientProfileService
    {
        private IClientProfileRepository _clientProfileRepository;
        public ClientProfileService(ClientProfileRepository clientProfileRepository)
        {
            this._clientProfileRepository = clientProfileRepository;
        }

        public ClientDetails AddClientDetailInfo(ClientDetailsModel clientDetails, int clientId)
        {
            return _clientProfileRepository.AddClientDetailInfo(clientDetails, clientId);
        }

        public ClientDetailsModel AddClientDetails(ClientDetailsModel clientProfilePicture)
        {
            return _clientProfileRepository.AddClientDetails(clientProfilePicture);
        }

        public WebWizardBid ApprovedByClient(int Id)
        {
            return _clientProfileRepository.ApprovedByClient(Id);
        }

        public WebWizardBid DeleteBidByClient(int Id)
        {
            return _clientProfileRepository.DeleteBidByClient(Id);
        }

        public NewsFeed DeleteClientNewsFeed(int id)
        {
            return _clientProfileRepository.DeleteClientNewsFeed(id);
        }

        public List<ShowAllClientModel> GetAllClientExceptMe(int clientId)
        {
            return _clientProfileRepository.GetAllClientExceptMe(clientId);
        }

        public List<ShowAllProjectForClientModel> GetAllProjectList()
        {
            return _clientProfileRepository.GetAllProjectList();
        }

        public List<ShowAllWebWizardModel> GetAllWebWizardList()
        {
            return _clientProfileRepository.GetAllWebWizardList();
        }

        public ClientDetails GetClientDetailsByClientId(int clientId)
        {
            return _clientProfileRepository.GetClientDetailsByClientId(clientId);
        }

        public object GetClientInformetionByClientId(int clientId)
        {
            return _clientProfileRepository.GetClientInformetionByClientId(clientId);
        }

        public IEnumerable<WebWizardNotificationModel> GetClientNotificationForAcceptNewsFeedBid(int clientId)
        {
            return _clientProfileRepository.GetClientNotificationForAcceptNewsFeedBid(clientId);
        }

        public object GetEightWebWizardList()
        {
            return _clientProfileRepository.GetEightWebWizardList();
        }

        public List<NewsFeed> GetMyNewsFeedListByClientId(int clientId)
        {
            return _clientProfileRepository.GetMyNewsFeedListByClientId(clientId);
        }

        public object GetProposalList(int newsFeedId)
        {
            return _clientProfileRepository.GetProposalList(newsFeedId);
        }

        public Data.Data.Entities.WebWizardPortfolio GetSingleProjectById(int id)
        {
            return _clientProfileRepository.GetSingleProjectById(id);
        }

        public ViewDetailsByBidIdModel GetViewDetailsByBidId(int Id)
        {
            return _clientProfileRepository.GetViewDetailsByBidId(Id);
        }

        public ClientDetails UpdateClientDetails(ClientDetails oldClientDetail, ClientDetailsModel clientDetails)
        {
            return _clientProfileRepository.UpdateClientDetails(oldClientDetail, clientDetails);
        }

        public ClientDetails UpdateClientProfilePicture(ClientDetails oldClientDetails,int clientId)
        {
            return _clientProfileRepository.UpdateClientProfilePicture(oldClientDetails, clientId);
        }
    }
}
