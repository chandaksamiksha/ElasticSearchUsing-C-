using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearch_Console
{
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

            IndexManager indexManager;
            InsertManager insertManager;
            UserConsole userConsole;

            indexManager = new IndexManager(client);
            indexManager.AddIndex();

            insertManager = new InsertManager(client);
            insertManager.InsertWorker();

            userConsole = new UserConsole(client);
            userConsole.PerformAction();

            Console.ReadKey(true);
        }
    }
}

