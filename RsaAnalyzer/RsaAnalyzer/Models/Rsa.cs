namespace RsaAnalyzer.Models
{
    internal class Rsa
    {
        public uint N { get; set; }
        public uint E { get; set; }
        public long D { get; set; }

        public Rsa()
        {
            
        }

        public Rsa(uint n, uint e, long d)
        {
            N = n;
            E = e;
            D = d;
        }
    }
}
