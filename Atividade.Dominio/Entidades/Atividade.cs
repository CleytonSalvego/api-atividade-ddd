using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade.Dominio.Entidades
{
    internal class Atividade : EntidadeBase
    {
        public Atividade(string titulo, string descricao, DateTime dataCriacao, string usuario)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            Usuario = usuario;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public bool Status { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string Usuario { get; private set; }


        public void FinalizarAtividade()
        {
            Status = true;
        }
        public void LiberarAtividade()
        {
            Status = false;
        }

        public void AtualizarAtividade(string titulo, string descricao)
        {
            Titulo = titulo;
            Descricao = descricao;
        }
    }
}
