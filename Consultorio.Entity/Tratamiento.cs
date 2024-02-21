using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.Entity
{
    public class Tratamiento
    {
        public int IdTratamiento { get; set; }
        public string Descripcion { get; set; }
        public int IdAtencion { get; set; }
    }
}
