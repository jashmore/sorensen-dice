using SorensenDice.Helper.Interfaces;
using SorensenDice.Helper.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace SorensenDice.Helper.Services
{
    public class DataService : IDataService
    {
        private static readonly string C_Conn = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        public List<MedicalTerminology> Search(string searchTerm)
        {            
            string fingerprint = string.Join(",", SorensenDiceHelper.ComputeFingerPrint(searchTerm));

            List<MedicalTerminology> results = new List<MedicalTerminology>();

            using (SqlConnection conn = new SqlConnection(C_Conn))
            {
                conn.Open();

                using (SqlCommand comm = new SqlCommand("GetMatchesBySorensenDice", conn))
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@IPInputFingerprint", System.Data.SqlDbType.NVarChar)
                    {
                        Direction = System.Data.ParameterDirection.Input,
                        Value = fingerprint
                    });

                    using (SqlDataReader reader = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while(reader.Read())
                        {
                            results.Add(new MedicalTerminology()
                            {
                                Code = reader["Code"].ToString(),
                                ShortDescription = reader["ShortDescription"].ToString(),
                                LongDescription = reader["LongDescription"].ToString()
                            });
                        }

                        reader.Close();
                    }
                }
            }

            return results;
        }
   }
}
