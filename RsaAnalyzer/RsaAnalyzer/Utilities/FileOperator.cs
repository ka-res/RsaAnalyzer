using System;
using System.IO;
using RsaAnalyzer.Models;

namespace RsaAnalyzer.Utilities
{
    internal class FileOperator
    {
        public static void SaveRsaToFile(string fileName, Rsa rsa)
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

        public static Rsa ReadRsaFromFile(string fileName, out Rsa rsa)
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

        public static void SaveTimeSpanToFile(string fileName, TimeSpan timeSpan, string stageName)
        {
            using (var fileStream = new FileStream(
                Path.Combine($"C:/users/{Environment.UserName}/Desktop", fileName),
                FileMode.Append))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine($"{stageName}: {TimeCounter.RepresentAsTicks(timeSpan)}");
                }
            }
        }

        //TODO: reading TimeSpan from file
        //public static TimeSpan ReadTimeSpanFromFile(string fileName, out TimeSpan timeSpan)
        //{
        //    using (var fileStream = new FileStream(
        //        Path.Combine($"C:/users/{Environment.UserName}/Desktop", fileName),
        //        FileMode.Open))
        //    {
        //        using (var streamReader = new StreamReader(fileStream))
        //        {
                    
        //        }
        //    }
        //}
    }
}
