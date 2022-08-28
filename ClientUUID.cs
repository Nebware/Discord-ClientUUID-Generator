using System.Text;

namespace Discord
{
    public class ClientUUID
    {
        /// <summary>
        /// ["client_uuid"] Returns identifier of Discord client iOS app.
        /// </summary>
        /// <param name="Entry" > If authorized use Discord User ID, else use <see href="https://github.com/Nebware/Discord-SnowFlake-Generator/blob/main/SnowFlake.cs">Discord SnowFlake</see></param>
        /// <returns></returns>
        public static string GenerateClientUUID(long Entry)
        {
            var Random = new Random().NextDouble();
            var randomPrefix = 0 | (int)Math.Floor(4294967296 * Random);
            var CreationTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();          
            var first = FirstBytes(Entry);
            var second = SecondBytes(Entry);
            var third = ThirdBytes(randomPrefix);
            var fourth = FourthBytes(CreationTime);
            var fifth = FifthBytes(CreationTime);
            var sixth = SixthBytes(0);
            var bytes = first.Concat(second).Concat(third).Concat(fourth).Concat(fifth).Concat(sixth).ToArray(); 
            string ClientUUID = new ASCIIEncoding().GetString(Encoding.ASCII.GetBytes(Convert.ToBase64String(bytes)));
            return ClientUUID;
            byte[] FirstBytes(long userID)
            {
                byte[] bytes;
                if (userID % 4294967296 <= 2147483647)
                {
                    bytes = BitConverter.GetBytes((int)(userID % 4294967296));

                }
                else
                {
                    bytes = BitConverter.GetBytes((int)(userID % 4294967296 - 2147483647));
                }
                return bytes;
            }
            byte[] SecondBytes(long userID)
            {
                byte[] bytes = BitConverter.GetBytes((int)(userID >> 32));             
                return bytes;
            }
            byte[] ThirdBytes(long randomPrefix)
            {
                byte[] bytes = BitConverter.GetBytes((int)(randomPrefix));          
                return bytes;
            }
            byte[] FourthBytes(long creationTime)
            {
                byte[] bytes;
                if (creationTime % 4294967296 <= 2147483647)
                {
                    bytes = BitConverter.GetBytes((int)(creationTime % 4294967296));
                }
                else
                {
                    bytes = BitConverter.GetBytes((int)(creationTime % 4294967296 - 2147483647));
                }
                return bytes;
            }
            byte[] FifthBytes(long creationTime)
            {
                byte[] bytes = BitConverter.GetBytes((int)(creationTime >> 32));             
                return bytes;
            }
            byte[] SixthBytes(long secuence)
            {
                byte[] bytes = BitConverter.GetBytes((int)(secuence));
                //var HEX = Convert.ToHexString(bytes).ToLower();
                //Console.WriteLine(HEX);
                return bytes;
            }

        }
    }
}
