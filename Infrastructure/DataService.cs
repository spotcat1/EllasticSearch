using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Nest;


namespace Infrastructure
{
    public class DataService
    {
        private readonly IMongoCollection<HsCodeDto> _mongoCollection;
        private readonly ElasticClient _elasticClient;
        private const string IndexName = "hscodes_index";

        public DataService(IConfiguration config)
        {
            // Setup MongoDB
            var mongoClient = new MongoClient(config["MongoDb:ConnectionString"]);
            var database = mongoClient.GetDatabase(config["MongoDb:Database"]);  
            _mongoCollection = database.GetCollection<HsCodeDto>("HsCodesCollection");

            // Setup Elasticsearch
            var elasticSearchUrl = config["ElasticSearch:Url"];
            var username = config["ElasticSearch:Username"];
            var password = config["ElasticSearch:Password"];

            var settings = new ConnectionSettings(new Uri(elasticSearchUrl))
                .BasicAuthentication(username, password)  
                .DefaultIndex("hscodes_index")           
                .DisablePing();                            

            _elasticClient = new ElasticClient(settings);
        }


        public async Task<HsCodeDto?> GetHsCodeById(string id)
        {

            id = id.Trim(); 

      
            var esResponse = await _elasticClient.GetAsync<HsCodeDto>(id, idx => idx.Index(IndexName));
            if (esResponse.Found)
            {
                return esResponse.Source;
            }

         
            var filter = Builders<HsCodeDto>.Filter.Eq("_id", id);  
            var hsCode = await _mongoCollection.Find(filter).FirstOrDefaultAsync();

            if (hsCode != null)
            {
                IndexHsCode(hsCode); 
                return hsCode;
            }

            Console.WriteLine($"ID {id} not found in MongoDB."); 
            return null;
        }

        public void IndexHsCode(HsCodeDto   hsCode)
        {
            _elasticClient.IndexDocument(hsCode);
        }
    }
}
