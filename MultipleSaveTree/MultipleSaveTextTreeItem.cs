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
	class MultipleSaveTextTreeItem : INameInfoSource, IImageInfoSource
	{
		/// <summary>
		/// MultipleSaveTextTreeItem symbolises this instance in the tree.
		/// </summary>
		public CustomDomainObjectText data { get; private set; }

        public MultipleSaveTextTreeItem(CustomDomainObjectText dataobj)
		{
			this.data = dataobj;
			this.Text = "MultipleSaveTextTreeItem";
		}

		public string Text { get; set; }

		#region INameInfoSource Members

		public NameInfo NameInfo
		{
			get { return new MultipleSaveTextTreeItemNameInfo(this); }
		}

		#endregion

		#region IImageInfoSource Members

		public ImageInfo ImageInfo
		{
			get { return new MultipleSaveTextTreeItemImageInfo(); }
		}
		
		#endregion
	}

}
