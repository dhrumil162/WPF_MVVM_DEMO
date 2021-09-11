using System.Collections.Generic;
using System.Linq;
using WPF.MVVM.APP.Model;

namespace WPF.MVVM.APP.DataAccess
{
    public class EmployeeRepository
    {
        private dbWPFMVVMAppEntities db = null;

        public EmployeeRepository()
        {
            db = new dbWPFMVVMAppEntities();
        }

        public tblEmployee Get(int id)
        {
            return db.tblEmployees.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<tblEmployee> GetAll()
        {
            return db.tblEmployees.ToList();
        }

        public void AddEmployee(tblEmployee emp)
        {
            if (emp != null)
            {
                db.tblEmployees.Add(emp);
                db.SaveChanges();
            }
        }

        public void UpdateEmployee(tblEmployee emp)
        {
            var objEmployee = this.Get(emp.Id);
            if (objEmployee != null)
            {
                objEmployee.EmployeeName = emp.EmployeeName;
                objEmployee.Address = emp.Address;
                objEmployee.Mobile = emp.Mobile;
                objEmployee.Age = emp.Age;
                objEmployee.Salary = emp.Salary;

                db.Entry(objEmployee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void RemoveEmployee(int id)
        {
            var objEmployee = this.Get(id);
            if (objEmployee != null)
            {
                db.Entry(objEmployee).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}