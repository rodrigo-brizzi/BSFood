﻿using BSFood.Apoio;
using BSFood.DataTransfer;
using BSFood.Models;

namespace BSFood.BusinessLogic.Interfaces
{
    public interface IFormaPagamento
    {
        Retorno RetornaFormaPagamento(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaFormaPagamento();

        Retorno RetornaListaFormaPagamento(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno SalvarFormaPagamento(tbFormaPagamento objFormaPagamento, int intFunCodigo);

        Retorno ExcluirFormaPagamento(int intCodigo);
    }
}
