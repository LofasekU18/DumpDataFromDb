class ResultSaver
{
    // field

    async Task WriteListOfExeAsync(string path, string exko, IEnumerable<ExData> selectedDataList)
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
}