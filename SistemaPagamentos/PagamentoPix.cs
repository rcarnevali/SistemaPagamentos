// Forma de pagamento PIX e aplica desconto de 5% ao valor da venda.using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaPagamentos
{
    public class PagamentoPix : FormaPagamento // Classe PagamentoPix como filha de FormaPagamento
    {
        // Implementa o nome específico desta forma de pagamento.
        public override string Descricao
        {
            get { return "PIX"; } // Devolve o texto que será apresentado no sistema
        }

        public override decimal CalculaValorFinal(decimal valor)
        {
            if (valor <= 0) throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser maior que zero.");

            // decimal desconto = valor * 0.05m; // Calcula 5% do valor original.

            decimal valorFinal = valor - (valor * 0.05m);

            valorFinal = Math.Round(valorFinal, 2);

            return valorFinal;
        }
    }
}