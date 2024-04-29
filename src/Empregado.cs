using System;
using System.Collections.Generic;

namespace irrf;

public class Empregado
{
    public SalarioCLT Salario { get; set; }
    public DeducoesPermitidas Deducoes { get; set; }
    public List<Dependente> Dependentes { get; set; }

    public Empregado()
    {
        Salario = new SalarioCLT();
        Dependentes = new List<Dependente>();
        Deducoes = new DeducoesPermitidas();
    }

    public void AdicionarDependente(Dependente dependente)
    {
        Dependentes.Add(dependente);
    }


    // RN001: Dedução de Despesas Médicas e Odontológicas
    public decimal CalcularDeducaoDespesasMedicas()
    {
        // Despesas médicas e odontológicas: gastos integrais com saúde do contribuinte e dependentes, sem limite de valor mensal, são dedutíveis da Base de Cálculo do IRRF.
        return Deducoes?.DespesasMedicas ?? 0;
    }

    // RN002: Dedução de Despesas com Educação
    public void CalcularDeducaoDespesasEducacao()
    {
        // Despesas com educação: gastos com educação do contribuinte e seus dependentes, limitados a R$296,79 por pessoa por mês, são dedutíveis da Base de Cálculo do IRRF.
        Deducoes.DespesasEducacao = Math.Min(Deducoes?.DespesasEducacao ?? 0, 296.79m * (1 + Dependentes.Count));
    }

    // RN003: Dedução por Dependentes
    public void CalcularDeducaoPorDependentes()
    {
        // Dependentes: uma dedução de R$189,59 por dependente por mês é permitida na Base de Cálculo do IRRF.
        Deducoes.DespesasComDependentes = 189.59m * Dependentes.Count;
    }

    // RN004: Dedução da Contribuição Previdenciária
    public void CalcularContribuicaoPrevidenciaria()
    {
        if (Salario?.Valor > 0)
        {
            Deducoes.ContribuicaoPrevidenciaria = Salario.Valor * 0.11m;
        }
    }

    // RN005: Aplicação das Alíquotas do IRRF
    public decimal CalcularAliquotaIRRF()
    {
        decimal baseCalculo = Salario?.Valor ?? 0;
        baseCalculo -= Deducoes?.DespesasMedicas ?? 0;
        baseCalculo -= Deducoes?.DespesasEducacao ?? 0;
        baseCalculo -= Deducoes?.DespesasComDependentes ?? 0;
        baseCalculo -= Deducoes?.ContribuicaoPrevidenciaria ?? 0;


        if (baseCalculo <= 1903.98m)
        {
            return 0;
        }
        else if (baseCalculo <= 2826.65m)
        {
            return 0.075m;
        }
        else if (baseCalculo <= 3751.05m)
        {
            return 0.15m;
        }
        else if (baseCalculo <= 4664.68m)
        {
            return 0.225m;
        }
        else
        {
            return 0.275m;
        }
    }

    // RN006: Cálculo do Imposto a Pagar
    public decimal CalcularImpostoAPagar()
    {
        decimal baseCalculo = Salario?.Valor ?? 0;
        return baseCalculo * CalcularAliquotaIRRF();
    }
}