# Análise de Despesas Pessoais
Este é um sistema que estou desenvolvendo para uso próprio com o objetivo de analisar gastos pessoais e familiares. O propósito é ter um sistema adaptado às minhas necessidades, as quais envolvem o armazenamnento de dados e a sua exibição em gráficos de modo dinâmico, a fim de substituir o controle manual de despesas que costumava ser feito em planilhas do Excel. Além do uso, o propósito deste projeto é colocar em prática os conhecimentos adquiridos recentemente no curso completo de C# combinados à minha bagagem, enriquecendo meu portifólio.

O funcionamento consiste em cadastrar as despesas fazendo upload de faturas ou extratos bancários no formato OFX ou ainda registrando as despesas manualmente. As despesas são organizadas em Estabelecimentos e Categorias. Estes são cadastrados manualmente e vinculados também manualmente à palavras-chave. Uma vez feita essa vinculação manual, as despesas serão automaticamente vinculadas aos respectivos Estabelecimentos e Categorias assim que cadastradas. A visualização dos registros é feita em modo de tabela, com diversas possibilidades de filtros, ou em forma de gráfico, também com a possibilidade de filtrar os dados, de escolher o tipo de gráfico e a forma como os dados serão exibidos nele.

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
<li>Entity Framework 7</li>
<li>LINQ</li>
<li>MySQL Database 8</li>
</ul>
<br/>

## Screenshots

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

### Cadastro ou Edição de Estabelecimento ou Categoria
<p>O cadastro e edição tanto de Estabelecimento quanto de Categoria funciona do mesmo modo, vinculando palavras-chave previamente cadastradas.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/establishment-edit.gif?raw=true"/>
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
