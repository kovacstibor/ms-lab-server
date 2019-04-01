using System;

namespace MSLab.Server.Models
{
    public class ConcertDetailedData
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public double TicketPrice { get; set; }
        public int NumberOfFreeSpaces { get; set; }
        public bool IsAccessible { get; set; }
        public string Genre { get; set; }
    }
}
