// contém a classe Venda e protege as regras de criação e pagamento de uma venda.
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaPagamentos
{
    public class Venda
    {
        public string Codigo { get; } // Declara o número da venda somente para leitura
        public Cliente Cliente { get; } // Declara o cliente da venda somente para leitura
        public decimal ValorCompra { get; private set; } // Declara o valor original com alteração permitida somente dentro da classe
        public SituacaoVenda Situacao { get; private set; } // Declara a situação com alteração permitida somente dentro da classe
        public FormaPagamento? FormaPagamentoUtilizada { get; private set; } // Declara a forma de pagamento, que começa sem valor
        public decimal? ValorFinal { get; private set; } // Declara o valor final, que começa sem valor

        public Venda(string codigo, Cliente cliente, decimal valorCompra) // Declara criação de uma nova venda
        {
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("O código da venda é obrigatório."); // Verifica se o código da venda foi informado.

            if (cliente is null) throw new ArgumentNullException(nameof(cliente), "O cliente da venda é obrigatório."); // Verifica se foi fornecido um objeto Cliente

            if (valorCompra <= 0) throw new ArgumentOutOfRangeException(nameof(valorCompra), "O valor da venda deve ser maior que zero."); // Verifica se o valor da compra é maior que zero

            Codigo = codigo;
            Cliente = cliente;
            ValorCompra = Math.Round(valorCompra, 2);

            Situacao = SituacaoVenda.Pendente; // Define a situação inicial da venda obrigatoriamente como Pendente
            FormaPagamentoUtilizada = null; // Inicia a forma de pagamento sem nenhum objeto associado
            ValorFinal = null; // Inicia o valor final sem nenhum valor calculado. 
        }

        public decimal RealizarPagamento(FormaPagamento formaPagamento) // Declara a operação responsável por realizar o pagamento da venda
        {

            if (formaPagamento is null) throw new ArgumentNullException(nameof(formaPagamento), "A forma de pagamento é obrigatória."); // Verifica se a forma de pagamento foi informada

            if (Situacao == SituacaoVenda.Pago) throw new InvalidOperationException("Esta venda já foi paga e não pode ser paga novamente."); // Verifica se a venda já está paga

            decimal valorCalculado = formaPagamento.CalculaValorFinal(ValorCompra); // Solicita ao objeto de pagamento que execute sua própria regra

            FormaPagamentoUtilizada = formaPagamento; // Armazena a forma de pagamento utilizada

            ValorFinal = valorCalculado;

            Situacao = SituacaoVenda.Pago; // Altera a situação somente depois que o cálculo foi concluído

            return valorCalculado;
        }
    }
}