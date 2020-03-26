using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Model
{
    class AuthorModel:PersonModel
    {
        #region fields
        private int _authornumber;
        public int _initalauthnumber = 0;
        private ObservableCollection<BookModel> _bookList = new ObservableCollection<BookModel>();
        #endregion

        #region properties

        public ObservableCollection<BookModel> BookList
        {
            get { return _bookList; }
            set
            {
                if (value != _bookList)
                {
                    _bookList = value;
                    OnPropertyChanged("BookList");
                }
            }
        }
        public int AuthorNumber
        {
            get { return _authornumber; }
            set
            {
                if (value != _authornumber)
                {
                    _authornumber  = value;
                    OnPropertyChanged("AuthorNumber");
                }
            }
        
        }

        #endregion
    }
}
