using CommandDotNet;
using DbUp;
using DbUp.Engine;
using DbUp.Engine.Transactions;
using DbUp.SqlServer;
using releasedb;
using Spectre.Console;

public class Program
{
    static int Main(string[] args)
    {
        AnsiConsole.Write(new FigletText("ReleaseDB")
        .LeftAligned()
        .Color(Color.Red));
        
        return new AppRunner<Program>().Run(args);
    }

    [Command("Upgrade",
    Usage = "Upgrade -c <Connectionstring> -t <AADTenantId> --ClientId <ClientId> --ClientSecretKey <clientSecretKey> --ScriptsFolder <ScriptsFolder>",
    Description = "Conects to an Azure SQL Database and execute scripts located in the scripts folder.")]
    public int Upgrade(UpgradeArguments arguments)
    {
        IConnectionManager connectionManager;
        if (isAzureSQLConnection(arguments))
            connectionManager = new AzureDbSQLConnectionManager(arguments.ConnectionString, arguments.AADTenantId, arguments.ClientId, arguments.ClientSecretKey);
        else
            connectionManager = new SqlConnectionManager(arguments.ConnectionString);
        return PrintResultInConsole(UpgradeDb(connectionManager, arguments.ScriptsFolder));
    }

    private bool isAzureSQLConnection(UpgradeArguments arguments) => !String.IsNullOrEmpty(arguments.AADTenantId);


    private int PrintResultInConsole(DatabaseUpgradeResult result)
    {
        if (!result.Successful)
        {
            AnsiConsole.MarkupLine($"{Emoji.Known.CrossMark} [red] Error Executing Scripts: {result.Error}[/]");
            return -1;
        }
        AnsiConsole.MarkupLine($"{Emoji.Known.Rocket} [green] Success![/]");
        return 0;
        
    }

    private DatabaseUpgradeResult UpgradeDb(IConnectionManager connection, string scriptFolder)
    {
        
        var ddlupgrade =
           DeployChanges.To
               .SqlDatabase(connection)
               .WithScriptsFromFileSystem(scriptFolder)
               .LogToConsole()
               .Build();

        return ddlupgrade.PerformUpgrade();
    }

}
