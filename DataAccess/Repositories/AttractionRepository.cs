using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using TripAdvisor;

namespace Where2GoNow.DataAccess.Repositories
{
    public class AttractionRepository
    {
        private const string TableName = "attractions";
        private const double Degrees2MilesRatio = 50;

        private IDynamoDBContext _dbContext { get; set; }

        public AttractionRepository()
        {
            AWSConfigsDynamoDB.Context.TypeMappings[typeof(Attraction)] = new Amazon.Util.TypeMapping(typeof(Attraction), TableName);

            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            _dbContext = new DynamoDBContext(new AmazonDynamoDBClient(RegionEndpoint.USWest2), config);
        }

        public async Task<IEnumerable<Attraction>> GetAttractions(double lat, double lng, double radius, int popularity)
        {
            var degrees = radius / Degrees2MilesRatio;
            return await _dbContext.ScanAsync<Attraction>(new[] { new ScanCondition("lat", ScanOperator.Between, lat - degrees, lat + degrees), new ScanCondition("lng", ScanOperator.Between, lng - degrees, lng + degrees), new ScanCondition("reviews", ScanOperator.GreaterThanOrEqual, popularity) }).GetRemainingAsync();
        }

        public async Task InsertAttractions(IEnumerable<Attraction> attractions)
        {
            var batch = _dbContext.CreateBatchWrite<Attraction>();
            batch.AddPutItems(attractions);
            await batch.ExecuteAsync();
        }
    }
}
