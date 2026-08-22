using System;

namespace SistemaPagamentos
{
    internal class Program
    {
        private static readonly List<Venda> vendas = new List<Venda>(); // Cria a lista que manterá as vendas enquanto o programa estiver aberto
        private static void Main(string[] args)
        {
            int opcao; // Define o resultado inicial da opção do menu.
            do
            {
                Console.Clear();
                ExibirMenu();

                Console.Write("Escolha uma opção: ");
                if (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 0 || opcao > 3)
                {
                    Console.WriteLine("Opção inválida");
                    Console.WriteLine("Pressione qualquer tecla para continuar."); // Solicita uma tecla antes de mostrar o menu novamente.
                    Console.ReadKey(); // Aguarda o usuário pressionar uma tecla
                    continue;
                }

                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        CadastrarVenda();
                        break;
                    case 2:
                        ListarVendas();
                        break;
                    case 3:
                        RealizarPagamento();
                        break;
                    case 0:
                        Console.WriteLine("Sistema encerrado");
                        break;
                    default:
                        Console.WriteLine("Operação Inválida");
                        break;
                }
            } while (opcao != 0);
        }


        private static void ExibirMenu() // Declara o método responsável por mostrar o menu.
        {
            Console.WriteLine("================================");
            Console.WriteLine("       SISTEMA DE VENDAS");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.WriteLine("1 - Cadastrar venda");
            Console.WriteLine("2 - Listar vendas");
            Console.WriteLine("3 - Realizar pagamento");
            Console.WriteLine("0 - Sair");

            Console.WriteLine();
        }

        private static void CadastrarVenda() // Declara o método responsável pelo cadastro de vendas
        {
            Console.WriteLine("==================");
            Console.WriteLine("CADASTRO DE VENDA");
            Console.WriteLine("==================");
            Console.WriteLine();

            int proximoNumero = vendas.Count + 1; // Calcula o numero da próxima venda.
            string codigoVenda = $"V{proximoNumero:D3}";

            Console.Write("Nome do cliente: ");

            string nome = Console.ReadLine() ?? string.Empty;
            nome = nome.Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("O nome do cliente é obrigatória");
                return;
            }

            Console.Write("CPF: ");

            string cpf = Console.ReadLine() ?? string.Empty;
            cpf = cpf.Trim();

            if (string.IsNullOrWhiteSpace(cpf))
            {
                Console.WriteLine("O CPF é uma informação obrigatória");
                return;
            }

            Console.Write("Valor da compra: R$ ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal valor) || valor <= 0)
            {
                Console.WriteLine("O valor da compra deve ser maior que zero");
                return;
            }

            valor = Math.Round(valor, 2);

            try
            {
                Cliente cliente = new Cliente(nome, cpf);

                Venda novaVenda = new Venda(codigoVenda, cliente, valor); // Cria a venda com situação inicial Pendente.
                vendas.Add(novaVenda); // Adiciona a nova venda à lista.

                Console.WriteLine();
                Console.WriteLine("Venda cadastrada com sucesso!");
                Console.WriteLine($"Situação: {novaVenda.Situacao}");
            }
            catch (ArgumentException erro) // Captura os erros de validação gerados pelas classes.
            {

                Console.WriteLine($"Não foi possível cadastrar a venda: {erro.Message}"); // Exibe o motivo pelo qual a venda não foi cadastrada.
            }
        }


        private static void ListarVendas()  // Lista todas as vendas cadastradas.
        {
            Console.WriteLine("===================");
            Console.WriteLine("VENDAS CADASTRADAS");
            Console.WriteLine("===================");
            Console.WriteLine();
            if (vendas.Count == 0)
            {
                Console.WriteLine("Nenhuma venda cadastrada");
                return;
            }

            foreach (Venda venda in vendas) // Percorre todas as vendas cadastradas
            {
                Console.WriteLine();
                Console.WriteLine($"Venda: {venda.Codigo}");
                Console.WriteLine($"Cliente: {venda.Cliente.Nome}");
                Console.WriteLine($"CPF: {venda.Cliente.CPF}");
                Console.WriteLine($"Valor original: R$ {venda.ValorCompra:F2}");
                Console.WriteLine($"Situação: {venda.Situacao}");

                if (venda.Situacao == SituacaoVenda.Pago) // Se a venda está paga
                {
                    Console.WriteLine($"Forma de pagamento: {venda.FormaPagamentoUtilizada?.Descricao}");
                    Console.WriteLine($"Valor final: R$ {venda.ValorFinal.Value:F2}");
                }
            }
        }

        private static void RealizarPagamento()
        {
            Console.WriteLine("===================");
            Console.WriteLine("REALIZAR PAGAMENTO");
            Console.WriteLine("===================");
            Console.WriteLine();

            if (vendas.Count == 0)
            {
                Console.WriteLine("Nenhuma venda cadastrada");
                return;
            }

            Console.Write("Código da venda: ");
            string codigoVenda = Console.ReadLine() ?? string.Empty;
            codigoVenda = codigoVenda.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(codigoVenda))
            {
                Console.WriteLine("O código da venda é obrigatório");
                return;
            }

            Venda? venda = BuscarVendaPorCodigo(codigoVenda);

            if (venda is null)
            {
                Console.WriteLine("Venda não encontrada.");
                return;
            }

            if (venda.Situacao == SituacaoVenda.Pago)
            {
                Console.WriteLine("Esta venda já foi paga.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Escolha a forma de pagamento:");
            Console.WriteLine("1 - PIX");
            Console.WriteLine("2 - Cartão de crédito");
            Console.WriteLine("3 - Dinheiro");

            Console.Write("Forma de pagamento: ");

            if (!int.TryParse(Console.ReadLine(), out int opcaoPagamento) || opcaoPagamento < 1 || opcaoPagamento > 3)
            {
                Console.WriteLine("Forma de pagamento inválida.");
                return;
            }

            FormaPagamento formaPagamento; // Declara uma variável que pode receber qualquer forma de pagamento.

            switch (opcaoPagamento)
            {
                case 1:
                    formaPagamento = new PagamentoPix();
                    break;
                case 2:
                    formaPagamento = new PagamentoCartao();
                    break;
                case 3:
                    formaPagamento = new PagamentoDinheiro();
                    break;
                default:
                    Console.WriteLine("Forma de pagamento inválida.");
                    return;
            }


            try
            {
                decimal valorFinal = venda.RealizarPagamento(formaPagamento); // Solicita à própria venda que realize o pagamento.

                Console.WriteLine();
                Console.WriteLine($"Valor original: R$ {venda.ValorCompra:F2}");
                Console.WriteLine($"Forma de pagamento: {formaPagamento.Descricao}");
                Console.WriteLine($"Valor final: R$ {valorFinal:F2}");
                Console.WriteLine();
                Console.WriteLine("Pagamento realizado com sucesso.");
            }
            catch (InvalidOperationException erro)
            {
                Console.WriteLine($"Não foi possível realizar o pagamento: {erro.Message}");
            }
        }

        private static Venda? BuscarVendaPorCodigo(string codigoVenda) // Declara um método que procura uma venda pelo codigo
        {

            foreach (Venda venda in vendas) // Percorre todas as vendas cadastradas.
            {
                if (venda.Codigo == codigoVenda)
                {
                    return venda;
                }
            }
            return null;
        }
    }
}