using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


string filePath = @"..\..\..\test1.txt"; 

try
{
    using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    using SHA1 sha1 = SHA1.Create();

    byte[] hashBytes = sha1.ComputeHash(fileStream);
    string base64Hash = Convert.ToBase64String(hashBytes);

    Console.WriteLine($"SHA1 Base64 Hash: {base64Hash}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
