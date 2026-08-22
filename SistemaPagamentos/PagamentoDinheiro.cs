// Forma de pagamento em dinheiro, que não aplica desconto nem acréscimo.
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaPagamentos
{
    public class PagamentoDinheiro : FormaPagamento // Declara a classe PagamentoDinheiro como filha de FormaPagamento.
    {
        public override string Descricao
        {
            get { return "Dinheiro"; }
        }

        public override decimal CalculaValorFinal(decimal valor)
        {
            if (valor <= 0) throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser maior que zero.");

            decimal valorFinal = Math.Round(valor, 2); // Arredonda o valor original para duas casas decimais

            return valorFinal;
        }
    }
}