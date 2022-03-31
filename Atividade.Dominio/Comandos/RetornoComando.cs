
using Atividade.Dominio.Comandos.Contratos;

namespace Atividade.Dominio.Comandos 
{
    internal class RetornoComando : IRetornoComando
    {
        public RetornoComando() { }

        public RetornoComando(bool sucesso, string mensagem, object dados) { 
        
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;  

        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
    }
}
