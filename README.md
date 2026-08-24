# Sistema de Pagamentos de uma Loja

## Sobre o projeto

Este projeto corresponde ao **segundo exercício individual do Bootcamp de Back-end da WoMakersCode**.

O objetivo é desenvolver uma aplicação de console em C# para cadastrar vendas, listar vendas cadastradas e realizar pagamentos utilizando diferentes formas de pagamento.

O exercício aplica conceitos de Programação Orientada a Objetos estudados no módulo, principalmente:

- Classes e objetos
- Encapsulamento
- Herança
- Abstração
- Polimorfismo
- Construtores
- Propriedades
- Validação de dados
- Listas
- Estruturas de decisão e repetição

## Requisitos de negócio

O sistema deve atender às seguintes regras:

1. Cada venda deve possuir um código único gerado automaticamente.
2. Os códigos devem seguir a sequência `V001`, `V002`, `V003` e assim por diante.
3. Cada venda deve estar associada a um cliente.
4. O cliente deve possuir nome e CPF.
5. O nome do cliente é obrigatório e deve possuir pelo menos dois caracteres.
6. O CPF é obrigatório, deve conter exatamente 11 números e não pode conter letras.
7. O valor da compra deve ser maior que zero.
8. Toda venda deve ser criada inicialmente com a situação `Pendente`.
9. Uma venda pendente pode receber pagamento por PIX, cartão de crédito ou dinheiro.
10. O pagamento por PIX concede 5% de desconto.
11. O pagamento por cartão de crédito acrescenta 3% ao valor da compra.
12. O pagamento em dinheiro mantém o valor original da compra.
13. Depois da realização do pagamento, a situação da venda deve ser alterada para `Pago`.
14. Uma venda paga não pode receber um segundo pagamento.
15. Quando uma informação digitada for inválida, o sistema deve apresentar uma mensagem e permitir uma nova tentativa dentro da mesma operação.
16. As vendas permanecem armazenadas somente enquanto o programa estiver aberto.

## Funcionamento do sistema

Ao iniciar a aplicação, o sistema apresenta o seguinte menu:

```text
1 - Cadastrar venda
2 - Listar vendas
3 - Realizar pagamento
0 - Sair
```

### Cadastrar venda

No cadastro, o sistema:

1. Gera automaticamente o próximo código da venda.
2. Solicita o nome do cliente.
3. Valida se o nome foi preenchido e possui pelo menos dois caracteres.
4. Solicita o CPF.
5. Remove pontos, traços e espaços do CPF.
6. Valida se o CPF possui exatamente 11 números.
7. Solicita o valor da compra.
8. Valida se o valor é numérico e maior que zero.
9. Cria o cliente.
10. Cria a venda com a situação `Pendente`.
11. Adiciona a venda à lista em memória.
12. Apresenta o código gerado para a venda.

### Listar vendas

A opção de listagem apresenta:

- Código da venda
- Nome do cliente
- CPF do cliente
- Valor original
- Situação da venda

Quando a venda estiver paga, também são apresentados:

- Forma de pagamento utilizada
- Valor final calculado

### Realizar pagamento

Para realizar um pagamento, o sistema:

1. Solicita o código da venda.
2. Procura a venda cadastrada na lista.
3. Informa quando o código não corresponde a uma venda.
4. Impede o pagamento de uma venda que já esteja paga.
5. Solicita uma forma de pagamento.
6. Cria o objeto correspondente à opção escolhida.
7. Calcula o valor final conforme a regra da forma de pagamento.
   1. _PIX:_ Aplica desconto de 5% sobre o valor original.
   2. _Cartão de crédito:_ Aplica acréscimo de 3% sobre o valor original.
   3. _Dinheiro:_ Mantém o valor original da compra.
8. Armazena a forma de pagamento e o valor final na venda.
9. Altera a situação da venda para `Pago`.
