// classe abstrata FormaPagamento, que define o comportamento comum dos pagamentos.
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaPagamentos
{
    public abstract class FormaPagamento // A classe é abstract porque não existe um pagamento genérico no sistema
    {
        public abstract string Descricao { get; }
        public abstract decimal CalculaValorFinal(decimal valor);
    }
}