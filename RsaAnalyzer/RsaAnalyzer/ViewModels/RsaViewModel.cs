using System.Windows.Input;
using RsaAnalyzer.Models;
using RsaAnalyzer.Utilities;

namespace RsaAnalyzer.ViewModels
{
    internal class RsaViewModel : BaseViewModel
    {
        private Rsa _rsa;
        private ushort _n;
        private ushort _e;
        private int _d;
        private byte _plainByte;
        private int _encryptedByte;
        private int _decryptedByte;

        public Rsa Rsa
        {
            get => _rsa;
            set
            {
                _rsa = value;
                OnPropertyChanged();
            }
        }

        public ushort N
        {
            get => _n;
            set
            {
                _n = value;
                OnPropertyChanged();
            }
        }

        public ushort E
        {
            get => _e;
            set
            {
                _e = value;
                OnPropertyChanged();
            }
        }

        public int D
        {
            get => _d;
            set
            {
                _d = value;
                OnPropertyChanged();
            }
        }

        public byte PlainByte
        {
            get => _plainByte;
            set
            {
                _plainByte = value;
                OnPropertyChanged();
            }
        }

        public int EncryptedByte
        {
            get => _encryptedByte;
            set
            {
                _encryptedByte = value;
                OnPropertyChanged();
            }
        }

        public int DecryptedByte
        {
            get => _decryptedByte;
            set
            {
                _decryptedByte = value;
                OnPropertyChanged();
            }
        }

        public string PublicKey => $"Pair: n = {N}, e = {E}";

        public string PrivateKey => $"Pair: d = {D}, n = {N}";

        public void PrepareRsa()
        {
            var values = RsaProvider.Run();
            N = values.Item1;
            E = values.Item2;
            D = values.Item3;

            Rsa = new Rsa(N, E, D);
            FileOperator.SaveToFile("paniLodzia.txt", Rsa);

            OnPropertyChanged(nameof(PublicKey));
            OnPropertyChanged(nameof(PrivateKey));
        }

        private ICommand _generatePrimes;
        private ICommand _encryptByte;
        private ICommand _decryptByte;

        public ICommand GeneratePrimes
        {
            get
            {
                return _generatePrimes ?? (_generatePrimes = new RelayCommand(
                           param => { PrepareRsa(); }
                       ));
            }
        }

        public ICommand EncryptByte
        {
            get
            {
                var result = new RsaProvider();
                return _encryptByte ?? (_encryptByte = new RelayCommand(
                           param => { result.EncryptValue(PlainByte, E, N);  }
                       ));
            }
        }

        public ICommand DecryptByte
        {
            get
            {
                var result = new RsaProvider();
                return _decryptByte ?? (_decryptByte = new RelayCommand(
                           param => { result.DecryptValue(EncryptedByte, D, N);  }
                       ));
            }
        }
    }
}
