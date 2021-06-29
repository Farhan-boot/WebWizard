using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;

namespace WebWizard.Controllers
{
    public class SubmittedProjectController : Controller
    {
        ServiceAccess Access = new ServiceAccess();

        // GET: SubmittedProject
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult NewsFeedList()
        {
            return View();
        }


        //Api method
        [HttpGet]
        public ActionResult GetNewsFeedList()
        {
            int clientId = (int)Session["ClientId"];
            var newsFeedList= Access.SubmittedProjectService.GetNewsFeedList(clientId);
            return Json(newsFeedList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetActiveBidWizardList(Wizard.Data.Data.Entities.WebWizardBid bidId)
        {
            var activeBidWizardList = Access.SubmittedProjectService.GetActiveBidWizardList(bidId.Id);

            return Json(activeBidWizardList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmittedProjectByClient(Wizard.Data.Data.Entities.SubmitProject submitProject)
        {
            var submittedProjectByClient = Access.SubmittedProjectService.SubmittedProjectByClient(submitProject);
            return Json(submittedProjectByClient,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitWorkStatusByClient(Wizard.Data.Data.Entities.SubmitProject submitProject)
        {
            var submitWorkStatus = Access.SubmittedProjectService.SubmitWorkStatusByClient(submitProject);
            return Json(submitWorkStatus, JsonRequestBehavior.AllowGet);
        }
        

    }
}