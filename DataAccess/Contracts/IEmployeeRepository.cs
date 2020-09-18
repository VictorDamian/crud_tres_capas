using DataAccess.Entities;

namespace DataAccess.Contracts
{
    //requiere un parametro, la clase a utilizar
    public interface IEmployeeRepository:IGEnericRepository<Employee>
    {
        //Other methods, example: --- IEnumerable<Employee> GetBySalary();

    }
}
