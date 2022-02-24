using CommandDotNet;
using DbUp;
using DbUp.Engine;
using DbUp.Helpers;

public class Program
{
    static int Main(string[] args)
    {
        return new AppRunner<Program>().Run(args);
    }

    [Command("Upgrade",
    Usage = "Upgrade <connectionstring> <ddl folder> [dml folder]",
    Description = "Execute scripts located in ddl folder and in ddl folder if exist",
    ExtendedHelpText = "more details and examples could be provided here")]
    public int Upgrade(string connectionString, string dmlfolder, string ddlfolder)
    {
        var result = UpgradeDb(connectionString, dmlfolder);
        if (result.Successful)
        {
            var ddlresult = UpgradeDb(connectionString, ddlfolder);
            return PrintResultInConsole(ddlresult);
        }
        else
        {
            return PrintResultInConsole(result);
        }
    }

    private int PrintResultInConsole(DatabaseUpgradeResult result)
    {
        if (result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error Processing DDL: {result.Error}");
            Console.ResetColor();
            return -1;
        }
        return 0;
    }

    private DatabaseUpgradeResult UpgradeDb(string connectionString, string scriptFolder)
    {
        var ddlupgrade =
           DeployChanges.To
               .SqlDatabase(connectionString)
               .WithScriptsFromFileSystem(scriptFolder)
               .LogToConsole()
               .JournalTo(new NullJournal())
               .Build();

        return ddlupgrade.PerformUpgrade();
    }

}
