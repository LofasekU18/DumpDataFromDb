using System.Diagnostics;

class TriggerOfSubApplication
{
    private string _processName;
    private string _filePath;

    public TriggerOfSubApplication()
{
    
}

   public async Task StartAndWaitForProcessAsync(string processName, string filePath)
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
}