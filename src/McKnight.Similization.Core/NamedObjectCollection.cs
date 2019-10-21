using System;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace McKnight.Similization.Core
{

    /// <summary>
    /// Defines a generic collection of <see cref="CoreItem"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NamedObjectCollection<T> : Collection<T> where T : NamedObject
    {
        private event CollectionChangeEventHandler collectionChanged;

        /// <summary>
        /// Gets the item with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T this[string name]
        {
            get
            {
                foreach (T t in this.Items)
                {
                    if (t.Name == name)
                        return t;                    
                }
                return null;
            }
        }

        /// <summary>
        /// Inserts the specified item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, T item)
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
            object item = this.Items[index];
            base.RemoveItem(index);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, item));
        }

        /// <summary>
        /// Occurs when an item is added or removed from the 
        /// <see cref="CoreItemCollection"/>.
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

        private void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (this.collectionChanged != null)
                this.collectionChanged(this, e);
        }
    }
}
