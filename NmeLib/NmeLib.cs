// I do not know C#
// This is what happens when people who do not know C#, try to write C#
// You jav have been warned.
using NmeLib.Converters;
using NmeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace NmeLib
{
    public class NmeLib
    {
        private static readonly JsonSerializer Serialization = new JsonSerializer()
        {
            Converters =
                {
                    new CheckThingConverter(),
                    new FloatSourceConverter(),
                    new JumpConverter(),
                    new ObjectSourceConverter(),
                    new StateActionConverter(),
                    new Vector3Converter(),
                    new StringEnumConverter()
                },
        };

        private static void DeserializeToStandardOut(string fullPath)
        {
            BulkSerializeReader ser;
            using (var fsread = File.OpenRead(fullPath)) ser = new BulkSerializeReader(fsread);
            var data = new SerialMoveset(ser);
            using var writer = new StreamWriter(Console.OpenStandardOutput());
            Serialization.Serialize(writer, data);
        }
        
        private static void SerializeToStandardOut(string fullPath)
        {
            var inputData = new JsonTextReader(File.OpenText(fullPath));
            var dataToWrite = Serialization.Deserialize<SerialMoveset>(inputData);
            
            var writer = new BulkSerializeWriter();

            dataToWrite.Write(writer);
            using var dsts = new StreamWriter(Console.OpenStandardOutput());
            writer.Serialize(dsts);
        }

        public static JObject Deserialize(string fullPath)
        {
            // This feels hacky
            BulkSerializeReader ser;
            using (var fsread = File.OpenRead(fullPath)) ser = new BulkSerializeReader(fsread);
            var data = new SerialMoveset(ser);
            Console.OpenStandardOutput();
            Console.WriteLine(data);
            StringWriter writer = new StringWriter(new StringBuilder());

            Serialization.Serialize(writer, data);
            JObject json = JObject.Parse(writer.ToString());
            return json;
        }
        
        public static void Serialize(string jsonData, string outputPath)
        {
            var inputData = new JsonTextReader(new StringReader(jsonData));
            var dataToWrite = Serialization.Deserialize<SerialMoveset>(inputData);
            
            var writer = new BulkSerializeWriter();

            dataToWrite.Write(writer);
            using var dsts = new StreamWriter(new FileStream(outputPath, FileMode.OpenOrCreate));
            writer.Serialize(dsts);
        }

        public static readonly Dictionary<string, string> CharacterList = new Dictionary<string, string>()
        {
            // Internal/region neutral name, localized name
            {"apple", "Spongebob"},
            {"star", "Patrick"},
            {"diver", "Sandy"},
            {"kite", "Aang"},
            {"clay", "Toph"},
            {"athlete", "Korra"},
            {"moon", "Leonardo"},
            {"pizza", "Michelangelo"},
            {"reporter", "April O'Neil"},
            {"cheese", "Shredder"},
            {"mascot", "Reptar"},
            {"duo", "Ren and Stimpy"},
            {"hero", "Toast Man"},
            {"snake", "Oblina"},
            {"alien", "Zim"},
            {"plasma", "Danny Phantom"},
            {"rascal", "Lincoln Loud"},
            {"goth", "Lucy Loud"},
            {"narrator", "Nigel Thornberry"},
            {"chimera", "CatDog"},
            {"rival", "Helga"},
            {"orb", "Garfield"}
        };

        private static void Write(string dst, string fullPath)
        {
            var watch = new Stopwatch();
            watch.Start();
            BulkSerializeReader ser;
            using (var fsread = File.OpenRead(fullPath))
                ser = new BulkSerializeReader(fsread);
            watch.Stop();
            Console.WriteLine($"Parsing took: {watch.Elapsed}");
            watch.Reset();
            watch.Start();
            var data = new SerialMoveset(ser);
            watch.Stop();
            Console.WriteLine($"Creation of type structure took: {watch.Elapsed}");
            watch.Reset();
            Console.WriteLine("Json dump...");
            var outpFile = Path.GetFileNameWithoutExtension(fullPath);
            outpFile = Path.Combine(dst, outpFile) + ".json";
            watch.Start();
            if (File.Exists(outpFile))
                File.Delete(outpFile);
            using var fs = File.OpenWrite(outpFile);
            using var writer = new StreamWriter(Console.OpenStandardOutput());
            Serialization.Serialize(writer, data);
            watch.Stop();
            Console.WriteLine($"Creation of JSON took: {watch.Elapsed}");
        }

        private static void Read(string path)
        {
            var watch = new Stopwatch();
            watch.Start();
            var data = new JsonTextReader(File.OpenText(path));
            var dataToWrite = Serialization.Deserialize<SerialMoveset>(data);
            watch.Stop();
            Console.WriteLine($"Deserialization took: {watch.Elapsed}");
            var outpFile = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)) + "_2.json";
            if (File.Exists(outpFile))
                File.Delete(outpFile);
            watch.Reset();
            watch.Start();
            using var fs = File.OpenWrite(outpFile);
            using var writer = new StreamWriter(fs);
            Serialization.Serialize(writer, dataToWrite);
            watch.Stop();

            Console.WriteLine($"Creation of JSON took: {watch.Elapsed}");
        }

        private static void ReadWrite(string fullPathJson, string dstFolder)
        {
            var watch = new Stopwatch();
            watch.Start();
            var data = new JsonTextReader(File.OpenText(fullPathJson));
            var dataToWrite = Serialization.Deserialize<SerialMoveset>(data);
            watch.Stop();
            Console.WriteLine($"Deserialization took: {watch.Elapsed}");
            var outpFile = Path.Combine(dstFolder, Path.GetFileNameWithoutExtension(fullPathJson)) + "_new.txt";
            if (File.Exists(outpFile))
                File.Delete(outpFile);
            watch.Reset();
            var writer = new BulkSerializeWriter();
            watch.Start();
            dataToWrite.Write(writer);
            using var fs = File.OpenWrite(outpFile);
            using var dsts = new StreamWriter(fs);
            writer.Serialize(dsts);
            watch.Stop();

            Console.WriteLine($"Writeout took: {watch.Elapsed}");
        }

        private static void Main(string[] args)
        {
            switch (args[0])
            {
                case "deserialize":
                    DeserializeToStandardOut(args[1]);
                    break;
                case "serialize":
                    SerializeToStandardOut(args[1]);
                    break;
            }
        }
    }
}