using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace BSFoodFramework.Apoio
{
    public class GerenciaCupom  : IDisposable
    {
        #region Propriedades

        private List<string> arrRelatorio;

        #endregion Propriedades



        #region Métodos

        public void AbreRelatorio(bool blnImprimeCabecalho = true)
        {
            this.arrRelatorio = new List<string>();
            if (blnImprimeCabecalho)
            {
                this.arrRelatorio.Add("=======================================");
                this.arrRelatorio.Add(FrameworkUtil.objConfigStorage.objEmpresa.emp_nomeFantasia);
                this.arrRelatorio.Add(FrameworkUtil.objConfigStorage.objEmpresa.emp_logradouro + "," + FrameworkUtil.objConfigStorage.objEmpresa.emp_numero + "-" + FrameworkUtil.objConfigStorage.objEmpresa.emp_bairro);
                this.arrRelatorio.Add("Fone: " + FrameworkUtil.objConfigStorage.objEmpresa.emp_telefone + (string.IsNullOrWhiteSpace(FrameworkUtil.objConfigStorage.objEmpresa.emp_fax) ? "" : ", " + FrameworkUtil.objConfigStorage.objEmpresa.emp_fax));
                this.arrRelatorio.Add("=======================================");
                this.arrRelatorio.Add(" ");
            }
        }

        public void LinhaRelatorio(string strLinha)
        {
            this.arrRelatorio.Add(FrameworkUtil.RetiraCaracterEspecial(strLinha));
        }

        public void FechaRelatorio(string strImpressora, bool blnImprimeRodape = true)
        {
            if (this.arrRelatorio != null && this.arrRelatorio.Count > 0)
            {
                intContador = 0;
                if (blnImprimeRodape)
                {
                    this.arrRelatorio.Add("=======================================");
                    this.arrRelatorio.Add("www.brizzisoft.com.br  " + DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
                    for(int i = 0; i < 8; i++)
                        this.arrRelatorio.Add("->");
                }
                int intHeight = ((this.arrRelatorio.Count + 1) * 26);
                PrintDocument objPrinter = new PrintDocument();
                objPrinter.PrintPage += new PrintPageEventHandler(objPrinter_PrintPage);
                objPrinter.PrinterSettings.PrinterName = strImpressora;
                PaperSize psCustom = new PaperSize("Custom", 520, intHeight);
                objPrinter.PrinterSettings.DefaultPageSettings.PaperSize = psCustom;
                objPrinter.DefaultPageSettings.PaperSize = psCustom;
                objPrinter.Print();
            }
        }

        public void CortaPapel(string strImpressora)
        {
            this.FechaRelatorio(strImpressora);
            this.AbreRelatorio(false);
        }

        public string RetornaRelatorioTexto()
        {
            string strRetorno = string.Empty;
            foreach (string strLinha in this.arrRelatorio)
            {
                strRetorno += strLinha + Environment.NewLine;
            }
            return strRetorno;
        }

        #endregion Métodos



        #region Eventos

        int intContador = 0;
        void objPrinter_PrintPage(object sender, PrintPageEventArgs e)
        {
            float floPosicao = 0;
            float floMargemEsquerda = 0;
            Font fnImpressao = new Font("Courier New", 8, FontStyle.Bold);
            SolidBrush sbCor = new SolidBrush(Color.Black);

            int contLinhasImpressas = 0;

            for (int i = intContador; i < arrRelatorio.Count; i++)
            {
                if (contLinhasImpressas < 78)
                {
                    string strTexto = arrRelatorio[i];
                    floPosicao += 15;
                    if (arrRelatorio[i].Length > 40)
                    {
                        int intQtdeVezes = strTexto.Length / 40;

                        if (intQtdeVezes > 1)
                        {
                            int intInicio = 0;
                            for (int j = 0; j < intQtdeVezes; j++)
                            {
                                e.Graphics.DrawString(strTexto.Substring(intInicio, 40), fnImpressao, sbCor, floMargemEsquerda, floPosicao, new StringFormat());
                                intInicio += 40;
                                floPosicao += 15;
                            }
                            e.Graphics.DrawString(strTexto.Substring(intInicio, strTexto.Length - intInicio), fnImpressao, sbCor, floMargemEsquerda, floPosicao, new StringFormat());
                        }
                        else
                        {
                            e.Graphics.DrawString(strTexto.Substring(0, 40), fnImpressao, sbCor, floMargemEsquerda, floPosicao, new StringFormat());
                            floPosicao += 15;
                            e.Graphics.DrawString(strTexto.Substring(40, strTexto.Length - 40), fnImpressao, sbCor, floMargemEsquerda, floPosicao, new StringFormat());
                        }
                    }
                    else
                        e.Graphics.DrawString(strTexto, fnImpressao, sbCor, floMargemEsquerda, floPosicao, new StringFormat());
                    contLinhasImpressas++;
                }
                else
                {
                    intContador += contLinhasImpressas - 1;
                    break;
                }
            }
            sbCor.Dispose();

            if (intContador >= arrRelatorio.Count || contLinhasImpressas < 78)
                e.HasMorePages = false;
            else
                e.HasMorePages = true;
        }

        #endregion Eventos


        public void Dispose()
        {

        }
    }
}
