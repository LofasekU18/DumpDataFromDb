using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Collections.Concurrent;

//TODO: 
//create Enviroment file or config file for access to connectionString and query
//NOTE: local debug
const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\data\\db\\odposta.mdb;";
// const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=J:\\odposta.mdb;";
const string query = $"SELECT TOP 10 Číslo, Code, Adresát,[Kód úkonu] FROM [Kniha úkonov];";
string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\output.txt";

DataAccess dataAccess= new DataAccess(connectionString);
await dataAccess.GetListOfExeAsync();
foreach (var item in dataAccess.DataList)
{
    System.Console.WriteLine(item.NumberOfPossition+item.Address);
}
ResultSaver resultSaver= new ResultSaver();
await resultSaver.WriteListOfExeAsync("test",dataAccess.DataList);



