namespace TopTeam.Gear.Utility
{
    using System;
    using System.Text;

    public static class Encryptyon
    {
        private static readonly byte[] mask = new byte[] { 85, 170, 73, 146, 204, 51, 195 };
        public static string Encrypt(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(bytes[i] ^ mask[i % mask.Length]);
            }

            return Convert.ToBase64String(bytes);
        }

        public static string Decrypt(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(bytes[i] ^ mask[i % mask.Length]);
            }

            return Encoding.UTF8.GetString(bytes);
        }

    }
}
