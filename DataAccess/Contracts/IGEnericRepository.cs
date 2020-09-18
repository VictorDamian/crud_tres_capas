using System.Collections.Generic;

namespace DataAccess.Contracts
{
    //reccibira un parametro generico
    public interface IGEnericRepository<Entity> where Entity:class
    {
        //declaracion de metodos comunes que sirve para todos los repositorios que la implementen
        int Add(Entity entity);
        int Edit(Entity entity);
        int Remove(int idPk);
        
        IEnumerable<Entity> GetAll();//metodo enumerable para obtener todos los registros de la entidad 

        /* cualquier clase o interfaz que implemente esta interface generico estara obligado a implementar todos los metodos*/
    }
}
