using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace ServerApp
{
    class Program
    {
        // almost everything here was taken from https://docs.microsoft.com/en-us/dotnet/api/system.io.memorymappedfiles.memorymappedfile.createnew?view=net-5.0
        // i'll write some extra code later, this is just to get a basic demo working
        static void Main(string[] args)
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("testmap", 10000))
            {
                bool oldinput = false;
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(1);
                }
                int i = 0;
                while (i != 1)
                {
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        if (oldinput != reader.ReadBoolean()) {
                            Console.WriteLine("Input updated to ", reader.ReadBoolean());
                        }
                    }
                }
            }
        }
    }
}
