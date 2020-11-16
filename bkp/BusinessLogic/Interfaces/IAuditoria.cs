using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IAuditoria
    {
        Retorno RetornaListaAuditoria(int intCodRegistro, object objTabela);

        void SalvarAuditoria(int intCodRegistro, enOperacao enAcao, object objTabela, int intFunCodigo);      
    }
}
