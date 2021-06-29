using WebWizard.Data.Repository;
using WebWizard.Service.WebWizard;
using Wizard.Data.Repository;
using Wizard.Service.ImageProcessing;
using Wizard.Service.Admin;
using Wizard.Service.Message;
using Wizard.Service.Search;
using Wizard.Service.WebWizard;

namespace WebWizard.Helper
{
    public class ServiceAccess
    {
        /*WebWizard Service Access Start*/
        public IWebWizardRegisteredService WebWizardRegisteredService { get { return new WebWizardRegisteredService(new WebWizardRegisteredRepository()); } }
        public IWebWizardLogInService WebWizardLogInService { get { return new WebWizardLogInService(new WebWizardLogInRepository()); } }
        public IWebWizardProfileService WebWizardProfileService { get { return new WebWizardProfileService(new WebWizardProfileRepository()); } }
        public IWebWizardPortfolioService WebWizardPortfolioService { get { return new WebWizardPortfolioService(new WebWizardPortfolioRepository()); } }
        /*WebWizard Service Access End*/

        /*NewsFeed Service Access Start*/
        public INewsFeedService NewsFeedService { get { return new NewsFeedService(new NewsFeedRepository()); } }
        /*NewsFeed Service Access End*/

        /*Client Service Access Start*/
        public IClientRegistrationService ClientRegistrationService { get { return new ClientRegistrationService(new ClientRegistrationRepository()); } }
        public IClientLogInService ClientLogInService { get { return new ClientLogInService(new ClientLogInRepository()); } }
        public IClientProfileService ClientProfileService { get { return new ClientProfileService(new ClientProfileRepository()); } }
        /*Client Service Access End*/

        /*Search Service Access Start*/
        public ISearchService SearchService { get { return new SearchService(new SearchRepository()); } }
        /*Search Service Access End*/

        /*SubmittedProject Service Access Start*/
        public ISubmittedProjectService SubmittedProjectService { get { return new SubmittedProjectService(new SubmittedProjectRepository()); } }
        /*SubmittedProject Service Access End*/

        /*Message Service Access Start*/
        public IMessageService MessageService { get { return new MessageService(new MessageRepository()); } }
        /*Message Service Access End*/

        /*Admin Service Access Start*/
        public IAdminService AdminService { get { return new AdminService(new AdminRepository()); } }
        /*Admin Service Access End*/

        /*ImageProcessing Service Access Start*/
        public IImageProcessingService ImageProcessingService { get { return new ImageProcessingService(); } }
        /*ImageProcessing Service Access End*/
    }
}