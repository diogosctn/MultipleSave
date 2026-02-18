using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultipleSave
{
    class CommandHandler : SimpleCommandHandler
    {
        public static string ID = "MultipleSave.NewCommand";

        #region SimpleCommandHandler Members

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return true;
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {
            MultipleSaveWorkstep workstep = new MultipleSaveWorkstep();
            MultipleSaveWorkstep.Arguments args = new MultipleSaveWorkstep.Arguments();
            MultipleSaveWorkstepUI workstepUI = new MultipleSaveWorkstepUI(workstep, args, null);
            workstepUI.Dock = DockStyle.Fill;

            Form form = new Form();
            form.Controls.Add(workstepUI);
            form.ShowIcon = false;
            form.Text = "Plugin Multiple Save Test";
            form.AutoSize = false;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = false;
            form.MinimizeBox = true;

            form.MinimumSize = workstepUI.MinimumSize;

            form.ClientSize = workstepUI.MinimumSize;
            PetrelSystem.ShowModeless(form);
        }

        #endregion
    }
}
