using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Lab2.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Microsoft.Identity.Client.NativeInterop;

namespace Lab3.WpfApplication.ViewModels
{
    public class MainWindowViewModel : ObservableObject, INotifyPropertyChanged
    {
        private BankDbContext _db;
        public MainWindowViewModel()
        {
            _db = new BankDbContext();
            SelectBankCommand = new RelayCommand(LoadAccounts);
        }

        private Bank[] _banks;

        public Bank[] Banks => _banks;

        public void Load()
        {
            _banks = _db.Banks.ToArray();
        }

        private List<Account> _accounts = new List<Account>();
        public List<Account> Accounts
        {
            get
            {
                return _accounts;
            }
            set
            {
                _accounts = value;
                OnPropertyChanged();
            }

            public Bank SelectedBank {  get; set; }
        }

        public ICommand SelectBankCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void LoadAccounts()
        {
            if (SelectedBank == null)
            {
                return;
            }

            Accounts = _db.Accounts.Where(b => b.Bank.Id == SelectedBank.Id).ToList();
        }
    }
}
