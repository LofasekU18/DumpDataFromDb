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


static async Task Main()
{
    const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=J:\\data.mdb;";
    const string query = $"SELECT Číslo, Code, Adresát,[Kód úkonu] FROM [Kniha úkonov];";
    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\output.txt";
    List<string> listOfKodUkonu = ["-2120063737", "-2052727437", "-2030258641", "-1917318598", "-1847281444", "-1808742348", "-1796241568", "-1786773114", "-1748838384", "-1694504395", "-1668446977", "-1665351517", "-1517761113", "-1505536988", "-1483927677", "-1412285989", "-1344265705", "-1267306452", "-1232565484", "-1217505685", "-1210935807", "-1107823104", "-1106413256", "-1084376640", "-1071305888", "-1063233504", "-701744320", "-619113172", "-590050635", "-494699571", "-276545597", "-238707365", "-212811081", "-91742702", "-87103988", "-36279323", "-25053951", "22", "24", "26", "28", "30", "32", "34", "89", "95", "106414959", "134550253", "152214958", "228590420", "280548393", "457680707", "465487351", "585958650", "695949508", "711819427", "751146987", "791230221", "823153261", "894890524", "935444745", "1197348685", "1303495360", "1609235670", "1626423248", "1768096735", "1920555376", "1940675200", "2058691153", "-2095445506", "-1925324557", "1024815598"];

    try
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        List<ExekucniPrikazy> listData = await GetListOfExe(connectionString, query);
        var selectedDataList = listData.Where(zaznam => listOfKodUkonu.Contains(zaznam.KodUkonu));
        sw.Stop();
        Console.WriteLine($"Vyhledat to trvalo: {sw.Elapsed}");
        sw.Restart();
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
                    await WriteListOfExe(filePath, exko, selectedDataListExko);
                    sw.Stop();
                    Console.WriteLine($"Vyhledat to zapsat: {sw.Elapsed}");
                    Console.WriteLine("Data byla připsána do souboru.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problém se souborem, tento problém:" + ex.Message);
                }
            }
            Console.ReadLine();
            using (Process process = Process.Start(filePath))
            {
                process.WaitForExit();
            }
            Console.Clear();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("SQL problem: " + ex.Message);
    }



}






static async Task<List<ExekucniPrikazy>> GetListOfExe(string connectionString, string query)
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
                Cislo = (int)reader.GetValue(1),
                Adresat = reader.GetValue(2).ToString(),
                KodUkonu = reader.GetValue(3).ToString()

            };
            dataList.Add(data);
        }

        await reader.CloseAsync();
    }
    return dataList;

}
static async Task WriteListOfExe(string path, string exko, IOrderedEnumerable<ExekucniPrikazy> selectedDataList)
{
    using (StreamWriter writer = new StreamWriter(path, true))
    {
        await writer.WriteLineAsync();
        await writer.WriteLineAsync(exko);
        await writer.WriteLineAsync();
        foreach (var data in selectedDataList)
        {
            string line = $"{data.Cislo} {data.Adresat}";
            await writer.WriteLineAsync(line);

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