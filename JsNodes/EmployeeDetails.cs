using System;
using System.Collections.Generic;
using System.Text;

namespace JsNodes
{
    public class EmployeeDetails
    {


        public int id { get; set; }
        public string name { get; set; }

        public string salary { get; set; }

        public EmployeeDetails(string name, string salary)
        {

            this.salary = salary;
            this.name = name;
        }

    }
   
}
