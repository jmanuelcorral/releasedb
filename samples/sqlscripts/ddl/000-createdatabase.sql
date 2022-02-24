IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'mydb')
  BEGIN
    CREATE DATABASE mydb
    USE mydb
  END
