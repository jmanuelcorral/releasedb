using CommandDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace releasedb
{
    public class UpgradeArguments :IArgumentModel
    {
        [Option(shortName: 'c', longName:"ConnectionString", Description = "A valid Azure SQL ConnectionString")]
        public string ConnectionString { get; set; } = "";
        
        [Option(shortName: 't', longName: "AADTenantId", Description ="Azure Active Directory Tenant Id")]
        public string AADTenantId { get; set; } = "";
        [Option(Description ="Your SPN Client Id")]
        public string ClientId { get; set; } = "";
        [Option(Description ="Your SPN Client Secret Key")]
        public string ClientSecretKey { get; set; } = "";
        [Option(Description ="The path for sql dml and ddl scripts folder")]
        public string ScriptsFolder { get; set; } = "";

    }
}
