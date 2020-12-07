using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTraining.Databases
{
    public class MSQLController
    {
        private string _connectionString { get; set; }
        private SqlConnection sqlConnection { get; set; }

        public MSQLController(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public void OpenConnection() 
        {
            sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
        }

        public void ClsoeConnection() 
        {
            sqlConnection.Close();
        }

        public DataTable ExecuteQueryMaintainConnection(string query) 
        {
            DataTable resultsTable = new DataTable();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = query;

            SqlDataReader reader = command.ExecuteReader();
            resultsTable.Load(reader);

            return resultsTable;
        }

        //Query
        public DataTable ExecuteQuery(string query) 
        {
            DataTable resultsTable = new DataTable();
            using (var conn = new SqlConnection(_connectionString)) 
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();
                resultsTable.Load(reader);
            }

            return resultsTable;

        }

        //Statement

        public int ExecuteStatement(string statement)
        {
            int affected = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = statement;

                affected = command.ExecuteNonQuery();
            }

            return affected;

        }

        //StoredProcs

        public int ExecuteProcedure(string procedureName)
        {
            int affected = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(procedureName, conn);
                command.CommandType = CommandType.StoredProcedure;

               affected = command.ExecuteNonQuery();
            }

            return affected;

        }

        //Scalar
    }
}
