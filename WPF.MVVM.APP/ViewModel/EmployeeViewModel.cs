using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF.MVVM.APP.DataAccess;
using WPF.MVVM.APP.Model;

namespace WPF.MVVM.APP.ViewModel
{
    public class EmployeeViewModel
    {
        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private EmployeeRepository _repository;
        private tblEmployee _empEntity = null;
        public EmployeeRecord EmployeeRecord { get; set; }
        public dbWPFMVVMAppEntities db { get; set; }

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), null);

                return _resetCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(), null);

                return _saveCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((int)param), null);

                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeleteStudent((int)param), null);

                return _deleteCommand;
            }
        }

        public EmployeeViewModel()
        {
            _empEntity = new tblEmployee();
            _repository = new EmployeeRepository();
            EmployeeRecord = new EmployeeRecord();
            GetAll();
        }

        public void ResetData()
        {
            EmployeeRecord.Id = 0;
            EmployeeRecord.EmployeeName = string.Empty;
            EmployeeRecord.Address = string.Empty;
            EmployeeRecord.Mobile = string.Empty;
            EmployeeRecord.Age = 0;
            EmployeeRecord.Salary = 0;
        }

        public void DeleteStudent(int id)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Employee", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.RemoveEmployee(id);
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                }
            }
        }

        public void SaveData()
        {
            if (EmployeeRecord != null)
            {
                _empEntity.EmployeeName = EmployeeRecord.EmployeeName;
                _empEntity.Address = EmployeeRecord.Address;
                _empEntity.Mobile = EmployeeRecord.Mobile;
                _empEntity.Age = EmployeeRecord.Age;
                _empEntity.Salary = EmployeeRecord.Salary;

                try
                {
                    if (EmployeeRecord.Id <= 0)
                    {
                        _repository.AddEmployee(_empEntity);
                        MessageBox.Show("New record successfully saved.");
                    }
                    else
                    {
                        _empEntity.Id = EmployeeRecord.Id;
                        _repository.UpdateEmployee(_empEntity);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                    ResetData();
                }
            }
        }

        public void EditData(int id)
        {
            var model = _repository.Get(id);
            EmployeeRecord.Id = model.Id;
            EmployeeRecord.EmployeeName = model.EmployeeName;
            EmployeeRecord.Address = model.Address;
            EmployeeRecord.Mobile = model.Mobile;
            EmployeeRecord.Age = (int)model.Age;
            EmployeeRecord.Salary = (int)model.Salary;
        }

        public void GetAll()
        {
            EmployeeRecord.EmployeeRecords = new ObservableCollection<EmployeeRecord>();
            _repository.GetAll().ForEach(data => EmployeeRecord.EmployeeRecords.Add(new EmployeeRecord()
            {
                Id = data.Id,
                EmployeeName = data.EmployeeName,
                Address = data.Address,
                Mobile = data.Mobile,
                Age = Convert.ToInt32(data.Age),
                Salary = Convert.ToInt32(data.Salary),
            }));
        }
    }
}