using BSFood.DataTransfer;
using BSFood.Models;

namespace BSFood.BusinessLogic.Interfaces
{
    public interface IMesa
    {
        Retorno RetornaMesa(int intCodigo);

        Retorno RetornaListaMesa(string strParametro, bool blnLivre = true, bool blnOcupado = true);

        Retorno SalvarMesa(tbMesa objMesa, int intFunCodigo);

        Retorno FecharMesa(tbMesa objMesa, int intFunCodigo);

        Retorno ExcluirMesa(tbMesa objMesa, int intFunCodigo);
    }
}