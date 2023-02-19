using Npgsql;

namespace CompanyEventManager.Models
{
    public class Attendee
    {
        public int attendeeId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string accessNumber { get; set; }
        public int status { get; set; }


        public Attendee() { }

        public Attendee(int attendeeId)
        {
            this.attendeeId = attendeeId;
        }

        public Attendee(string name, string surname, string accessNumber, int status)
        {
            this.name = name;
            this.surname = surname;
            this.accessNumber = accessNumber;
            this.status = status;
        }

        public Attendee(int attendeeId, string name, string surname, string accessNumber)
        {
            this.attendeeId = attendeeId;
            this.name = name;
            this.surname = surname;
            this.accessNumber = accessNumber;
        }

        public Attendee(int attendeeId, string name, string surname, string accessNumber, int status)
        {
            this.attendeeId = attendeeId;
            this.name = name;
            this.surname = surname;
            this.accessNumber = accessNumber;
            this.status = status;
        }

        public static Attendee MapToAttendee(NpgsqlDataReader reader)
        {
            try
            {
                int attendeeId = (int)(reader["attendeeid"] as int?);
                string name = reader["name"] as string;
                string surname = reader["surname"] as string;
                string accessNumber = reader["accessnumber"] as string;
                return new Attendee(attendeeId, name, surname, accessNumber);
            }
            catch (NullReferenceException e)
            {
                return new Attendee(1, "Test", "Test", "Test", 0);
            }
        }
    }
}
