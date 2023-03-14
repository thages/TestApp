#language: pt-br

Funcionalidade: Indicador de Performance

Fundo: Preparar banco de dados para os testes
	Dado  a limpeza e cadastro de exemplos

Cenário: Criar indicador de performance
	Dado preenchimento e envio de um commando
	Então uma resposta do tipo Indicador de Performance deve ser retornada

Cenário: Listar indicador de performance
	Dado a requisicao de uma lista
	Então uma lista deve ser retornada

Cenário: Exibir indicador de performance do tipo media
	Dado a requisicao de uma performance do tipo media
	Então um detalhe de indicador por media deve ser retornado 

Cenário: Exibir indicador de performance do tipo soma
	Dado a requisicao de um indicador do tipo soma
	Então uma resposta do tipo indicador por soma deve ser retornada