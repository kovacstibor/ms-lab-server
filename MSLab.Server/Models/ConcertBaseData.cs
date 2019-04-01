using System;

namespace MSLab.Server.Models
{
    public class ConcertBaseData
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public double TicketPrice { get; set; }
    }
}
