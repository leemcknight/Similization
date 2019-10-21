using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace McKnight.Similization.Client
{
	/// <summary>
	/// A Strongly typed collection containing <see cref="IDiplomacyTaskLink"/> objects.
	/// </summary>
	public class DiplomacyTaskLinkCollection : CollectionBase, IEnumerable<IDiplomacyTaskLink>
	{
		/// <summary>
		/// Gets the <see cref="IDiplomacyTaskLink"/> at the specified index.
		/// </summary>
		public IDiplomacyTaskLink this[int index]
		{
			get 
			{
				return (IDiplomacyTaskLink)InnerList[index];
			}
		}

		/// <summary>
		/// Adds the <c>IDiplomacyTaskLink</c> to the collection.
		/// </summary>
		/// <param name="taskLink">The task link to add.</param>
		/// <returns>An <c>int</c> representing the index of the task link in the collection.</returns>
		public int Add(IDiplomacyTaskLink taskLink)
		{
			int index = InnerList.Add(taskLink);

			CollectionChangeEventArgs e = new CollectionChangeEventArgs(CollectionChangeAction.Add, taskLink);
			OnCollectionChanged(e);
			return index;
		}

		/// <summary>
		/// Removes the <c>IDiplomacyTaskLink</c> from the collection.
		/// </summary>
		/// <param name="taskLink"></param>
		public void Remove(IDiplomacyTaskLink taskLink)
		{
			InnerList.Remove(taskLink);
			CollectionChangeEventArgs e = new CollectionChangeEventArgs(CollectionChangeAction.Remove, taskLink);
			OnCollectionChanged(e);
		}

        /// <summary>
        /// Returns an enumerator that can interate through the collection.
        /// </summary>
        /// <returns></returns>
        public new IEnumerator<IDiplomacyTaskLink> GetEnumerator()
        {
            foreach (IDiplomacyTaskLink taskLink in InnerList)
            {
                yield return taskLink;
            }
        }
		
		private event CollectionChangeEventHandler collectionChanged;


		/// <summary>
		/// Occurs when a <c>IDiplomacyTaskLink</c> is added or removed from the collection.
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


		/// <summary>
		/// Fires the <c>CollectionChanged</c> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if(this.collectionChanged != null)
			{
				this.collectionChanged(this,e);
			}
		}

		/// <summary>
		/// Fires the <c>Cleared</c> event.
		/// </summary>
		protected override void OnClearComplete()
		{
			CollectionChangeEventArgs e = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
			OnCollectionChanged(e);
			base.OnClearComplete();
		}

		/// <summary>
        /// Copies a range of elements from the <see cref="DiplomacyTaskLinkCollection"/> to a
        /// compatible one-dimensional array, starting at the specified index of
        /// the target array.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		/// <param name="count"></param>
		public void CopyTo(int index, IDiplomacyTaskLink[] array, int arrayIndex, int count)
		{
			this.InnerList.CopyTo(index, array, arrayIndex, count);
		}

        /// <summary>
        /// Copies the entire <see cref="DiplomacyTaskLinkCollection"/> to a compatible
        /// one-dimensional array, starting at the specified index of the target
        /// array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
		public void CopyTo(IDiplomacyTaskLink[] array, int arrayIndex)
		{
			this.InnerList.CopyTo(array, arrayIndex);
		}

        /// <summary>
        /// Copies the entire <see cref="DiplomacyTaskLinkCollection"/> to a compatible
        /// one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array"></param>
		public void CopyTo(IDiplomacyTaskLink[] array)
		{
			this.InnerList.CopyTo(array);
		}

        /// <summary>
        /// Inserts an element into the <see cref="DiplomacyTaskLinkCollection"/> at the specified
        /// index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="taskLink"></param>
		public void Insert(int index, IDiplomacyTaskLink taskLink)
		{
			this.InnerList.Insert(index, taskLink);
		}

        /// <summary>
        /// Searches for the specified <see cref="IDiplomacyTaskLink"/> and returns the zero-based index of
        /// the first occurrence within the range of elements in the
        /// <see cref="DiplomacyTaskLinkCollection"/> that starts at the specified index and contains
        /// the specified number of elements.
        /// </summary>
        /// <param name="taskLink"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
		public int IndexOf(IDiplomacyTaskLink taskLink, int startIndex, int count)
		{
			return this.InnerList.IndexOf(taskLink, startIndex, count);
		}

        /// <summary>
        /// Searches for the specified <see cref="IDiplomacyTaskLink"/> and returns the zero-based index of
        /// the first occurrence within the range of elements in the
        /// <see cref="DiplomacyTaskLinkCollection"/> that extends from the specified index to the
        /// last element.
        /// </summary>
        /// <param name="taskLink"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
		public int IndexOf(IDiplomacyTaskLink taskLink, int startIndex)
		{
			return this.InnerList.IndexOf(taskLink, startIndex);
		}

        /// <summary>
        /// Searches for the specified <see cref="IDiplomacyTaskLink"/> and returns the zero-based index of
        /// the first occurrence within the entire <see cref="DiplomacyTaskLinkCollection"/>.
        /// </summary>
        /// <param name="taskLink"></param>
        /// <returns></returns>
		public int IndexOf(IDiplomacyTaskLink taskLink)
		{
			return this.InnerList.IndexOf(taskLink);
		}

        /// <summary>
        /// Determines whether a <see cref="IDiplomacyTaskLink"/> is in the <see cref="DiplomacyTaskLinkCollection"/>.
        /// </summary>
        /// <param name="taskLink"></param>
        /// <returns></returns>
        public bool Contains(IDiplomacyTaskLink taskLink)
        {
            return this.InnerList.Contains(taskLink);
        }
	}
}
