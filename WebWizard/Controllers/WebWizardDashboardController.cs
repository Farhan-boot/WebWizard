using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using WebWizard.Models;
using Wizard.Data.Data;
using Wizard.Model.WebWizard;
using Emgu.CV;
using Emgu.CV.Structure;

namespace WebWizard.Controllers
{
    public class WebWizardDashboardController : Controller
    {
        ServiceAccess Access = new ServiceAccess();
        // GET: WebWizardDashboard

        public new ActionResult Profile()
        {
            try
            {
                if (Convert.ToBoolean(Session["Status"]) == true && Convert.ToInt32(Session["WebWizardId"]) != 0)
                {
                    if (Session["FirstName"].ToString() != "" && Session["LastName"].ToString() != "" && Session["Email"].ToString() != "" && Session["NameTitle"].ToString() != "")
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "LogIn");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        [HttpPost]
        public bool WebWizardDetails(WebWizardDetailsModel webWizardDetails)
        {
            webWizardDetails.WebWizardId = (int)Session["WebWizardId"];
            var webWizardDetail = Access.WebWizardProfileService.GetWebWizardDetailsByWebWizardId(webWizardDetails.WebWizardId);
            if (webWizardDetail != null)
            {
                // Update Wizard Detail
                Wizard.Data.Data.Entities.WebWizardDetails updateWebWizardDetail = Access.WebWizardProfileService.UpdateWebWizardDetails(webWizardDetails, webWizardDetail);
            }
            else
            {
                // Add Wizard Detail
                Location myLocation = new Location();
                myLocation.Latitude = webWizardDetails.Latitude;
                myLocation.Longitude = webWizardDetails.Longitude;
                var getLocation = Access.WebWizardProfileService.AddLocation(myLocation);
                webWizardDetails.LocationId = getLocation.Id;
                bool isAddWizardSkills = Access.WebWizardProfileService.AddWebWizardSkills(webWizardDetails.Skills, webWizardDetails.WebWizardId);
                WebWizardDetailsModel addWizardDetails = Access.WebWizardProfileService.AddWebWizardDetails(webWizardDetails);
            }
            return true;
        }

        public JsonResult GetEducationList()
        {
            var educationList = Access.WebWizardProfileService.EducationList();
            return Json(educationList.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDesignationList()
        {
            var designationList = Access.WebWizardProfileService.DesignationList();
            return Json(designationList.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSkillList()
        {
            var skillList = Access.WebWizardProfileService.SkillList();
            return Json(skillList.ToList(), JsonRequestBehavior.AllowGet);
        }

        // Show WebWizard Dashboard Element
        [HttpGet]
        public JsonResult GetWebWizardDetail()
        {
            int webWizardId = (int)Session["WebWizardId"];
            var webWizardDetail = Access.WebWizardProfileService.GetWebWizardDetailsByWebWizardId(webWizardId);
            
            if (webWizardDetail != null)
            {

                if (webWizardDetail.WebWizardProfileImageUrl == null)
                {
                    webWizardDetail.WebWizardProfileImageUrl = "/Assets/images/user-login.png";
                    Session["WebWizardProfileImageUrl"] = "/Assets/images/user-login.png";
                }
                else
                {
                    webWizardDetail.WebWizardProfileImageUrl = "/Assets/WebWizardDashboard/ProfileImage/" + webWizardDetail.WebWizardProfileImageUrl;
                    Session["WebWizardProfileImageUrl"] = webWizardDetail.WebWizardProfileImageUrl;
                }
            }

            return Json(webWizardDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetWebWizardRegistrationDetail()
        {
            int webWizardId = (int)Session["WebWizardId"];
            var webWizardRegistrationDetail = Access.WebWizardRegisteredService.GetWebWizardRegistrationDetailByWebWizardId(webWizardId);
            webWizardRegistrationDetail.Password = PasswordEncryptOrDecrypt.DecodePassword(webWizardRegistrationDetail.Password);
            return Json(webWizardRegistrationDetail, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetWebWizardDesignation()
        {
            int webWizardId = (int)Session["WebWizardId"];
            var webWizardDetail = Access.WebWizardProfileService.GetWebWizardDetailsByWebWizardId(webWizardId);
            if (webWizardDetail != null)
            {
                if (webWizardDetail.DesignationId != 0)
                {
                    var webWizardDesignation = Access.WebWizardProfileService.GetWebWizardDesignationByDesignationId(webWizardDetail.DesignationId);
                    return Json(webWizardDesignation, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetWebWizardEducation()
        {
            int webWizardId = (int)Session["WebWizardId"];
            var webWizardDetail = Access.WebWizardProfileService.GetWebWizardDetailsByWebWizardId(webWizardId);
            if (webWizardDetail != null)
            {
                if (webWizardDetail.EducationId != 0)
                {
                    var webWizardEducation = Access.WebWizardProfileService.GetWebWizardEducationByEducationId(webWizardDetail.EducationId);
                    return Json(webWizardEducation, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetWebWizardLocationDetail()
        {
            int webWizardId = (int)Session["WebWizardId"];
            var webWizardRegistrationDetail = Access.WebWizardRegisteredService.GetWebWizardRegistrationDetailByWebWizardId(webWizardId);
            var webWizardLocationDetail = Access.WebWizardRegisteredService.GetWebWizardLocationDetailByStateId(webWizardRegistrationDetail.StateId);
            return Json(webWizardLocationDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetWebWizardSkills()
        {
            int webWizardId = (int)Session["WebWizardId"];
            IEnumerable<Wizard.Data.Data.Entities.WebWizardSkills> webWizardSkillsId = Access.WebWizardProfileService.GetWebWizardSkillsIdByWebWizardId(webWizardId);

            var webWizardSkills = Access.WebWizardProfileService.GetSkillListBywebWizardSkillsId(webWizardSkillsId);
            return Json(webWizardSkills, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProfilePicture(WebWizardDetailsModel webWizardProfilePicture)
        {
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            var file = Request.Files[0] as HttpPostedFileBase;
            actualFileName = file.FileName;
            fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            int size = file.ContentLength;
            // ResizeImage.ResizeStream(500, file.InputStream, Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "UserAssets\\ProfilePhotos\\", Path.GetFileName(fileName)));
            file.SaveAs(Path.Combine(Server.MapPath("~/Assets/WebWizardDashboard/ProfileImage"), fileName));

            //image Face Detection
            
            string rootDirectory = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string imagePath = rootDirectory+"Assets\\WebWizardDashboard\\ProfileImage\\"+ fileName;
            string facePath = rootDirectory + "FaceData\\haarcascade_frontalface_default.xml";

            string[] name = {imagePath,facePath};
            //ImageProcessingApp.FaceDetection.Main(name);

            //image Face Detection

            int webWizardId = (int)Session["WebWizardId"];
            if (fileName != null)
            {
                webWizardProfilePicture.ProfilePicture = fileName;
                var oldWebWizardDetails = Access.WebWizardProfileService.GetWebWizardDetailsByWebWizardId(webWizardId);
                WebWizardDetailsModel newWebWizardDetails = new WebWizardDetailsModel();

                if (oldWebWizardDetails == null)
                {
                    webWizardProfilePicture.WebWizardId = webWizardId;
                    webWizardProfilePicture.LocationId = 1;
                    webWizardProfilePicture.Education = 1;
                    webWizardProfilePicture.Designation = 1;
                    Access.WebWizardProfileService.AddWebWizardDetails(webWizardProfilePicture);
                }
                else
                {
                    oldWebWizardDetails.WebWizardProfileImageUrl = webWizardProfilePicture.ProfilePicture;
                    Access.WebWizardProfileService.AddWebWizardProfilePicture(oldWebWizardDetails);
                    Session["WebWizardProfileImageUrl"] = "/Assets/WebWizardDashboard/ProfileImage/" + webWizardProfilePicture.ProfilePicture;
                }

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MyNewsFeed()
        {
            return View();
        }
        public JsonResult MyNewsFeedList()
        {
            int WebWizardId = (int)Session["WebWizardId"];
            var myNewsFeedList = Access.WebWizardProfileService.GetNewsFeedListByWebWizardId(WebWizardId);
            return Json(myNewsFeedList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteWebWizardNewsFeed(Wizard.Data.Data.Entities.NewsFeed NewsFeed)
        {
            var deleteWebWizard = Access.WebWizardProfileService.DeleteWebWizardNewsFeed(NewsFeed.Id);
            return Json(deleteWebWizard, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNotificationForWebWizard()
        {
            var notificationForWebWizard = Access.WebWizardProfileService.NotificationListForWebWizard().Where(x=>x.IsWebWizard==false);

            return Json(notificationForWebWizard.OrderByDescending(x=>x.Id).Take(10), JsonRequestBehavior.AllowGet);
        }

        public ActionResult WebWizardBidList()
        {
            return View();
        }
        [HttpGet]
        public JsonResult MyBidList()
        {
            int webWizardId = (int)Session["WebWizardId"];
            var myBidList = Access.WebWizardProfileService.WebWizardBidList(webWizardId);
            return Json(myBidList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WorkOrder(int id)
        {
            int webWizardId = (int)Session["WebWizardId"];
            var workOrder = Access.WebWizardProfileService.WorkOrder(id, webWizardId);
            WorkOrder wo = new WorkOrder();
            wo.NewsFeedId = workOrder.NewsFeedId;
            wo.ClientId = workOrder.ClientId;
            wo.BidPrice = workOrder.BidPrice;
            wo.ClientAddress = workOrder.ClientAddress;
            wo.ClientGmail = workOrder.ClientGmail;
            wo.ClientImagePath = workOrder.ClientImagePath;
            wo.ClientName = workOrder.ClientName;
            wo.ClientNumber = workOrder.ClientNumber;
            wo.Date = workOrder.Date;
            wo.Id = workOrder.Id;
            wo.ProjectType = workOrder.ProjectType;
            wo.ProposalPrice = workOrder.ProposalPrice;
            return View(wo);
        }

        [HttpPost]
        public ActionResult SubmitProject(SubmitProject submitProject)
        {
            Wizard.Data.Data.Entities.SubmitProject myObject =new Wizard.Data.Data.Entities.SubmitProject();

            int webWizardId = (int)Session["WebWizardId"];
            submitProject.WebWizardId = webWizardId;

            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            var file = submitProject.File;
            actualFileName = file.FileName;

            var folder = Server.MapPath("~/ClientAssets/ClientDashboard/SubmittedProject/"+ submitProject.ClientId);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;
                submitProject.ProjectUrl = fileName;
                submitProject.PostDate = DateTime.Now;
                submitProject.SubmitWorkStatus = "Pending";
                file.SaveAs(Path.Combine(Server.MapPath("~/ClientAssets/ClientDashboard/SubmittedProject/"+submitProject.ClientId), fileName));
                myObject.Id = submitProject.Id;
                myObject.NewsFeedId = submitProject.NewsFeedId;
                myObject.ClientId = submitProject.ClientId;
                myObject.WebWizardId = submitProject.WebWizardId;
                myObject.Comment = submitProject.Comment;
                myObject.ProjectUrl = submitProject.ProjectUrl;
                myObject.PostDate = submitProject.PostDate;
                myObject.SubmitWorkStatus = submitProject.SubmitWorkStatus;

                var mySubmitProject= Access.WebWizardProfileService.SubmitProject(myObject);
               return Json(mySubmitProject, JsonRequestBehavior.AllowGet);
            }
            else
            {
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;
                submitProject.ProjectUrl = fileName;
                submitProject.PostDate = DateTime.Now;
                submitProject.SubmitWorkStatus = "Pending";
                file.SaveAs(Path.Combine(Server.MapPath("~/ClientAssets/ClientDashboard/SubmittedProject/" + submitProject.ClientId), fileName));
                myObject.Id = submitProject.Id;
                myObject.NewsFeedId = submitProject.NewsFeedId;
                myObject.ClientId = submitProject.ClientId;
                myObject.WebWizardId = submitProject.WebWizardId;
                myObject.Comment = submitProject.Comment;
                myObject.ProjectUrl = submitProject.ProjectUrl;
                myObject.PostDate = submitProject.PostDate;
                myObject.SubmitWorkStatus = submitProject.SubmitWorkStatus;


                var mySubmitProject = Access.WebWizardProfileService.SubmitProject(myObject);
                return Json(mySubmitProject, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult GetSubmittedProjectByNewsFeedId(Wizard.Data.Data.Entities.SubmitProject getSubmitProject)
        {
            int webWizardId = (int)Session["WebWizardId"];
            var getSubmittedProjectByNewsFeedId = Access.WebWizardProfileService.GetSubmittedProjectByNewsFeedId(getSubmitProject.NewsFeedId);
            return Json(getSubmittedProjectByNewsFeedId.Where(x=>x.WebWizardId== webWizardId).ToList(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetWebWizardNotification()
        {
            int webWizardId = (int)Session["WebWizardId"];
            var notificationByWebWizardId = Access.WebWizardProfileService.GetNotificationByWebWizardId(webWizardId);
            return Json(notificationByWebWizardId.OrderByDescending(x=>x.Id).Take(5), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetWebWizardMessageList(int pageNumber,int pageSize)
        {
            int webWizardId = (int)Session["WebWizardId"];
            var messageListByWebWizardId = Access.MessageService.GetWebWizardMessageList(webWizardId).ToPagedList(pageNumber, pageSize);
            return Json(messageListByWebWizardId.OrderByDescending(x=>x.MessageId).Where(x=>x.UserId!= webWizardId).ToList(), JsonRequestBehavior.AllowGet);
        }

        bool isFirstTime = true;
        [HttpPost]
        public JsonResult LoadMessageList(string sender, string senderType, string Receiver, string ReceiverType,int pageNumber,int pageSize)
        {
            IEnumerable<Wizard.Data.Data.Entities.Message> msgList;
            if (isFirstTime==true)
            {
                 msgList = Access.MessageService.GetUserMessageList(sender, senderType, Receiver, ReceiverType).OrderBy(x => x.Id).Reverse().ToPagedList(pageNumber, pageSize);
                isFirstTime = false;
            }
            else
            {
                 msgList = Access.MessageService.GetUserMessageList(sender, senderType, Receiver, ReceiverType).OrderBy(x => x.Id).Reverse().ToPagedList(pageNumber, pageSize).Reverse();
            }
           

            return Json(msgList, JsonRequestBehavior.AllowGet);
        }


    }
}