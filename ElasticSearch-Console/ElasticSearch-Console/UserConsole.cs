using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearch_Console
{
    public class UserConsole
    {
        private ElasticClient client;
        SearchManager searchManager;

        public UserConsole(ElasticClient client)
        {
            this.client = client;
        }

        public void PerformAction()
        {
            Console.WriteLine("Enter Document to be Searched");
            var input = Console.ReadLine();
            searchManager = new SearchManager(client);
            List<Hotel> result = searchManager.SearchWorker(input);
            foreach (var hotel in result)
            {
                Console.WriteLine(hotel.HotelId);
                Console.WriteLine(hotel.Name);
                Console.WriteLine(hotel.NoOfRooms);
                Console.WriteLine(hotel.Address);
            }
        }
    }
}
