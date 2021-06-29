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
    public interface IClientLogInRepository
    {
        ClientRegisterModel GetLogIn(LogInModel logIn);
    }

    public class ClientLogInRepository : IClientLogInRepository
    {
        private SqlConnection Conn;
        public ClientRegisterModel GetLogIn(LogInModel logIn)
        {
            ClientRegisterModel clientRegister = new ClientRegisterModel();
            string ConnStr =
               ConfigurationManager.ConnectionStrings["WebWizardConnection"].ConnectionString;
            Conn = new SqlConnection(ConnStr);
            try
            {
                Conn.Open();
                string sqlquery = "select * from ClientRegistration where Email=" + "'" + logIn.Email + "'"+ " and Password=" + "'"+ logIn.Password+ "'";
                SqlCommand command = new SqlCommand(sqlquery, Conn);
                SqlDataReader sReader;
                sReader = command.ExecuteReader();
                while (sReader.Read())
                {
                    //check Status
                    bool Status = Convert.ToBoolean(sReader["Status"].ToString());

                    if (Status!=true)
                    {
                        clientRegister.Status = false;
                        return clientRegister;
                    }
                    else
                    {
                        //insert object
                        clientRegister.ClientId = Convert.ToInt32(sReader["ClientId"].ToString());
                        clientRegister.FirstName = sReader["FirstName"].ToString();
                        clientRegister.LastName = sReader["LastName"].ToString();
                        clientRegister.Email = sReader["Email"].ToString();
                        clientRegister.NameTitle = sReader["NameTitle"].ToString();
                        clientRegister.StateId = Convert.ToInt32(sReader["StateId"].ToString());
                        clientRegister.Password = sReader["Password"].ToString();
                        clientRegister.TermsAndConditionsId = Convert.ToInt32(sReader["TermsAndConditionsId"].ToString());
                        clientRegister.VerificationCode = sReader["VerificationCode"].ToString();
                        clientRegister.Status = Convert.ToBoolean(sReader["Status"].ToString());
                        //webWizardRegister.CreateDate = (System.DateTime)(sReader["CreateDate"]);
                        //webWizardRegister.UpdateDate = (System.DateTime)(sReader["UpdateDate"]);
                        //webWizardRegister.CreateBy = Convert.ToInt32(sReader["CreateBy"].ToString());
                        //webWizardRegister.UpdateBy = Convert.ToInt32(sReader["UpdateBy"].ToString());

                        return clientRegister;
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

            return clientRegister;
        }
    }
}
