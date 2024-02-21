using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.Entity
{
    public class Atencion
    {
        public int IdAtencion { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public string DniPaciente { get; set; }
        public string DniMedico{ get; set; }
        public string CorreoPaciente{ get; set; }
        public string CorreoMedico { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreMedico { get; set; }
        public string Diagnostico { get; set; }
        public string DescDiagnostico { get; set; }
        public string DescTratamiento { get; set; }
        public IEnumerable<MedicamentoAtencion> Medicamentos { get; set; }

    }
}
