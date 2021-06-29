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
    public interface ISubmittedProjectRepository
    {
        IEnumerable<Wizard.Data.Data.Entities.NewsFeed> GetNewsFeedList(int clientId);
        IEnumerable<Wizard.Data.CustomModel.ActiveBidWizard> GetActiveBidWizardList(int bidId);
        IEnumerable<Wizard.Data.Data.Entities.SubmitProject> SubmittedProjectByClient(Wizard.Data.Data.Entities.SubmitProject submitProject);
        Wizard.Data.Data.Entities.SubmitProject SubmitWorkStatusByClient(Wizard.Data.Data.Entities.SubmitProject submitProject);
    }

    public class SubmittedProjectRepository : ISubmittedProjectRepository
    {
        public IEnumerable<ActiveBidWizard> GetActiveBidWizardList(int bidId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var activeBid = db.WebWizardBid.Where(x => x.NewsfeedId == bidId && x.Status == true).ToList();

                var result = activeBid.Join(db.WebWizardRegistration,
                 x => x.WebWizardId,
                 y => y.WebWizardId,
                (x, y) => new { x.WebWizardId, x.NewsfeedId, y.FirstName, y.LastName }).ToList();

                var bidWizard = result.Join(db.WebWizardDetails,
                           x => x.WebWizardId,
                           y => y.WebWizardId,
                          (x, y) => new
                          {
                              x.FirstName,
                              x.LastName,
                              x.WebWizardId,
                              x.NewsfeedId,
                              y.WebWizardProfileImageUrl,
                          }).ToList();

                List<ActiveBidWizard> activeBidWizardList = new List<ActiveBidWizard>();
                foreach (var wizard in bidWizard)
                {
                    ActiveBidWizard obj = new ActiveBidWizard();
                    obj.FirstName = wizard.FirstName;
                    obj.LastName = wizard.LastName;
                    obj.WebWizardId = wizard.WebWizardId;
                    obj.NewsfeedId = wizard.NewsfeedId;
                    obj.WebWizardProfileImageUrl = wizard.WebWizardProfileImageUrl;
                    activeBidWizardList.Add(obj);
                }
                return activeBidWizardList;
            }
        }

            public IEnumerable<NewsFeed> GetNewsFeedList(int clientId)
            {
                using (WebWizardConnection db = new WebWizardConnection())
                {
                    return db.NewsFeed.Where(x => x.UserId == clientId && x.IsWebWizard == false).ToList();
                }

            }

        public IEnumerable<SubmitProject> SubmittedProjectByClient(SubmitProject submitProject)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.SubmitProject.Where(x => x.NewsFeedId == submitProject.NewsFeedId && x.WebWizardId == submitProject.WebWizardId).ToList();
            }
        }

        public SubmitProject SubmitWorkStatusByClient(SubmitProject submitProject)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
               var workStatusByClient= db.SubmitProject.SingleOrDefault(x=>x.Id==submitProject.Id&&x.NewsFeedId== submitProject.NewsFeedId&&x.ClientId==submitProject.ClientId);
                workStatusByClient.SubmitWorkStatus = submitProject.SubmitWorkStatus;
                db.SaveChanges();
                return workStatusByClient;
            }
        }
    }
    }
