using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Helps
{
    //REQUIERE REFERENCIA A LA LIBERIA: VALIDACIONES DE DATOS || using System.ComponentModel.DataAnnotations;
    public class DataValidation
    {
        private ValidationContext context; //campo de la validacion
        private List<ValidationResult> results ;//lista para los resultados de la validacion
        private bool valid; //un campo booleano para verrificar que es valido 

        private string message;

        //en el constructor recibiremos un obj de intancia la cual se validara
        public DataValidation(object instance)
        {
            context = new ValidationContext(instance);
            results = new List<ValidationResult>();
            //determinamos si el valido o no
            valid = Validator.TryValidateObject(instance, context, results, true);
        }

        public bool Validate()
        {
            //si no es valido, recorremos la lista de resultado y enviamos el msj
            if (valid==false)
            {
                foreach (ValidationResult item in results)
                {
                    message += item.ErrorMessage + "\n";
                }
                System.Windows.Forms.MessageBox.Show(message);
            }
            return valid;
        }
    }
}
