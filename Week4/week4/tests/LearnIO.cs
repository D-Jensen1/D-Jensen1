using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace tests;

[TestClass]
public class LearnIO
{
    [TestMethod]
    public void SizeOfThings()
    {
        Assert.AreEqual(4, sizeof(int));
        Assert.AreEqual(8, sizeof(long));
        byte[] byteArray = new byte[1024 * 1024];
        Assert.AreEqual(1024 * 1024, byteArray.Length);

        HashAlgorithm hasher = MD5.Create();

        byte[] greeting = Encoding.UTF8.GetBytes("HelloWorld");
        byte[] thumbprint = hasher.ComputeHash(greeting);
    }

    [TestMethod]
    public void GenerateAListOfFilesTest()
    {
        // static class Directory
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string[] fileNames = Directory.GetFiles(currentDirectory);

        HashAlgorithm hasher = SHA256.Create();

        // instance class DirectoryInfo
        DirectoryInfo dirInfo = new DirectoryInfo(currentDirectory);
        Dictionary<byte[], FileInfo> hashLookup = new();
        FileInfo[] files = dirInfo.GetFiles();

        foreach (var file in files)
        {
            using (FileStream fs = file.OpenRead()) // using keyword automatically returns resources
                                                    // being used once complete (fs.Close())
            {
                byte[] fileHash = hasher.ComputeHash(fs);
                hashLookup.Add(fileHash, file);
            }

        }
    }

    [TestMethod]
    public void WorkWithStream()
    {
        string fileName = "test.bin";
        string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(directory, fileName);
        
        Stream aStream = new FileStream(filePath,FileMode.OpenOrCreate);
        for (byte i = 1; i < 128; i++)
        {
            aStream.WriteByte(i);
        }
        Debug.Print(aStream.Position.ToString());
        Debug.Print($"Can Seek: {aStream.CanSeek}");
        Debug.Print($"Can Read: {aStream.CanRead}");
        Debug.Print($"Can Write: {aStream.CanWrite}");

        if (aStream.CanSeek) aStream.Position = 0;
        for (byte i = 127; i > 0; i--)
        {
            aStream.WriteByte(i);
        }

        if (aStream.CanSeek) aStream.Position = 50;
        byte[] buffer = new byte[10 * 1024 * 1024]; // creates 10MB buffer
        while (aStream.Position < aStream.Length)
        {
            // scoops up 10MB at a time or whatever data remains if under 10MB
            aStream.ReadExactly(buffer, 0, (int)Math.Min(buffer.Length, aStream.Length - aStream.Position));
        }

        aStream.Close();

    }

    [TestMethod]
    public void ReadWriterTest()
    {
        // pipe and filter

        // Binary Reader/Writer provides binary primitive reader/writer helper methods
        string fileName = "binRW.bin";
        string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(directory, fileName);
        Stream aStream = new FileStream(filePath, FileMode.OpenOrCreate);
        
        BinaryWriter writer = new BinaryWriter(aStream);

        writer.Write(100);  // 4 bytes
        writer.Write(500.50); // 8 bytes
        writer.Write(250.50f); // 8 bytes
        writer.Write(123.4567m); //16 bytes
        writer.Write(true); // 1 byte
        writer.Write('h');// 1 byte
        writer.Write(long.MaxValue); // 8 bytes
        aStream.Position = 0;

        BinaryReader reader = new BinaryReader(aStream);
        Assert.AreEqual(100, reader.ReadInt32());
        Assert.AreEqual(500.50, reader.ReadDouble());
        Assert.AreEqual(250.50f, reader.ReadSingle());
        Assert.AreEqual(123.4567m, reader.ReadDecimal());
        Assert.AreEqual(true, reader.ReadBoolean());
        Assert.AreEqual('h', reader.ReadChar());
        Assert.AreEqual(long.MaxValue, reader.ReadInt64());
        Assert.AreEqual(42, aStream.Length);
        aStream.Close();



        //BinaryReader reader = new BinaryReader(aStream);


        // Stream Reader/Writer provides text writing helper methods

    }
}
