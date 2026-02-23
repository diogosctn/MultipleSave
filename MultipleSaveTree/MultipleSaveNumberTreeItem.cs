using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel.UI;
using System.Drawing;

namespace MultipleSave
{
	/// <summary>
	/// TreeItems usually appear in the Petrel Input tree, as a leaf.
	/// (cannot contain more elements under itself)
	/// </summary>
	class MultipleSaveNumberTreeItem : INameInfoSource, IImageInfoSource
	{
        /// <summary>
        /// MultipleSaveNumberTreeItem symbolises this instance in the tree.
        /// </summary>
        public CustomDomainObjectText data { get; private set; }

        public MultipleSaveNumberTreeItem(CustomDomainObjectText dataobj)
		{
			this.data = dataobj;
			this.Text = "MultipleSaveNumberTreeItem";
		}

		public string Text { get; set; }

		#region INameInfoSource Members

		public NameInfo NameInfo
		{
			get { return new MultipleSaveNumberTreeItemNameInfo(this); }
		}

		#endregion

		#region IImageInfoSource Members

		public ImageInfo ImageInfo
		{
			get { return new MultipleSaveNumberTreeItemImageInfo(); }
		}
		
		#endregion
	}

}
