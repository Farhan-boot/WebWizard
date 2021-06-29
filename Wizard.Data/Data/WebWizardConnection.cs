using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data.Entities;

namespace Wizard.Data.Data
{
    public class WebWizardConnection : DbContext
    {
        public DbSet<PrimaryKeyGenerator> PrimaryKeyGenerator { get; set; }
        public DbSet<Location> Location { get; set; }
        public  DbSet<WebWizardRegistration> WebWizardRegistration { get; set; }
        public  DbSet<WebWizardDetails> WebWizardDetails { get; set; }
        public  DbSet<WebWizardSkills> WebWizardSkills { get; set; }
        public  DbSet<TermsAndConditions> TermsAndConditions { get; set; }
        public  DbSet<Technology> Technology { get; set; }
        public  DbSet<Skills> Skills { get; set; }
        public  DbSet<RunOnServer> RunOnServer { get; set; }
        public  DbSet<ProjectType> ProjectType { get; set; }
        public  DbSet<Education> Education { get; set; }
        public  DbSet<Designation> Designation { get; set; }
        public  DbSet<Countries> Countries { get; set; }
        public  DbSet<BackendLanguage> BackendLanguage { get; set; }
        public  DbSet<WebWizardPortfolio> WebWizardPortfolio { get; set; }
        public  DbSet<NewsFeed> NewsFeed { get; set; }
        public DbSet<ClientRegistration> ClientRegistration { get; set; }
        public DbSet<ClientDetails> ClientDetails { get; set; }
        public DbSet<WebWizardBid> WebWizardBid { get; set; }
        public DbSet<SubmitProject> SubmitProject { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<AdminProfile> AdminProfile { get; set; }
    }
}
