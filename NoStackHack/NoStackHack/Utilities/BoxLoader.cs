using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Utilities
{
    public static class BoxLoader
    {

        public delegate void OnBoxLoad(Box box);

        public static void LoadFromFile(string filePath, OnBoxLoad onBoxRead)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Box file does not exist at {filePath}");

            using (var reader = new StreamReader(File.OpenRead(filePath)))
            {
                var line = reader.ReadLine();

                var parts = line.Split(',').Select(x => x.Trim()).ToList();
                var position = new Vector2(float.Parse(parts[0]), float.Parse(parts[1]));
                var size = new Vector2(float.Parse(parts[2]), float.Parse(parts[3]));
                onBoxRead(new Box(position, size));
            }
            
        }

        public static List<Box> LoadFromFile(string filePath)
        {
            var boxes = new List<Box>();
            LoadFromFile(filePath, box => boxes.Add(box));
            return boxes;
        }

    }
}
