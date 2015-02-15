using System;
using System.IO;
using CubeProject.Data.Converters;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;

namespace CubeProject.BatchConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidateArguments(args);

            if (IsDirectory(args[0]))
            {
                ConvertDirectoryContents(args[0], args[1]);
            }
            else
            {
                ConvertFile(args[0], args[1]); 
            }

            Console.WriteLine("Conversion complete. Press any key to exit...");
            Console.ReadKey();

        }

        private static void ConvertDirectoryContents(string sourcePath, string destPath)
        {
            Console.WriteLine("Processing Directory:" + sourcePath);
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);

            foreach (var file in Directory.GetFiles(sourcePath))
            {
                ConvertFile(file, destPath);
            }

            foreach (var directory in Directory.GetDirectories(sourcePath))
            {
                ConvertDirectoryContents(directory, Path.Combine(destPath, Path.GetDirectoryName(directory)));
            }
        }

        private static void ConvertFile(string sourcePath, string destPath)
        {
            if (Path.GetExtension(sourcePath) != ".gif")
            {
                Console.WriteLine("[NOT A GIF] - Skipping File:" + sourcePath);
                return;
            }

            Console.WriteLine("Converting File:" + sourcePath);
            string finalDestPath = destPath + Path.GetFileNameWithoutExtension(sourcePath) + ".pmz";
            byte[] fileData = File.ReadAllBytes(sourcePath);
            Animation anim = GifConverter.Convert(fileData);
            ZippedAnimationSerializer zas = new ZippedAnimationSerializer();
            File.WriteAllBytes(finalDestPath, zas.Serialize(anim));
        }

        private static bool IsDirectory(string sourcePath)
        {
            return Directory.Exists(sourcePath) && !File.Exists(sourcePath);
        }

        private static void ValidateArguments(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please provide path parameters.");
                Environment.Exit(1);
            }

            string sourcePath = args[0];
            string targetPath = args[1];

            if (!Directory.Exists(sourcePath) && !File.Exists(sourcePath))
            {
                Console.WriteLine("Source path does not exist.");
                Environment.Exit(1);
            }
        }
    }
}
