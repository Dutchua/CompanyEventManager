using CompanyEventManager.Models;
using CompanyEventManager.Utility;
using Npgsql;

namespace CompanyEventManager.Querys
{
    public class RegisterQuery
    {

        public static int Insert(Register register)
        {
            string sqlcommand = $"INSERT INTO {Settings.registerTable} (attendeeid, registered, status) VALUES (@attendeeid, @registered::bit, @status::bit)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("attendeeid", register.attendeeId);
                command.Parameters.AddWithValue("registered", register.registered);
                command.Parameters.AddWithValue("status", 0);
                int result = (int)command.ExecuteNonQuery();
                return result;
            }
        }

        public static Register SelectByID(Register register)
        {
            string sqlcommand = $"SELECT * FROM {Settings.registerTable} WHERE attendeeid = {register.attendeeId}";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Register mapRegister = Register.MapToRegister(reader);
                        return mapRegister;
                    }
                }
                return new Register(0, 1, 0);
            }
        }

        public static List<Register> SelectAll()
        {
            string sqlcommand = $"SELECT * FROM {Settings.registerTable}";
            List<Register> mapRegister = new List<Register>();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mapRegister.Add(Register.MapToRegister(reader));
                    }
                    return mapRegister;
                }
            }
        }

        public static int SoftDelete(Register register)
        {
            string sqlcommand = $"UPDATE {Settings.registerTable} SET status=1::bit WHERE attendeeid=(@attendeeid)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("attendeeid", register.attendeeId);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public static int HardDelete(Register register)
        {
            string sqlcommand = $"DELETE FROM {Settings.registerTable} WHERE attendeeid=(@attendeeid)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("attendeeid", register.attendeeId);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public static int Update(Register register)
        {
            UpdateBuilder builder = new UpdateBuilder(Settings.registerTable);
            builder.Set("registered", register.registered);
            builder.removeComma();
            builder.Where("attendeeid", register.attendeeId);

            string sqlcommand = builder.ToString();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                int result = command.ExecuteNonQuery();
                return result;
            }
        }
    }
}
