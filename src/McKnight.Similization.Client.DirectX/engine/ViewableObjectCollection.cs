using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace LJM.Similization.Client.DirectX.Engine
{
    /// <summary>
    /// A stronly typed collection of <see cref="ViewableObject"/> objects.
    /// </summary>
    public class ViewableObjectCollection : Collection<ViewableObject>
    {
        private event CollectionChangeEventHandler collectionChanged;

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, ViewableObject item)
        {
            base.InsertItem(index, item);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            ViewableObject vo = this[index];
            base.RemoveItem(index);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, vo));
        }

        private void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (collectionChanged != null)
                this.collectionChanged(this, e);
        }

        /// <summary>
        /// Occurs when a <see cref="ViewableObject"/> is added or removed from the 
        /// <see cref="ViewableObjectCollection"/>.
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
