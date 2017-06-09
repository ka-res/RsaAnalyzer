using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsaAnalyzer.Responsibility;

namespace RsaAnalyzer.Interfaces
{
    public interface IRsaAnalyzer
    {
        Tuple<uint, uint, long> Run();
        long ModuloPow(long value, long pow, uint modulo);
        RsaProvider.ExtendedEuclideanResult ExtendedEuclidean(long a, long b);
        uint ReturnN(ushort p, ushort q);
        uint ReturnPhi(ushort p, ushort q);
        List<uint> ReturnPossibleE(uint phi);
        Tuple<uint, long> ReturnEAndD(IReadOnlyList<uint> possibleE, uint phi);
        long EncryptValue(ushort plainByte, uint e, uint n);
        int DecryptValue(long encryptedByte, long d, uint n);
    }
}
