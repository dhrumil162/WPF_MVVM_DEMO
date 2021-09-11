using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WPF.MVVM.APP.ViewModel;

namespace WPF.MVVM.APP.Model
{
    public class EmployeeRecord : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _employeeName;
        public string EmployeeName
        {
            get
            {
                return _employeeName;
            }
            set
            {
                _employeeName = value;
                OnPropertyChanged("EmployeeName");
            }
        }

        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        private string _mobile;
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
                OnPropertyChanged("Mobile");
            }
        }

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        private int _salary;
        public int Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                _salary = value;
                OnPropertyChanged("Salary");
            }
        }

        private ObservableCollection<EmployeeRecord> _employeeRecords;
        public ObservableCollection<EmployeeRecord> EmployeeRecords
        {
            get
            {
                return _employeeRecords;
            }
            set
            {
                _employeeRecords = value;
                OnPropertyChanged("EmployeeRecords");
            }
        }

        private void EmployeeModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("EmployeeRecords");
        }
    }
}
