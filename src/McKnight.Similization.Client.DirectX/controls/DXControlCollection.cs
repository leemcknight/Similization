using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LJM.Similization.Client.DirectX.Controls
{
    /// <summary>
    /// Class representing a strongly typed collection of <see cref="DXControl"/> objects.
    /// </summary>
    public class DXControlCollection : Collection<DXControl>
    {
        private CollectionChangeEventHandler collectionChanged;

        protected override void InsertItem(int index, DXControl item)
        {
            base.InsertItem(index, item);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
        }

        protected override void RemoveItem(int index)
        {
            DXControl ctl = this[index];
            base.RemoveItem(index);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, ctl));
        }

        /// <summary>
        /// Fires the <i>CollectionChanged</i> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (this.collectionChanged != null)
                this.collectionChanged(this, e);
        }

        /// <summary>
        /// Occurs when a <see cref="DXControl"/> is added or removed from the collection.
        /// </summary>
        public event CollectionChangeEventHandler CollectionChanged
        {
            add
            {
                this.collectionChanged += value;
            }

            remove
            {
                this.collectionChanged -= value;
            }
        }
    }
}
