# ApiCommoditiesBR
![.NET Core](https://github.com/caiomarruda/ApiCommoditiesBr/workflows/.NET%20Core/badge.svg)

## Sobre
Esta API têm como função converter os dados de Indicadores de Commodities do CEPEA USP (cepea.esalq.usp.br) para o formato REST, obtidos através do widget gerado no site.

## Observações
+ Esta aplicação é independente e não têm qualquer vínculo com a instituição CEPEA USP.
+ A Aplicação têm como objetivo apenas traduzir os dados obtidos do widget gerado no site CEPEA USP para o formato REST.
+ O uso dessa aplicação destina-se apenas a estudos e seu uso é de total responsabilidade do usuário.


## Configuração
Para configurar, basta inserir a URL que está dentro do embeded gerado no site do CEPEA (https://www.cepea.esalq.usp.br/br/widget.aspx) no parâmetro CommoditiesUrl em appSettings.config .

## Projetos Open Source utilizados
HtmlAgilityPack by zzzProjects

TimeZoneConverter by mj1856

## Licença de Uso
[MIT](https://choosealicense.com/licenses/mit/)