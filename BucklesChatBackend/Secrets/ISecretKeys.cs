namespace BucklesChatBackend.Secrets
{
    public interface ISecretKeys
    {
        public string PostgresConnectionString { get; set; }

        public string MongoDBConnectionString { get; set; }
    }
}
