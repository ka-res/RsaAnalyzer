using System;
using System.IO;
using RsaAnalyzer.Models;

namespace RsaAnalyzer.Utilities
{
    class FileOperator
    {
        public static void SaveToFile(string fileName, Rsa rsa)
        {
            using (var fileStream = new FileStream(
                Path.Combine($"C:/users/{Environment.UserName}/Desktop", fileName),
                    FileMode.OpenOrCreate))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(rsa.N);
                    streamWriter.WriteLine(rsa.E);
                    streamWriter.WriteLine(rsa.D);
                }
            }
        }

        public static Rsa ReadFromFile(string fileName, out Rsa rsa)
        {
            using (var fileStream = new FileStream(
                Path.Combine($"C:/users/{Environment.UserName}/Desktop", fileName),
                    FileMode.Open))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    var n = Convert.ToUInt16(streamReader.ReadLine());
                    var e = Convert.ToUInt16(streamReader.ReadLine());
                    var d = Convert.ToInt32(streamReader.ReadLine());
                    
                    rsa = new Rsa(n, e, d);

                    return rsa;
                }
            }
        }
    }
}
