# Análise de Despesas Pessoais
Este é um sistema que estou desenvolvendo para uso próprio com o objetivo de analisar gastos pessoais e familiares. O propósito é ter um sistema capaz de armazenar dados e exibí-los em gráficos a fim de substituir o controle que costumava ser feito em planilhas do Excel.

O funcionamento consiste em cadastrar as despesas fazendo upload de faturas ou extratos bancários no formato OFX ou ainda registrando as despesas manualmente. As despesas são organizadas em Estabelecimentos e Categorias que são cadastrados manualmente e vinculados às despesas através de palavras-chave. A visualização dos registros é feita em modo de tabela com possibilidade de filtrar a busca, ou em variados tipos de gráfico, também com a possibilidade de filtrar os dados e ainda escolher a forma como os dados serão exibidos.

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
<p>Na tela de upload são enviados os arquivos no formato OFX, podendo ser enviados vários de uma só vez.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/upload.gif?raw=true"/>
<br/>

### Movimentações
<p>Nesta tela é exibida uma tabela com todas as movimentações cadastradas. O formulário de busca permite filtrar os resultados conforme os critérios desejados.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/movements-edit.gif?raw=true"/>
<br/>

### Cadastro ou Edição de Estabelecimento ou Categoria
<p>O cadastro e edição tanto de Estabelecimento quanto de Categoria funciona do mesmo modo, vinculando palavras-chave previamente cadastradas.</p>
<img src="https://github.com/marliseborba/img/blob/main/expenses/establishment.gif?raw=true"/>
