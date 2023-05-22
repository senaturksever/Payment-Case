﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace UPayment_Case.DataAccess
{
    public class Database
    {

        SqlConnection connection = new SqlConnection("");


        public SqlCommand Command(string query, string con ="")
        {
            var cmd = new SqlCommand(query, connection);

            connection.Open();


            return cmd;
        }
        

        public DataTable AccountInfo()
        {
            DataTable data = new DataTable();
            var query = "SELECT [Id]" +
                 ",[MemberId]" +
                 ",[MerchantId]" +
                 ",[ApiKey]" +
                 ",[UserCode]" +
                 " FROM [InfoAccount] WHERE [MerchantId] = '1894'";
            var res = Command(query);
            
            var adapter = new SqlDataAdapter(res);
            adapter.Fill(data);
            connection.Close();
            return data;
        }

        public void PaymentInsertData(string customerId,string orderId,string merchantId,string totalAmount,string cardNumber,string status)
        {
            var query = "INSERT INTO [PaymentInfo]" +
                "([CustomerId]" +
                ",[OrderId]" +
                ",[MerchantId]" +
                ",[TotalAmount]" +
                ",[CardNumber]" +
                ",[Status])" +
                "VALUES" +
                "(@customerId" +
                ",@orderId" +
                ",@merchantId" +
                ",@totalAmount" +
                ",@cardNumber" +
                ",@status)";
            var res = Command(query);

            res.Parameters.AddWithValue("@customerId", customerId);
            res.Parameters.AddWithValue("@orderId", orderId);
            res.Parameters.AddWithValue("@merchantId", merchantId);
            res.Parameters.AddWithValue("@totalAmount", totalAmount);
            res.Parameters.AddWithValue("@cardNumber", cardNumber);
            res.Parameters.AddWithValue("@status", status);
           
            res.ExecuteNonQuery();
            connection.Close();

        }
    }
}