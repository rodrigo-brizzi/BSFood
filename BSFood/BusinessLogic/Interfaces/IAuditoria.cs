using BSFood.Apoio;
using BSFood.DataTransfer;

namespace BSFood.BusinessLogic.Interfaces
{
    public interface IAuditoria
    {
        Retorno RetornaListaAuditoria(int intCodRegistro, object objTabela);

        void SalvarAuditoria(int intCodRegistro, enOperacao enAcao, object objTabela, int intFunCodigo);      
    }
}
