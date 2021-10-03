using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using KFC.App_Code;
namespace commonapp.App_Code
{
    public class common
    {
        public common()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int add(String Name, String Age, String NidIqama, int radiobtn, int Selectddl)
        {
                       
                    CRUD myCrud = new CRUD();
                    string mySql = @"INSERT patient_info(p_name,p_age,p_nationalid_iqama,p_gender_id,p_dep_id)
                         VALUES(@name,@age,@nationalid,@radioid,@ddlDep)";
                    Dictionary<string, object> myPara = new Dictionary<string, object>();
                    myPara.Add("@name", Name);
                    myPara.Add("@age", Age);
                    myPara.Add("@nationalid", NidIqama);
                    myPara.Add("@radioid", radiobtn);
                    myPara.Add("@ddlDep", Selectddl);

                    int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                    return rtn;
              
        }       // end function 

        public String sucsessmessage(int rtn)
        {
            if (rtn >= 1)
            { return "Success"; }
            else
            { return "Failed"; }

        }
        public int checkredundcy(String txtbox)
        {  CRUD mycrud= new CRUD();
            String checkid = @"select p_nationalid_iqama from patient_info";
            SqlDataReader dr = mycrud.getDrPassSql(checkid);
            while (dr.Read())
            {
                if (String.Equals(txtbox, dr["p_nationalid_iqama"].ToString()))
                { return 1; }
            }
            return 0;

        }

        public String checkfield(String Name, String Age, String NidIqama, int radiobtn, int Selectddl)
        {

            String valditetext = "";
            if (String.IsNullOrEmpty(Name))
            {
                return valditetext = "Please enter Name ...!";

            }
            else if (String.IsNullOrEmpty(Age))
            {
                return valditetext = "Please enter Age    ...!";

            }
            else if (String.IsNullOrEmpty(NidIqama))
            {
                return valditetext = "Please enter National id / iqama  ...!";

            }
            else if (radiobtn == -1)
            {
                return valditetext = "Select Gender  ...!";

            }
            else if (Selectddl == 0)
            {
                return valditetext = "Select Department   ...!";
            }
            else { return valditetext = ""; }


        }

        public String selectbytype(int sqlselector)
        {
            string mySql = "";
            switch (sqlselector)
            {
                case 1:
                    mySql = @"SELECT p.p_id, p.p_name,p.p_nationalid_iqama,p.p_age,g.p_gender,d.p_dep
                   FROM patient_info p
                    INNER JOIN department1 d ON p.p_dep_id = d.p_dep_id 
                    INNER JOIN gendertb g ON g.p_gender_id = p.p_gender_id
                    where p.p_name = @p_name";
                    break;
                case 2:
                    mySql = @"SELECT p.p_id, p.p_name,p.p_nationalid_iqama,p.p_age,g.p_gender,d.p_dep
                   FROM patient_info p
                    INNER JOIN department1 d ON p.p_dep_id = d.p_dep_id 
                    INNER JOIN gendertb g ON g.p_gender_id = p.p_gender_id
                    where p.p_age = @p_age";
                    break;
                case 3:
                    mySql = @"SELECT p.p_id, p.p_name,p.p_nationalid_iqama,p.p_age,g.p_gender,d.p_dep
                   FROM patient_info p
                    INNER JOIN department1 d ON p.p_dep_id = d.p_dep_id 
                    INNER JOIN gendertb g ON g.p_gender_id = p.p_gender_id
                    where p.p_nationalid_iqama = @p_nationalid";
                    break;
                case 4:
                    mySql = @"SELECT p.p_id, p.p_name,p.p_nationalid_iqama,p.p_age,g.p_gender,d.p_dep
                   FROM patient_info p
                    INNER JOIN department1 d ON p.p_dep_id = d.p_dep_id 
                    INNER JOIN gendertb g ON g.p_gender_id = p.p_gender_id
                    where p.p_gender_id = @p_gender_id";
                    break;
                case 5:
                    mySql = @"SELECT p.p_id, p.p_name,p.p_nationalid_iqama,p.p_age,g.p_gender,d.p_dep
                   FROM patient_info p
                    INNER JOIN department1 d ON p.p_dep_id = d.p_dep_id 
                    INNER JOIN gendertb g ON g.p_gender_id = p.p_gender_id
                    where p.p_dep_id = @p_dep_id";
                    break;

                default:
                    mySql = "";
                    break;
            }
            return mySql;

        }
        //verify that email in correct format 
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }




    }

}