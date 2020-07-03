# Engenharia de Software (API)

Este projeto é baseado em serviços AWS para disponibilização de recursos RestFul através de uma API Gateway.

* serverless.template - template para criação de recursos na AWS via *CloudFormation*
* Function.cs - main classe com os "entry points" para as funções Lambdas (serveless) 
* appsettings.json - arquivo de configurações. Existe um arquivo exemplo na aplicação "appsettings.change.json". Renomear o arquivo e alteração as configurações.
* swagger-v1.yaml - arquivo com a documentação Swagger da API

## Design da Aplicação

A aplicação foi modelada com base em DDD (Domain-Driven Design). As principais estruuras são comentadas a seguir:

`Presentation` Interface com o usuário. Foi utilizado Lambda para expor as funcionalidades da aplicação.

 **ResquestModel e ResponseModel ** contém classes de IO de  informações da aplicação para não expor o modelo de dados do dominio.

`Service` Serviços que atendem ao negócio. Nessa aplicação foi abstraida a estrutura de *Serviços de Aplicação.*  
*Pode estar abaixo da estrtura do domain.*

`Infra` Processos que suportam a aplicação
 
 `CrossCutting` Estrutura que é *cross* entre todas as outras da aplicação.
 
 `Domain` Contém toda lógica de negócios da aplicação. Foram usadas interfaces para definição de ações do negócio e a responsabilidade de implementação fica nas outras camadas.
