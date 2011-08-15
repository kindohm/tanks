using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tanks.Web.Services
{
    public partial class SubmitScore : System.Web.UI.Page
    {

        DateTime GameDate 
        { 
            get { 
                string date = this.GetParam("gameDate");
                bool result;
                DateTime dateValue;
                result = DateTime.TryParse(date, out dateValue);
                if (!result)
                {
                    dateValue = DateTime.MinValue;
                }
                return dateValue;
            } 
        }

       
        string UserName { get { return this.GetParam("userName"); } }
        string UserUrl { get { return this.GetParam("userUrl"); } }
        string Location { get { return this.GetParam("location"); } }
        int Score { get { return this.GetIntParam("score"); } }
        int Shots { get { return this.GetIntParam("shots"); } }
        int Hits { get { return this.GetIntParam("hits"); } }
        int Kills { get { return this.GetIntParam("kills"); } }
        int HitsTaken { get { return this.GetIntParam("hitsTaken"); } }
        double ShotPercent { get { return this.GetDoubleParam("shotPercent"); } }
        int Flipped { get { return this.GetIntParam("flipped"); } }
        int ProjectilesHit { get { return this.GetIntParam("projectilesHit"); } }
        int RateOfFire { get { return this.GetIntParam("rateOfFire"); } }
        int Damage { get { return this.GetIntParam("damage"); } }
        int MaxHP { get { return this.GetIntParam("maxHP"); } }
        int Rounds { get { return this.GetIntParam("rounds"); } }
        int Powerups { get { return this.GetIntParam("powerupsHit"); } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ProcessRequest();
        }

        string GetParam(string key)
        {
            if (string.IsNullOrEmpty(this.Request[key]))
            {
                return string.Empty;
            }
            else
            {
                return this.Request[key];
            }
        }

        int GetIntParam(string key)
        {
            string paramVal = this.GetParam(key);
            bool result;
            int val;
            result = int.TryParse(paramVal, out val);
            if (!result)
            {
                val = int.MinValue;
            }
            return val;
        }

        double GetDoubleParam(string key)
        {
            string paramVal = this.GetParam(key);
            bool result;
            double val;
            result = double.TryParse(paramVal, out val);
            if (!result)
            {
                val = double.MinValue;
            }
            return val;
        }

        void ProcessRequest()
        {
            string responseText = "<response><result>{0}</result><message>{1}</message></response>";
            this.Response.Clear();
            string result = this.ValidateInputs();
            string output;

            if (string.IsNullOrEmpty(result))
            {
                string dataResult = this.InsertData();
                if (string.IsNullOrEmpty(dataResult))
                {
                    output = "Success";
                }
                else
                {
                    output = "Failure";
                    result = dataResult;
                }
            }
            else
            {
                output = "Failure";
            }

            responseText = string.Format(responseText, output, result);
            this.Response.ContentType = "text/xml";
            this.Response.Write(responseText);
            this.Response.End();
        }

        string ValidateInputs()
        {
            StringBuilder output = new StringBuilder();
            string format = "{0} was not provided.  ";
            if (this.GameDate == DateTime.MinValue)
            {
                output.Append("GameDate was not valid.  ");
            }

            if (string.IsNullOrEmpty(this.UserName))
            {
                output.AppendFormat(format, "UserName");
            }

            if (this.Score == int.MinValue)
            {
                output.Append("Score was not valid.  ");
            }

            return output.ToString();
        }

        string GetColumns(bool param)
        {
            string p = string.Empty;
            string comma = string.Empty;
            if (param)
            {
                p = "@";
            }

            StringBuilder sb = new StringBuilder();
            string format = "{0}{1}{2}";

            sb.AppendFormat(format, comma, p, "UserName");
            comma = ", ";
            sb.AppendFormat(format, comma, p, "GameDate");
            sb.AppendFormat(format, comma, p, "Score");
            sb.AppendFormat(format, comma, p, "Location");
            sb.AppendFormat(format, comma, p, "Url");
            sb.AppendFormat(format, comma, p, "Shots");
            sb.AppendFormat(format, comma, p, "Hits");
            sb.AppendFormat(format, comma, p, "HitsTaken");
            sb.AppendFormat(format, comma, p, "ShotPercent");
            sb.AppendFormat(format, comma, p, "Flipped");
            sb.AppendFormat(format, comma, p, "ProjectilesHit");
            sb.AppendFormat(format, comma, p, "RateOfFire");
            sb.AppendFormat(format, comma, p, "Damage");
            sb.AppendFormat(format, comma, p, "MaxHP");
            sb.AppendFormat(format, comma, p, "Rounds");
            sb.AppendFormat(format, comma, p, "Powerups");
            sb.AppendFormat(format, comma, p, "Kills");

            return sb.ToString();
        }

        string InsertData()
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tanks"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;

                        string cols = this.GetColumns(false);
                        string paramCols = this.GetColumns(true);

                        command.CommandText = "Insert into tank_Score (" + cols + ") " +
                            "values (" + paramCols + ")";

                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@UserName", this.UserName);
                        command.Parameters.AddWithValue("@GameDate", this.GameDate);
                        command.Parameters.AddWithValue("@Score", this.Score);
                        command.Parameters.AddWithValue("@Url", this.UserUrl);
                        command.Parameters.AddWithValue("@Location", this.Location);
                        command.Parameters.AddWithValue("@Shots", this.Shots);
                        command.Parameters.AddWithValue("@Hits", this.Hits);
                        command.Parameters.AddWithValue("@HitsTaken", this.HitsTaken);
                        command.Parameters.AddWithValue("@ShotPercent", this.ShotPercent);
                        command.Parameters.AddWithValue("@Flipped", this.Flipped);
                        command.Parameters.AddWithValue("@ProjectilesHit", this.ProjectilesHit);
                        command.Parameters.AddWithValue("@RateOfFire", this.RateOfFire);
                        command.Parameters.AddWithValue("@Damage", this.Damage);
                        command.Parameters.AddWithValue("@MaxHP", this.MaxHP);
                        command.Parameters.AddWithValue("@Rounds", this.Rounds);
                        command.Parameters.AddWithValue("@Powerups", this.Powerups);
                        command.Parameters.AddWithValue("@Kills", this.Kills);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }

                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

    }
}
