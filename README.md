# ApiCommoditiesBR
![.NET Core](https://github.com/caiomarruda/ApiCommoditiesBr/workflows/.NET%20Core/badge.svg)

## Sobre
Esta API foi desenvolvida no formato REST e têm como objetivo converter os dados de Indicadores de Commodities do site CEPEA USP (cepea.esalq.usp.br) para o formato de exibição em JSON, obtidos através da url widget gerada no site.

## Observações
+ Esta aplicação é independente e não têm qualquer vínculo com a instituição CEPEA USP.
+ A aplicação têm apenas como objetivo converter os dados obtidos do widget gerado no site CEPEA USP para o formato de exibição em JSON.
+ Esta aplicação destina-se apenas para fins de estudos e o seu uso é de total responsabilidade do usuário.


## Configuração
Para configurar, basta inserir a URL que está dentro do embeded gerado no site do CEPEA (https://www.cepea.esalq.usp.br/br/widget.aspx) no parâmetro CommoditiesUrl em appSettings.config .

## Projetos Open Source utilizados
HtmlAgilityPack por zzzProjects

TimeZoneConverter por mj1856

## Licença de Uso
[MIT](https://choosealicense.com/licenses/mit/)