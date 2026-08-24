using System;

namespace SistemaPagamentos
{
    public class Cliente
    {
        public string Nome { get; private set; } //Permite consultar o nome, mas somente a classe Cliente pode alterá-lo
        public string CPF { get; } //Permite consultar o CPF, mas impede alterações depois da criação do cliente

        //Cliente é CONSTRUTOR da classe: 1) ter exatamente o mesmo nome da classe; 2) não possuir tipo de retorno e 3) ser executado quando usamos new
        public Cliente(string nome, string cpf) // Valida informações do cliente.
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome do cliente é obrigatório"); // Verifica se o nome está vazio, nulo ou contém somente espaços.

            nome = nome.Trim();

            if (nome.Length < 2) throw new ArgumentException("O nome do cliente deve possuir pelo menos 2 caracteres");

            if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("O CPF é uma informação obrigatória"); // Verifica se o CPF está vazio, nulo ou contém somente espaços.

            cpf = cpf.Replace(".", "").Replace("-", "").Replace(" ", "").Trim();

            if (cpf.Length != 11) throw new ArgumentException("O CPF deve possuir exatamente 11 números"); // Verifica se o CPF contém exatamente 11 caracteres.

            foreach (char caractere in cpf) // Percorre cada caractere do CPF.
            {
                // Verifica se o caractere atual não é um número.
                if (!char.IsDigit(caractere)) throw new ArgumentException("O CPF deve conter somente números");
            }

            Nome = nome; // Armazena o nome validado na propriedade Nome.
            CPF = cpf; // Armazena o CPF validado na propriedade CPF.
        }
    }
}