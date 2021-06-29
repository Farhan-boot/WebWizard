using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Wizard.Data.Data;

namespace WebWizard.Helper
{

    public class AppHelper
    {

    }

    public static class UserClientForChat
    {

        public static UserClient SetValueForClientChatUser(string ClientId, string ClientFullName, string ClientEmail, string ClientProfileImageUrl, string UserType)
        {
            UserClient myUser = new UserClient();
            myUser.ClientId = ClientId;
            myUser.ClientFullName = ClientFullName;
            myUser.ClientEmail = ClientEmail;
            myUser.ClientProfileImageUrl = ClientProfileImageUrl;
            myUser.UserType = UserType;
            return myUser;
        }

    }

    public class UserClient
    {
        public string ClientId { get; set; }
        public string ClientFullName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientProfileImageUrl { get; set; }
        public string UserType { get; set; }
    }

    //ResizeImage
    public static class ResizeImage
    {
        public static void ResizeStream(int imageSize, Stream filePath, string outputPath)
        {
            var image = Image.FromStream(filePath);
            int thumbnailSize = imageSize;
            int newWidth, newHeight;
            if (image.Width > image.Height)
            {
                newWidth = thumbnailSize;
                newHeight = image.Height * thumbnailSize / image.Width;
            }
            else
            {
                newWidth = image.Width * thumbnailSize / image.Height;
                newHeight = thumbnailSize;
            }
            var thumbnailBitmap = new Bitmap(newWidth, newHeight);
            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);
            thumbnailBitmap.Save(outputPath, image.RawFormat);
            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
        }
    }

    //Encrypt Or Decrypt Password
    public static class PasswordEncryptOrDecrypt
    {
        //this function Convert to Encord your Password 
        public static string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decode your Password 
        public static string DecodePassword(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }


    //Human Face Detection
    public static class FaceDetection
    {
        //static readonly  CascadeClassifier classifier = new CascadeClassifier("haarcascade_frontalface.xml");
       
        public static bool HumanFaceDetection(HttpPostedFileBase imageFileName)
        {
            try
            {
                //string rootDirectory = System.IO.Path.GetFullPath(@"..\");
                //string rootDirectory = HostingEnvironment.ApplicationPhysicalPath;
                //string lbpFacepath = "E:/Office/Dream Project/Project/WebWizardSolution/WebWizard/FaceData/lbpcascade_frontalface_improved.xml";
                //string modelPath = rootDirectory + "data/lbfmodel.yaml";

                 //CascadeClassifier classifier = new CascadeClassifier();
               // FacemarkLBFParams facemarkLBF = new FacemarkLBFParams();
               // FacemarkLBF facemark = new FacemarkLBF(facemarkLBF);
                 

               
                    try
                    {
                    string rootDirectory = HostingEnvironment.ApplicationPhysicalPath;
                    string rootFile = rootDirectory +"/FaceData/haarcascade_frontalface_default.xml";
                        CascadeClassifier classifier = new CascadeClassifier(rootFile);
                    var a = 1;
                       // var imgGray = img.Convert<Gray, byte>().Clone();
                        //Rectangle[] faces = classifier.DetectMultiScale(imgGray,1.1,4);

                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }     
                
                //Image<Bgr, Byte> img = new Image<Bgr, Byte>(imageFileName.FileName);
                //var img = new Bitmap(imageFileName.FileName).ToImage<Bgr, byte>();
                // var imgGray = img.Convert<Gray, byte>();
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
               // MessageBox.Show(ex.Message);
            }
            return true;
        }

    }


}