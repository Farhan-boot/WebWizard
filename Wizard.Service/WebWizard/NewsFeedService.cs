using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data.Entities;
using Wizard.Data.Repository;
using Wizard.Model.Clients;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Service.WebWizard
{
    public interface INewsFeedService
    {
        NewsFeed AddWebWizardNewsFeed(NewsFeedModel newsFeedModel);
        NewsFeed AddClientNewsFeed(NewsFeedModel newsFeedModel);
        List<ShowNewsFeedModel> GetNewsFeed();
        List<ShowNewsFeedModel> GetNewsFeedForClient();
        NewsFeedCounter GetNewsFeedCounter();
        List<ShowAllWizardExceptMeModel> GetAllWizardExceptMe(int webWizardId);
        IEnumerable<Data.Data.Entities.WebWizardPortfolio> GetAllWizardProjecExceptMyProjecs(int webWizardId);
        List<ShowAllClientModel> GetAllClient(int clientId);
        Wizard.Data.Data.Entities.WebWizardBid AddBidForClient(Wizard.Data.Data.Entities.WebWizardBid newsFeedBid);
        Wizard.Data.Data.Entities.NewsFeed ClientNewsFeedDetails(int id);
        Wizard.Data.Data.Entities.WebWizardBid UpdateNewsFeedBidForClient(Wizard.Data.Data.Entities.WebWizardBid newsFeedBid);
    }
    public class NewsFeedService : INewsFeedService
    {
        private INewsFeedRepository _newsFeedRepository;
        public NewsFeedService(NewsFeedRepository newsFeedRepository)
        {
            this._newsFeedRepository = newsFeedRepository;
        }

        public WebWizardBid AddBidForClient(WebWizardBid newsFeedBid)
        {
            return _newsFeedRepository.AddBidForClient(newsFeedBid);
        }

        public NewsFeed AddClientNewsFeed(NewsFeedModel newsFeedModel)
        {
            return _newsFeedRepository.AddClientNewsFeed(newsFeedModel);
        }

        public NewsFeed AddWebWizardNewsFeed(NewsFeedModel newsFeedModel)
        {
            return _newsFeedRepository.AddWebWizardNewsFeed(newsFeedModel);
        }

        public NewsFeed ClientNewsFeedDetails(int id)
        {
            return _newsFeedRepository.ClientNewsFeedDetails(id);
        }

        public List<ShowAllClientModel> GetAllClient(int clientId)
        {
            return _newsFeedRepository.GetAllClient(clientId);
        }

        public List<ShowAllWizardExceptMeModel> GetAllWizardExceptMe(int webWizardId)
        {
            return _newsFeedRepository.GetAllWizardExceptMe(webWizardId);
        }

        public IEnumerable<Data.Data.Entities.WebWizardPortfolio> GetAllWizardProjecExceptMyProjecs(int webWizardId )
        {
            return _newsFeedRepository.GetAllWizardProjecExceptMyProjecs(webWizardId);
        }

        public List<ShowNewsFeedModel> GetNewsFeed()
        {
            return _newsFeedRepository.GetNewsFeed();
        }

        public NewsFeedCounter GetNewsFeedCounter()
        {
            return _newsFeedRepository.GetNewsFeedCounter();
        }

        public List<ShowNewsFeedModel> GetNewsFeedForClient()
        {
            return _newsFeedRepository.GetNewsFeedForClient();
        }

        public WebWizardBid UpdateNewsFeedBidForClient(WebWizardBid newsFeedBid)
        {
            return _newsFeedRepository.UpdateNewsFeedBidForClient(newsFeedBid);
        }
    }
}
