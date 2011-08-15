using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Tanks.Web.Services
{
    public partial class TopScoreData : System.Web.UI.Page
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            System.Threading.Thread.Sleep(1000);
            this.Response.Clear();
            this.Response.ContentType = "text/xml";
            this.LoadData();
            this.Response.End();
        }

        string GetSortColumn()
        {
            string clause = " ORDER BY ";
            string key = "sort";
            if (this.Request[key] == null)
            {
                clause += " SCORE DESC ";
            }
            else
            {
                string column = this.Request[key];
                switch (column.ToLower())
                {
                    case "score":
                        clause += " SCORE DESC ";
                        break;
                    case "shotpercent":
                        clause += " ShotPercent DESC ";
                        break;
                    case "kills":
                        clause += " Kills DESC ";
                        break;
                    case "hitstaken":
                        clause += " HitsTaken ASC ";
                        break;
                    case "rounds":
                        clause += " rounds DESC ";
                        break;
                }
            }
            return clause;
        }

        string GetCount()
        {
            string key = "count";
            if (this.Request[key] == null)
            {
                return "25";
            }
            else
            {
                return this.Request[key];
            }

        }

        void LoadData()
        {
            this.Response.Write("<topScores>");
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["tanks"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        string count = this.GetCount();
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "Select top " + count + " * from tank_Score " + this.GetSortColumn();
                        command.CommandType = System.Data.CommandType.Text;
                        IDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            this.Response.Write("<score ");
                            this.Response.Write("userName=\"" + (string)reader["UserName"] + "\" ");

                            if (reader["Location"] == DBNull.Value)
                            {
                                this.Response.Write("location=\"\" ");
                            }
                            else
                            {
                                this.Response.Write("location=\"" + (string)reader["Location"] + "\" ");
                            }

                            if (reader["Url"] == DBNull.Value)
                            {
                                this.Response.Write("url=\"\" ");
                            }
                            else
                            {
                                this.Response.Write("url=\"" + (string)reader["Url"] + "\" ");
                            }

                            this.Response.Write("gameScore=\"" + ((int)reader["Score"]).ToString("#,###,##0") + "\" ");
                            this.Response.Write("kills=\"" + ((int)reader["Kills"]).ToString("#,##0") + "\" ");
                            this.Response.Write("rounds=\"" + ((int)reader["Rounds"]).ToString("#,##0") + "\" ");
                            this.Response.Write("shotPercent=\"" + ((double)reader["ShotPercent"]).ToString("##.0%") + "\" ");
                            this.Response.Write("hitsTaken=\"" + ((int)reader["HitsTaken"]).ToString("#,##0") + "\" ");
                            this.Response.Write("gameDate=\"" + ((DateTime)reader["GameDate"]).ToString() + "\" />");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.Response.Write("<error>" + ex.Message + "</error>");
            }
            this.Response.Write("</topScores>");
                
        }

        


    }
}
