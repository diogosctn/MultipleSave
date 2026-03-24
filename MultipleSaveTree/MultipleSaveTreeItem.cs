using Slb.Ocean.Core;
using Slb.Ocean.Petrel.Basics;
using Slb.Ocean.Petrel.UI;
using System;
using System.Drawing;

namespace MultipleSave
{
	/// <summary>
	/// TreeItems usually appear in the Petrel Input tree, as a leaf.
	/// (cannot contain more elements under itself)
	/// </summary>
	class MultipleSaveTreeItem : IObservableElementCollection, INameInfoSource, IImageInfoSource
	{
		/// <summary>
		/// MultipleSaveTreeItem symbolises this instance in the tree.
		/// </summary>
		public CustomDomainObjectComplete data;

        /// <summary>
        /// Contains the list of the subitems.
        /// </summary>
        protected System.Collections.ArrayList children = new System.Collections.ArrayList();

        public MultipleSaveTreeItem(CustomDomainObjectComplete dataobj)
		{
			this.data = dataobj;
            this.Text = "MultipleSaveTreeItem";
		}

		public string Text { get; set; }

		#region INameInfoSource Members

		public NameInfo NameInfo
		{
			get { return new MultipleSaveTreeItemNameInfo(this); }
		}

        #endregion

        #region IImageInfoSource Members

        public event EventHandler<ElementEnumerableChangeEventArgs> EnumerableChanged;

        public ImageInfo ImageInfo
		{
			get { return new MultipleSaveTreeItemImageInfo(); }
		}

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
    }

}
