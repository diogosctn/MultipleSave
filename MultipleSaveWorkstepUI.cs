using System;
using System.Drawing;
using System.Windows.Forms;

using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Core;

namespace MultipleSave
{
    /// <summary>
    /// This class is the user interface which forms the focus for the capabilities offered by the process.  
    /// This often includes UI to set up arguments and interactively run a batch part expressed as a workstep.
    /// </summary>
    partial class MultipleSaveWorkstepUI : UserControl
    {
        private MultipleSaveWorkstep workstep;
        /// <summary>
        /// The argument package instance being edited by the UI.
        /// </summary>
        private MultipleSaveWorkstep.Arguments args;
        /// <summary>
        /// Contains the actual underlaying context.
        /// </summary>
        private WorkflowContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleSaveWorkstepUI"/> class.
        /// </summary>
        /// <param name="workstep">the workstep instance</param>
        /// <param name="args">the arguments</param>
        /// <param name="context">the underlying context in which this UI is being used</param>
        public MultipleSaveWorkstepUI(MultipleSaveWorkstep workstep, MultipleSaveWorkstep.Arguments args, WorkflowContext context)
        {
            InitializeComponent();

            this.workstep = workstep;
            this.args = args;
            this.context = context;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Toda alteração de dados no Petrel deve ocorrer dentro de uma Transação
            //using (ITransaction trans = DataManager.NewTransaction())
            //{
            //    try
            //    {
            //        trans.Lock(PetrelProject.PrimaryProject);

            //        SampleCustomDomainObject dados = Data  ;

            //        dados.MinhaInformacao = txtInfo.Text;

            //        double val = 0;
            //        double.TryParse(txtNumber.Text, out val);
            //        dados.UmValorNumerico = val;

            //        trans.Commit();

            //        lblStatus.Text = "Status: Objeto criado na Input Tree!";
            //    }
            //    catch (Exception ex)
            //    {
            //        lblStatus.Text = "Erro ao criar o objeto: " + ex.Message;
            //    }
            //}

        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
        }
    }
}
