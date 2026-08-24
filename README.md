# Sistema de Pagamentos de uma Loja

## Sobre o projeto
Este projeto corresponde ao **segundo exercício individual do Bootcamp de Back-end da WoMakersCode**.

O objetivo é desenvolver uma aplicação de console em C# para cadastrar vendas, listar vendas cadastradas e realizar pagamentos utilizando diferentes formas de pagamento. O exercício aplica conceitos de Programação Orientada a Objetos estudados no módulo.

## Funcionamento do sistema
Ao iniciar a aplicação, o sistema apresenta o seguinte menu:

```text
1 - Cadastrar venda
2 - Listar vendas
3 - Realizar pagamento
0 - Sair
```

### Cadastrar venda
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
- Código da venda
- Nome do cliente
- CPF do cliente
- Valor original
- Situação da venda

Quando a venda estiver paga, também são apresentados:
- Forma de pagamento utilizada
- Valor final calculado

### Realizar pagamento
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
