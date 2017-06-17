using System.Windows;
using RsaAnalyzer.Interfaces;
using RsaAnalyzer.Models;
using RsaAnalyzer.Responsibility;
using RsaAnalyzer.Utilities;
using System.Text;
using System;
using System.Collections.Generic;

namespace RsaAnalyzer.ViewModels
{
    internal class ShellViewModel : BaseViewModel
    {
        private Rsa _rsa;

        private uint _n;
        private uint _e;
        private long _d;

        private string _plainByte;
        private string _encryptedByte;
        private string _decryptedByte;

        private volatile bool _encrypting;
        private volatile bool _decrypting;
        private volatile bool _repeating;
        //ustawiona długość wiadomości na 100 znaków.
        private int tab_size=100;
        private long[] tab;

        public ShellViewModel()
        {
            _generatePrimes = new RelayCommand(p =>
            {
                PrepareRsa();

                OnPropertyChanged(nameof(PublicKey));
                OnPropertyChanged(nameof(PrivateKey));
            });
            _encryptByte = new RelayCommand(p =>
            {
                var result = new RsaProvider();

                if (E <= 0 || N <= 0 || D <= 0)
                {
                    MessageBox.Show("Please generate primes first!");
                }
                else if (!Encrypting)
                {
                    long[] tab_test = new long[tab_size];
                    for (int i = 0; i < result.EcryptValue2(PlainByte,E,N).Length; i++)
                    {
                        EncryptedByte = result.EcryptValue2(PlainByte, E, N)[i].ToString();
                        
                        tab_test[i] = Convert.ToInt64(EncryptedByte);
                    }
                    tab = tab_test;

                    OnPropertyChanged(nameof(tab));

                    Encrypting = true;
                }
                else
                {
                    Encrypting = false;
                }

            }, p => !Encrypting);

            _decryptByte = new RelayCommand(p =>
            {
                var result = new RsaProvider();

                if (E <= 0 || N <= 0 || D <= 0)
                {
                    MessageBox.Show("Please generate primes first!");
                }
                else if (!Decrypting)
                {
                    DecryptedByte = result.DecryptValue2(tab, D, N);

                    OnPropertyChanged(nameof(DecryptedByte));

                    Decrypting = true;
                }
                else
                {
                    Decrypting = false;
                }

            }, p => !Decrypting);

            _repeat = new RelayCommand(p =>
                {
                    Encrypting = false;
                    Decrypting = false;

                });
        }

        public bool Encrypting
        {
            get => _encrypting;
            set
            {
                _encrypting = value;
                OnPropertyChanged();

                GeneratePrimes.RaiseCanExecuteChanged();
                EncryptByte.RaiseCanExecuteChanged();
            }
        }

        public bool Decrypting
        {
            get => _decrypting;
            set
            {
                _decrypting = value;
                OnPropertyChanged();

                GeneratePrimes.RaiseCanExecuteChanged();
                DecryptByte.RaiseCanExecuteChanged();
            }
        }

        public bool Repeating
        {
            get => _repeating;
            set
            {
                _repeating = value;
                OnPropertyChanged();

                GeneratePrimes.RaiseCanExecuteChanged();
                EncryptByte.RaiseCanExecuteChanged();
                DecryptByte.RaiseCanExecuteChanged();
            }
        }

        public Rsa Rsa
        {
            get => _rsa;
            set
            {
                _rsa = value;
                OnPropertyChanged();
            }
        }

        public uint N
        {
            get => _n;
            set
            {
                _n = value;
                OnPropertyChanged();
            }
        }

        public uint E
        {
            get => _e;
            set
            {
                _e = value;
                OnPropertyChanged();
            }
        }

        public long D
        {
            get => _d;
            set
            {
                _d = value;
                OnPropertyChanged();
            }
        }

        public string PlainByte
        {
            get => _plainByte;
            set
            {
                //_plainByte = Convert.ToUInt16(value);
                _plainByte = value;
                OnPropertyChanged();
            }
        }

        public string EncryptedByte
        {
            get => _encryptedByte;
            set
            {
                _encryptedByte = value;
                OnPropertyChanged();
            }
        }

        public string DecryptedByte
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
            var rsaProvider = new RsaProvider();
            var values = rsaProvider.Run();
            N = values.Item1;
            E = values.Item2;
            D = values.Item3;

            Rsa = new Rsa(N, E, D);

            FileOperator.SaveRsaToFile("paniLodzia.txt", Rsa);
        }

        private RelayCommand _generatePrimes;
        private RelayCommand _encryptByte;
        private RelayCommand _decryptByte;
        private RelayCommand _repeat;

        public RelayCommand GeneratePrimes
        {
            get => _generatePrimes;
            set
            {
                _generatePrimes = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand EncryptByte
        {
            get => _encryptByte;
            set
            {
                _encryptByte = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand DecryptByte
        {
            get => _decryptByte;
            set
            {
                _decryptByte = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand Repeat
        {
            get => _repeat;
            set
            {
                _repeat = value;
                OnPropertyChanged();
            }
        }

        public string GenerateButtonContent => "Generate primes";

        public string EncryptButtonContent => "Encrypt message";

        public string DecryptButtonContent => "Decrypt message";

        public string RepeatButtonContent => "Clear";
        public string ClueMessage => "To restart for new message " +
                                     "\npress the button below";

        public string AboutMessage => "© 2017 " +
                                      "\nErnest Jędrzejczyk " +
                                      "\nKamil Reszka " +
                                      "\nKamil Witkowski " +
                                      "\nfor WULS";
    }
}
