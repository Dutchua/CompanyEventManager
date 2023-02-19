using Npgsql;
using CompanyEventManager.Models;
using CompanyEventManager.Utility;

namespace CompanyEventManager.Querys
{
    public static class AttendeeQuery
    {
        public static int Insert(Attendee attendee)
        {
            string sqlcommand = $"INSERT INTO {Settings.attendeeTable} (name, surname, accessnumber, status) VALUES (@name, @surname, @accessnumber, @status)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("name", attendee.name);
                command.Parameters.AddWithValue("surname", attendee.surname);
                command.Parameters.AddWithValue("accessnumber", attendee.accessNumber);
                command.Parameters.AddWithValue("status", attendee.status);
                int result = (int)command.ExecuteNonQuery();
                return result;
            }
        }

        public static Attendee SelectByID(Attendee attendee)
        {
            string sqlcommand = $"SELECT * FROM {Settings.attendeeTable} WHERE attendeeid = {attendee.attendeeId}";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Attendee mapPerson = Attendee.MapToAttendee(reader);
                        return mapPerson;
                    }
                }
                return new Attendee("Josh", "Jennings", "Something went wrong", 1);
            }
        }

        public static List<Attendee> SelectAll()
        {
            string sqlcommand = $"SELECT * FROM {Settings.attendeeTable}";
            List<Attendee> mapAttendee = new List<Attendee>();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mapAttendee.Add(Attendee.MapToAttendee(reader));
                    }
                    return mapAttendee;
                }
            }
        }

        public static int SoftDelete(Attendee attendee)
        {
            string sqlcommand = $"UPDATE {Settings.attendeeTable} SET status=1::bit WHERE attendeeid=(@attendeeid)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("attendeeid", attendee.attendeeId);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public static int HardDelete(Attendee attendee)
        {
            string sqlcommand = $"DELETE FROM {Settings.attendeeTable} WHERE attendeeid=(@attendeeid)";
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                command.Parameters.AddWithValue("attendeeid", attendee.attendeeId);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public static int Update(Attendee attendee)
        {
            UpdateBuilder builder = new UpdateBuilder(Settings.attendeeTable);
            string name = !string.IsNullOrEmpty(attendee.name) ? builder.Set("name", attendee.name) : "";
            string surname = !string.IsNullOrEmpty(attendee.surname) ? builder.Set("surname", attendee.surname) : "";
            string accessNumber = !string.IsNullOrEmpty(attendee.accessNumber) ? builder.Set("accessnumber", attendee.accessNumber) : "";
            builder.removeComma();
            builder.Where("attendeeid", attendee.attendeeId);

            string sqlcommand = builder.ToString();
            Console.WriteLine(sqlcommand);
            using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, DatabaseConnect.Connection))
            {
                int result = command.ExecuteNonQuery();
                return result;
            }
        }
    }
}
