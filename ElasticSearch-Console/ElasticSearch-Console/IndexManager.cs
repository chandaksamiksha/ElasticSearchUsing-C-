using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using System.Threading.Tasks;

namespace ElasticSearch_Console
{
    public class IndexManager
    {
        public ElasticClient client;

        public IndexManager(ElasticClient client)
        {
            this.client = client;
        }

        public void AddIndex()
        {
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
            
        }
    }
}
