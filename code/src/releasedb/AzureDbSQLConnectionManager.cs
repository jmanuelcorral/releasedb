using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.Support;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace releasedb
{
    public class AzureDbSQLConnectionManager : DatabaseConnectionManager
    {

        /// <summary>
        /// Manages Sql Database Connections
        /// </summary>
        /// <param name="connectionString"></param>
        public AzureDbSQLConnectionManager(string connectionstring, string aadTenantId, string clientId, string clientSecretKey) : base(new DelegateConnectionFactory((log, dbManager) =>
             {
                 var conn = new SqlConnection(connectionstring);
                 string AadInstance = "https://login.windows.net/{0}";
                 string ResourceId = "https://database.windows.net/";
                 AuthenticationContext authenticationContext = new AuthenticationContext(string.Format(AadInstance, aadTenantId));
                 ClientCredential clientCredential = new ClientCredential(clientId, clientSecretKey);
                 AuthenticationResult authenticationResult = authenticationContext.AcquireTokenAsync(ResourceId, clientCredential).ConfigureAwait(false).GetAwaiter().GetResult(); ;
                 conn.AccessToken = authenticationResult.AccessToken;

                 if (dbManager.IsScriptOutputLogged)
                     conn.InfoMessage += (sender, e) => log.WriteInformation($"{{0}}", e.Message);

                 return conn;
             }))
        { }
        
        public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents)
        {
            var commandSplitter = new SqlCommandSplitter();
            var scriptStatements = commandSplitter.SplitScriptIntoCommands(scriptContents);
            return scriptStatements;
        }

     
    }
}
