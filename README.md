# Exemplo simples com regras de negócio para mostrar rastreabilidade entre regras, código e testes de unidade

## Contexto - IRRF

### Definição de Termos:

- **Empregado**: Pessoa física que trabalha sob regime CLT e recebe remuneração mensal do empregador.
- **Salário CLT**: Remuneração mensal paga ao empregado, conforme as normas da Consolidação das Leis do Trabalho.
- **Imposto de Renda Retido na Fonte (IRRF)**: Imposto descontado pelo empregador do salário do empregado, antes do pagamento.
- **Deduções Permitidas**: Valores que podem ser subtraídos do salário para cálculo do IRRF, incluindo despesas médicas, educação e dependentes.
- **Base de Cálculo do IRRF**: Salário do empregado após subtração das deduções permitidas e outras deduções legais, como a contribuição previdenciária.
- **Despesas Médicas e Odontológicas**: Gastos com tratamentos de saúde do empregado e dependentes, dedutíveis integralmente sem limites mensais.
- **Despesas com Educação**: Gastos com educação do empregado e dependentes, limitados a um teto mensal.
- **Dependentes**: Pessoas que economicamente dependem do empregado, como filhos ou outros familiares, com dedução fixa mensal.
- **Contribuição Previdenciária**: Contribuição mensal obrigatória descontada do salário do empregado para a seguridade social, dedutível no cálculo do IRRF.

## Categorias:

### Cálculo da Base de Cálculo do IRRF

- **RN001: Dedução de Despesas Médicas e Odontológicas**
- **RN002: Dedução de Despesas com Educação**
- **RN003: Dedução por Dependentes**
- **RN004: Dedução da Contribuição Previdenciária**

### Determinação do Imposto

- **RN005: Aplicação das Alíquotas do IRRF**
- **RN006: Cálculo do Imposto a Pagar**

## Regras de Negócio e Casos de Teste

### RN003: Dedução por Dependentes

- `TestarDeducaoPorDependentes_RN003_DependenteNulo`: Verifica se uma exceção é lançada quando um dependente nulo é adicionado.
- `TestarDeducaoPorDependentes_RN003_ListaDependentesNula`: Verifica se uma exceção é lançada quando a lista de dependentes é nula.

### RN004: Dedução por Previdência

- `TestarContribuicaoPrevidenciaria_RN004`: Verifica se a contribuição previdenciária é calculada corretamente como 11% do salário.
- `TestarContribuicaoPrevidenciaria_RN004_SalarioNulo`: Verifica se a contribuição previdenciária é 0 quando o salário é nulo.
- `TestarContribuicaoPrevidenciaria_RN004_SalarioZero`: Verifica se a contribuição previdenciária é 0 quando o salário é zero.

### RN005: Base de Cálculo

- `TestarBaseCalculo_RN005`: Verifica se a base de cálculo é calculada corretamente como salário menos todas as deduções.
- `TestarBaseCalculo_RN005_BaseCalculoNegativa`: Verifica se a base de cálculo é 0 quando as deduções são maiores que o salário.

### RN006: Cálculo do Imposto de Renda

- `TestarCalculoIR_RN006`: Verifica se o imposto de renda é calculado corretamente para diferentes faixas de salário.