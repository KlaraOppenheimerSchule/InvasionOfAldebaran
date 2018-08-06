using System.IO;
using System.Xml.Serialization;

namespace InvasionOfAldebaran.Helper
{
    public class Serializer
    {
        public static void SerializeObject<T>(T item, string filePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                xs.Serialize(writer, item);
            }
        }

        public static object DeserializeXml<T>(string filePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return xs.Deserialize(reader);
            }
        }
    }
}