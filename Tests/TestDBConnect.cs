using Npgsql;
using Xunit;
using CompanyEventManager.Utility;
namespace CompanyEventManager.Tests
{
    public class TestDBConnect
    {
        [Fact]
        public void OpenConnectionTest()
        {
            DatabaseConnect.Connect();
            Assert.True(DatabaseConnect.Connection.State == System.Data.ConnectionState.Open, "Connection should be open.");
            DatabaseConnect.Disconnect();
        }

        [Fact]
        public void ClosedConnectionTest()
        {
            DatabaseConnect.Connect();
            Assert.True(DatabaseConnect.Connection.State == System.Data.ConnectionState.Open, "Connection should be open.");
            DatabaseConnect.Disconnect();
            Assert.True(DatabaseConnect.Connection.State == System.Data.ConnectionState.Closed, "Connection should be closed.");
        }
    }
}
