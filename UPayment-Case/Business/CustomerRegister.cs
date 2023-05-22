using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UPayment_Case.DataAccess;
using UPayment_Case.Models;

namespace UPayment_Case.Business
{
    public class CustomerRegister
    {
        new Database database = new Database();
        public CustomerCardDto Register(string name, string surname, string date, string identityNo, string status)
        {
            var query = "INSERT INTO [Customer]" +
                    "([Name]," +
                    "[Surname]" +
                    ",[BirthDate]" +
                    ",[IdentityNo]" +
                    ",[Status])" +
                    "VALUES" +
                    "(@name" +
                    ",@surname" +
                    ",@date" +
                    ",@identityNo" +
                    ",@status)";
            var res = database.Command(query);

            res.Parameters.AddWithValue("@name", name);
            res.Parameters.AddWithValue("@surname" , surname);
            res.Parameters.AddWithValue ("@date" , date);
            res.Parameters.AddWithValue("@identityNo", identityNo);
            res.Parameters.AddWithValue("@status", status);

            var result= res.ExecuteNonQuery();

            return new CustomerCardDto();

        }

    }
}