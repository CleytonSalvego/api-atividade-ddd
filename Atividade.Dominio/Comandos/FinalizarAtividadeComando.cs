using Flunt.Notifications;
using Flunt.Validations;

namespace Atividade.Dominio.Comandos
{
    internal class FinalizarAtividadeComando : Notifiable, IComando
    {

        public FinalizarAtividadeComando() { }

        public FinalizarAtividadeComando(Guid id, string usuario) { 
        
            Id = id;    
            Usuario = usuario;
        
        }

        public Guid Id { get; set; }
        public string Usuario { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Usuario, 3, "Usuário", "Usuário inválido.")
                );
        }
    }
}
