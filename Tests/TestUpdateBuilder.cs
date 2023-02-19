using Xunit;
using CompanyEventManager.Utility;
namespace CompanyEventManager.Tests
{
    public class TestUpdateBuilder
    {
        [Fact]
        public void UpdateQueryBuilderSingleUpdateTest()
        {
            UpdateBuilder builder = new UpdateBuilder("Test");
            builder.Set("testColumn", "testData");
            builder.removeComma();
            Assert.Equal("UPDATE Test SET testColumn = 'testData' ", builder.ToString());
        }

        [Fact]
        public void UpdateQueryBuilderMultipleUpdateTest()
        {
            UpdateBuilder builder = new UpdateBuilder("Test");
            builder.Set("testColumn1", "testData1");
            builder.Set("testColumn2", "testData2");
            builder.removeComma();
            Assert.Equal("UPDATE Test SET testColumn1 = 'testData1', testColumn2 = 'testData2' ", builder.ToString());
        }

        [Fact]
        public void UpdateQueryBuilderWhereUpdateTest()
        {
            UpdateBuilder builder = new UpdateBuilder("Test");
            builder.Set("testColumn", "testData");
            builder.removeComma();
            builder.Where("id", 1);
            Assert.Equal("UPDATE Test SET testColumn = 'testData' WHERE id = 1", builder.ToString());
        }
    }
}
