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
List<string> listOfTypeOfExData = ["-2120063737", "-2052727437", "-2030258641", "-1917318598", "-1847281444", "-1808742348", "-1796241568", "-1786773114", "-1748838384", "-1694504395", "-1668446977", "-1665351517", "-1517761113", "-1505536988", "-1483927677", "-1412285989", "-1344265705", "-1267306452", "-1232565484", "-1217505685", "-1210935807", "-1107823104", "-1106413256", "-1084376640", "-1071305888", "-1063233504", "-701744320", "-619113172", "-590050635", "-494699571", "-276545597", "-238707365", "-212811081", "-91742702", "-87103988", "-36279323", "-25053951", "22", "24", "26", "28", "30", "32", "34", "89", "95", "106414959", "134550253", "152214958", "228590420", "280548393", "457680707", "465487351", "585958650", "695949508", "711819427", "751146987", "791230221", "823153261", "894890524", "935444745", "1197348685", "1303495360", "1609235670", "1626423248", "1768096735", "1920555376", "1940675200", "2058691153", "-2095445506", "-1925324557", "1024815598"];

DataAccess dataAccess= new DataAccess(connectionString,query);
await dataAccess.GetListOfExeAsync();
foreach (var item in dataAccess.DataList)
{
    System.Console.WriteLine(item.NumberOfPossition+item.Address);
}



