using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCore.NLayer.DataAccess;

internal static class ConnectionStrings
{
    public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "THURA",
        InitialCatalog = "ENPDotNetCore",//database name
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true,
    };

}
