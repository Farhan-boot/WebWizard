using System;
using System.Collections.Generic;
using System.Text;
using Wizard.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Wizard.Model.WebWizard;
using System.Linq;
using Wizard.Data.Data;
using Wizard.Data.Data.Entities;

namespace WebWizard.Data.Repository
{
    public interface IWebWizardRegisteredRepository
    {
        IEnumerable<Wizard.Data.Data.Entities.TermsAndConditions> TermAndConditionList();
        IEnumerable<Wizard.Data.Data.Entities.Countries> GetCountryList();
        WebWizardRegisterModel Save(WebWizardRegisterModel obj);
        bool GetByEmail(WebWizardRegisterModel WebWizardRegister);
        Wizard.Data.Data.Entities.WebWizardRegistration GetWebWizardRegistrationDetailByWebWizardId(int webWizardId);
        Wizard.Data.Data.Entities.Countries GetWebWizardLocationDetailByStateId(int stateId);
        Wizard.Data.Data.Entities.WebWizardRegistration UpdateAdvanced(int webWizardId, Wizard.Data.Data.Entities.WebWizardRegistration advancedSettings);
        Wizard.Data.Data.Entities.WebWizardRegistration GetWebWizardByEmail(string email);
        Wizard.Data.Data.Entities.WebWizardRegistration GetRegisteredWebWizardByEmail(string email, string password);
    }

    public class WebWizardRegisteredRepository : IWebWizardRegisteredRepository
    {
        public WebWizardRegisteredRepository()
        {

        }
        private SqlConnection Conn;
        public IEnumerable<Wizard.Data.Data.Entities.Countries> GetCountryList()
        {
            List<Wizard.Data.Data.Entities.Countries> CountryList = new List<Wizard.Data.Data.Entities.Countries>();
            string ConnStr =
                ConfigurationManager.ConnectionStrings["WebWizardConnection"].ConnectionString;
            Conn = new SqlConnection(ConnStr);
            try
            {
                Conn.Open();
                string sqlquery = "select Id,Iso,Name,ISNULL(Iso3, '0'),ISNULL(NumCode, 0),PhoneCode from Countries";
                SqlCommand command = new SqlCommand(sqlquery, Conn);
                SqlDataReader sReader;
                sReader = command.ExecuteReader();
                while (sReader.Read())
                {
                    Wizard.Data.Data.Entities.Countries Country = new Wizard.Data.Data.Entities.Countries();
                    Country.Id = Convert.ToInt32(sReader["Id"].ToString());
                    Country.Iso = sReader["Iso"].ToString();
                    Country.Name = sReader["Name"].ToString();
                    //Country.Iso3 = sReader["Iso3"].ToString();
                    //Country.NumCode = Convert.ToInt32(sReader["NumCode"].ToString());
                    Country.PhoneCode = Convert.ToInt32(sReader["PhoneCode"].ToString());
                    CountryList.Add(Country);
                }
            }
            catch (SqlException se)
            {
            }
            finally
            {
                Conn.Close();
            }
            return CountryList;
        }

        public WebWizardRegisterModel Save(WebWizardRegisterModel obj)
        {
            var webWizardId = Wizard.Data.KeyGenerator.PrimaryKey.GetKey();

            using (WebWizardConnection db = new WebWizardConnection())
            {
                Wizard.Data.Data.Entities.WebWizardRegistration rg = new WebWizardRegistration();
                rg.CreateBy = obj.CreateBy;
                rg.CreateDate = obj.CreateDate;
                rg.Email = obj.Email;
                rg.FirstName = obj.FirstName;
                rg.LastName = obj.LastName;
                rg.NameTitle = obj.NameTitle;
                rg.NoOfEmployees = obj.NoOfEmployees;
                rg.Password = obj.Password;
                rg.StartAsCompany = obj.StartAsCompany;
                rg.StartAsFreelancer = obj.StartAsFreelancer;
                rg.StateId = obj.StateId;
                rg.Status = true;
                rg.TermsAndConditionsId = obj.TermsAndConditionsId;
                rg.UpdateDate = obj.UpdateDate;
                rg.VerificationCode = obj.VerificationCode;
                rg.WebWizardId = webWizardId.SetPrimaryKey;
                db.WebWizardRegistration.Add(rg);
                db.SaveChanges();
                if (rg.WebWizardId== webWizardId.SetPrimaryKey)
                {
                    Wizard.Data.Data.Entities.WebWizardDetails dtls = new Wizard.Data.Data.Entities.WebWizardDetails();
                    dtls.WebWizardId = rg.WebWizardId;
                    dtls.Status = true;
                    dtls.CreateDate = DateTime.Now;
                    dtls.UpdateDate = DateTime.UtcNow;
                    dtls.CreateBy = dtls.WebWizardId;
                    dtls.UpdateBy = dtls.WebWizardId;
                    dtls.LocationId = 1;
                    dtls.EducationId = 1;
                    dtls.DesignationId = 1;
                    db.WebWizardDetails.Add(dtls);
                    db.SaveChanges();
                }
                return obj;
            }

        }

