using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Zadej exko");
        string exko = Console.ReadLine();

        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=J:\\data.mdb;";
        string query = $"SELECT Code, Adresát,[Kód úkonu] FROM [Kniha úkonov] WHERE Číslo LIKE '{exko}';";

        //string query = $"SELECT Code, Adresát FROM [Kniha úkonov] WHERE Číslo LIKE {exko} AND ([Kód úkonu] LIKE '-2120063737' OR [Kód úkonu] LIKE '-2052727437' OR [Kód úkonu] LIKE '-2030258641' OR [Kód úkonu] LIKE '-1917318598' OR [Kód úkonu] LIKE '-1847281444' OR [Kód úkonu] LIKE '-1808742348' OR [Kód úkonu] LIKE '-1796241568' OR [Kód úkonu] LIKE '-1786773114' OR [Kód úkonu] LIKE '-1748838384' OR [Kód úkonu] LIKE '-1694504395' OR [Kód úkonu] LIKE '-1668446977' OR [Kód úkonu] LIKE '-1665351517' OR [Kód úkonu] LIKE '-1517761113' OR [Kód úkonu] LIKE '-1505536988' OR [Kód úkonu] LIKE '-1483927677' OR [Kód úkonu] LIKE '-1412285989' OR [Kód úkonu] LIKE '-1344265705' OR [Kód úkonu] LIKE '-1267306452' OR [Kód úkonu] LIKE '-1232565484' OR [Kód úkonu] LIKE '-1217505685' OR [Kód úkonu] LIKE '-1210935807' OR [Kód úkonu] LIKE '-1107823104' OR [Kód úkonu] LIKE '-1106413256' OR [Kód úkonu] LIKE '-1084376640' OR [Kód úkonu] LIKE '-1071305888' OR [Kód úkonu] LIKE '-1063233504' OR [Kód úkonu] LIKE '-701744320' OR [Kód úkonu] LIKE '-619113172' OR [Kód úkonu] LIKE '-590050635' OR [Kód úkonu] LIKE '-494699571' OR [Kód úkonu] LIKE '-276545597' OR [Kód úkonu] LIKE '-238707365' OR [Kód úkonu] LIKE '-212811081' OR [Kód úkonu] LIKE '-91742702' OR [Kód úkonu] LIKE '-87103988' OR [Kód úkonu] LIKE '-36279323' OR [Kód úkonu] LIKE '-25053951' OR [Kód úkonu] LIKE '22' OR [Kód úkonu] LIKE '24' OR [Kód úkonu] LIKE '26' OR [Kód úkonu] LIKE '28' OR [Kód úkonu] LIKE '30' OR [Kód úkonu] LIKE '32' OR [Kód úkonu] LIKE '34' OR [Kód úkonu] LIKE '89' OR [Kód úkonu] LIKE '95' OR [Kód úkonu] LIKE '106414959' OR [Kód úkonu] LIKE '134550253' OR [Kód úkonu] LIKE '152214958' OR [Kód úkonu] LIKE '228590420' OR [Kód úkonu] LIKE '280548393' OR [Kód úkonu] LIKE '457680707' OR [Kód úkonu] LIKE '465487351' OR [Kód úkonu] LIKE '585958650' OR [Kód úkonu] LIKE '695949508' OR [Kód úkonu] LIKE '711819427' OR [Kód úkonu] LIKE '751146987' OR [Kód úkonu] LIKE '791230221' OR [Kód úkonu] LIKE '823153261' OR [Kód úkonu] LIKE '894890524' OR [Kód úkonu] LIKE '935444745' OR [Kód úkonu] LIKE '1197348685' OR [Kód úkonu] LIKE '1303495360' OR [Kód úkonu] LIKE '1609235670' OR [Kód úkonu] LIKE '1626423248' OR [Kód úkonu] LIKE '1768096735' OR [Kód úkonu] LIKE '1920555376' OR [Kód úkonu] LIKE '1940675200' OR [Kód úkonu] LIKE '2058691153' OR [Kód úkonu] LIKE '-2095445506' OR [Kód úkonu] LIKE '-1925324557' OR [Kód úkonu] LIKE '1024815598') ORDER BY CODE;";
        string filePath = "output.txt";
        List<string> listOfKodUkonu = ["-2120063737", "-2052727437", "-2030258641", "-1917318598", "-1847281444", "-1808742348", "-1796241568", "-1786773114", "-1748838384", "-1694504395", "-1668446977", "-1665351517", "-1517761113", "-1505536988", "-1483927677", "-1412285989", "-1344265705", "-1267306452", "-1232565484", "-1217505685", "-1210935807", "-1107823104", "-1106413256", "-1084376640", "-1071305888", "-1063233504", "-701744320", "-619113172", "-590050635", "-494699571", "-276545597", "-238707365", "-212811081", "-91742702", "-87103988", "-36279323", "-25053951", "22", "24", "26", "28", "30", "32", "34", "89", "95", "106414959", "134550253", "152214958", "228590420", "280548393", "457680707", "465487351", "585958650", "695949508", "711819427", "751146987", "791230221", "823153261", "894890524", "935444745", "1197348685", "1303495360", "1609235670", "1626423248", "1768096735", "1920555376", "1940675200", "2058691153", "-2095445506", "-1925324557", "1024815598"];
        List<ExekucniPrikazy> dataList = new List<ExekucniPrikazy>();
        var selectedDataList = dataList.Where(zaznam => listOfKodUkonu.Contains(zaznam.KodUkonu)).OrderBy(zaznam => zaznam.Cislo);


        try
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var listData = await GetList(connectionString,query);
            //using (OleDbConnection connection = new OleDbConnection(connectionString))
            //{
            //    OleDbCommand command = new OleDbCommand(query, connection);
            //    connection.Open();
            //    OleDbDataReader reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        ExekucniPrikazy data = new ExekucniPrikazy
            //        {
            //            Cislo = (int)reader.GetValue(0),
            //            Adresat = reader.GetValue(1).ToString(),
            //            KodUkonu = reader.GetValue(2).ToString(),

            //        };
            //        dataList.Add(data);
            //    }

            //    reader.Close();
            //}
            sw.Stop();
            Console.WriteLine($"Trvalo to: {sw.Elapsed}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("SQL problem" + ex.Message);
        }



        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine();
                writer.WriteLine(exko);
                writer.WriteLine();
                foreach (var data in selectedDataList)
                {
                    string line = $"{data.Cislo} {data.Adresat}";
                    writer.WriteLine(line);

                }
                Console.WriteLine("Data byla připsána do souboru.");
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("Problém se souborem, tento problém:" + ex.Message);
        }

        Console.ReadLine();
    }


    public record ExekucniPrikazy
    {
        public int Cislo { get; init; }
        public string Adresat { get; init; }

        public string KodUkonu { get; init; }

    }

    public static async Task<List<ExekucniPrikazy>> GetList(string connectionString, string query)
    {
        List<ExekucniPrikazy> dataList = new List<ExekucniPrikazy>();
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            OleDbCommand command = new OleDbCommand(query, connection);
            await connection.OpenAsync();
            OleDbDataReader reader = (OleDbDataReader) await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                ExekucniPrikazy data = new ExekucniPrikazy
                {
                    Cislo = (int)reader.GetInt32(0),
                    Adresat = reader.GetString(1),
                    KodUkonu = reader.GetString(2)

                };
                dataList.Add(data);
            }

            reader.CloseAsync();
        }
        return dataList;

    }
}