## API para controle de atividades
 
#### Esta API foi construida através do curso da plataforma Balta.io colocando em prática o DDD, CQRS, EF 6 e Autenticação por token com JWT Token.
 
####  Criando a estrutura do projeto.
Nosso projeto será estruturado da seguinte forma.<br>
•	**Atividade.Dominio** (Onde ficará toda a nossa regra de negócio)<br>
•	**Atividade.Dominio.Api** (Será a API Rest)<br>
•	**Atividade.Dominio.Infra** (Estrutura de bando de dados)<br>
•	**Atividade.Dominio.Testes** (Aqui ficarão todos os nossos testes unitário)<br><br>
Conhecendo nossa estrutura, na pasta raiz do projeto crie quatro pasta chamadas: Atividade.Dominio, Atividade.Dominio.Api, Atividade.Dominio.Infra, Atividade.Dominio.Testes.<br>
Agora em cada pasta vamos criar os projetos, abra o prompt de comando ou Windows terminal e dentro de cada pasta a seguir, rode os comandos específicos.<br>
 •	Atividade.Dominio<br>
   -	dotnet new classlib<br>
 •	Atividade.Dominio.Api<br>
   -	dotnet new webapi<br>
 •	Atividade.Dominio.Infra<br>
   -	dotnet new classlib<br>
 •	Atividade.Dominio.Testes<br>
   -	dotnet new mstest<br>
Com todos os projetos dos nossos domínios criados, precisamos criar agora a solução deste projeto, para isso vá até a pasta raiz do projeto e rode o seguinte comando.<br>
```
dotnet new sln
```
Após esse comando a estrutura de pastas e arquivos em nosso projeto deverá estar da seguinte forma<br>

