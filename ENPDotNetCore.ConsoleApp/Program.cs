﻿// See https://aka.ms/new-console-template for more information
using ENPDotNetCore.ConsoleApp.EFCoreExamples;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

/*SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();

stringBuilder.DataSource = "THURA";
stringBuilder.InitialCatalog = "ENPDotNetCore";//database name
stringBuilder.UserID = "sa";
stringBuilder.Password = "sasa@123";
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);



connection.Open();
Console.WriteLine("Connection open");
string query = "select * from Tbl_Blog;";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);//new query (cmd)
DataTable dt = new DataTable();//create table in sql
adapter.Fill(dt);// when click execute, result comes into dt
connection.Close();
Console.WriteLine("Connection close");

//dataset => datatable
//datatable=>talblerow
//tablerow=>tablecolumn
foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine("BLog Id =>" + dr["BlogId"]);
    Console.WriteLine("BLog Tilte =>" + dr["BlogTitle"]);
    Console.WriteLine("BLog Author =>" + dr["BlogAuthor"]);
    Console.WriteLine("BLog Content =>" + dr["BlogContent"]);
    Console.WriteLine("------------------------------");
}*/
//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();  
//adoDotNetExample.Create("title11", "author11", "content11");
//adoDotNetExample.Update(11, "test", "test", "test");
//adoDotNetExample.Delete(13);
//adoDotNetExample.Edit(20);

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();
EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();

Console.ReadKey();
