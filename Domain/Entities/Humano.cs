using Domain.Common;

namespace Domain.Entities
{
    public class Humano : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public char Sexo { get; set; }
        public int Edad { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
    }
}
