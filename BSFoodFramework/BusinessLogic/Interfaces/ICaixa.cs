﻿using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface ICaixa
    {
        Retorno RetornaCaixa(int intCodigo);

        Retorno RetornaListaCaixa(string strCodigo, string strFuncionario, string strStatus, string strDataAbertura, int intSkip, int intTake);

        Retorno RetornaListaCaixaAberto();

        Retorno RetornaListaCaixaOperacao();

        Retorno AbrirCaixa(tbCaixa objCaixa, int intFunCodigo);

        Retorno LancarMovimentoCaixa(tbCaixaMovimento objCaixaMovimento, int intFunCodigo);

        Retorno RetornaCaixaFechamento(int intCodigo);

        Retorno FecharCaixa(FechamentoCaixa objFechamentoCaixa, int intFunCodigo);

        Retorno ExcluirCaixa(int intCodigo);
    }
}
