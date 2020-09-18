using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    //ESTA SE ENCARGA DE LA CONEXION con la BD
    //agrega referencia a DataAcces: system.configuration, de esta manera podemos acceder a la cadena de app.config
    public abstract class Repository
    {
        private readonly string connectionString;//encapsulada y solo lectura por la clase
        public Repository()//contructor
        {
            connectionString = ConfigurationManager.ConnectionStrings["connMycompany"].ToString();
        }
        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection (connectionString);
        }
    }
}