![image](https://user-images.githubusercontent.com/48839351/161112289-a84c7520-e0ed-4875-bfab-a97f535bf2e1.png)

 
Agora nosso próximo passo é adicionar nossos dominios a solução do projeto, para isso, na raiz do projeto rode os comando abaixo.<br>
```
dotnet sln add ./Atividade.Dominio
dotnet sln add ./Atividade.Dominio.Api
dotnet sln add ./Atividade.Dominio.Infra
dotnet sln add ./Atividade.Dominio.Testes
```
Feito isso, agora podemos rodar o comando dotnet build e algo semelhante a isso deverá ser apresentado.<br>

![image](https://user-images.githubusercontent.com/48839351/161112337-03e3fc22-11bd-48db-9353-d05f8ee2d6c9.png)

 
Por fim, agora precisamos adicionar as referência entre os domínios.<br>
**Atividade.Dominio:** Não irá referenciar nenhum projeto, mas todos irão referencialo.<br>
**Atividade.Dominio.Api:** Irá referenciar a Atividade.Dominio e Atividade.Dominio.Infra<br>
Para adicionar essas referência, acessa a pasta Atividade.Dominio.Api e no prompt de comando rode os comandos abaixo.<br>
```
dotnet add reference ../Atividade.Dominio
dotnet add reference ../Atividade.Dominio.Infra
```
 ![image](https://user-images.githubusercontent.com/48839351/161112560-4af8fcef-97a1-42b9-8b0e-f95f1317cf00.png)

**Atividade.Dominio.Infra:** Irá referencia a Atividade.Dominio apenas.<br>
Acesse a pasta Atividade.Dominio.Infra e rode o comando a baixo.<br>
```
dotnet add reference ../Atividade.Dominio
```
 
![image](https://user-images.githubusercontent.com/48839351/161112572-4c940e89-d16e-4add-8674-7bbcc0903188.png)


**Atividade.Dominio.Testes:** Inicialmente iremos referenciar apenas Atividade.Dominio para executarmos os testes de unidade, contudo, mais a frente iremos ter que referenciar os outros projetos para realizarmos os testes.<br>
Acesse a pasta Atividade.Dominio.Testes e rode o comando abaixo.<br>

![image](https://user-images.githubusercontent.com/48839351/161112616-c9633136-b463-401e-a31f-283248033f2e.png)

 
Finalizada essa parte, nosso projeto está completo e preparado para iniciarmos a codificação do domínio.<br>

### Alterando o nosso Dominio
O início da nossa codificação será feita em cima do nosso domínio, onde toda a regra de negócio ficará, portanto nesta etapa inicial iremos trabalhar somente na Atividade.Dominio.<br>
Para abra o projeto no Visual Studio ou Visual Code e teremos uma estrutura semelhante a essa.<br>

![image](https://user-images.githubusercontent.com/48839351/161112690-4b94e4b8-5b61-45c7-9f7e-7531e5af816b.png)

 
### Criando nossas entidades
Para termos um forte modelo em nossa aplicação iremos criar nossas entidades, para isso em Atividade.Dominio crie uma pasta chamada Entidades e dentro dela ficarão todas os nossos modelos da aplicação as chamadas entidades.<br>

![image](https://user-images.githubusercontent.com/48839351/161112751-e818510b-ccd5-4dfe-8d54-d68a6033f94d.png)

 
E para começar vamos criar uma entidade base que será herdada por todas as outras entidades, sendo crie uma classe chamada EntidadeBase.cs com o código abaixo.<br>
Essa entidade será uma classe abstrata (abstract) para que outras entidades possam utilizá-la mas não consiga instancia-la. E colocaremos dentro dela tudo que for comum entre todas as entidades, que no nosso caso será o Id gerado automaticamente por um Guid.<br>
O código da nossa EntidadeBase ficará da seguinte forma.<br>
```
namespace Atividade.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        public EntidadeBase()
        {
            Id = Guid.NewGuid();    
        }
        public Guid Id { get; private set; }
    }
}
```

Nossa entidade será public abstract para que outras entidades possam usá-la mas não instancia-la.<br>
Temos um construtor, onde será gerado o Id automaticamente pelo Guid.<br>
E por fim temos nossa propriedade public Guid Id, que está como private set para que as entidades possam estendê-la mas não consigam alterar o Id, por que essa geração ou alteração deve ser feita apenas por nossa EntidadeBase.<br>
Nosso método construtor está como public, cabe relembrar dos principais modificadores que temos no C#.<br>
**Protected:** Os filhos tem acesso aos métodos.<br>
**Public:** Todos os métodos públicos da classe poderão ser acessados por qualquer outra classe.<br>
**Private:** Os métodos da classe são totalmente privados e não podem ser acessados por outras classes.<br>
Mas ainda temos um alteração em nossa EntidadeBase, que utilizar a interface IEquatable para compararmos se dois Ids são iguais.<br>
Para implementar essa interface estendemos nossa classe EntidadeBase: IEquatable<EntidadeBase> e implementar o método Equals como abaixo.<br>
```
 public bool Equals(EntidadeBase other)
{
	return Id = other.Id;
}
 ```
 
**IEquatable:** Essa interface realiza a comparação de dois objetos do mesmo tipo através do método Equals retornado true ou false.<br>
Após essa alteração nossa classe ficará da seguinte forma.<br>
```
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
```

Agora com nossa classe EntidadeBase concluída vamos criar as outras entidades de nossa aplicação.<br>

 ### Criando nossa entidade Atividade
 
Abaixo está o código de nossa entidade Atividade.cs<br>
``` 
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
```
 
 
Nessa entidade temos as propriedade da Atividade que estão como private set para que nenhuma outra classe consiga alterar o valor de suas propriedades, somente a Atividade poderá alterar duas propriedades. E nossa entidade Atividade está herdando nossa EnitdadeBase por que o Id da atividade será criado pela EntidadeBase.<br>
Temos também um construtor, para setar as propriedades.<br>
E por fim temos os métodos de FinalizarAtividade, LiberarAtividade e AtualizarAtividade que serão os responsáveis por alterar as informações de algumas propriedades.<br>

### Criando os Comandos para gravação 
 
Como vamos utilizar CQRS, nós teremos os comandos para realizar a parte de inserção e escrita dos dados, para isso acesse a pasta Atividade.Domain e crie uma nova pasta chamada Comandos, nela ficará toda parte relacionada a escrita dos dados.<br>
Para iniciar vamos criar uma interface para padronizar o retorno, deixando mais organizado os dados que iremos retornar ao ser chamado um comando.<br>
Crie uma nova pasta chamada contratos e uma nossa classe dentro desta pasta chamada IComando.cs. Todos os comandos que serão criados posteriormente deverão utilizar esse contrato e serão obrigados a implementar nossa função Validate().<br>
Após criar a classe IComando.cs insira o código abaixo.<br>
 
``` 
namespace Atividade.Dominio.Comandos
{
    public interface IComando
    {
        bool Validate();
    }
}
```
 
**Imagem da organização do nosso projeto até o momento** <br>
 
 ![image](https://user-images.githubusercontent.com/48839351/161113383-2d6eb12c-5b26-4a4f-b55f-d1153e105cdd.png)


 
## Utilizando o Flunt para validação e notificação.
 
Para evitarmos inserir várias condicionais em nosso código e conseguir realizar validações e notificações iremos utilizar um pacote chamado Flunt, criado pelo André Baltieri, link do projeto: https://github.com/andrebaltieri/Flunt <br>
Para instalar basta rodar o comando abaixo acessando a pasta Atividade.Dominio.<br>

 *Instale essa versão 1.0.4 
 ```
 dotnet add package Flunt 1.0.4 
 ```
 
 ![image](https://user-images.githubusercontent.com/48839351/161114004-c8ec52f0-8f76-4813-825e-6eb704237efb.png)

Após instalado podemos utilizar esse pacote importando através do using em nossa interface.<br>
Sendo assim em nossa interface IComando.cs iremos utilizar o Flunt.Validation e estender nossa interface com IValidatable. <br>
Como o IValidatable já possui a função Validate(), nós não precisamos dessa função mais em nosso IComando.cs, portanto, nosso código ficará da seguinte forma.<br>
  ```
using Flunt.Validations;

namespace Atividade.Dominio.Comandos
{
    public interface IComando : IValidatable 
    {
    }
}
 ```
Com nossa interface e validações prontas, vamos começar a criar os comandos da nossa aplicação. Um comando nada mais é do que uma ação em nossa aplicação, portanto, se eu preciso criar e atualizar uma atividade por exemplo, teremos neste caso dois comandos um para criar e outro para alterar.<br>


### Criando Comando Criar Atividade CriarAtividadeComando.cs
 
Acesse a pasta Atividade.Domain e crie uma classe chamada CriarAtividadeComando.cs.<br>
Para criar uma atividade nós precisamos preencher algumas informações importantes, e essas informações estão descritas em nossa Entidade Atividade.cs, lá no seu construtor precisamos passar as seguintes informações: Título, Descricao, DataCriacao e Usuario. Sendo assim, são exatamente essas propriedades que teremos em nosso comando CriarAtividadeComando.cs <br>
Esse comando irá ser utilizado para as entradas das informações em nossa API, ou seja, toda vez que a API receber uma requisição será gerado um Comando e o ASP.NET através do ModelBiding irá fazer um de-para/parse entre o JSON recebido na requisição e nosso comando.<br>
Para nosso comando nós teremos dois construtores, um parameterless (sem parâmetros) para caso seja necessário criar um comando sem passar nenhum parâmetro e outro com os parâmetros necessários para que o comando seja criado já com todos os parâmetros. <br>
E por fim, agora devemos estender nossa interface IComando.cs e implementar a funação Validade();<br>
Resumindo, nossos Comandos irão funcionar semelhante a DTO (Data Transfer Object) ou VieewModel para a entrada de dados em nossa API.<br>
Concluíndo esta primeira etapa da construção do nosso comando CriarAtividadeComando.cs o código ficou da seguinte forma.<br>
 
 ```
using System;

namespace Atividade.Dominio.Comandos
{
    internal class CriarAtividadeComando : IComando
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
            //Aqui iremos utilizar as validações utilizando Flunt
        }
    }
}
 ```
 
### Implementando as validações do nosso comando CriarAtividadeComando.cs
 
Agora vamos trabalhar com o Flunt para podermos realizar as validações necessárias das nossas propriedades e posteriormente listar esses erros encontrados, basicamente vamos enfileirar as validações e apresentar posteriormente. <br>
Para isso vamos estender nosso comando para Notifiable  e importar o Flunt.Notifications com o using. <br>
 
![image](https://user-images.githubusercontent.com/48839351/161114094-9ab10c14-20b1-452a-86ba-c8416e770ca6.png)

 
E para a validação nós iremos utilizar os contratos disponibilizados pelo nosso pacote do Flunt. <br>
Sendo assim dentro da função Validate() vamos criar um contrato e inserir todas as validações necessárias do nosso comando, veja como isso ficará abaixo. <br>
 
 ![image](https://user-images.githubusercontent.com/48839351/161114136-e95d3f76-68d0-4397-9010-9d7bedcef99b.png)

 
**Explicando**<br>
 
**AddNotications:** Implementa a adição de mais de uma notificação no mesmo contrato.<br>
**New Contract:** Abra um novo contrato de validação com o Flunt.<br>
**.Requires():** Diz que todos os campos nas validações devem ser obrigatórios.<br>
**.HasMinLen(Propriedade, Tamanho, Propriedade, Mensagem):** Esta é apenas uma das validações proporcionadas pelo Flunt, para saber mais acesse o github e analise todas as validações existentes e possíveis.<br>
Em nosso exemplo: .HasMinLen(Usuario, 3, "Usuário", "Mínimo de caracteres inválido. O Usuário deve possuir 3 caracteres ou mais."). Quer dizer que é requerido pelo menos 3 caracteres para o usuário para ser válido.<br>
 
**Imagem do nosso comando CriarAtividadeComando.cs completo**<br>
 
 ![image](https://user-images.githubusercontent.com/48839351/161114162-c82c0f13-7d46-44e7-a2cd-da5804060b0c.png)

 
### Criando nosso Comando de Retorno RetornoComando.cs 
Todos os comandos que iremos utilizar em nossa aplicação deverão retirar algum coisa após serem chamados, por esse motivo vamos padronizar e fazer com que todos os comandos retornem um RetornoComando, sendo assim vamos criar dois arquivos uma classe chamada RetornoComando.cs e uma interface IRetornoComando.cs ficando nossa estrutura da seguinte forma.<br>
 
Nossa interface IRetornoComando.cs deverá possuir o seguinte código.<br>
  ```
namespace Atividade.Dominio.Comandos.Contratos
{
    internal interface IRetornoComando
    {
    }
}
 ```
 
Nossa classe RetornoComando.cs deverá implementar nossa interface IRetornoComando e o código ficará da seguinte forma.<br>

 ```
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
 ```
Sucesso: Apresenta se a requisição foi feita executada com sucesso.<br>
Mensagem: Será apresentada quando houver algum erro/problema.<br>
Dados: São os dados de retorno.<br>
Bom com nosso RetornoComando criado agora poderemos dar sequência, pois em nosso Handler (vamos criar mais a frente) sempre irá entrar um Comando e sempre será retornado um RetornoComando, assim criamos uma padronização de entrada e saída para nossa aplicação.<br>
Agora vamos continuar a criar nossos comandos, teremos comandos para finalizar, liberar e atualizar uma atividade.<br>
 
### Criando nosso comando FinalizarAtividadeComando
Na pasta de comandos crie uma classe chamado FinalizarAtividadeComando.cs.<br>
Nosso comando irá recebe dois parâmetros o Id e o usuário para podermos identificar a atividade e marca-la como finalizada, o código é semelhante ao nosso comando CriarAtividadeComando.cs, veja como ficou o código abaixo.<br>
 ```
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
 ```
### Criando nosso comando LiberarAtividadeComando<br>
 
Na pasta de comandos crie uma classe chamado LiberarAtividadeComando.cs.<br>
Nosso comando irá recebe dois parâmetros o Id e o usuário para podermos identificar a atividade e marca-la como liberada (Não finalizada), o código é semelhante ao comando FinalizarAtividadeComando.cs veja o código abaixo.<br>

```
using Flunt.Notifications;
using Flunt.Validations;

namespace Atividade.Dominio.Comandos
{
    internal class LiberarAtividadeComando : Notifiable, IComando
    {

        public LiberarAtividadeComando() { }

        public LiberarAtividadeComando(Guid id, string usuario)
        {

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
 ```


### Criando nosso comando AtualizarAtividadeComando
 
Na pasta de comandos crie uma classe chamado AtualizarAtividadeComando.cs.```
Nosso comando irá receber quatro parâmetros o Id e o usuário para podermos identificar a atividade e os parâmetros título e descrição para podermos atualizar as informações desta atividade, veja como ficará o código abaixo.```
 
 ```
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
  ```
 
Com isso finalizamos a criação dos nossos comandos, nossa estrutura do projeto ficou da seguinte forma.```
 








