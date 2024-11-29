using System.Collections.Generic;

class Controller {


    // Naplnit fieldy, objekty

//  public async Task RunAsync()
//  {
// try
// {
//     sw.Start();
//     System.Console.WriteLine("Načítám úkony z DB");
//     List<ExData> listData = await GetListOfExeAsync(connectionString, query);
//     selectedDataList = listData.Where(zaznam => listOfKodUkonu.Contains(zaznam.TypeOfExData));
//     sw.Stop();
//     Console.WriteLine($"Načtení trvalo: {sw.Elapsed}");
//     sw.Restart();
// }
// catch (Exception ex)
// {
//     Console.WriteLine("SQL problem: " + ex.Message);
// }
// while (true)
// {
//     Console.WriteLine("Zadej exko, 'k' pro ukonceni");
//     string exko = Console.ReadLine();
//     if (exko == "k")
//     {
//         break;
//     }
//     else
//     {
//         try
//         {
//             sw.Start();
//             var selectedDataListExko = selectedDataList.Where(ex => ex.Exko == exko).OrderBy(ex => ex.Cislo);
//             await WriteListOfExeAsync(filePath, exko, selectedDataListExko);
//             sw.Stop();
//             Console.WriteLine($"Vyhledat to zabralo: {sw.Elapsed}");
//             Console.WriteLine("Data byla připsána do souboru.");
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Problém se souborem, tento problém:" + ex.Message);
//         }
//     }
//     await StartAndWaitForProcessAsync("notepad.exe", filePath);
//     System.Console.WriteLine("Hotovo");
//     Thread.Sleep(1000);
//     System.Console.Clear();
// }
//  }


}