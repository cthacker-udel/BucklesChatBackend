namespace BucklesChatBackend.Encryption
{
    public static class EncryptionConstants
    {

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
