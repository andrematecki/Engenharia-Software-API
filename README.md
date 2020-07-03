# Engenharia de Software (API)

Este projeto � baseado em servi�os AWS para disponibiliza��o de recursos RestFul atrav�s de uma API Gateway.

* serverless.template - template para cria��o de recursos na AWS via *CloudFormation*
* Function.cs - main classe com os "entry points" para as fun��es Lambdas (serveless) 
* appsettings.json - arquivo de configura��es. Existe um arquivo exemplo na aplica��o "appsettings.change.json". Renomear o arquivo e altera��o as configura��es.
* swagger-v1.yaml - arquivo com a documenta��o Swagger da API

## Design da Aplica��o

A aplica��o foi modelada com base em DDD (Domain-Driven Design). As principais estruuras s�o comentadas a seguir:

`Presentation` Interface com o usu�rio. Foi utilizado Lambda para expor as funcionalidades da aplica��o.

 **ResquestModel e ResponseModel ** cont�m classes de IO de  informa��es da aplica��o para n�o expor o modelo de dados do dominio.

`Service` Servi�os que atendem ao neg�cio. Nessa aplica��o foi abstraida a estrutura de *Servi�os de Aplica��o.*  
*Pode estar abaixo da estrtura do domain.*

`Infra` Processos que suportam a aplica��o
 
 `CrossCutting` Estrutura que � *cross* entre todas as outras da aplica��o.
 
 `Domain` Cont�m toda l�gica de neg�cios da aplica��o. Foram usadas interfaces para defini��o de a��es do neg�cio e a responsabilidade de implementa��o fica nas outras camadas.
