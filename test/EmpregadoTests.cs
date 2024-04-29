
using System;
using System.Collections.Generic;
using Xunit;
using irrf;
public class EmpregadoTests
{
    // RN001
    [Fact]
    public void TestarDeducaoDespesasMedicas_RN001()
    {
        var empregado = new Empregado
        {
            Salario = new SalarioCLT { Valor = 3000 },
            Deducoes = new DeducoesPermitidas { DespesasMedicas = 500 }
        };

        empregado.CalcularDeducaoDespesasMedicas();
        Assert.Equal(500, empregado.Deducoes.DespesasMedicas);
    }



    // RN002
    [Fact]
    public void TestarDeducaoDespesasEducacao_RN002()
    {
        var empregado = new Empregado
        {
            Salario = new SalarioCLT { Valor = 3000 },
            Deducoes = new DeducoesPermitidas { DespesasEducacao = 500 },
            Dependentes = new List<Dependente> { new Dependente(), new Dependente() }
        };

        decimal despesasEducacaoEsperada = Math.Min(500, 296.79m * (1 + 2)); // Despesas com educação limitadas a R$296,79 por pessoa por mês

        empregado.CalcularDeducaoDespesasEducacao();

        Assert.Equal(despesasEducacaoEsperada, empregado.Deducoes.DespesasEducacao);
    }

    [Fact]
    public void TestarDeducaoDespesasEducacao_RN002_ValorMaiorQueLimite()
    {
        var empregado = new Empregado
        {
            Salario = new SalarioCLT { Valor = 3000 },
            Deducoes = new DeducoesPermitidas { DespesasEducacao = 5000 }, // Valor maior que o limite
            Dependentes = new List<Dependente> { new Dependente(), new Dependente() }
        };

        empregado.CalcularDeducaoDespesasEducacao();

        Assert.Equal(296.79m * (1 + 2), empregado.Deducoes.DespesasEducacao); // A dedução é limitada a R$296,79 por pessoa por mês
    }
    // RN003
    [Fact]
    public void TestarDeducaoPorDependentes_RN003()
    {
        var empregado = new Empregado
        {
            Salario = new SalarioCLT { Valor = 3000 },
            Deducoes = new DeducoesPermitidas(),
            Dependentes = new List<Dependente> { new Dependente(), new Dependente() }
        };

        decimal deducaoPorDependentesEsperada = 189.59m * 2; // Dedução de R$189,59 por dependente

        empregado.CalcularDeducaoPorDependentes();

        Assert.Equal(deducaoPorDependentesEsperada, empregado.Deducoes.DespesasComDependentes);
    }

    [Fact]
    public void TestarDeducaoPorDependentes_RN003_SemDependentes()
    {
        var empregado = new Empregado
        {
            Salario = new SalarioCLT { Valor = 3000 },
            Deducoes = new DeducoesPermitidas(),
            Dependentes = new List<Dependente>() // Sem dependentes
        };

        empregado.CalcularDeducaoPorDependentes();

        Assert.Equal(0, empregado.Deducoes.DespesasComDependentes); // Sem dependentes, a dedução deve ser 0
    }

    // RN004
    [Fact]
    public void TestarContribuicaoPrevidenciaria_RN004()
    {
        var empregado = new Empregado
        {
            Salario = new SalarioCLT { Valor = 3000 },
            Deducoes = new DeducoesPermitidas()
        };

        decimal contribuicaoPrevidenciariaEsperada = 3000 * 0.11m; // Contribuição previdenciária é 11% do salário

        empregado.CalcularContribuicaoPrevidenciaria();

        Assert.Equal(contribuicaoPrevidenciariaEsperada, empregado.Deducoes.ContribuicaoPrevidenciaria);
    }

    [Fact]
    public void TestarContribuicaoPrevidenciaria_RN004_SalarioNulo()
    {
        var empregado = new Empregado
        {
            Salario = null, // Salário nulo
            Deducoes = new DeducoesPermitidas()
        };

        empregado.CalcularContribuicaoPrevidenciaria();

        Assert.Equal(0, empregado.Deducoes.ContribuicaoPrevidenciaria); // Sem salário, a contribuição previdenciária deve ser 0
    }

    // RN005


    [Theory]
    [InlineData(1903.98, 0)] // Base de cálculo até R$1.903,98 tem alíquota de 0%
    [InlineData(2826.65, 0.075)] // Base de cálculo de R$1.903,99 até R$2.826,65 tem alíquota de 7.5%
    [InlineData(3751.05, 0.15)] // Base de cálculo de R$2.826,66 até R$3.751,05 tem alíquota de 15%
    [InlineData(4664.68, 0.225)] // Base de cálculo de R$3.751,06 até R$4.664,68 tem alíquota de 22.5%
    [InlineData(4664.69, 0.275)] // Base de cálculo acima de R$4.664,68 tem alíquota de 27.5%
    public void TestarCalculoAliquotaIRRF(decimal baseCalculo, decimal aliquotaEsperada)
    {
        var empregado = new Empregado
        {
            Salario = new SalarioCLT { Valor = baseCalculo },
            Deducoes = new DeducoesPermitidas()
        };

        decimal aliquotaCalculada = empregado.CalcularAliquotaIRRF();

        Assert.Equal(aliquotaEsperada, aliquotaCalculada);
    }
}






