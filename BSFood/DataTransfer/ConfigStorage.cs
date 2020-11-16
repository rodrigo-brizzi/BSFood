using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataTransfer
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
