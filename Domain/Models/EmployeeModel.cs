using System;
using System.Collections.Generic;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using Domain.ValuesObjects;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class EmployeeModel:IDisposable
    {
        /* PARA VALIDACIONES AGREGAR REFERENCIA A DOMAIN:  */
        //aqui van los campos de las tablas
        private int idPk;
        private string idNumber;
        private string name;
        private string mail;
        private DateTime birthday;
        private int age;

        private IEmployeeRepository employeeRepository;

        //get -- el valor del State puede ser asignado desde fuera de la clase, pero el valor solo de la propia clase
        public EntityState State { private get; set; }

        //lista global para empleados
        private List<EmployeeModel> listEmployees;

        //PROPIEDADES DE LOS CAMPOS || MODELO VISTA || VALIDACIONES DE DATOS || las adv no se muestran por si solas need a class(helps)
        public int IdPk { get => idPk; set => idPk = value; }

        [Required(ErrorMessage ="The fiel identification number is requerid")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Identification number must be only numbers")]
        [StringLength(maximumLength: 10, MinimumLength = 4, ErrorMessage ="Identification number must be 10 digits")]

        public string IdNumber { get => idNumber; set => idNumber = value; }

        [Required]
        [RegularExpression("^[a-zA-Z ]+$",ErrorMessage ="The fiel name must be only letter")]
        [StringLength(maximumLength: 100, MinimumLength = 4)]

        public string Name { get => name; set => name = value; }

        [Required]
        [EmailAddress]
        public string Mail { get => mail; set => mail = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public int Age { get => age; private set => age = value; }


        public EmployeeModel()
        {
            employeeRepository = new EmployeeRepository();
        }
        public string saveChange()
        {
            string message=null;
            try
            {
                var employeeDataModel = new Employee();
                employeeDataModel.idPk = idPk;
                employeeDataModel.idNumber = idNumber;
                employeeDataModel.name = name;
                employeeDataModel.mail = mail;
                employeeDataModel.birthday =birthday;

                switch (State)
                {
                    case EntityState.Added:
                        employeeRepository.Add(employeeDataModel);
                        message = "Seccessfully record";
                        break;
                    case EntityState.Modified:
                        employeeRepository.Edit(employeeDataModel);
                        message = "Seccessfully edited";
                        break;
                    case EntityState.Deleted:
                        employeeRepository.Remove(idPk);
                        message = "Seccessfully deleted";
                        break;
                }
            }
            catch(Exception ex){
                System.Data.SqlClient.SqlException sqlExe = ex as System.Data.SqlClient.SqlException;
                if (sqlExe!=null&&sqlExe.Number==2627)
                {
                    message = "Duplicate record";
                }else{ message = ex.ToString();}
            }
            return message;
        }

        /// obtener todos los registros
        /// <GetAll>
        /// CADA VEZ QUE PULSEMOS UNA TECLA INVOCARA ESTE METODO INVOCARA EL METODO DEL
        /// REPOSITORIO Y ACCEDER A LA BD
        /// POR LO QUE CONSUMIRA ANCHO DE BANDA, PARA ELLO ES MEJOR GUARDAR LA LISTA DE
        /// EMPLEADOS EN UNA VARIABLE GLOBAL
        /// </getall>

        public List<EmployeeModel> getAll()
        {
            var employeeDataModel = employeeRepository.GetAll();
            listEmployees = new List<EmployeeModel>();

            foreach (Employee item in employeeDataModel) {
                var birthDate = item.birthday;
                listEmployees.Add(new EmployeeModel {
                    idPk = item.idPk,
                    idNumber = item.idNumber,
                    name = item.name,
                    mail = item.mail,
                    birthday = item.birthday,
                    age = CalculateAge(birthDate)
                });
            }
            return listEmployees;
        }

        public IEnumerable<EmployeeModel> FindById(string filter)
        {
            //contains es igual al operador sql LIKE
            //UTILIZAR LA VARIABLE LISTA--- DE ESTA MENERA LA CONSULTA NO LA HARA A LA BD COMO GETALL
            //PARA EVADIR LAS MAYSUCULAS
            return listEmployees.FindAll(e => e.idNumber.IndexOf(filter) >=0 || e.name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        //CALSE INDEPENDIENTE EN OBJETOS DE VALORES
        // CALCULOS DE VALORES ------
        public int CalculateAge(DateTime date)
        {
            DateTime dateNow = DateTime.Now;
            return dateNow.Year - date.Year;
        }

        public void Dispose()
        {
            
        }
    }
}


