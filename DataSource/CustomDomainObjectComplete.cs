using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Basics;
using Slb.Ocean.Petrel.Data;
using Slb.Ocean.Petrel.Data.Persistence;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;

namespace MultipleSave
{
    /// <summary>
    /// If there is any change in the object type (number or type of class members changes, release version changes, etc.), a new version of the type should be provided.
    /// If the release is also changed, the updated Release should also be provided.
    /// ArchivableAliasAttribute allows type remapping along with ArchivableAttribute.This allows moving a type to a different namespace, assembly, or to change the signing key.
    /// To get more information please refer to Ocean Development User guide and chm.
    /// </summary>
    [Archivable(Version = 1, FromRelease = "2024.1")]
    public class CustomDomainObjectComplete : IIdentifiable, INameInfoSource, IDeletable
    {
        [Archived]
        private string _name = "Multiple Save | Complete";

        [Archived]
        private string _text = null;

        [Archived]
        private double _number = double.NaN;

        [ArchivableContextInject]
        private StructuredArchiveDataSource _dataSource;

        private CustomDomainObjectComplete(Droid droid, StructuredArchiveDataSource dataSource)
        {
            _dataSource = dataSource;
            Droid = droid;
            dataSource.AddItem(Droid, this);
        }

        public CustomDomainObjectComplete(StructuredArchiveDataSource dataSource)
            : this(dataSource.GenerateDroid(), dataSource)
        {
        }

        public CustomDomainObjectComplete(string name, StructuredArchiveDataSource dataSource)
            : this(dataSource.GenerateDroid(), dataSource)
        {
            _name = name;
        }

        public CustomDomainObjectComplete(CustomDomainObjectComplete customDomainObject, StructuredArchiveDataSource dataSource)
            : this(customDomainObject.Droid, dataSource)
        {
            
        }

        #region IIdentifiable Members

        [Archived]
        public Droid Droid
        {
            get; private set;
        }

        /// <summary>
        /// Propriedade pública para acessar e modificar o valor do texto.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    if (_dataSource != null) _dataSource.IsDirty = true;
                }
            }
        }

        /// <summary>
        /// Propriedade pública para acessar e modificar o valor numérico.
        /// </summary>
        public double Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    if (_dataSource != null) _dataSource.IsDirty = true;
                }
            }
        }

        #endregion

        #region Serialization Events

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject is deserialized.");
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject is deserializing...");
        }

        [OnSerialized]
        void OnSerialized(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject is serialized.");
        }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject is serializing...");
        }

        #endregion

        #region INameInfoSource Members

        public NameInfo NameInfo
        {
            get { return new DefaultNameInfo(_name); }
        }

        #endregion

        #region IDeletable Members
        //IDeletable.DeletableInfo just informs the object can be deleted
        public DeletableInfo DeletableInfo
        {
            get { return new DeletableInfo(); }
        }

        //Implementation of IDeletable.Delete
        public void Delete()
        {
            Deleted?.Invoke(this, new EventArgs());
        }
        public event EventHandler Deleted = null;
        #endregion
    }

}