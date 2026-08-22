// Forma de pagamento com cartão de crédito e acrescenta uma taxa de 3% ao valor da venda.using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaPagamentos
{
    public class PagamentoCartao : FormaPagamento // Declara a classe PagamentoCartao como filha de FormaPagamento.
    {
        public override string Descricao
        {
            get { return "Cartão de crédito"; }
        }

        public override decimal CalculaValorFinal(decimal valor)
        {
            if (valor <= 0) throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser maior que zero");

            // decimal taxa = valor * 0.03m; // Calcula 3% do valor original.

            decimal valorFinal = valor + (valor * 0.03m);

            valorFinal = Math.Round(valorFinal, 2);

            return valorFinal;
        }
    }
}