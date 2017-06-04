using System;
using System.Collections.Generic;

namespace RsaAnalyzer.Utilities
{
    internal class RsaProvider
    {
        private static readonly Random Random = new Random();

        public static Tuple<ushort, ushort, int> Run()
        {
            Responsibility.SieveofEratosthenes sieveofEratosthenes =
                new Responsibility.SieveofEratosthenes();

            var primes = sieveofEratosthenes.RunAlgorithm();

            byte p = primes[Random.Next(0, primes.Count)],
                q = primes[Random.Next(0, primes.Count)];

            var n = ReturnN(p, q);
            var phi = ReturnPhi(p, q);
            var possibleE = ReturnPossibleE(phi);

            var eAndD = ReturnEAndD(possibleE, phi);
            var e = eAndD.Item1;
            var d = eAndD.Item2;

            return new Tuple<ushort, ushort, int>(n, e, d);
        }

        private static int ModuloPow(int value, int pow, int modulo)
        {
            var result = value;
            for (var i = 0; i < pow - 1; i++)
            {
                result = (result * value) % modulo;
            }
            return result;
        }

        private static ExtendedEuclideanResult ExtendedEuclidean(int a, int b)
        {
            var u1 = 1;
            var u3 = a;
            var v1 = 0;
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

            var tmp2 = u1 * (a);
            tmp2 = u3 - (tmp2);
            var res = tmp2 / (b);

            var result = new ExtendedEuclideanResult
            {
                u1 = u1,
                u2 = res,
                gcd = u3

            };

            return result;
        }

        private struct ExtendedEuclideanResult
        {
            public int u1;
            public int u2;
            public int gcd;
        }

        private static ushort ReturnN(byte p, byte q)
        {
            return (ushort)(p * q);
        }

        private static ushort ReturnPhi(byte p, byte q)
        {
            return (ushort)((p - 1) * (q - 1));
        }

        static List<ushort> ReturnPossibleE(ushort phi)
        {
            var result = new List<ushort>();

            for (ushort i = 2; i < phi; i++)
            {
                if (ExtendedEuclidean(i, phi).gcd == 1)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        private static Tuple<ushort, int> ReturnEAndD(IReadOnlyList<ushort> possibleE, ushort phi)
        {
            ushort e;
            int d;

            do
            {
                e = possibleE[Random.Next(0, possibleE.Count)];

                d = ExtendedEuclidean(e % phi, phi).u1;
            } while (d < 0);

            return new Tuple<ushort, int>(e, d);
        }

        public int EncryptValue(byte plainByte, ushort e, ushort n)
        {
            return ModuloPow(plainByte, e, n);
        }

        public int DecryptValue(int encryptedByte, int d, ushort n)
        {
            return (byte)ModuloPow(encryptedByte, d, n);
        }
    }
}
