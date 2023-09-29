# Análise de Despesas Pessoais
Este é um sistema que estou desenvolvendo para uso próprio com o objetivo de analisar gastos pessoais e familiares. O propósito é ter um sistema adaptado às minhas necessidades, as quais envolvem o armazenamnento de dados e a sua exibição em gráficos de modo dinâmico, a fim de substituir o controle manual de despesas domésticas que costumava ser feito em planilhas do Excel. Além do uso, o propósito deste projeto é colocar em prática os conhecimentos adquiridos recentemente no curso completo de C# combinados à minha bagagem, também enriquecendo meu portifólio.

<br/>

## Uso

O uso parte do cadastro de despesas através do upload de faturas ou extratos bancários no formato OFX ou ainda do registro manual das despesas. As despesas são organizadas em Estabelecimentos e Categorias. Estes são cadastrados manualmente e vinculados também manualmente à palavras-chave. Uma vez feita essa vinculação manual, as despesas serão automaticamente vinculadas aos respectivos Estabelecimentos e Categorias assim que cadastradas. A visualização dos registros é feita em modo de tabela, com diversas possibilidades de filtros, ou em forma de gráfico, também com a possibilidade de filtrar os dados, de escolher o tipo de gráfico e a forma como os dados serão exibidos nele. Por ainda estar em fase de desenvolvimento, o sistema não está hospedado de forma como possa ser visitado, mas é possível conferir o funcionamento de suas telas <a href="#telas">mais abaixo!</a>

<br/>

## Tecnologias e Recursos
<ul>
<li>ASP.NET Core 7</li>
<li>Padrão MVC</li>
<li>Razor Pages</li>
<li>Bootstrap 5</li>
<li>JQuery DataTables</li>
<li>Bibliotecas Chart.js, Newtonsoft JSON e OfxSharp</li>
<li>Linguagens C#, HTML e JavaScript</li>
<li>Entity Framework 7 - Code First</li>
<li>LINQ</li>
<li>MySQL Database 8</li>
</ul>
<br/>

## Desenvolvimento

O sistema foi desenvolvido a partir do template ASP .Net Core Web App do Visual Studio 2022, que inclui estrutura MVC e páginas Razor com Bootstrap. A persistência de dados é feita com a biblioteca Entity Framework, na abordagem Code-First. À estrutura MVC foi adicionada a camada Data com o DbContext que associa os DbSets de cada entidade ao Entity Framework. Também foi criada a camada Service para intermediar a comunicação entre Controller e Data e tratar das regras de negócio de cada entidade. À camada Model foi adicionada a subpasta ViewModel que é ignorada pela estrutura MVC e pelo EF, servindo apenas como auxiliar para levar dados às Views.

Todos os dados possuem operações CRUD. A criação parte de formulários html que direcionam o conteúdo para Actions específicas do Controller associado à View em questão. 
O ulpload de documentos .OFX é feito a partir de um input do tipo file que envia uma lista IList de objetos IFormFile para a Controller. Os objetos IFormFile são convertidos em instâncias de FileStream que são convertidas em instâncias de OFXDocument através da biblioteca OfxSharp, o que possibilita acessar os dados do documento para atribuí-los a objetos que serão persistidos no banco de dados pelo EF.

O ponto alto deste projeto é a geração de gráficos e as tecnologias envolvidas no seu desenvolvimento são justamente das quais eu ainda não possuía conhecimento, como JavaScript e arquivos JSON. Aprendi a usar (o básico) dessas tecnologias "sob demanda", conforme era necessário adicionar ao projeto. Escolhi a biblioteca ChartJS por possuir os recursos de que eu necessitava, por me parecer esteticamente agradável e também por ser muito difundida entre desenvolvedores, tendo bastante material para consulta disponível. Os gráficos são construídos a partir de arquivos JSON e para isso aprendi como são estruturados estes arquivos, como montá-los e como editá-los conforme os padrões da biblioteca ChartJS. Também incluí JavaScript em algumas funcionalidades dinâmicas das Views como a exibição de opções nos formulários de busca. Utilizei diversos recursos do Bootstrap, biblioteca da qual ja possuía algum conhecimento prévio. Utilizei o recurso DataTables da biblioteca JQuery para aprimorar a exibição de dados em tabela, incluindo busca e paginação. Embora há anos não tenha proximidade com front-end, gostei muito de rever alguns tópicos e conhecer novos.

O sistema ainda está em fase de desenvolvimento, faltando ainda incluir diversas funcionalidades relevantes como tratamento de exceções e validação de dados. Também está aberto à criação de novos recursos, como geração de gráficos diferentes e mais complexos. Outro recurso a se considerar é a inclusão de mais usuários, pois apesar de o sistema ter sido modelado para uso pessoal e familiar, poderá ser remodelado para abranger mais usuários, conforme houver interesse de terceiros.

<br/>

## Telas

### Tela Inicial
<p>Na tela inicial é exibido um resumo das despesas.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/home-index.gif?raw=true"/>
<br/>

### Upload de Extrato ou Fatura
<p>Na tela de upload são enviados os arquivos no formato OFX, podendo ser enviados vários de uma só vez. Ao salvar as movimentações, elas sao automaticamente vinculadas às categorias e estabelecimentos relacionados de acordo com as palavras-chave encontradas.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/upload.gif?raw=true"/>
<br/>

### Movimentações
<p>Nesta tela é exibida uma tabela com todas as movimentações cadastradas. O formulário de busca permite filtrar os resultados conforme os critérios desejados.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/movements.gif?raw=true"/>
<br/>

<p>Além do upload de arquivos OFX, é possível cadastrar movimentações manualmente.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/movements-create.gif?raw=true"/>
<br/>

### Cadastro de Palavras-chave
<p>O cadastro de Palavras-chave é feito para que se possa vincular um Estabelecimento ou Categoria a elas.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/keywords-create.gif?raw=true"/>
<br/>

### Cadastro ou Edição de Estabelecimento ou Categoria
<p>O cadastro e edição tanto de Estabelecimento quanto de Categoria funciona do mesmo modo, vinculando palavras-chave previamente cadastradas.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/establishment-create.gif?raw=true"/>
<br/>

### Gerando Gráficos
<p>Na tela de gráficos é possível filtrar os dados consultados, escolher o tipo de gráfico desejado e configurar o modo como os dados serão exibidos.</p>

#### Gráfico em Linhas
<img src="https://github.com/marliseborba/img/blob/main/expenses/chart-line.gif?raw=true"/>
<br/>

#### Gráfico em Barras
<img src="https://github.com/marliseborba/img/blob/main/expenses/chart-bar.gif?raw=true"/>
<br/>

#### Gráfico Pizza
<img src="https://github.com/marliseborba/img/blob/main/expenses/chart-pie.gif?raw=true"/>
<br/>

#### Gráfico Donut
<img src="https://github.com/marliseborba/img/blob/main/expenses/chart-donut.gif?raw=true"/>
<br/>
