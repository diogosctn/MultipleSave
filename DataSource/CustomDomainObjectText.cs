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
    [Archivable(Version = 1)]
    public class CustomDomainObjectText : PersistedObject, INameInfoSource
    {
        [Archived]
        private string _name = "Multiple Save | Text";

        [Archived]
        private string _value = null;

        [ArchivableContextInject]
        private StructuredArchiveDataSource _dataSource;

        public CustomDomainObjectText(Droid parent) : base(parent, true, false)
        {
        }

        #region PersistedObject Members

        protected override StructuredArchiveDataSource DataSource =>
            DataSourceTextFactory.Get(DataManager.DataSourceManager);

        #endregion

        #region IIdentifiable Members
        /// <summary>
        /// Propriedade pública para acessar e modificar o valor do texto.
        /// </summary>
        public string Text
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    FlagDirty();
                }
            }
        }

        #endregion

        #region Serialization Events

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject3 is deserialized.");
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject3 is deserializing...");
        }

        [OnSerialized]
        void OnSerialized(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject3 is serialized.");
        }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject3 is serializing...");
        }

        #endregion

        #region INameInfoSource Members

        public NameInfo NameInfo
        {
            get { return new DefaultNameInfo(_name); }
        }

        #endregion
    }

}