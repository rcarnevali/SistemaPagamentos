// Contém a classe Venda e protege as regras de criação e pagamento de uma venda.
using System;

namespace SistemaPagamentos
{
    public class Venda
    {
        public string Codigo { get; } //Permite consultar o código, mas impede alterações depois da criação da venda
        public Cliente Cliente { get; } //Permite consultar o cliente, mas impede  substituição depois da criação da venda
        public decimal ValorCompra { get; private set; } //Permite consultar o valor da compora, mas somente a classe Venda pode alterar
        public SituacaoVenda Situacao { get; private set; } //Permite consultar a situação da venda, mas somente a classe Venda pode alterar
        public FormaPagamento? FormaPagamentoUtilizada { get; private set; } //Armazena a forma de pagamento depois da conclusão do pagamento. O ? define que ela que começa sem valor. Somente a classe Venda pode alterar
        public decimal? ValorFinal { get; private set; } //Armazena o valor final depois da conclusão do pagamento. O ? define que ela que começa sem valor. Somente a classe Venda pode alterar

        public Venda(string codigo, Cliente cliente, decimal valorCompra) //Declara criação de uma nova venda
        {
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("O código da venda é obrigatório."); //Verifica se o código da venda foi informado.

            if (cliente is null) throw new ArgumentNullException(nameof(cliente), "O cliente da venda é obrigatório."); //Verifica se foi fornecido um objeto Cliente

            if (valorCompra <= 0) throw new ArgumentException("O valor da venda deve ser maior que zero."); //Verifica se o valor da compra é maior que zero

            Codigo = codigo;
            Cliente = cliente;
            ValorCompra = Math.Round(valorCompra, 2);

            Situacao = SituacaoVenda.Pendente; //Define a situação inicial da venda obrigatoriamente como Pendente
            FormaPagamentoUtilizada = null; //Inicia a forma de pagamento sem nenhum objeto associado
            ValorFinal = null; //Inicia o valor final sem nenhum valor calculado. 
        }

        public decimal RealizarPagamento(FormaPagamento formaPagamento) //Realiza o pagamento e devolve o valor final calculado
        {

            if (formaPagamento is null) throw new ArgumentNullException(nameof(formaPagamento), "A forma de pagamento é obrigatória."); //Verifica se a forma de pagamento foi informada

            if (Situacao == SituacaoVenda.Pago) throw new InvalidOperationException("Esta venda já foi paga e não pode ser paga novamente."); //Verifica se a venda já está paga

            decimal valorCalculado = formaPagamento.CalculaValorFinal(ValorCompra); //Executa a regra da forma de pagamento escolhida e retorna o valor final

            FormaPagamentoUtilizada = formaPagamento; //Armazena a forma de pagamento utilizada

            ValorFinal = valorCalculado;

            Situacao = SituacaoVenda.Pago; //Altera a situação somente depois que o cálculo foi concluído

            return valorCalculado;
        }
    }
}