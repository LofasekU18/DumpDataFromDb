using System.Data.OleDb;
class DataAccess
{
    private List<ExData> _dataList;
    private string _connectionString;
    private const string _query =  $"SELECT TOP 10 Číslo, Code, Adresát,[Kód úkonu] FROM [Kniha úkonov];";

    public List<ExData> DataList { get { return _dataList; } }
    public DataAccess(string connectionString)
    {
        _connectionString = connectionString;
        _dataList = new List<ExData>();
    }
    public async Task GetListOfExeAsync()
    {
        using (OleDbConnection connection = new OleDbConnection(_connectionString))
        {
            OleDbCommand command = new OleDbCommand(_query, connection);
            await connection.OpenAsync();
            OleDbDataReader reader = (OleDbDataReader)await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                ExData data = new ExData
                {
                    ExNumber = reader.GetValue(0).ToString(),
                    NumberOfPossition = reader.GetValue(1) as int? ?? 0,
                    Address = reader.GetValue(2).ToString(),
                    TypeOfExData = reader.GetValue(3).ToString()

                };
                _dataList.Add(data);
            }

            await reader.CloseAsync();
        }
    }
}