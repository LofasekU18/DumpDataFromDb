using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Collections.Concurrent;

//TODO: 
//create Enviroment file or config file for access to connectionString and query
//NOTE: local debug
//const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\data\\odposta.mdb;";
const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=J:\\odposta.mdb;";
const string query = $"SELECT Číslo, Code, Adresát,[Kód úkonu] FROM [Kniha úkonov];";
string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\output.txt";

//TODO: 
//change to dictonary and add name of Ukonu
List<string> listOfKodUkonu = ["-2120063737", "-2052727437", "-2030258641", "-1917318598", "-1847281444", "-1808742348", "-1796241568", "-1786773114", "-1748838384", "-1694504395", "-1668446977", "-1665351517", "-1517761113", "-1505536988", "-1483927677", "-1412285989", "-1344265705", "-1267306452", "-1232565484", "-1217505685", "-1210935807", "-1107823104", "-1106413256", "-1084376640", "-1071305888", "-1063233504", "-701744320", "-619113172", "-590050635", "-494699571", "-276545597", "-238707365", "-212811081", "-91742702", "-87103988", "-36279323", "-25053951", "22", "24", "26", "28", "30", "32", "34", "89", "95", "106414959", "134550253", "152214958", "228590420", "280548393", "457680707", "465487351", "585958650", "695949508", "711819427", "751146987", "791230221", "823153261", "894890524", "935444745", "1197348685", "1303495360", "1609235670", "1626423248", "1768096735", "1920555376", "1940675200", "2058691153", "-2095445506", "-1925324557", "1024815598"];
Stopwatch sw = new Stopwatch();
IEnumerable<ExekucniPrikazy> selectedDataList = new List<ExekucniPrikazy>();
try
{
    sw.Start();
    System.Console.WriteLine("Načítám úkony z DB");
    List<ExekucniPrikazy> listData = await GetListOfExeAsync(connectionString, query);
    selectedDataList = listData.Where(zaznam => listOfKodUkonu.Contains(zaznam.KodUkonu));
    sw.Stop();
    Console.WriteLine($"Načtení trvalo: {sw.Elapsed}");
    sw.Restart();
}
catch (Exception ex)
{
    Console.WriteLine("SQL problem: " + ex.Message);
}
while (true)
{
    Console.WriteLine("Zadej exko, 'k' pro ukonceni");
    string exko = Console.ReadLine();
    if (exko == "k")
    {
        break;
    }
    else
    {
        try
        {
            sw.Start();
            var selectedDataListExko = selectedDataList.Where(ex => ex.Exko == exko).OrderBy(ex => ex.Cislo);
            await WriteListOfExeAsync(filePath, exko, selectedDataListExko);
            sw.Stop();
            Console.WriteLine($"Vyhledat to zabralo: {sw.Elapsed}");
            Console.WriteLine("Data byla připsána do souboru.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Problém se souborem, tento problém:" + ex.Message);
        }
    }
    await StartAndWaitForProcessAsync("notepad.exe", filePath);
    System.Console.WriteLine("Hotovo");
    Thread.Sleep(1000);
    System.Console.Clear();
}

async Task<List<ExekucniPrikazy>> GetListOfExeAsync(string connectionString, string query)
{
    List<ExekucniPrikazy> dataList = new List<ExekucniPrikazy>();
    using (OleDbConnection connection = new OleDbConnection(connectionString))
    {
        OleDbCommand command = new OleDbCommand(query, connection);
        await connection.OpenAsync();
        OleDbDataReader reader = (OleDbDataReader)await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            ExekucniPrikazy data = new ExekucniPrikazy
            {
                Exko = reader.GetValue(0).ToString(),
                Cislo = reader.GetValue(1) as int? ?? 0,
                Adresat = reader.GetValue(2).ToString(),
                KodUkonu = reader.GetValue(3).ToString()

            };
            dataList.Add(data);
        }

        await reader.CloseAsync();
    }
    return dataList;

}
async Task WriteListOfExeAsync(string path, string exko, IEnumerable<ExekucniPrikazy> selectedDataList)
{
    using (StreamWriter writer = new StreamWriter(path, true))
    {
        await writer.WriteLineAsync();
        await writer.WriteLineAsync(exko);
        await writer.WriteLineAsync();
        foreach (var data in selectedDataList)
        {
            await writer.WriteLineAsync($"{data.Cislo} {data.Adresat}");
        }
    }
}
async Task StartAndWaitForProcessAsync(string processName, string filePath)
{
    using (Process process = new Process())
    {
        process.StartInfo.FileName = processName;
        process.StartInfo.Arguments = filePath;
        process.StartInfo.UseShellExecute = false;
        try
        {
            process.Start();
            await process.WaitForExitAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Problém s externí aplikací " + ex.Message);
        }
    }
}
class ExekucniPrikazy
{
    public string Exko { get; init; }
    public int Cislo { get; init; }
    public string Adresat { get; init; }
    public string KodUkonu { get; init; }
}