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
    public class CustomDomainObjectText : IIdentifiable, INameInfoSource, ICopyable, IMovableElement
    {
        [Archived]
        private string _name = "Multiple Save | Text";

        [Archived]
        private string _value = null;

        [ArchivableContextInject]
        private StructuredArchiveDataSource _dataSource;

        private CustomDomainObjectText(Droid droid, StructuredArchiveDataSource dataSource)
        {
            _dataSource = dataSource;
            Droid = droid;
            dataSource.AddItem(Droid, this);
        }

        public CustomDomainObjectText(StructuredArchiveDataSource dataSource)
            : this(dataSource.GenerateDroid(), dataSource)
        {
        }

        public CustomDomainObjectText(string name, StructuredArchiveDataSource dataSource)
            : this(dataSource.GenerateDroid(), dataSource)
        {
            _name = name;
        }

        public CustomDomainObjectText(CustomDomainObjectText customDomainObject, StructuredArchiveDataSource dataSource)
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
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
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

        #region ICopyable

        public object CreateSnapshot()
        {
            return new CustomDomainObjectText("Copy of " + _name, _dataSource);
        }

        public object Copy(CopyContext context)
        {
            CustomDomainObjectText newCustomDomainObject = null;

            if (context.TargetCollection is IInput || context.TargetCollection is Collection)
            {
                if (context.KeepIdentity)
                {
                    newCustomDomainObject = new CustomDomainObjectText(this, _dataSource);
                }
                else
                {
                    newCustomDomainObject = context.Snapshot as CustomDomainObjectText ?? (CustomDomainObjectText)CreateSnapshot();
                }
            }

            return newCustomDomainObject;
        }

        #endregion

        #region IMovableElement

        public bool CanMove(MovingContext context)
        {
            return context.NewParent is IInput || context.NewParent is Collection;
        }

        public void OnMoving(MovingContext context)
        {
        }

        #endregion

    }

}