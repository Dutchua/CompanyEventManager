using CompanyEventManager.Models;
using CompanyEventManager.Utility;
using Npgsql;

namespace CompanyEventManager.Querys
{
    public class BucksQuery
    {

        public static int Insert(Bucks bucks)
        {
            string sqlcommand = $"INSERT INTO {Settings.bucksTable} (attendeeid, amount, status) VALUES (@attendeeid, @amount, @status::bit)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("attendeeid", bucks.attendeeId);
                command.Parameters.AddWithValue("amount", bucks.amount);
                command.Parameters.AddWithValue("status", 0);
                int result = (int)command.ExecuteNonQuery();
                return result;
            }
        }

        public static Bucks SelectByID(Bucks bucks)
        {
            string sqlcommand = $"SELECT * FROM {Settings.bucksTable} WHERE attendeeid = {bucks.attendeeId}";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bucks mapBuck = Bucks.MapToBuck(reader);
                        return mapBuck;
                    }
                }
                return new Bucks(0, 247.69m, 0);
            }
        }

        public static List<Bucks> SelectAll()
        {
            string sqlcommand = $"SELECT * FROM {Settings.bucksTable}";
            List<Bucks> mapBuck = new List<Bucks>();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mapBuck.Add(Bucks.MapToBuck(reader));
                    }
                    return mapBuck;
                }
            }
        }

        public static int SoftDelete(Bucks bucks)
        {
            string sqlcommand = $"UPDATE {Settings.bucksTable} SET status=1::bit WHERE attendeeid=(@attendeeid)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("attendeeid", bucks.attendeeId);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public static int HardDelete(Bucks bucks)
        {
            string sqlcommand = $"DELETE FROM {Settings.bucksTable} WHERE attendeeid=(@attendeeid)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("attendeeid", bucks.attendeeId);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public static int Update(Bucks bucks)
        {
            UpdateBuilder builder = new UpdateBuilder(Settings.bucksTable);
            builder.Set("amount", bucks.amount);
            builder.removeComma();
            builder.Where("attendeeid", bucks.attendeeId);

            string sqlcommand = builder.ToString();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                int result = command.ExecuteNonQuery();
                return result;
            }
        }
    }
}
