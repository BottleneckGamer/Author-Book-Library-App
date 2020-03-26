using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Helper;

namespace FinalExam.Model
{
    class BookModel:ObservableObject
    {
        #region fields

        private int _booknumber;
        public int _initialbooknumber = 0;
        private string _title;
        private string _publisher;
        private int _price;
        private string _booktype;

        #endregion

        #region properties

        public int BookNumber
        {
            get
            {
                
                return _booknumber;
            }
            set
            {
                if (value != _booknumber)
                {
                    _booknumber = value;
                    OnPropertyChanged("BookNumber");
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Publisher
        {
            get { return _publisher; }
            set
            {
                if (value != _publisher)
                {
                    _publisher = value;
                    OnPropertyChanged("Publisher");
                }
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public string BookType
        {
            get { return _booktype; }
            set
            {
                if (value != _booktype)
                {
                    _booktype = value;
                    OnPropertyChanged("BookType");
                }
            }
        }
        #endregion
    }
}
