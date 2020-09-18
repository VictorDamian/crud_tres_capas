using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    //ESTA CLASE SE ENCARGARA DE EJECUTAR LAS TRANSACCIONES QUE SE USARAN EN TODOS LOS REPOSITORIOS
    public abstract class MasterRepository:Repository
    {
        /// <summary>
        /// SI TENEMOS FUNCIONES COMUNES EN MUCHAS CLASES ES MEJOR CREAR UNA FUNCION GENERICA
        /// QUE SIRVA A TODOS
        /// 
        /// TOMAR EN CUENTA EL TIEMPO DE VIDA DE UN OJETO AL CREAR UNA INSTACIA
        /// *SEPARAR ESPCIO EN LA RAM *NO CREAR TANTAS INSTANCIAS
        /// 
        /// UTILIZAR LA FUNCION USING: EL OBJ SqlConnection EXISTIRA HASTA QUE LAS LINEAS DENTRO
        /// DE LOS CORCHETES TERMINEN DE EJECUTAR
        /// AL TERMINAR EL OBJ SERA DECHADO Y LIBERARA LOS RECURSOS(RAM)
        /// 
        /// ****investigar patron Dispose pattern
        /// </summary>


        protected List<SqlParameter> parameters;
        protected int ExecuteNonQuery(string transactSql) {
            //METODO PARA EJECUTAR COMANDOS DE NO CONSULTA, INSERTAR, ELIMINAR, EDITAR
            using (var connection = GetSqlConnection()) {
                connection.Open();
                using (var command = new SqlCommand()) {
                    command.Connection = connection;
                    command.CommandText = transactSql;
                    command.CommandType = CommandType.Text; 
                    /* No es recomendado usar procedimientos almacenados que contengan logica de
                     * negocio, ya que se sacrifica el mantenimiento a cambio de rendimiento */

                    //agregar parametros al comando de la lista  PARAMETROS que se enviara desdelos repositorios de "ENTITIES"
                    foreach (SqlParameter item in parameters) {
                        command.Parameters.Add(item);
                    }
                    int result = command.ExecuteNonQuery();
                    parameters.Clear();
                    return result;
                }
            }
        }
        //METODO PARA COMANDOS DE CONSULTA, LEER FILAS DE TABLA  y MOSTRAR DATOS DE UNA TABLA
        protected DataTable ExecuteReader(string transactSql) {
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transactSql;
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    using (var table = new DataTable()) {
                        table.Load(reader);//carga el resultado del lector a la tabla 
                        reader.Dispose();
                        return table;
                    }
                }
            }
        }
    }
}
