﻿namespace BSFoodFramework.Models
{
    public class tbPerfilAcessoMenu
    {
        public bool pam_permiteAlteracao { get; set; }
        public bool pam_permiteExclusao { get; set; }
        public bool pam_permiteInclusao { get; set; }
        public bool pam_permiteVisualizacao { get; set; }
        public bool pam_toolBar { get; set; }

        public int men_codigo { get; set; }
        public virtual tbMenu tbMenu { get; set; }

        public int pac_codigo { get; set; }
        public virtual tbPerfilAcesso tbPerfilAcesso { get; set; }
    }
}
