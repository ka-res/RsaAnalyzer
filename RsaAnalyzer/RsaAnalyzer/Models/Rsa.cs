namespace RsaAnalyzer.Models
{
    class Rsa
    {
        public ushort N { get; set; }
        public ushort E { get; set; }
        public int D { get; set; }

        public Rsa()
        {
            
        }

        public Rsa(ushort n, ushort e, int d)
        {
            N = n;
            E = e;
            D = d;
        }
    }
}
