using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearch_Console
{
    public class SearchManager
    {
        private ElasticClient client;

        public SearchManager(ElasticClient client)
        {
            this.client = client;
        }

        public List<Hotel> SearchWorker(string input)
        {
            List<Hotel> hotelList = new List<Hotel>();
            var query = input.ToLower();
            var response = client.Search<Hotel>(s => s
                                .From(0)
                                .Size(100)
                                .Index("hotel")
                                .Type("myHotel")
                                .Query(q =>
                                        q.Term(t => t.Address, query)
                                        )
                                );
            foreach (var hit in response.Hits)
            {
                var hotel = new Hotel();
                hotel.Name = hit.Source.Name;
                hotel.HotelId = hit.Source.HotelId;
                hotel.NoOfRooms = hit.Source.NoOfRooms;
                hotel.Address = hit.Source.Address;

                hotelList.Add(hotel);
            }
            return hotelList;
        }
    }
}
