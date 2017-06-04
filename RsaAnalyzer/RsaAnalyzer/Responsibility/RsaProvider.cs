﻿using System;
using System.Collections.Generic;

namespace RsaAnalyzer.Utilities
{
    internal class RsaProvider
    {
        private static readonly Random Random = new Random();

        public static Tuple<uint, uint, long> Run()
        {
            Responsibility.SieveofEratosthenes sieveofEratosthenes =
                new Responsibility.SieveofEratosthenes();

            var primes = sieveofEratosthenes.RunAlgorithm();

            ushort p = primes[Random.Next(0, primes.Count)],
                q = primes[Random.Next(0, primes.Count)];

            var n = ReturnN(p, q); // uint
            var phi = ReturnPhi(p, q); // uint
            var possibleE = ReturnPossibleE(phi); // uint

            var eAndD = ReturnEAndD(possibleE, phi);
            var e = eAndD.Item1;
            var d = eAndD.Item2;

            return new Tuple<uint, uint, long>(n, e, d);
        }

        private static long ModuloPow(long value, long pow, long modulo)
        {
            long result = value;
            for (long i = 0; i < pow - 1; i++)
            {
                result = (result * value) % modulo;
            }
            return result;
        }

        private static ExtendedEuclideanResult ExtendedEuclidean(long a, long b)
        {
            long u1 = 1;
            var u3 = a;
            long v1 = 0;
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
            public long u1;
            public long u2;
            public long gcd;
        }

        private static uint ReturnN(ushort p, ushort q)
        {
            return (uint)(p * q);
        }

        private static uint ReturnPhi(ushort p, ushort q)
        {
            return (uint)((p - 1) * (q - 1));
        }

        static List<uint> ReturnPossibleE(uint phi)
        {
            var result = new List<uint>();

            for (uint i = 2; i < phi; i++)
            {
                if (ExtendedEuclidean(i, phi).gcd == 1)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        private static Tuple<uint, long> ReturnEAndD(IReadOnlyList<uint> possibleE, uint phi)
        {
            uint e;
            long d;

            do
            {
                e = possibleE[Random.Next(0, possibleE.Count)];

                d = ExtendedEuclidean(e % phi, phi).u1;
            } while (d < 0);

            return new Tuple<uint, long>(e, d);
        }

        public long EncryptValue(ushort plainByte, uint e, uint n)
        {
            return ModuloPow(plainByte, e, n);
        }

        public long DecryptValue(long encryptedByte, long d, uint n)
        {
            return (ushort)ModuloPow(encryptedByte, d, n);
        }
    }
}
