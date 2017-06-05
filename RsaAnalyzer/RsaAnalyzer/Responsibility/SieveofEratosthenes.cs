using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaAnalyzer.Responsibility
{
    public class SieveOfEratosthenes
    {
        // private long n = 9 * 1234567l; // max range of primes to look for

        //private byte n = 255;

        private ushort _n = 255;

        private bool[] _tableOfNumbers;
        private List<ushort> _primeNumbersList;

        public List<ushort> RunAlgorithm()
        {
            _primeNumbersList = new List<ushort>();
            _tableOfNumbers = new bool[_n];//by default they're all false

            for (uint i = 2; i < _n; i++)
            {
                _tableOfNumbers[i] = true;//set all numbers to true
            }
            //weed out the non primes by finding mutiples 
            for (uint j = 2; j < _n; j++)
            {
                if (!_tableOfNumbers[j]) continue;
                for (long p = 2; (p * j) < _n; p++)
                {
                    _tableOfNumbers[p * j] = false;
                }
            }

            for (ushort i = 0; i < _tableOfNumbers.LongLength; i++)
            {
                if (_tableOfNumbers[i])
                {
                    _primeNumbersList.Add(i);
                }
            }

            return _primeNumbersList;
            //Uptill here e[] sorta of contains a list of primes
            //the index represent the actual number and the value at the index represents if the number is prime
            //Example:
            //e[4], e[100] will all be false since 2,4,100 are not primes
            //e[5], e[7], e[11], e[13] will all be true because 5,7,11,13 are all prime numbers
        }
    }
}
