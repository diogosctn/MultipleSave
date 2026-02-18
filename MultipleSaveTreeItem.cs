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
	class MultipleSaveTreeItem : INameInfoSource, IImageInfoSource
	{
		/// <summary>
		/// MultipleSaveTreeItem symbolises this instance in the tree.
		/// </summary>
		private object data;

		public MultipleSaveTreeItem(object dataobj)
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

		public ImageInfo ImageInfo
		{
			get { return new MultipleSaveTreeItemImageInfo(); }
		}
		
		#endregion
	}

}
