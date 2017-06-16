using System;
using System.Collections.Generic;
using RsaAnalyzer.Interfaces;

namespace RsaAnalyzer.Responsibility
{
    public class RsaProvider : IRsaAnalyzer
    {
        private static readonly Random Random = new Random();

        public Tuple<uint, uint, long> Run()
        {
            var sieveofEratosthenes = new SieveOfEratosthenes();

            var allPrimes = sieveofEratosthenes.RunAlgorithm();

            var primes = allPrimes.GetRange(allPrimes.Count / 2, 
                allPrimes.Count / 2);

            ushort p = primes[Random.Next(0, primes.Count)],
                   q = primes[Random.Next(0, primes.Count)];

            var n = ReturnN(p, q);
            var phi = ReturnPhi(p, q);
            var possibleE = ReturnPossibleE(phi);

            var eAndD = ReturnEAndD(possibleE, phi);
            var e = eAndD.Item1;
            var d = eAndD.Item2;

            return new Tuple<uint, uint, long>(n, e, d);
        }

        public long ModuloPow(long value, long pow, uint modulo)
        {
            var result = value;
            for (var i = 0; i < pow - 1; i++)
            {
                result = result * value;
                result = result % modulo;
            }
            return result;
        }

        public ExtendedEuclideanResult ExtendedEuclidean(long a, long b)
        {
            long u1 = 1;
            long v1 = 0;
            var u3 = a;
            var v3 = b;

            while (v3 > 0)
            {
                var q0 = u3 / v3;
                var q1 = u3 % v3;
                var tmp = v1 * q0;
                var tn = u1 - tmp;

                u1 = v1;
                v1 = tn;
                u3 = v3;
                v3 = q1;
            }

            var tmp2 = u1 * a;
            tmp2 = u3 - tmp2;
            var res = tmp2 / b;

            var result = new ExtendedEuclideanResult
            {
                U1 = u1,
                U2 = res,
                Gcd = u3
            };

            return result;
        }

        public struct ExtendedEuclideanResult
        {
            public long U1;
            public long U2;
            public long Gcd;
        }

        public uint ReturnN(ushort p, ushort q)
        {
            return (uint)(p * q);
        }

        public uint ReturnPhi(ushort p, ushort q) //uint
        {
            return (uint)((p - 1) * (q - 1));
        }

        public List<uint> ReturnPossibleE(uint phi)
        {
            var result = new List<uint>();

            for (ushort i = 2; i < phi; i++)
            {
                if (ExtendedEuclidean(i, phi).Gcd == 1)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public Tuple<uint, long> ReturnEAndD(IReadOnlyList<uint> possibleE, 
            uint phi)
        {
            uint e;
            long d;

            do
            {
                e = possibleE[Random.Next(0, possibleE.Count)];

                long arg1 = e % phi;
                long arg2 = phi;


                d = ExtendedEuclidean(arg1, arg2).U1;
            } while (d < 0);

            return new Tuple<uint, long>(e, d);
        }

        public long EncryptValue(ushort plainByte, uint e, uint n)
        {
            return ModuloPow(plainByte, e, n);
        }

        /*
        //funkcja pobiera z inputa wiadomosc do zaszyfrownaia w postaci stringa. koduje w iso, zeby byly polskie znaki.
        //zakodowany tekst w iso szyfruje za pomoca klucza publicznego. 
        //todo: zmienic inputa, zeby pobieral tekst w formie string, nie ushort.
         public long[] EcryptValue2(string tekst_do_szyfrowania, uint e, uint n)
            {
            //tekst do szyfrowania pobierany z inputa.
            byte[] tab = Encoding.GetEncoding("iso-8859-2").GetBytes(tekst_do_szyfrowania);
            long[] szyfr = new long[tab.Length];
            for (int i = 0; i < tab.Length; i++)
            {
                szyfr[i] = ModuloPow((long)tab[i], e, n);
            }
            return szyfr;
            }
            */

        //funkcja zamienia tablice z szyfrem na tekst. 
        /*
        public string SzyfrNaString(long[] tab)
        {
            string szyfr = "";
            for (int i = 0; i < tab.Length; i++)
            {
                szyfr += tab[i].ToString();
            }
            return szyfr;
        }
        */
        public int DecryptValue(long encryptedByte, long d, uint n)
        {
            return (ushort)ModuloPow(encryptedByte, d, n);
        }
        // funkcja pobiera szyfr w postaci tablicy i zamienia na wiadomosc poczatkowa.
        /*public string DecryptValue2(long[] tab, long d, uint n)
        {
            string wiadomosc_poczatkowa = "";
            for (int i = 0; i < tab[i]; i++)
            {
                long m = ModuloPow(tab[i], d, n);
                wiadomosc_poczatkowa += Encoding.GetEncoding("iso-8859-2").GetString(new byte[] {(byte)m });
            }
            return wiadomosc_poczatkowa;
        }
        */
    }
}
