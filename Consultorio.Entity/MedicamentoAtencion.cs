namespace Consultorio.Entity
{
    public class MedicamentoAtencion
    {
        public int IdAtencion { get; set; }
        public int IdMedicamento { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
    }
}
