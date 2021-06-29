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

namespace Wizard.Service.Search
{
    public interface ISubmittedProjectService
    {
        IEnumerable<Wizard.Data.Data.Entities.NewsFeed> GetNewsFeedList(int clientId);
        IEnumerable<Wizard.Data.CustomModel.ActiveBidWizard>  GetActiveBidWizardList(int bidId);
        IEnumerable<Wizard.Data.Data.Entities.SubmitProject> SubmittedProjectByClient(Wizard.Data.Data.Entities.SubmitProject submitProject);
        Wizard.Data.Data.Entities.SubmitProject SubmitWorkStatusByClient(Wizard.Data.Data.Entities.SubmitProject submitProject);
    }
    public class SubmittedProjectService : ISubmittedProjectService
    {
        private ISubmittedProjectRepository _submittedProjectRepository;
        public SubmittedProjectService(SubmittedProjectRepository submittedProjectRepository)
        {
            this._submittedProjectRepository = submittedProjectRepository;
        }

        public IEnumerable<ActiveBidWizard> GetActiveBidWizardList(int bidId)
        {
            return _submittedProjectRepository.GetActiveBidWizardList(bidId);
        }

        public IEnumerable<NewsFeed> GetNewsFeedList(int clientId)
        {
            return _submittedProjectRepository.GetNewsFeedList(clientId);
        }

        public IEnumerable<SubmitProject> SubmittedProjectByClient(SubmitProject submitProject)
        {
            return _submittedProjectRepository.SubmittedProjectByClient(submitProject);
        }

        public SubmitProject SubmitWorkStatusByClient(SubmitProject submitProject)
        {
            return _submittedProjectRepository.SubmitWorkStatusByClient(submitProject);
        }
    }
}
