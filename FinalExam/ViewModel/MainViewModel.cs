using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FinalExam.Model;
using FinalExam.View;
using Helper;


namespace FinalExam.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public MainViewModel()
        {
           
        }
        #region fields for authors/book
        public int initialbook = 0, initial= 0, initialcus = 0;
        private ObservableCollection<AuthorModel> _authorList = new ObservableCollection<AuthorModel>();
        private ObservableCollection<BookModel> _authorBookList = new ObservableCollection<BookModel>();
        private ObservableCollection<BookModel> _allBookList = new ObservableCollection<BookModel>();
        private AuthorModel _currentAuthor;
        private AuthorModel _selectedAuthor;
        private ICommand _openAuthorViewCommand;
        private ICommand _closeAuthorViewCommand;
        private ICommand _saveAuthorCommand;
        private ICommand _deleteAuthorCommand;
        private ICommand _openAddBookCommand;
        private ICommand _closeAddBookCommand;
        private BookModel _currentBook;
        private BookModel _selectedBook;
        private ICommand _addbook;
        private ICommand _deletebook;
        private AuthorView AV;
        private BookView BV;
        #endregion
        #region fields for customers
        private CustomerView CV;
        private BuyView BuyV;
        private AddFundsView ADF;
        private CustomerModel _currentCustomer;
        private CustomerModel _selectedCustomer;
        private ObservableCollection<BookModel> _boughtBookList = new ObservableCollection<BookModel>();
        private ObservableCollection<CustomerModel> _customerList = new ObservableCollection<CustomerModel>();
        private ICommand _openCustomerView;
        private ICommand _closeCustomerView;
        private ICommand _openBuycommand;
        private ICommand _buybook;
        private ICommand _closebuyCommand;
        private ICommand _openAddFunds;
        private ICommand _closeAddFunds;
        private ICommand _addfunds;
        private ICommand _addCustomer;
        private ICommand _deleteCustomer;

        #endregion
        #region properties for customers

        public ObservableCollection<CustomerModel> CustomerList
        {
            get { return _customerList; }
            set
            {
                _customerList = value;
                OnPropertyChanged("CustomerList");
                OnPropertyChanged("BoughtBookList");
            }
        }

        public ObservableCollection<BookModel> BoughtBookList
        {
            get { return _boughtBookList; }
            set
            {
                _boughtBookList = value;
                OnPropertyChanged("BoughtBookList");
            }
        }
        public CustomerModel CurrentCustomer
        {
            get
            {
                return _currentCustomer;
            }
            set
            {
                if (value != _currentCustomer)
                {
                    _currentCustomer = value;
                    OnPropertyChanged("CurrentCustomer");
                }
            }
        }

        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (value != _selectedCustomer)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged("SelectedCustomer");
                }
            }
        }

        public ICommand AddCustomer
        {
            get
            {
                if (_addCustomer == null)
                {
                    _addCustomer = new RelayCommand(
                        param => SaveCustomer(),
                        param => true
                    );
                }
                return _addCustomer;
            }
        }

        private void SaveCustomer()
        {
            CustomerModel cst = new CustomerModel();
            cst.LastName = CurrentCustomer.LastName;
            cst.FirstName = CurrentCustomer.FirstName;
            cst.Contact = CurrentCustomer.Contact;
            cst.Gender = CurrentCustomer.Gender;
            cst.Address = CurrentCustomer.Address;
            cst.Wallet = CurrentCustomer.Wallet;
            cst.CustomerNumber = initialcus += 1;
            CustomerList.Add(cst);
            CloseCust();
        }

        public ICommand DeleteCustomer
        {
            get
            {
                if (_deleteCustomer == null)
                {
                    _deleteCustomer = new RelayCommand(
                        param => DeleteCust(),
                        param => (SelectedCustomer != null)
                    );
                }
                return (_deleteCustomer);
            }
        }

        private void DeleteCust()
        {
            CustomerList.Remove(_selectedCustomer);
        }

        public ICommand OpenCustomerView
        {
            get
            {
                if (_openCustomerView == null)
                {
                    _openCustomerView = new RelayCommand(
                        param => OpenCust(),
                        param => true
                    );
                }
                return (_openCustomerView);
            }
        }
        public ICommand CloseCustomerView
        {
            get
            {
                if (_closeCustomerView == null)
                {
                    _closeCustomerView = new RelayCommand(
                        param => CloseCust(),
                        param => true
                    );
                }
                return (_closeCustomerView);
            }
        }

        public ICommand OpenBuyCommand
        {
            get
            {
                if (_openBuycommand == null)
                {
                    _openBuycommand = new RelayCommand(
                        param => OpenBuy(),
                        param => (SelectedCustomer != null)
                    );
                }
                return (_openBuycommand);
            }
        }

        public ICommand CloseBuyCommand
        {
            get
            {
                if (_closebuyCommand == null)
                {
                    _closebuyCommand = new RelayCommand(
                        param => CloseBuy(),
                        param => true
                    );
                }
                return (_closebuyCommand);
            }
        }

        public ICommand OpenAddFunds
        {
            get
            {
                if (_openAddFunds == null)
                {
                    _openAddFunds = new RelayCommand(
                        param => OpenFunds(),
                        param => (SelectedCustomer != null)
                    );
                }
                return (_openAddFunds);
            }
        }

        public ICommand CloseAddFunds
        {
            get
            {
                if (_closeAddFunds == null)
                {
                    _closeAddFunds  = new RelayCommand(
                        param => CloseFunds(),
                        param => true
                    );
                }
                return (_closeAddFunds);
            }
        }

        public ICommand AddFunds
        {
            get
            {
                if (_addfunds == null)
                {
                    _addfunds = new RelayCommand(
                        param => AddWallet(),
                        param => true
                    );
                }
                return (_addfunds);
            }
        }

        public ICommand BuyBook
        {
            get
            {
                if (_buybook == null)
                {
                    _buybook = new RelayCommand(
                        param => Buying(),
                        param => true
                    );
                }
                return (_buybook);
            }
        }

        private void Buying()
        {
            SelectedCustomer.BooksBoughtList.Add(SelectedBook);
            if (SelectedBook.Price > SelectedCustomer.Wallet)
            {
                MessageBox.Show("Insufficient Funds! Add Funds Now!");
            }
            else
            {
                SelectedCustomer.Wallet -= SelectedBook.Price;
                MessageBox.Show("Book successfully bought!");
            }

            CloseBuy();
        }

        private void AddWallet()
        {
            SelectedCustomer.Wallet += SelectedCustomer.Deposit;
            SelectedCustomer = null;
            CloseFunds();
            MessageBox.Show("Funds Deposited!");
        }

        private void CloseFunds()
        {
            SelectedCustomer = null;
            ADF.Hide();
            
        }

        private void OpenFunds()
        {
            ADF = new AddFundsView();
            ADF.Show();
        }

        private void CloseBuy()
        {
            SelectedCustomer = null;
            SelectedBook = null;
            BuyV.Hide();
        }

        private void OpenBuy()
        {
            BuyV = new BuyView();
            BuyV.Show();
        }

        private void CloseCust()
        {
            SelectedCustomer = null;
            CurrentCustomer = null;
            CV.Hide();
        }

        private void OpenCust()
        {
            CurrentCustomer = new CustomerModel();
            CV = new CustomerView();
            CV.Show();
        }

        #endregion 

        #region properties for authors/book
        public ObservableCollection<AuthorModel> AuthorList
        {
            get { return _authorList; }
            set
            {
                _authorList = value;
                OnPropertyChanged("AuthorList");
                OnPropertyChanged("AuthorBookList");
                OnPropertyChanged("AllBookList");
            }
        }

        public ObservableCollection<BookModel> AllBookList
        {
            get { return _allBookList; }
            set
            {
                _allBookList  = value;
                OnPropertyChanged("AllBookList");
            }
        }
        public ObservableCollection<BookModel> AuthorBookList
        {
            get { return _authorBookList; }
            set
            {
                _authorBookList = value;
                OnPropertyChanged("AuthorBookList");
            }
        }

        public AuthorModel CurrentAuthor
        {
            get
            {
                return _currentAuthor;
            }
            set
            {
                if (value != _currentAuthor)
                {
                    _currentAuthor = value;
                    OnPropertyChanged("CurrentAuthor");
                    OnPropertyChanged("AuthorBookList");
                }
            }
        }

        public AuthorModel SelectedAuthor
        {
            get
            {
                return _selectedAuthor;
            }
            set
            {
                if (value != _selectedAuthor)
                {
                    _selectedAuthor = value;
                    OnPropertyChanged("SelectedAuthor");
                    OnPropertyChanged("AuthorBookList");
                }
            }
        }

        public BookModel CurrentBook
        {
            get
            {
                return _currentBook;
            }
            set
            {
                if (value != _currentBook)
                {
                    _currentBook = value;
                    OnPropertyChanged("CurrentBook");
                }
            }
        }

        public BookModel SelectedBook
        {
            get
            {
                return _selectedBook;
            }
            set
            {
                if (value != _selectedBook)
                {
                    (_selectedBook) = value;
                    OnPropertyChanged("SelectedBook");
                }
            }
        }
        public ICommand OpenAuthorViewCommand
        {
            get
            {
                if (_openAuthorViewCommand == null)
                {
                    _openAuthorViewCommand = new RelayCommand(
                        param => OpenAuthorView(),
                        param => true
                    );
                }
                return (_openAuthorViewCommand);
            }
        }

        public ICommand CloseAuthorViewCommand
        {
            get
            {
                if (_closeAuthorViewCommand == null)
                {
                    _closeAuthorViewCommand = new RelayCommand(
                        param => CloseAuthorView(),
                        param => true
                    );
                }
                return (_closeAuthorViewCommand);
            }
        }

        public ICommand SaveAuthorCommand
        {
            get
            {
                if (_saveAuthorCommand == null)
                {
                    _saveAuthorCommand  = new RelayCommand(
                        param => AuthorChecker(),
                        param => true
                    );
                }
                return _saveAuthorCommand;
            }
        }

        public ICommand DeleteAuthorCommand
        {
            get
            {
                if (_deleteAuthorCommand == null)
                {
                    _deleteAuthorCommand = new RelayCommand(
                        param => DeleteAuthor(),
                        param => (SelectedAuthor!=null)
                    );
                }
                return _deleteAuthorCommand;
            }
        }

        public ICommand OpenAddBookCommand
        {
            get
            {
                if (_openAddBookCommand == null)
                {
                    _openAddBookCommand = new RelayCommand(
                        param => OpenBookView(),
                        param => true
                    );
                }
                return _openAddBookCommand;
            }
        }

        public ICommand CloseBookViewCommand
        {
            get
            {
                if (_closeAddBookCommand  == null)
                {
                    _closeAddBookCommand = new RelayCommand(
                        param => CloseBookView(),
                        param => true
                    );
                }
                return _closeAddBookCommand;
            }
        }

        public ICommand AddBookCommand
        {
            get
            {
                if (_addbook == null)
                {
                    _addbook  = new RelayCommand(
                        param => SaveBook(),
                        param => true
                    );
                }
                return _addbook;
            }
        }

        public ICommand DeleteBookCommand
        {
            get
            {
                if (_deletebook == null)
                {
                    _deletebook = new RelayCommand(
                        param => DeleteBook(),
                        param => (SelectedBook!=null)
                    );
                }
                return _deletebook;
            }
        }



        #endregion
        #region helpers for authors/books
        private void OpenBookView()
        {
            CurrentBook = new BookModel();
            BV = new BookView();
            BV.Show();
        }

        private void CloseBookView()
        {
            SelectedBook = null;
            BV.Close();
        }
        private void SaveBook()
        {
            if (SelectedAuthor != null)
            {
                BookModel bk = new BookModel();
                bk.Title = CurrentBook.Title;
                bk.Publisher = CurrentBook.Publisher;
                bk.BookType = CurrentBook.BookType;
                bk.Price = CurrentBook.Price;
                bk.BookNumber = initialbook += 1;
                AuthorBookList.Add(bk);
                AllBookList.Add(bk);
            }
            else
            {
                MessageBox.Show("Must create author first!");
            }

            SelectedBook = null;
            BV.Close();
        }
        private void DeleteBook()
        {
            AuthorBookList.Remove(SelectedBook);
            AllBookList.Remove(SelectedBook);
            CurrentBook = null;
            SelectedBook = null;
            MessageBox.Show("Book Deleted");
        }
        private void OpenAuthorView()
        {
            if (SelectedAuthor == null)
            {
                CurrentAuthor = new AuthorModel();
            }
            else
            {
                var q = new AuthorModel();
                q = _selectedAuthor;
                CurrentAuthor = SelectedAuthor ?? new AuthorModel();
                CurrentAuthor.LastName = q.LastName;
                CurrentAuthor.FirstName = q.FirstName;
                CurrentAuthor.AuthorNumber = q.AuthorNumber;
                CurrentAuthor.BookList = q.BookList;
                AuthorBookList = q.BookList;
                CurrentAuthor.Birthdate = q.Birthdate;
                CurrentAuthor.Gender = q.Gender;
            }
            AV = new AuthorView();
            AV.Show();
        }
        private void CloseAuthorView()
        {
            SelectedAuthor = null;
            CurrentAuthor = null;
            AV.Hide();
        }
        private void DeleteAuthor()
        {
            AuthorList.Remove(_selectedAuthor);
            SelectedAuthor = null;
        }

        private void UpdateAuthor()
        {
            _selectedAuthor.AuthorNumber = _currentAuthor.AuthorNumber;
            _selectedAuthor.BookList = _currentAuthor.BookList;
            _selectedAuthor.LastName = _currentAuthor.LastName;
            _selectedAuthor.FirstName = _currentAuthor.FirstName;
            _selectedAuthor.Gender = _currentAuthor.Gender;
            _selectedAuthor.Birthdate = _currentAuthor.Birthdate;
        }
        private void SaveAuthor()
        {
            AuthorModel aut = new AuthorModel();
            aut.LastName = CurrentAuthor.LastName;
            aut.FirstName = CurrentAuthor.FirstName;
            aut.Birthdate = CurrentAuthor.Birthdate;
            aut.Gender = CurrentAuthor.Gender;
            aut.AuthorNumber = initial += 1;
            AuthorList.Add(aut);
            AV.Close();
        }

        private void AuthorChecker()
        {
            if (SelectedAuthor == null)
            {
                SaveAuthor();
            }
            else
            {
                if (CurrentAuthor.Equals(SelectedAuthor))
                {
                    MessageBox.Show("Updated");
                }
                else
                {
                    UpdateAuthor();
                }
            }
            SelectedAuthor = null;
            CurrentAuthor = null;
            AuthorBookList = null;
            AV.Close();
        }
        #endregion 

    }
}
