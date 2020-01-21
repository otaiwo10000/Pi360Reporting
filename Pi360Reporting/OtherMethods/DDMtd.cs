using MPR.Report.Data.Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Pi360Reporting.OtherMethods
{
    public class DDMtd
    {
        MPRContext2 entityContext = new MPRContext2();
        string connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        public List<string> OtherInfo(int yr)
        {
            var objList = new List<string>();
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {                
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select distinct MainCaption from OtherInformationBSINMix where year = @YEAR union select distinct MainCaption from OtherInformationBSINMixTotalLines where year = @YEAR";
                //cmd.CommandText = "select distinct MainCaption from OtherInformationBSINMix union select distinct MainCaption from OtherInformationBSINMixTotalLines";
                cmd.Parameters.AddWithValue("@YEAR", yr);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                string res;

                while (reader.Read())
                {
                    //obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;
                    res  = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "";

                    objList.Add(res);
                }
                con.Close();
            }
            return objList;
        }

    }
}