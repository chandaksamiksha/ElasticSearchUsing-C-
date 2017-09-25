using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearch_Console
{
    public class Hotel
    {
        public int HotelId { set; get; }

        public string Name { set; get; }

        public string Address { set; get; }

        public int NoOfRooms { set; get; }
    }
    class Program
    {
        public static Uri node;
        public static ConnectionSettings settings;
        public static ElasticClient client;

        static void Main(string[] args)
        {
            node = new Uri("http://localhost:9200/");
            settings = new ConnectionSettings(node);
            client = new ElasticClient(settings);

            var indexSettings = new IndexSettings { NumberOfReplicas = 1, NumberOfShards = 2 };

            var indexConfig = new IndexState
            {
                Settings = indexSettings
            };

            if (!client.IndexExists("hotel").Exists)
            {
                client.CreateIndex("hotel", c => c
                .InitializeUsing(indexConfig)
                .Mappings(m => m.Map<Hotel>(mp => mp.AutoMap())));
            }
            InsertWorker();

            Console.WriteLine("Enter Document to be Searched");
            var input = Console.ReadLine();
            var result=SearchWorker(input);
            Console.WriteLine(result);
        }

        public static object SearchWorker(string input)
        {
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
                return response;
            }
        

        public static void InsertWorker()
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

