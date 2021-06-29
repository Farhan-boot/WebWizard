using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Repository;
using Wizard.Model.WebWizard;
using Wizard.Models;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Web.Hosting;

namespace Wizard.Service.ImageProcessing
{
    public interface IImageProcessingService
    {
        bool HumanFaceDetection(string imageName);
    }
    public class ImageProcessingService : IImageProcessingService
    {
        public ImageProcessingService()
        {
            
        }

        public bool HumanFaceDetection(string imageName)
        {
            //string rootDirectory = HostingEnvironment.ApplicationPhysicalPath;
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(imageName);

            throw new NotImplementedException();
        }
    }
}
