using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Model
{
    class CustomerModel: PersonModel
    {
        #region fields
        private int _customernumber;
        private string _address;
        private int _contact;
        private int _wallet;
        private int _deposit;
        private ObservableCollection<BookModel> _booksboughtlist = new ObservableCollection<BookModel>();
        #endregion
        #region properties

        public ObservableCollection<BookModel> BooksBoughtList
        {
            get { return _booksboughtlist; }
            set
            {
                if (value != _booksboughtlist)
                {
                    (_booksboughtlist) = value;
                    OnPropertyChanged("BooksBoughtList");
                }
            }
        }
        
        public int Deposit
        {
            get { return _deposit; }
            set
            {
                if (value != _deposit)
                {
                    (_deposit) = value;
                    OnPropertyChanged("Deposit");
                }
            }
        }
        public int CustomerNumber
        {
            get { return _customernumber; }
            set
            {
                if (value != _customernumber)
                {
                    (_customernumber) = value;
                    OnPropertyChanged("CustomerNumber");
                }
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                if (value != _address)
                {
                    (_address) = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public int Contact
        {
            get { return _contact; }
            set
            {
                if (value != _contact)
                {
                    (_contact) = value;
                    OnPropertyChanged("Contact");
                }
            }
        }

        public int Wallet
        {
            get { return _wallet; }
            set
            {
                if (value != _wallet)
                {
                    (_wallet ) = value;
                    OnPropertyChanged("Wallet");
                }
            }
        }
        #endregion
    }
}
