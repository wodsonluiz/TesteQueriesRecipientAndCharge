using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TesteQueriesRecipeintAndCharge
{
    internal class Repository
    {
        const string conn = "Data Source=localhost,1433;Initial Catalog=SUBSCRIBER-SQL;User ID=sa;Password=Giventofly#10;Trusted_Connection=False";
        public void QueryWithADO(string[] pgid, int accountId, string createdAt)
        {
            var chargesRecipients = new List<ChargeRecipient>();
            var query = new ChargeAndRecipientQueriesBuilder().GetChargeRecipientWithSplitQuery(pgid);

            using (var connection = new SqlConnection(conn))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    for (int i = 0; i < pgid.Length; i++)
                    {
                        string paramName = "@pgid" + i;
                        cmd.Parameters.AddWithValue(paramName, pgid[i]);
                    }

                    cmd.Parameters.AddWithValue("@AccountId", accountId);
                    cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chargesRecipients.Add(
                                new ChargeRecipient()
                                {
                                    ChargeId = reader["ChargeId"] as string,
                                    RecipientId = reader["RecipientId"] as string,
                                    PgId = reader["PgId"] as string
                                });
                        }
                    }
                }
            }
        }
        public void QueryWithADOAndWithoutParameters(string[] pgid, int accountId, string createdAt)
        {
            var chargesRecipients = new List<ChargeRecipient>();
            var query = new ChargeAndRecipientQueriesBuilder().GetChargeRecipientWithSplitQueryWithoutParameters(pgid);

            using (var connection = new SqlConnection(conn))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AccountId", accountId);
                    cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chargesRecipients.Add(
                                new ChargeRecipient()
                                {
                                    ChargeId = reader["ChargeId"] as string,
                                    RecipientId = reader["RecipientId"] as string,
                                    PgId = reader["PgId"] as string
                                });
                        }
                    }
                }
            }
        }
        public void QueryWithDapper(string[] pgid, int accountId, string createdAt)
        {
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                var parametros = new DynamicParameters();

                var query = new ChargeAndRecipientQueriesBuilder().GetChargeRecipientWithSplitQueryUsingDapper(pgid);

                for (int i = 0; i < pgid.Length; i++)
                {
                    string paramName = "@pgid" + i;
                    parametros.Add(paramName, pgid[i]);
                }

                parametros.Add("@AccountId", accountId);
                parametros.Add("@CreatedAt", createdAt);

                // Executando a consulta com Dapper
                var chargesRecipients = connection.Query<ChargeRecipient>(query, parametros);
            }
        }
        public void QueryWithDapperWithoutParameters(string[] pgid, int accountId, string createdAt)
        {
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                var parametros = new DynamicParameters();

                var query = new ChargeAndRecipientQueriesBuilder().GetChargeRecipientWithSplitQueryUsingDapperWithoutParameters(pgid);

                parametros.Add("@AccountId", accountId);
                parametros.Add("@CreatedAt", createdAt);

                // Executando a consulta com Dapper
                var chargesRecipients = connection.Query<ChargeRecipient>(query, parametros);
            }
        }
        public void QueryWithADOOpenJson(string[] pgid, int accountId, string createdAt)
        {
            var chargesRecipients = new List<ChargeRecipient>();
            var query = new ChargeAndRecipientQueriesBuilder().GetChargeRecipientWithOpenJson(pgid);

            using (var connection = new SqlConnection(conn))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AccountId", accountId);
                    cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chargesRecipients.Add(
                                new ChargeRecipient()
                                {
                                    ChargeId = reader["ChargeId"] as string,
                                    RecipientId = reader["RecipientId"] as string,
                                    PgId = reader["PgId"] as string
                                });
                        }
                    }
                }
            }
        }
        public void QueryWithDapperAndOpenJson(string[] pgid, int accountId, string createdAt)
        {
            using (var connection = new SqlConnection(conn))
            {
                string ids = JsonConvert.SerializeObject(pgid);
    
                connection.Open();
                var parametros = new DynamicParameters();

                var query = new ChargeAndRecipientQueriesBuilder().GetQueryWithOpenJson();

                parametros.Add("@Ids", ids);
                parametros.Add("@AccountId", accountId);
                parametros.Add("@CreatedAt", createdAt);

                // Executando a consulta com Dapper
                var chargesRecipients = connection.Query<ChargeRecipient>(query, parametros);
            }
        }

        public void QueryWithDapperAndOpenJson2(string[] pgid, int accountId)
        {
            using (var connection = new SqlConnection(conn))
            {
                string ids = JsonConvert.SerializeObject(pgid);

                connection.Open();
                var parametros = new DynamicParameters();

                var query = new ChargeAndRecipientQueriesBuilder().GetQueryWithOpenJson2();

                parametros.Add("@Ids", ids);
                parametros.Add("@AccountId", accountId);

                // Executando a consulta com Dapper
                var chargesRecipients = connection.Query<RecipientDefault>(query, parametros);
            }
        }
    }

    public class RecipientDefault
    {
        public string RecipientId { get; set; }
        public string PgId { get; set; }
    }
}
