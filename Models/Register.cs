using Npgsql;

namespace CompanyEventManager.Models
{
    public class Register
    {
        public int attendeeId { get; set; }
        public int registered { get; set; }
        public int status { get; set; }

        public Register() { }

        public Register(int attendeeId)
        {
            this.attendeeId = attendeeId;
        }

        public Register(int attendeeId, int registered)
        {
            this.attendeeId = attendeeId;
            this.registered = registered;
        }

        public Register(int attendeeId, int registered, int status)
        {
            this.attendeeId = attendeeId;
            this.registered = registered;
            this.status = status;
        }

        public static Register MapToRegister(NpgsqlDataReader reader)
        {
            try
            {
                int attendeeId = (int)(reader["attendeeid"] as int?);
                int registered = (int)(decimal)(reader["registered"] as int?);
                return new Register(attendeeId, registered);
            }
            catch (NullReferenceException e)
            {
                return new Register(1, 1, 0);
            }
        }
    }
}
