using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Data.Repository
{
    public interface IWebWizardLogInRepository
    {
        WebWizardRegisterModel GetLogIn(LogInModel logIn);
    }

    public class WebWizardLogInRepository : IWebWizardLogInRepository
    {
        private SqlConnection Conn;
        public WebWizardRegisterModel GetLogIn(LogInModel logIn)
        {
            WebWizardRegisterModel webWizardRegister = new WebWizardRegisterModel();
            string ConnStr =
               ConfigurationManager.ConnectionStrings["WebWizardConnection"].ConnectionString;
            Conn = new SqlConnection(ConnStr);
            try
            {
                Conn.Open();
                string sqlquery = "select * from WebWizardRegistration where Email=" + "'" + logIn.Email + "'"+ " and Password=" + "'"+ logIn.Password+ "'";
                SqlCommand command = new SqlCommand(sqlquery, Conn);
                SqlDataReader sReader;
                sReader = command.ExecuteReader();
                while (sReader.Read())
                {
                    //check Status
                    bool Status = Convert.ToBoolean(sReader["Status"].ToString());

                    if (Status!=true)
                    {
                        webWizardRegister.Status = false;
                        return webWizardRegister;
                    }
                    else
                    {
                        //insert object
                        webWizardRegister.WebWizardId = Convert.ToInt32(sReader["WebWizardId"].ToString());
                        webWizardRegister.FirstName = sReader["FirstName"].ToString();
                        webWizardRegister.LastName = sReader["LastName"].ToString();
                        webWizardRegister.Email = sReader["Email"].ToString();
                        webWizardRegister.NameTitle = sReader["NameTitle"].ToString();
                        webWizardRegister.StateId = Convert.ToInt32(sReader["StateId"].ToString());
                        webWizardRegister.Password = sReader["Password"].ToString();
                        webWizardRegister.StartAsCompany = Convert.ToBoolean(sReader["StartAsCompany"].ToString());
                        webWizardRegister.NoOfEmployees = Convert.ToInt32(sReader["NoOfEmployees"].ToString());
                        webWizardRegister.StartAsFreelancer = Convert.ToBoolean(sReader["StartAsFreelancer"].ToString());
                        webWizardRegister.TermsAndConditionsId = Convert.ToInt32(sReader["TermsAndConditionsId"].ToString());
                        webWizardRegister.VerificationCode = sReader["VerificationCode"].ToString();
                        webWizardRegister.Status = Convert.ToBoolean(sReader["Status"].ToString());
                        //webWizardRegister.CreateDate = (System.DateTime)(sReader["CreateDate"]);
                        //webWizardRegister.UpdateDate = (System.DateTime)(sReader["UpdateDate"]);
                        //webWizardRegister.CreateBy = Convert.ToInt32(sReader["CreateBy"].ToString());
                        //webWizardRegister.UpdateBy = Convert.ToInt32(sReader["UpdateBy"].ToString());

                        return webWizardRegister;
                    }
              
                }
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                Conn.Close();
            }

            return webWizardRegister;
        }
    }
}
