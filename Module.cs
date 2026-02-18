using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace MultipleSave
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    [ModuleAppearance(typeof(ModuleAppearance))]
    public class Module : IModule
    {
        public Module()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region IModule Members

        /// <summary>
        /// This method runs once in the Module life; when it loaded into the petrel.
        /// This method called first.
        /// </summary>
        public void Initialize()
        {
            DataManager.WorkspaceEvents.Opened += this.WorkspaceOpened;
            DataManager.WorkspaceEvents.Closing += this.WorkspaceClosing;
            DataManager.WorkspaceEvents.Closed += this.WorkspaceClosed;

            // TODO:  Add Module.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            // Register MultipleSave.DataSourceText
            PetrelSystem.AddDataSourceFactory(MultipleSave.DataSourceTextFactory.Instance);
            // Register TreeItem
            CoreSystem.Services.AddService(typeof(MultipleSave.MultipleSaveTreeFolder), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory), MultipleSave.MultipleSaveTreeFolderFactory.Instance);
            CoreSystem.Services.AddService(typeof(MultipleSave.MultipleSaveTreeFolder), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory), MultipleSave.MultipleSaveTreeFolderFactory.Instance);
            CoreSystem.Services.AddService(typeof(MultipleSave.MultipleSaveTreeItem), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory), MultipleSave.MultipleSaveTreeItemFactory.Instance);
            CoreSystem.Services.AddService(typeof(MultipleSave.MultipleSaveTreeItem), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory), MultipleSave.MultipleSaveTreeItemFactory.Instance);
            CoreSystem.Services.AddService(typeof(MultipleSave.MultipleSaveTextTreeItem), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory), MultipleSave.MultipleSaveTextTreeItemFactory.Instance);
            CoreSystem.Services.AddService(typeof(MultipleSave.MultipleSaveTextTreeItem), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory), MultipleSave.MultipleSaveTextTreeItemFactory.Instance);
            CoreSystem.Services.AddService(typeof(MultipleSave.MultipleSaveNumberTreeItem), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory), MultipleSave.MultipleSaveNumberTreeItemFactory.Instance);
            CoreSystem.Services.AddService(typeof(MultipleSave.MultipleSaveNumberTreeItem), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory), MultipleSave.MultipleSaveNumberTreeItemFactory.Instance);
            PetrelSystem.CommandManager.CreateCommand(MultipleSave.MultipleSaveTreeFolderCommandHandler.ID, new MultipleSave.MultipleSaveTreeFolderCommandHandler());
            PetrelSystem.CommandManager.CreateCommand(MultipleSave.MultipleSaveTreeItemCommandHandler.ID, new MultipleSave.MultipleSaveTreeItemCommandHandler());
            PetrelSystem.CommandManager.CreateCommand(MultipleSave.MultipleSaveTextTreeItemCommandHandler.ID, new MultipleSave.MultipleSaveTextTreeItemCommandHandler());
            PetrelSystem.CommandManager.CreateCommand(MultipleSave.MultipleSaveNumberTreeItemCommandHandler.ID, new MultipleSave.MultipleSaveNumberTreeItemCommandHandler());
            // Register CommandHandler
            PetrelSystem.CommandManager.CreateCommand(MultipleSave.CommandHandler.ID, new MultipleSave.CommandHandler());
            // Register MultipleSave.MultipleSaveWorkstep
            MultipleSave.MultipleSaveWorkstep multiplesaveworkstepInstance = new MultipleSave.MultipleSaveWorkstep();
            PetrelSystem.WorkflowEditor.AddUIFactory<MultipleSave.MultipleSaveWorkstep.Arguments>(new MultipleSave.MultipleSaveWorkstep.UIFactory());
            PetrelSystem.WorkflowEditor.Add(multiplesaveworkstepInstance);

            // TODO:  Add Module.Integrate implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {
            // Add Ribbon Configuration file
            PetrelSystem.ConfigurationService.AddConfiguration(MultipleSave.Properties.Resources.OceanRibbon);
            // Add Ribbon Configuration file
            PetrelSystem.ConfigurationService.AddConfiguration(MultipleSave.Properties.Resources.OceanRibbonConfiguration);

            // TODO:  Add Module.IntegratePresentation implementation
        }

        /// <summary>
        /// IModule interface does not define this method. 
        /// It is an eventhandler method, which is subscribed in the Initialize() method above,
        /// and is called every time when Petrel creates or loads a project.
        /// </summary>
        private void WorkspaceOpened(object sender, WorkspaceEventArgs args)
        {

            // TODO:  Add Workspace Opened eventhandler implementation
        }

        /// <summary>
        /// IModule interface does not define this method. 
        /// It is an eventhandler method, which is subscribed in the Initialize() method above,
        /// and is called every time before Petrel closes a project.
        /// </summary>
        private void WorkspaceClosing(object sender, WorkspaceCancelEventArgs args)
        {
            // TODO:  Add Workspace Closing eventhandler implementation
        }

        /// <summary>
        /// IModule interface does not define this method. 
        /// It is an eventhandler method, which is subscribed in the Initialize() method above,
        /// and is called every time after Petrel closed a project.
        /// </summary>
        private void WorkspaceClosed(object sender, WorkspaceEventArgs args)
        {
            // TODO:  Add Workspace Closed eventhandler implementation
        }

        /// <summary>
        /// This method runs once in the Module life.
        /// right before the module is unloaded. 
        /// It usually happens when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            // Unregister DataSourceText
            PetrelSystem.RemoveDataSourceFactory(MultipleSave.DataSourceTextFactory.Instance);
            CoreSystem.Services.RemoveService(typeof(MultipleSave.MultipleSaveTreeFolder), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory));
            CoreSystem.Services.RemoveService(typeof(MultipleSave.MultipleSaveTreeFolder), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory));
            CoreSystem.Services.RemoveService(typeof(MultipleSave.MultipleSaveTreeItem), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory));
            CoreSystem.Services.RemoveService(typeof(MultipleSave.MultipleSaveTreeItem), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory));
            CoreSystem.Services.RemoveService(typeof(MultipleSave.MultipleSaveTextTreeItem), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory));
            CoreSystem.Services.RemoveService(typeof(MultipleSave.MultipleSaveTextTreeItem), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory));
            CoreSystem.Services.RemoveService(typeof(MultipleSave.MultipleSaveNumberTreeItem), typeof(Slb.Ocean.Petrel.UI.INameInfoFactory));
            CoreSystem.Services.RemoveService(typeof(MultipleSave.MultipleSaveNumberTreeItem), typeof(Slb.Ocean.Petrel.UI.IImageInfoFactory));
            // Unregister MultipleSave.MultipleSaveWorkstep
            PetrelSystem.WorkflowEditor.RemoveUIFactory<MultipleSave.MultipleSaveWorkstep.Arguments>();
            DataManager.WorkspaceEvents.Opened -= this.WorkspaceOpened;
            DataManager.WorkspaceEvents.Closing -= this.WorkspaceClosing;
            DataManager.WorkspaceEvents.Closed -= this.WorkspaceClosed;

            // TODO:  Add Module.Disintegrate implementation
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add Module.Dispose implementation
        }

        #endregion

    }

    #region ModuleAppearance Class

    /// <summary>
    /// Appearance (or branding) for a Slb.Ocean.Core.IModule.
    /// This is associated with a module using Slb.Ocean.Core.ModuleAppearanceAttribute.
    /// </summary>
    internal class ModuleAppearance : IModuleAppearance
    {
        /// <summary>
        /// Description of the module.
        /// </summary>
        public string Description
        {
            get { return "Module"; }
        }

        /// <summary>
        /// Display name for the module.
        /// </summary>
        public string DisplayName
        {
            get { return "Module"; }
        }

        /// <summary>
        /// Returns the name of a image resource.
        /// </summary>
        public string ImageResourceName
        {
            get { return null; }
        }

        /// <summary>
        /// A link to the publisher or null.
        /// </summary>
        public Uri ModuleUri
        {
            get { return null; }
        }
    }

    #endregion
}