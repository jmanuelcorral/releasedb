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
    Usage = "Upgrade <connectionstring> <scripts folder>",
    Description = "Execute scripts located in the scripts folder.",
    ExtendedHelpText = "more details and examples could be provided here")]
    public int Upgrade(string connectionString, string scriptFolder, string ddlfolder)
    {
        return PrintResultInConsole(UpgradeDb(connectionString, scriptFolder));
    }

    private int PrintResultInConsole(DatabaseUpgradeResult result)
    {
        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error Executing Scripts: {result.Error}");
            Console.ResetColor();
            return -1;
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🚀 Success!");
        Console.ResetColor();
        return 0;
        
    }

    private DatabaseUpgradeResult UpgradeDb(string connectionString, string scriptFolder)
    {
        EnsureDatabase.For.SqlDatabase(connectionString);
        var ddlupgrade =
           DeployChanges.To
               .SqlDatabase(connectionString)
               .WithScriptsFromFileSystem(scriptFolder)
               .LogToConsole()
               .Build();

        return ddlupgrade.PerformUpgrade();
    }

}
