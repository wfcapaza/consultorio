using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.Entity
{
    public class DiagnosticoEntity
    {
        public int IdDiagnostico { get; set; }
        public string Diagnostico { get; set; }
        public string Descripcion { get; set; }
        public int IdAtencion { get; set; }
    }
}
