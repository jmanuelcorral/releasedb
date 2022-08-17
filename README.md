![.NET Core](https://github.com/jmanuelcorral/releasedb/workflows/.NET%20Core/badge.svg) ![Nuget](https://img.shields.io/nuget/dt/releasedb) ![Nuget](https://img.shields.io/nuget/v/releasedb)

# releasedb

A sql script runner command line tool

## Give a Star! :star:

If you like or are using this utility, please give it a star. Thanks!

## Breaking Changes

From version 2 and future releases we will use journaling on database updates, the scripts now doesn't need to be idempotent, and we will not separate ddl from dml

## Sample usage

if is the case that you are using an SQL Database with a "standard" connectionstring
```powershell
> releasedb Upgrade -c "connectionstring" --ScriptsFolder .\yourScriptsFolder
```

Or maybe you need to connect your database with an SPN created in your Azure Active Directory:
```powershell
> releasedb Upgrade -c "Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=yourdatabase;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;Connection Timeout=30;" 
-t "Your tenantId"
--ClientId "Your SPN ClientID" 
--ClientSecretKey "Your SPN Client Secret" 
--ScriptsFolder .\yourScriptsFolder
```

With this tool you can update a sql database with a list of scripts. The tool launch these scripts in order.


## How to install

At the moment, we have packaged the solution as a dotnet global tool. You only need dotnet core (2, 3 or NET5) installed in your machine and install as a global tool typing:

```powershell
>   dotnet tool install --global releasedb
```

If you don't want or have dotnet tooling, you also can install downloading a valid version from [releases](https://github.com/jmanuelcorral/releasedb/releases).
