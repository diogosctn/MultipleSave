using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  MultipleSave
{
	/// <summary>
	/// PresentationFactory can retreive the appropriate Presentation instance
	/// for the given domainobject type.
	/// Singleton class.
	/// </summary>
	class MultipleSaveTreeItemFactory : INameInfoFactory, IImageInfoFactory
	{
		/// <summary>
		/// Singleton member variable.
		/// </summary>
		private static MultipleSaveTreeItemFactory instance = new MultipleSaveTreeItemFactory();
		/// <summary>
		/// Gets the singleton instance.
		/// </summary>
		public static MultipleSaveTreeItemFactory Instance
		{
			get { return instance; }
		}

		/// <summary>
		/// Private constructor, to prevent instantiation.
		/// <seealso cref="Instance"/>
		/// </summary>
		private MultipleSaveTreeItemFactory()
		{
            DataManager.WorkspaceEvents.Opened += PersistedInitialize;
        }

        private void PersistedInitialize(object sender, EventArgs e)
        {
            List<CustomDomainObjectComplete> CompleteItems = DataSourceCompleteFactory.Get(DataManager.DataSourceManager).Items.OfType<CustomDomainObjectComplete>().ToList();
            List<CustomDomainObjectText> TextItems = DataSourceTextFactory.Get(DataManager.DataSourceManager).Items.OfType<CustomDomainObjectText>().ToList();
            List<CustomDomainObjectNumber> NumberItems = DataSourceNumberFactory.Get(DataManager.DataSourceManager).Items.OfType<CustomDomainObjectNumber>().ToList();

            foreach (var item in CompleteItems)
            {
                using (ITransaction txn = DataManager.NewTransaction())
                {
                    try
                    {
                        MultipleSaveTreeItem multipleSaveTreeItem = new MultipleSaveTreeItem(item);

                        TextItems.ForEach(textItem =>
                        {
                            if (textItem.ParentDroid != item.Droid) return;
                            MultipleSaveTextTreeItem multipleSaveTextTreeItem = new MultipleSaveTextTreeItem(textItem);
                            multipleSaveTreeItem.AddElement(multipleSaveTextTreeItem, new Slb.Ocean.Petrel.Basics.AddElementContext());
                        });

                        NumberItems.ForEach(numberItem =>
                        {
                            if (numberItem.ParentDroid != item.Droid) return;
                            MultipleSaveNumberTreeItem multipleSaveNumberTreeItem = new MultipleSaveNumberTreeItem(numberItem);
                            multipleSaveTreeItem.AddElement(multipleSaveNumberTreeItem, new Slb.Ocean.Petrel.Basics.AddElementContext());
                        });

                        txn.Lock(PetrelProject.PrimaryProject);
                        PetrelProject.PrimaryProject.Extensions.Add(multipleSaveTreeItem);
                        txn.Commit();
                    }
                    catch (Exception ex)
                    {
                        txn.Abandon();
                        PetrelLogger.ErrorBox("Erro ao salvar: " + ex.Message);
                    }
                }
            }
        }

        #region INameInfoFactory Members

        /// <summary>
        /// Retreives the appropriate NameInfo instance for the given domainobject.
        /// </summary>
        /// <param name="domainObject">the domainobject which needs NameInfo</param>
        /// <returns>the NameInfo instance</returns>
        public NameInfo GetNameInfo(object domainObject)
		{
			if (domainObject is INameInfoSource)
				return (domainObject as INameInfoSource).NameInfo;
			else
				return null;
		}

		#endregion

		#region IImageInfoFactory Members

		/// <summary>
		/// Retreives the appropriate ImageInfo instance for the given domainobject.
		/// </summary>
		/// <param name="domainObject">the domainobject which needs ImageInfo</param>
		/// <returns>the ImageInfo instance</returns>
		public ImageInfo GetImageInfo(object domainObject)
		{
			if (domainObject is IImageInfoSource)
				return (domainObject as IImageInfoSource).ImageInfo;
			else
				return null;
		}

		#endregion
	}
}

