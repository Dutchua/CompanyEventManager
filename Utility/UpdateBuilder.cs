using CompanyEventManager;
using System.Text;

namespace CompanyEventManager.Utility
{
    class UpdateBuilder
    {
        private StringBuilder queryBuilder;

        public UpdateBuilder(string tableName)
        {
            queryBuilder = new StringBuilder();
            queryBuilder.Append($"UPDATE {tableName} SET ");
        }

        public void Set(string columnName, int value)
        {
            queryBuilder.Append($"{columnName} = {value}, ");
        }

        public string Set(string columnName, string value)
        {
            queryBuilder.Append($"{columnName} = '{value}', ");
            return "Set";
        }

        public string Set(string columnName, decimal value)
        {
            queryBuilder.Append($"{columnName} = '{value}', ");
            return "Set";
        }

        public void removeComma()
        {
            queryBuilder.Remove(queryBuilder.Length - 2, 1);
        }

        public void Where(string columnName, int condition)
        {
            queryBuilder.Append($"WHERE {columnName} = {condition}");
        }

        public void BridgeAnd(string columnName, int condition)
        {
            queryBuilder.Append($" AND {columnName} = {condition}");
        }


        public override string ToString()
        {
            return queryBuilder.ToString();
        }
    }
}
