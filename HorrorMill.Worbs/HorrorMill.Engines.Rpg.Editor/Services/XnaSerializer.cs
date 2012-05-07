using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace HorrorMill.Engines.Rpg.Editor.Services
{
    static public class XnaSerializer
    {
        public static void Serialize<T>(string filename, T data)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(filename, settings))
                IntermediateSerializer.Serialize<T>(writer, data, referenceRelocationPath: null);        // attention, I remember from somewhere that this serializer is not available on the phone...
        }

        public static T Deserialize<T>(string filename)
        {
            T data;
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            using (XmlReader reader = XmlReader.Create(stream))
                data = IntermediateSerializer.Deserialize<T>(reader, referenceRelocationPath: null);
            return data;
        }
         
    }
}