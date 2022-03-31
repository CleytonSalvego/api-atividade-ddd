using Flunt.Notifications;
using Flunt.Validations;

namespace Atividade.Dominio.Comandos
{
    internal class AtualizarAtividadeComando : Notifiable, IComando
    {

        public AtualizarAtividadeComando() { }

        public AtualizarAtividadeComando(Guid id, string usuario, string titulo, string descricao)
        {

            Id = id;
            Usuario = usuario;
            Titulo = titulo;
            Descricao = descricao;

        }

        public Guid Id { get; set; }
        public string Usuario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Titulo, 3, "Título", "Mínimo de caracteres inválido. O Título deve possuir 3 caracteres ou mais.")
                    .HasMinLen(Descricao, 3, "Descrição", "Mínimo de caracteres inválido. A Descrição deve possuir 3 caracteres ou mais.")
                    .HasMinLen(Usuario, 3, "Usuário", "Usuário inválido.")
                );
        }
    }
}
