![.NET Core](https://github.com/jmanuelcorral/releasedb/workflows/.NET%20Core/badge.svg) ![Nuget](https://img.shields.io/nuget/dt/releasedb) ![Nuget](https://img.shields.io/nuget/v/releasedb)

# releasedb

A sql script runner command line tool

## Give a Star! :star:

If you like or are using this utility, please give it a star. Thanks!

## Sample usage


```powershell
> releasedb "connectionstring" .\ddlFolder .\dmlFolder
```

With this tool you can update a sql database with a list of scripts. The tool at this moment doesn't use journaling, this causes a 


## How to install

At the moment, we have packaged the solution as a dotnet global tool. You only need dotnet core (2, 3 or NET5) installed in your machine and install as a global tool typing:

```powershell
>   dotnet tool install --global releasedb
```

If you don't want or have dotnet tooling, you also can install downloading a valid version from [releases](https://github.com/jmanuelcorral/releasedb/releases).