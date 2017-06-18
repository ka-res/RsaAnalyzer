using System.Collections.Generic;

namespace RsaAnalyzer.Responsibility
{
    public class SieveOfEratosthenes
    {
        private const ushort N = 255;

        private bool[] _tableOfNumbers;
        private List<ushort> _primeNumbersList;

        public List<ushort> RunAlgorithm()
        {
            _primeNumbersList = new List<ushort>();
            _tableOfNumbers = new bool[N];

            for (uint i = 2; i < N; i++)
            {
                _tableOfNumbers[i] = true;
            }

            for (uint j = 2; j < N; j++)
            {
                if (!_tableOfNumbers[j]) continue;
                for (long p = 2; (p * j) < N; p++)
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
        }
    }
}
