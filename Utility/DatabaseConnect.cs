using Npgsql;
namespace CompanyEventManager.Utility
{
    public static class DatabaseConnect
    {
        public static NpgsqlConnection Connection;

        public static void Connect()
        {
            try
            {
                Connection = new NpgsqlConnection(Settings.connString);
                Connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Disconnect()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}