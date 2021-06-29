using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using Wizard.Model.WebWizard;

namespace WebWizard.Controllers
{
    public class WebWizardPortfolioController : Controller
    {
        ServiceAccess Access = new ServiceAccess();
        // GET: WebWizardPortfolio
        public ActionResult Index()
        {
            return View();
        }
        // GET: Project Type
        public ActionResult GetProjectType()
        {
            var listOfProjectType = Access.WebWizardPortfolioService.ListOfProjectType();
            return Json(listOfProjectType, JsonRequestBehavior.AllowGet);
        }
        // GET: Technology
        public ActionResult GetTechnology()
        {
            var listOfTechnology = Access.WebWizardPortfolioService.ListOfTechnology();
            return Json(listOfTechnology, JsonRequestBehavior.AllowGet);
        }

        // GET: Backend language
        public ActionResult GetBackendlanguage()
        {
            var listOfBackendlanguage = Access.WebWizardPortfolioService.ListOfBackendlanguage();
            return Json(listOfBackendlanguage, JsonRequestBehavior.AllowGet);
        }

        // GET: Run on server
        public ActionResult GetRunOnServer()
        {
            var listOfRunOnServer = Access.WebWizardPortfolioService.ListOfRunOnServer();
            return Json(listOfRunOnServer, JsonRequestBehavior.AllowGet);
        }

        // GET: Web Wizard Portfolio List
        public ActionResult GetWebWizardPortfolioListByWizardId()
        {
            int WebWizardId = (int)Session["WebWizardId"];
            string webWizardFolderName = Convert.ToString(Session["WebWizardId"]);
            //Call the Access
            var listOfWebWizardPortfolio = Access.WebWizardPortfolioService.GetWebWizardPortfolioListByWizardId(WebWizardId);

            if (listOfWebWizardPortfolio!=null)
            {
                foreach (var webWizardPortfolio in listOfWebWizardPortfolio)
                {
                    bool folderExists = Directory.Exists(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardFolderName + "/ImageThumbnail"));
                    if (folderExists)
                    {
                        string fullPath = "/WebWizardAssets/WebWizardPortfolio/"+ webWizardFolderName+ "/ImageThumbnail/" + webWizardPortfolio.ProjectImagePath;
                        webWizardPortfolio.ProjectImagePath= fullPath;
                    }                      //  string fullPath = Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardFolderName + "/ImageThumbnail" + webWizardPortfolio.ProjectImagePath);

                }
            }
           

            return Json(listOfWebWizardPortfolio, JsonRequestBehavior.AllowGet);
        }


