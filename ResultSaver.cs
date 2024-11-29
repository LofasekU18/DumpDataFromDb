class ResultSaver
{
    // field
    private string _path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\vysledek.txt";

public async Task WriteListOfExeAsync(string exNumber, IEnumerable<ExData> selectedDataList)
{
    using (StreamWriter writer = new StreamWriter(_path, true))
    {
        await writer.WriteLineAsync();
        await writer.WriteLineAsync(exNumber);
        await writer.WriteLineAsync();
        foreach (var data in selectedDataList)
        {
            await writer.WriteLineAsync($"{data.NumberOfPossition} {data.Address}");
        }
    }
}
}