using System;

namespace DataAccess.Entities
{
    public class Employee
    {
        //aqui van los campos de las tablas
        public int idPk { get; set; }
        public string idNumber { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public DateTime birthday { get; set; }
    }
}
