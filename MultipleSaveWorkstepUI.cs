using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Workflow;
using System;
using System.Drawing;
using System.Windows.Forms;

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
            MultipleSaveTreeManager.CreateFolder();
            MultipleSaveTreeManager.CreateFullDataItem();
            MultipleSaveTreeManager.CreateNumberDataItem();

            MultipleSaveTreeManager.SaveTextDataItem(txtInfo.Text);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            txtInfo.Text = MultipleSaveTreeManager.GetTextFromSelectedObject();
        }
    }
}
