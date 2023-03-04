namespace BucklesChatBackend.Encryption
{
    public static class EncryptionConstants
    {

        public static long GenerateId()
        {
            return new Random().NextInt64(Int64.MaxValue);
        }

    }
}
