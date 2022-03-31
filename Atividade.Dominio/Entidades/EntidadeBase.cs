using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade.Dominio.Entidades
{
    public abstract class EntidadeBase: IEquatable<EntidadeBase>
    {
        public EntidadeBase()
        {
            Id = Guid.NewGuid();    
        }
        public Guid Id { get; private set; }

        public bool Equals(EntidadeBase? other)
        {
            return Id == other.Id;
        }
    }
}