        // POST: Take Prot
        [HttpPost]
        public ActionResult AddWebWizardPortfolio(WebWizardPortfolio webWizardPortfolio)
        {
            if (IsValidToPortfolio(webWizardPortfolio) == true)
            {
                // Valid 
                string webWizardId = Convert.ToString(Session["WebWizardId"]);
                webWizardPortfolio.WebWizardId = (int)Session["WebWizardId"];
                webWizardPortfolio.Status = false;
                webWizardPortfolio.IsDelete = false;

                //Project Assect Folder Create
                bool folderExists = Directory.Exists(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardId));
                if (!folderExists)
                {
                    Directory.CreateDirectory(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardId));
                    //Thumbnail Folder Create
                    bool ThumbnailFolderExists = Directory.Exists(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardId + "/ImageThumbnail"));
                    if (!ThumbnailFolderExists)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardId + "/" + "ImageThumbnail"));

                        //For Zip file
                        string Message, fileName, actualFileName;
                        Message = fileName = actualFileName = string.Empty;
                        actualFileName = webWizardPortfolio.ZipFile.FileName;
                        fileName = Guid.NewGuid() + Path.GetExtension(webWizardPortfolio.ZipFile.FileName);
                        int size = webWizardPortfolio.ZipFile.ContentLength;
                        webWizardPortfolio.ZipFile.SaveAs(Path.Combine(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardId), fileName));
                        webWizardPortfolio.ProjectZipFilePath = fileName;
                        webWizardPortfolio.ProjectSize = size;

                        //For Thumbnail file
                        string MessageThumbnail, fileNameThumbnail, actualFileNameThumbnail;
                        MessageThumbnail = fileNameThumbnail = actualFileNameThumbnail = string.Empty;
                        actualFileNameThumbnail = webWizardPortfolio.ImageFile.FileName;
                        fileNameThumbnail = Guid.NewGuid() + Path.GetExtension(webWizardPortfolio.ImageFile.FileName);
                        int sizeThumbnail = webWizardPortfolio.ImageFile.ContentLength;
                        // webWizardPortfolio.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardId+ "/" + "ImageThumbnail"), fileNameThumbnail));
                        // Resize Image
                        ResizeImage.ResizeStream(500, webWizardPortfolio.ImageFile.InputStream, Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "WebWizardAssets\\WebWizardPortfolio\\" + webWizardId+ "\\ImageThumbnail\\", Path.GetFileName(fileNameThumbnail)));
                        webWizardPortfolio.ProjectImagePath = fileNameThumbnail;
                    }
                }
                else
                {
                    //For Zip file
                    string Message, fileName, actualFileName;
                    Message = fileName = actualFileName = string.Empty;
                    actualFileName = webWizardPortfolio.ZipFile.FileName;
                    fileName = Guid.NewGuid() + Path.GetExtension(webWizardPortfolio.ZipFile.FileName);
                    int size = webWizardPortfolio.ZipFile.ContentLength;
                    webWizardPortfolio.ZipFile.SaveAs(Path.Combine(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardId), fileName));
                    webWizardPortfolio.ProjectZipFilePath = fileName;
                    webWizardPortfolio.ProjectSize = size;

                    //For Thumbnail file
                    string MessageThumbnail, fileNameThumbnail, actualFileNameThumbnail;
                    MessageThumbnail = fileNameThumbnail = actualFileNameThumbnail = string.Empty;
                    actualFileNameThumbnail = webWizardPortfolio.ImageFile.FileName;
                    fileNameThumbnail = Guid.NewGuid() + Path.GetExtension(webWizardPortfolio.ImageFile.FileName);
                    int sizeThumbnail = webWizardPortfolio.ImageFile.ContentLength;
                    // webWizardPortfolio.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/WebWizardAssets/WebWizardPortfolio/" + webWizardId+ "/" + "ImageThumbnail"), fileNameThumbnail));
                    // Resize Image
                    ResizeImage.ResizeStream(500, webWizardPortfolio.ImageFile.InputStream, Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "WebWizardAssets\\WebWizardPortfolio\\" + webWizardId + "\\ImageThumbnail\\", Path.GetFileName(fileNameThumbnail)));
                    webWizardPortfolio.ProjectImagePath = fileNameThumbnail;
                }

                //Call the Access
                var portfolio = Access.WebWizardPortfolioService.AddPortfolio(webWizardPortfolio);

            }
            else
            {
                // inValid 
            }

            return RedirectToAction("Profile", "WebWizardDashboard");
        }

        private bool IsValidToPortfolio(WebWizardPortfolio webWizardPortfolio)
        {
            bool chack = false;

            if (!string.IsNullOrEmpty(webWizardPortfolio.ProjectTitle))
            {
                chack = true;
                if (!string.IsNullOrEmpty(webWizardPortfolio.ProjectDescription))
                {
                    if (webWizardPortfolio.ZipFile != null)
                    {
                        chack = true;

                        if (webWizardPortfolio.ImageFile != null)
                        {
                            chack = true;
                            if (webWizardPortfolio.ProjectTypeId != 0)
                            {
                                chack = true;
                                if (webWizardPortfolio.TechnologyId != 0)
                                {
                                    chack = true;

                                    if (webWizardPortfolio.BackendLanguageId != 0)
                                    {
                                        chack = true;
                                        if (webWizardPortfolio.RunOnServerId != 0)
                                        {
                                            chack = true;
                                        }
                                        else
                                        {
                                            chack = false;
                                            return chack;
                                        }
                                    }
                                    else
                                    {
                                        chack = false;
                                        return chack;
                                    }

                                }
                                else
                                {
                                    chack = false;
                                    return chack;
                                }
                            }
                            else
                            {
                                chack = false;
                                return chack;
                            }
                        }
                        else
                        {
                            chack = false;
                            return chack;
                        }

                    }
                    else
                    {
                        chack = false;
                        return chack;
                    }
                }
                else
                {
                    chack = false;
                    return chack;
                }
            }
            else
            {
                chack = false;
                return chack;
            }

            return chack;
        }


    }
}