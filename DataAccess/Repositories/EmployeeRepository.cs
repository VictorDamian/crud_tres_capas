using System;
using System.Collections.Generic;
using DataAccess.Contracts;
using DataAccess.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    //AQUI SE IMPLEMENTARA LA INTERFAZ
    public class EmployeeRepository : MasterRepository, IEmployeeRepository
    {
        //CAMPOS O ATRIBUTOS
        private string selectAll;
        private string update;
        private string delete;
        private string insert;
        //PROPIEDADES-..


        //CONSTRUCTOR
        public EmployeeRepository() {
            //CARGAMOS PARAMETROS 
            selectAll = "select * from Employee";
            insert = "insert into Employee values(@idNumber, @name, @mail, @birthday)";
            update = "update Employee set IdNumber=@idNumber, Name=@name, Mail=@mail, Birthday=@birthday where idPk=@idPk";
            delete = "delete from Employee where idPk=@idPk";
        }

        public int Add(Employee entity)
        {
            //EJECUTAMOS
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idNumber", entity.idNumber));
            parameters.Add(new SqlParameter("@name", entity.name));
            parameters.Add(new SqlParameter("@mail", entity.mail));
            parameters.Add(new SqlParameter("@birthday", entity.birthday));
            return ExecuteNonQuery(insert);
        }

        public int Edit(Employee entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idPk",entity.idPk));
            parameters.Add(new SqlParameter("@idNumber", entity.idNumber));
            parameters.Add(new SqlParameter("@name", entity.name));
            parameters.Add(new SqlParameter("@mail", entity.mail));
            parameters.Add(new SqlParameter("@birthday", entity.birthday));
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Employee> GetAll()
        {
/* " VAR " ES UNA VARIABLE IMPLICITA, es decir puede ser cualquier tipo de dato dependiendo que valor se asigne */
/* En este caso DataTable */
            var tableResult = ExecuteReader(selectAll);
            var listEmployee = new List<Employee>();
            foreach (DataRow item in tableResult.Rows) {//por cada iteracion agregamos un nuevo empleado a la lista de empleados
                listEmployee.Add(new Employee {
                    idPk = Convert.ToInt32(item[0]),
                    idNumber = item[1].ToString(),
                    name = item[2].ToString(),
                    mail = item[3].ToString(),
                    birthday =Convert.ToDateTime(item[4])
                });
            }
            return listEmployee;
        }

        public int Remove(int idPk)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idPk", idPk));
            return ExecuteNonQuery(delete);
        }

        //METODOS O COMPORTAMIENTOS DEL OBJETO
    }
}
