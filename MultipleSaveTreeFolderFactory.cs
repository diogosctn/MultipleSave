using System;
using System.Drawing;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel.UI;


namespace MultipleSave
{
	/// <summary>
	/// Factory can retreive the appropriate NameInfo or ImageInfo instance
	/// for the given domainobject type.
	/// Singleton class.
	/// </summary>
	class  MultipleSaveTreeFolderFactory : INameInfoFactory, IImageInfoFactory
	{
		/// <summary>
		/// Singleton member variable.
		/// </summary>
		private static MultipleSaveTreeFolderFactory instance = new  MultipleSaveTreeFolderFactory();
		/// <summary>
		/// Gets the singleton instance.
		/// </summary>
		public static  MultipleSaveTreeFolderFactory Instance
		{
			get { return instance; }
		}

		/// <summary>
		/// Private constructor, to prevent instantiation.
		/// <seealso cref="Instance"/>
		/// </summary>
		private  MultipleSaveTreeFolderFactory()
		{
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
