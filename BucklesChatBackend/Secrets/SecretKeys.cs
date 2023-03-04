namespace BucklesChatBackend.Secrets
{

    public class SecretKeys: ISecretKeys
    {

        public string PostgresConnectionString { get; set; }

        public string MongoDBConnectionString { get; set; }

        public SecretKeys(string psqlConnectionString, string mongoDBConnectionString)
        {
            PostgresConnectionString = psqlConnectionString;
            MongoDBConnectionString = mongoDBConnectionString;
        }

    }
}
