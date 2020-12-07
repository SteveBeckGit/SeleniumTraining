using ExampleTraining.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTraining.Applications.Models
{
    public class DepositModel
    {
        public string Username { get; set; }
        public string AccountNumber { get; set; }
        public string DepositAmount { get; set; }

        public static List<DepositModel> GetFromDB() 
        {
            MSQLController controller = new MSQLController("someConnectiongString");
            DataTable table = controller.ExecuteQuery("SELECT ALL FROM CUSTOMERS");

            var t = table.AsEnumerable().Select(row => row);
            t = table.AsEnumerable().Where(row => row["Username"].ToString().Contains("Harry"));

            List<DepositModel> deposits = table.AsEnumerable().Select(row => new DepositModel()
            {
                Username = row["Username"].ToString(),
                AccountNumber = row["AccountNumber"].ToString(),
                DepositAmount = row["DepositAmount"].ToString()
            }).ToList();

            return deposits;
        }

        public static List<DepositModel> MockData()
        {


            List<DepositModel> deposits = new List<DepositModel>() {
                new DepositModel()
                {
                    Username= "Hermoine Granger",
                    AccountNumber = "1001",
                    DepositAmount = "404"
                },
                 new DepositModel()
                {
                    Username= "Harry Potter",
                    AccountNumber = "1004",
                    DepositAmount = "404"
                }
            };

            return deposits;
        }
    }
}
