using BSFoodFramework.Models;

namespace BSFoodFramework.DataTransfer
{
    public class ConfigStorage
    {
        public tbFuncionario objFuncionario { get; set; }
        public tbPerfilAcesso objPerfilAcesso { get; set; }
        public tbEmpresa objEmpresa { get; set; }
        public tbConfiguracao objConfiguracao { get; set; }
        public int intCaiCodigo { get; set; }
    }
}
