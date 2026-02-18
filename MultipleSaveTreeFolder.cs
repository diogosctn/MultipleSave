using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Basics;

namespace MultipleSave
{
    /// <summary>
    /// This class contains the subitems of the MultipleSaveTreeFolder.
    /// </summary>
    class MultipleSaveTreeFolder : IObservableElementCollection, INameInfoSource, IImageInfoSource
    {
        object data;

        /// <summary>
        /// Contains the list of the subitems.
        /// </summary>
        protected System.Collections.ArrayList children = new System.Collections.ArrayList();

        public MultipleSaveTreeFolder(object data)
        {
			this.Name = "MultipleSaveTreeFolder";

            this.EnumerableChanged += this.TreeFolder1_ChildrenChanged;
            this.data = data;

            this.GetData();
        }

        public MultipleSaveTreeFolder()
        {
            this.Name = "MultipleSaveTreeFolder";

            this.EnumerableChanged += this.TreeFolder1_ChildrenChanged;

            this.GetData();
        }

        private void TreeFolder1_ChildrenChanged(object sender, ElementEnumerableChangeEventArgs e)
        {
        }

        /// <summary>
        /// This method should fill the folder with the subitems.
        /// </summary>
        private void GetData()
        {
            this.children.Clear();

            // TODO: implement the filling of the folder.
        }

        /// <summary>
        /// The name of the folder. This will be displayed in the UI.
        /// </summary>
        public string Name { get; set; }

        #region IObservableElementEnumerable Members

        public event EventHandler<ElementEnumerableChangeEventArgs> EnumerableChanged;

        #endregion

        #region IEnumerable Members

        public System.Collections.IEnumerator GetEnumerator()
        {
            return children.GetEnumerator();
        }

        #endregion

        #region IObservableElementCollection Members

        /// <summary>
        /// Adds the given element to the folder.
        /// </summary>
        /// <param name="element">The child is being added.</param>
        /// <param name="context">The context for the add.</param>
        public void AddElement(object element, AddElementContext context)
        {
            this.children.Add(element);
            this.OnChildrenChanged(ElementEnumerableChangeAction.Add);
        }

        /// <summary>
        /// Returns if the element can be added to the folder or not.
        /// </summary>
        /// <param name="element">The element to be added.</param>
        /// <returns>True when it can be added, otherwise false.</returns>
        public bool CanAddElement(object element, AddElementContext context)
        {
            return element is object;
        }

        /// <summary>
        /// Indicates if the given child can be removed or not.
        /// </summary>
        /// <param name="element">The child to remove.</param>
        /// <param name="context">The context of the removing.</param>
        /// <returns>True when it can be removed, otherwise false.</returns>
        public bool CanRemoveElement(object element, RemoveElementContext context)
        {
            return this.children.Contains(element);
        }

        /// <summary>
        /// Removes the element from the folder.
        /// </summary>
        /// <param name="element">The element is being removed.</param>
        /// <param name="context">The context for the removing.</param>
        public void RemoveElement(object element, RemoveElementContext context)
        {
            if (this.children.Contains(element))
            {
                this.children.Remove(element);
                this.OnChildrenChanged(ElementEnumerableChangeAction.Remove);
            }
        }

        #endregion

        /// <summary>
        /// Fires the <seealso cref="EnumerableChanged"/> event.
        /// </summary>
        protected void OnChildrenChanged(ElementEnumerableChangeAction action)
        {
            EnumerableChanged?.Invoke(this, new ElementEnumerableChangeEventArgs(this, action));
        }

        #region INameInfoSource Members

		public NameInfo NameInfo
		{
			get { return new MultipleSaveTreeFolderNameInfo(this); }
		}

		#endregion

		#region IImageInfoSource Members

		public ImageInfo ImageInfo
		{
			get { return new MultipleSaveTreeFolderImageInfo(); }
		}
		
		#endregion
    }
}
