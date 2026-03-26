using Slb.Ocean.Core;
using Slb.Ocean.Petrel.Basics;
using Slb.Ocean.Petrel.Data;
using Slb.Ocean.Petrel.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MultipleSave
{
    /// <summary>
    /// Represents the base structure of objects that consumes the Petrel's Data source
    /// providing some basic methods and members
    /// </summary>
    [Archivable(Version = 1)]
    public abstract class PersistedObject : IIdentifiable, IDeletable, IExtensible
    {
        protected PersistedObject(Droid parent,
            bool addToDataSourceAtConstruction = false, bool isDeletable = false)
        {
            if (addToDataSourceAtConstruction) this.Droidize();
            SetParent(parent);
            _isDeletable = isDeletable;
        }

        #region IIdentifiable Members

        [Archived(Name = "Droid")]
        private Droid _droid;

        public Droid Droid
        {
            get
            {
                return _droid;
            }
            private set
            {
                if (DataSource != null) DataSource.IsDirty = true;
                _droid = value;
            }
        }

        [Archived(Name = "ParentDroid")]
        private Droid _parentDroid;

        public Droid ParentDroid
        {
            get
            {
                return _parentDroid;
            }
            set
            {
                if (DataSource != null) DataSource.IsDirty = true;
                _parentDroid = value;
            }
        }

        protected abstract StructuredArchiveDataSource DataSource { get; }

        /// <summary>
        /// Signalizes to data Source that some change happened
        /// </summary>
        protected void FlagDirty()
        {
            if (DataSource != null && Droid != null && Droid != Droid.Empty) DataSource.IsDirty = true;
        }

        /// <summary>
        /// Generates a DROID to the object and adds it to the data source
        /// </summary>
        public void Droidize()
        {
            if (Droid != null && Droid != Droid.Empty) return;
            if (DataSource != null)
            {
                Droid = DataSource.GenerateDroid();
                DataSource.AddItem(this.Droid, this);
            }
        }

        /// <summary>
        /// Clears the droid value. Does not remove from the dataSource.
        /// </summary>
        public void ClearDroid()
        {
            if (Droid != null && !Droid.IsEmpty)
            {
                Droid = Droid.Empty;
            }
        }

        /// <summary>
        /// Removes from data Source and clears the droid value
        /// </summary>
        public void Erase()
        {
            MakeOrphan();
            if (Droid == null || Droid.IsEmpty)
            {
                return;
            }

            DataSource?.RemoveItem(Droid);
            Droid = Droid.Empty;
        }

        public void SetParent(Droid newParent)
        {
            if(newParent == null) return;
            if (!newParent.Equals(ParentDroid))
            {
                MakeOrphan();
                ParentDroid = newParent;
            }
        }

        private void MakeOrphan()
        {
            ParentDroid = Droid.Empty;
        }

        #endregion

        #region IExtensible Members

        public ExtensionData ExtensionData { get; set; }

        #endregion

        #region IDeletable Members

        public event EventHandler Deleted;

        [Archived(Name = "IsDeletable")]
        private bool _isDeletable;

        public DeletableInfo DeletableInfo
        {
            get { return new DeletableInfo(_isDeletable); }
        }

        public void Delete()
        {
            Erase();
            Deleted?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
