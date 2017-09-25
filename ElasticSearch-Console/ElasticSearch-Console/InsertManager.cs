using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearch_Console
{
    public class InsertManager
    {
        public ElasticClient client;

        public InsertManager(ElasticClient client)
        {
            this.client = client;
        }

        public void InsertWorker()
        {
            var listOfHotel = PopulateHotels();

            foreach (var hotel in listOfHotel.Select((value, counter) => new { counter, value }))
            {
                client.Index(hotel.value, i => i
                    .Index("hotel")
                    .Type("myHotel")
                    .Id(hotel.counter)
                    );
            }
        }

        public static List<Hotel> PopulateHotels()
        {
            return new List<Hotel>
            {
                new Hotel {HotelId = 1, Name = "Abc", Address = "Vimannagar", NoOfRooms = 45},
                new Hotel {HotelId = 2, Name = "Def", Address = "Kharadi", NoOfRooms = 35},
                new Hotel {HotelId = 3, Name = "Ghi", Address = "Wadgaon Sheri", NoOfRooms = 25},
            };
        }
    }
}
