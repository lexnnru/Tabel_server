﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data
{
    public class Employee
    {
        public List<Employee> GetAllEmployees(Model.DataBase_manager db, DateTime date)
        {
            List<Employee> employees = new List<Employee>();
            List<List<string>> Lists = db.GetAllRowFromTable(db.NameOfTablenamberUserTable);
            Lists.ForEach(i => {
                employees.Add(new Employee()
                {
                    tabelNumber = i[0],
                    family = i[1],
                    name = i[2],
                    parentName = i[3],
                    dataOfEmployment = Convert.ToInt64(i[4]),
                    dateOfDismiss= Convert.ToInt64(i[5]),
                    salary = Convert.ToInt32(i[6]),
                    post=i[7],


                });
            });
            return employees;
        }
        public string tabelNumber {get; set;}
        public string family { get; set; }
        public string name { get; set; }
        public string parentName { get; set; }
        public string mail { get; set; }
        public Int64 dataOfEmployment { get; set; }
        public Int64 dateOfDismiss { get; set; }
        public int salary { get; set; }
        public string post { get; set; }
    }
}
