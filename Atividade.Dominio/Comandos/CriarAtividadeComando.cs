using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Atividade.Dominio.Comandos
{
    internal class CriarAtividadeComando : Notifiable, IComando
    {
        public CriarAtividadeComando() { }

        public CriarAtividadeComando(string titulo, 
                                     string descricao, 
                                     DateTime dataCriacao, 
                                     string usuario) { 
            Titulo = titulo;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            Usuario = usuario;
        }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Usuario { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Titulo,3,"Título","Mínimo de caracteres inválido. O Título deve possuir 3 caracteres ou mais.")
                    .HasMinLen(Descricao, 3, "Descrição", "Mínimo de caracteres inválido. A Descrição deve possuir 3 caracteres ou mais.")
                    .HasMinLen(Usuario, 3, "Usuário", "Mínimo de caracteres inválido. O Usuário deve possuir 3 caracteres ou mais.")
                );
        }
    }
}