        public IEnumerable<Wizard.Data.Data.Entities.TermsAndConditions> TermAndConditionList()
        {
            List<Wizard.Data.Data.Entities.TermsAndConditions> TermsAndConditionsList = new List<Wizard.Data.Data.Entities.TermsAndConditions>();
            string ConnStr =
                ConfigurationManager.ConnectionStrings["WebWizardConnection"].ConnectionString;
            Conn = new SqlConnection(ConnStr);
            try
            {
                Conn.Open();
                string sqlquery = "select * from TermsAndConditions";
                SqlCommand command = new SqlCommand(sqlquery, Conn);
                SqlDataReader sReader;
                sReader = command.ExecuteReader();
                while (sReader.Read())
                {
                    Wizard.Data.Data.Entities.TermsAndConditions TermAndCondition = new Wizard.Data.Data.Entities.TermsAndConditions();
                    TermAndCondition.Id = Convert.ToInt32(sReader["Id"].ToString());
                    TermAndCondition.TermsAndConditionsTitle = sReader["TermsAndConditionsTitle"].ToString();
                    TermAndCondition.TermsAndConditionsDescription = sReader["TermsAndConditionsDescription"].ToString();
                    TermsAndConditionsList.Add(TermAndCondition);
                }
            }
            catch (SqlException se)
            {
            }
            finally
            {
                Conn.Close();
            }
            return TermsAndConditionsList;
        }

        public bool GetByEmail(WebWizardRegisterModel WebWizardRegister)
        {
            string ConnStr =
               ConfigurationManager.ConnectionStrings["WebWizardConnection"].ConnectionString;
            Conn = new SqlConnection(ConnStr);
            try
            {
                Conn.Open();
                string sqlquery = "select * from WebWizardRegistration where Email=" + "'" + WebWizardRegister.Email + "'";
                SqlCommand command = new SqlCommand(sqlquery, Conn);
                SqlDataReader sReader;
                sReader = command.ExecuteReader();
                while (sReader.Read())
                {
                    string Email = sReader["Email"].ToString();
                    if (Email != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException se)
            {
            }
            finally
            {
                Conn.Close();
            }

            return false;
        }

        public Wizard.Data.Data.Entities.WebWizardRegistration GetWebWizardRegistrationDetailByWebWizardId(int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.WebWizardRegistration.SingleOrDefault(x => x.WebWizardId == webWizardId);
            }
        }

        public Wizard.Data.Data.Entities.Countries GetWebWizardLocationDetailByStateId(int stateId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var Countries = db.Countries.SingleOrDefault(x => x.Id == stateId);
                if (Countries.Iso3 == null || Countries.NumCode == null)
                {
                    Countries.Iso3 = null;
                    Countries.NumCode = 0;
                }
                return Countries;
            }
        }

        public Wizard.Data.Data.Entities.WebWizardRegistration UpdateAdvanced(int webWizardId, Wizard.Data.Data.Entities.WebWizardRegistration advancedSettings)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var webWizard = db.WebWizardRegistration.SingleOrDefault(x => x.WebWizardId == webWizardId);
                webWizard.FirstName = advancedSettings.FirstName;
                webWizard.LastName = advancedSettings.LastName;
                webWizard.Email = advancedSettings.Email;
                webWizard.Password = advancedSettings.Password;
                db.SaveChanges();
                return webWizard;
            }
        }

        public WebWizardRegistration GetWebWizardByEmail(string email)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var webWizard = db.WebWizardRegistration.SingleOrDefault(x => x.Email == email);
                return webWizard;
            }
        }

        public WebWizardRegistration GetRegisteredWebWizardByEmail(string email, string password)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var webWizard = db.WebWizardRegistration.SingleOrDefault(x => x.Email == email);
                webWizard.Password = password;
                db.SaveChanges();
                return webWizard;
            }
        }
    }
}
