using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class HumanoSpecification : Specification<Humano>
    {
        public HumanoSpecification(string nombre, char sexo, int edad, decimal altura, decimal peso)
        {
            Query.Where(x => x.Nombre == nombre && x.Sexo==sexo && x.Edad == edad && x.Altura == altura && x.Peso == peso);
        }
    }
}
