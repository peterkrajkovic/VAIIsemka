using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ServerSide.Database;
using static System.Reflection.Metadata.BlobBuilder;
using System.Data;
using System.IO;
using System.Reflection.Metadata;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Microsoft.Extensions.Options;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace ServerSide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {

            Configuration.Default.ApiKey.Add("api-key", "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");

            var apiInstance = new AccountApi();

            try
            {
                // Get your account information, plan and credits details
                GetAccount result = apiInstance.GetAccount();
            }
            catch (Exception e)
            {
            }

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

               
            }

            return "swsww";
            }
        }
}
