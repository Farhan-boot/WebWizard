using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;

namespace ImageProcessingApp
{
    public class FaceDetection
    {
       public static void Main(string[] args)
        {
          
            if (args.Length>0)
            {
                //string rootDirectory = HostingEnvironment.ApplicationPhysicalPath;
                // string rootDirectory = System.IO.Path.GetFullPath(@"../../FaceData/haarcascade_frontalface_default.xml");
                CascadeClassifier classifier = new CascadeClassifier(args[1]);
                FacemarkLBFParams facemarkLBF = new FacemarkLBFParams();
                FacemarkLBF facemark = new FacemarkLBF(facemarkLBF);
                Image<Bgr, Byte> img = new Image<Bgr, Byte>(args[0]);
                var imgGray = img.Convert<Gray, byte>();
                var faces = classifier.DetectMultiScale(imgGray);
            }
            Console.ReadKey();
        }

       public static bool HumanFaceDetection(string fileName)
        {
            try
            {


              //  string fiame = "E:/Office/Dream Project/Project/WebWizardSolution/ImageProcessingApp\jesy.jpeg";

              //  Image<Bgr, Byte> img = new Image<Bgr, Byte>(fiame);

                // string rootDirectory = System.IO.Path.GetFullPath(@"..\..\");
                //string rootDirectory = HostingEnvironment.ApplicationPhysicalPath;
                //string lbpFacepath = "E:/Office/Dream Project/Project/WebWizardSolution/WebWizard/FaceData/lbpcascade_frontalface_improved.xml";
                //string modelPath = rootDirectory + "data/lbfmodel.yaml";

                //string rootDirectory = System.IO.Path.GetFullPath(@"..\..\");
                //string lbpFacepath = rootDirectory + "data/lbpcascade_frontalface_improved.xml";
                //string modelPath = rootDirectory + "data/lbfmodel.yaml";


               // string rootDirectory = System.IO.Path.GetFullPath(@"../../data/haarcascade_frontalface_default.xml");
               // CascadeClassifier classifier = new CascadeClassifier(rootDirectory);




                //CascadeClassifier classifier = new CascadeClassifier(rootDirectory+"haarcascade_frontalface.xml");
                //  FacemarkLBFParams facemarkLBF = new FacemarkLBFParams();
                //FacemarkLBF facemark = new FacemarkLBF(facemarkLBF);


                // Image<Bgr, Byte> img = new Image<Bgr, Byte>(fileName);

                //var img = new Bitmap(imageFileName.FileName).ToImage<Bgr, byte>();


                //var imgGray = img.Convert<Gray, byte>();
               // var faces = classifier.DetectMultiScale(imgGray);

                //facemark.LoadModel(modelPath);
                //VectorOfVectorOfPointF landmarks = new VectorOfVectorOfPointF();
                //VectorOfRect rects = new VectorOfRect(faces);
                //bool result = facemark.Fit(imgGray, rects, landmarks);
                //if (result)
                //{
                //    for (int i = 0; i < faces.Length; i++)
                //    {
                //        FaceInvoke.DrawFacemarks(img, landmarks[i], new MCvScalar(0, 0, 255));
                //        var p = landmarks[i][33];
                //        CvInvoke.Circle(img, new Point((int)p.X, (int)p.Y), 5, new MCvScalar(0, 255, 0), -1);
                //    }
                //}

                // pictureBox1.Image = img.ToBitmap();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }

    }
}
