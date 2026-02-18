using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace MultipleSave
{
    /// <summary>
    /// This class contains all the methods and subclasses of the MultipleSaveWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class MultipleSaveWorkstep : Workstep<MultipleSaveWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override MultipleSaveWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
        {
            return new Arguments(dataSourceManager);
        }
        /// <summary>
        /// Copies the Arguments instance.
        /// </summary>
        /// <param name="fromArgumentPackage">the source Arguments instance</param>
        /// <param name="toArgumentPackage">the target Arguments instance</param>
        protected override void CopyArgumentPackageCore(Arguments fromArgumentPackage, Arguments toArgumentPackage)
        {
            DescribedArgumentsHelper.Copy(fromArgumentPackage, toArgumentPackage);
        }

        /// <summary>
        /// Gets the unique identifier for this Workstep.
        /// </summary>
        protected override string UniqueIdCore
        {
            get
            {
                return "261670a3-e63b-4377-9377-724b965df9ec";
            }
        }
        #endregion

        #region IExecutorSource Members and Executor class

        /// <summary>
        /// Creates the Executor instance for this workstep. This class will do the work of the Workstep.
        /// </summary>
        /// <param name="argumentPackage">the argumentpackage to pass to the Executor</param>
        /// <param name="workflowRuntimeContext">the context to pass to the Executor</param>
        /// <returns>The Executor instance.</returns>
        public Slb.Ocean.Petrel.Workflow.Executor GetExecutor(object argumentPackage, WorkflowRuntimeContext workflowRuntimeContext)
        {
            return new Executor(argumentPackage as Arguments, workflowRuntimeContext);
        }

        public class Executor : Slb.Ocean.Petrel.Workflow.Executor
        {
            Arguments arguments;
            WorkflowRuntimeContext context;

            public Executor(Arguments arguments, WorkflowRuntimeContext context)
            {
                this.arguments = arguments;
                this.context = context;
            }

            public override void ExecuteSimple()
            {
                // TODO: Implement the workstep logic here.
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for MultipleSaveWorkstep.
        /// Each public property is an argument in the package.  The name, type and
        /// input/output role are taken from the property and modified by any
        /// attributes applied.
        /// </summary>
        public class Arguments : DescribedArgumentsByReflection
        {
            public Arguments()
                : this(DataManager.DataSourceManager)
            {                
            }

            public Arguments(IDataSourceManager dataSourceManager)
            {
            }
        }
    
        #region IAppearance Members
        public event EventHandler<TextChangedEventArgs> TextChanged;
        protected void RaiseTextChanged()
        {
            this.TextChanged?.Invoke(this, new TextChangedEventArgs(this));
        }

        public string Text
        {
            get { return Description.Name; }
            private set 
            {
                // TODO: implement set
                this.RaiseTextChanged();
            }
        }

        public event EventHandler<ImageChangedEventArgs> ImageChanged;
        protected void RaiseImageChanged()
        {
            this.ImageChanged?.Invoke(this, new ImageChangedEventArgs(this));
        }

        public System.Drawing.Bitmap Image
        {
            get { return PetrelImages.Modules; }
            private set 
            {
                // TODO: implement set
                this.RaiseImageChanged();
            }
        }
        #endregion

        #region IDescriptionSource Members

        /// <summary>
        /// Gets the description of the MultipleSaveWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return MultipleSaveWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the MultipleSaveWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class MultipleSaveWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private static MultipleSaveWorkstepDescription instance = new MultipleSaveWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static MultipleSaveWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of MultipleSaveWorkstep
            /// </summary>
            public string Name
            {
                get { return "MultipleSaveWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of MultipleSaveWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return ""; }
            }
            /// <summary>
            /// Gets the detailed description of MultipleSaveWorkstep
            /// </summary>
            public string Description
            {
                get { return ""; }
            }

            #endregion
        }
        #endregion

        public class UIFactory : WorkflowEditorUIFactory
        {
            /// <summary>
            /// This method creates the dialog UI for the given workstep, arguments
            /// and context.
            /// </summary>
            /// <param name="workstep">the workstep instance</param>
            /// <param name="argumentPackage">the arguments to pass to the UI</param>
            /// <param name="context">the underlying context in which the UI is being used</param>
            /// <returns>a Windows.Forms.Control to edit the argument package with</returns>
            protected override System.Windows.Forms.Control CreateDialogUICore(Slb.Ocean.Petrel.Workflow.Workstep workstep, object argumentPackage, WorkflowContext context)
            {
                return new MultipleSaveWorkstepUI((MultipleSaveWorkstep)workstep, (Arguments)argumentPackage, context);
            }
        }
    }
}