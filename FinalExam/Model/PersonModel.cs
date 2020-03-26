using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;


namespace FinalExam.Model
{
    class PersonModel : ObservableObject
    {
        #region Fields

        private string _lastName;
        private string _firstName;
        private DateTime _birthdate = DateTime.Now;
        private string _gender;
        private int _age;


        #endregion

        #region Properties

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        public DateTime Birthdate
        {
            get { return _birthdate; }
            set
            {
                if (value != _birthdate)
                {
                    _birthdate = value;
                    OnPropertyChanged("Birthdate");
                }
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                if (value != _gender)
                {
                    _gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }

        public int Age
        {
            get
            {
                TimeSpan diff = Birthdate - DateTime.Now;
                _age = Convert.ToInt32(Math.Floor(diff.TotalDays / 365));
                return Math.Abs(_age);
            }
        }
    }
}

#endregion
