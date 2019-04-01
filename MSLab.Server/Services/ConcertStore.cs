using MSLab.Server.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MSLab.Server.Services
{
    public interface IConcertStore
    {
        IEnumerable<ConcertBaseData> GetConcerts(string filter);
        ConcertDetailedData GetConcertById(int concertId);
        void CreateNewConcert(ConcertDetailedData newConcert);
        void UpdateConcert(int concertId, ConcertDetailedData modifiedConcertData);
        void DeleteConcert(int concertId);
    }

    public class ConcertStore : IConcertStore
    {
        private readonly Dictionary<int, ConcertDetailedData> _concerts;

        public ConcertStore()
        {
            _concerts = new Dictionary<int, ConcertDetailedData>
            {
                [0] = new ConcertDetailedData { Id = 0, Artist = "Iron Maiden", Date = new DateTime(2015, 2, 3), Genre = "metal", Location = "London", IsAccessible = true, NumberOfFreeSpaces = 6848, TicketPrice = 13000.0d },
                [1] = new ConcertDetailedData { Id = 1, Artist = "Metallica", Date = new DateTime(2014, 11, 2), Genre = "metal", Location = "San Fransisco", IsAccessible = true, NumberOfFreeSpaces = 8486, TicketPrice = 25000.0d },
                [2] = new ConcertDetailedData { Id = 2, Artist = "Madonna", Date = new DateTime(2019, 8, 30), Genre = "pop", Location = "New York", IsAccessible = false, NumberOfFreeSpaces = 6846, TicketPrice = 17000.0d },
                [3] = new ConcertDetailedData { Id = 3, Artist = "R-Go", Date = new DateTime(2012, 5, 13), Genre = "pop", Location = "Budapest", IsAccessible = true, NumberOfFreeSpaces = 6488, TicketPrice = 8000.0d },
                [4] = new ConcertDetailedData { Id = 4, Artist = "Eminem", Date = new DateTime(2014, 9, 27), Genre = "rap", Location = "Chicago", IsAccessible = true, NumberOfFreeSpaces = 1684, TicketPrice = 8000.0d },
                [5] = new ConcertDetailedData { Id = 5, Artist = "ABBA", Date = new DateTime(2013, 1, 12), Genre = "pop", Location = "Tallin", IsAccessible = false, NumberOfFreeSpaces = 8347, TicketPrice = 7000.0d },
                [6] = new ConcertDetailedData { Id = 6, Artist = "Queen", Date = new DateTime(2018, 3, 31), Genre = "rock", Location = "Paris", IsAccessible = false, NumberOfFreeSpaces = 1867, TicketPrice = 24000.0d },
                [7] = new ConcertDetailedData { Id = 7, Artist = "Tankcsapda", Date = new DateTime(2017, 12, 16), Genre = "metal", Location = "Debrecen", IsAccessible = true, NumberOfFreeSpaces = 823, TicketPrice = 21500.0d },
                [8] = new ConcertDetailedData { Id = 8, Artist = "Sabaton", Date = new DateTime(2016, 10, 5), Genre = "metal", Location = "Warshaw", IsAccessible = false, NumberOfFreeSpaces = 684, TicketPrice = 12000.0d }
            };
        }

        public void CreateNewConcert(ConcertDetailedData newConcert)
        {
            var newConcertId = Enumerable.Range(0, int.MaxValue).First(i => !_concerts.ContainsKey(i));

            newConcert.Id = newConcertId;
            _concerts.Add(newConcertId, newConcert);
        }

        public void DeleteConcert(int concertId)
        {
            if (!_concerts.ContainsKey(concertId) || _concerts[concertId] is null)
            {
                throw new InvalidOperationException();
            }

            _concerts[concertId] = null;
        }

        public ConcertDetailedData GetConcertById(int concertId)
        {
            if (!_concerts.ContainsKey(concertId) || _concerts[concertId] is null)
            {
                throw new InvalidOperationException();
            }

            return _concerts[concertId];
        }

        public IEnumerable<ConcertBaseData> GetConcerts(string filter)
        {
            return _concerts.Values.Where(c => c != null)
                                   .Where(c => string.IsNullOrWhiteSpace(filter) || c.Artist == null || c.Artist.ToLower().Contains(filter.ToLower())) 
                                   .Select(c => new ConcertBaseData
                                   {
                                       Artist = c.Artist,
                                       Date = c.Date,
                                       Id = c.Id,
                                       Location = c.Location,
                                       TicketPrice = c.TicketPrice
                                   });
        }

        public void UpdateConcert(int concertId, ConcertDetailedData modifiedConcertData)
        {
            if (!_concerts.ContainsKey(concertId) || _concerts[concertId] is null)
            {
                throw new InvalidOperationException();
            }

            var concertToModify = _concerts[concertId];
            concertToModify.Artist = modifiedConcertData.Artist;
            concertToModify.Date = modifiedConcertData.Date;
            concertToModify.Genre = modifiedConcertData.Genre;
            concertToModify.IsAccessible = modifiedConcertData.IsAccessible;
            concertToModify.Location = modifiedConcertData.Location;
            concertToModify.NumberOfFreeSpaces = modifiedConcertData.NumberOfFreeSpaces;
            concertToModify.TicketPrice = modifiedConcertData.TicketPrice;
        }
    }
}
