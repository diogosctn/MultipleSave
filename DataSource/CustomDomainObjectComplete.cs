using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Basics;
using Slb.Ocean.Petrel.Data;
using Slb.Ocean.Petrel.Data.Persistence;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.UI;
using System;
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
    public class CustomDomainObjectComplete : PersistedObject, INameInfoSource
    {
        [Archived]
        private string _name = "Multiple Save | Complete";

        [Archived]
        private string _text = null;

        [Archived]
        private double _number = double.NaN;

        [ArchivableContextInject]
        private StructuredArchiveDataSource _dataSource;

        public CustomDomainObjectComplete() : base(Droid.Empty, true, false)
        {
        }

        #region PersistedObject Members

        protected override StructuredArchiveDataSource DataSource =>
            DataSourceCompleteFactory.Get(DataManager.DataSourceManager);

        #endregion

        #region IIdentifiable Members
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
                    FlagDirty();
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
                    FlagDirty();
                }
            }
        }

        #endregion

        #region Serialization Events

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject1 is deserialized.");
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject1 is deserializing...");
        }

        [OnSerialized]
        void OnSerialized(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject1 is serialized.");
        }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            PetrelLogger.InfoOutputWindow("ArchivableCustomDomainObject1 is serializing...");
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