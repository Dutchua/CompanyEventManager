using Npgsql;

namespace CompanyEventManager.Models
{
    public class Bucks
    {
        public int attendeeId { get; set; }
        public decimal amount { get; set; }
        public int status { get; set; }

        public Bucks() { }

        public Bucks(int attendeeId)
        {
            this.attendeeId = attendeeId;
        }

        public Bucks(int attendeeId, decimal amount)
        {
            this.attendeeId = attendeeId;
            this.amount = amount;
        }

        public Bucks(int attendeeId, decimal amount, int status)
        {
            this.attendeeId = attendeeId;
            this.amount = amount;
            this.status = status;
        }

        public static Bucks MapToBuck(NpgsqlDataReader reader)
        {
            try
            {
                int attendeeId = (int)(reader["attendeeid"] as int?);
                int amount = (int)(decimal)(reader["amount"] as decimal?);
                return new Bucks(attendeeId, amount);
            }
            catch (NullReferenceException e)
            {
                return new Bucks(1, 247.69m, 0);
            }
        }
    }
}
