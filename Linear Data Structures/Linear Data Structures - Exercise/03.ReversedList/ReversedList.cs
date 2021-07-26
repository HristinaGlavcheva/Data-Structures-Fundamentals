namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
                
            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                
                return this.items[this.Count - 1 - index];
            }
            set
            {
                this.ValidateIndex(index);

                this.items[index] = value;
            }
        }

        
        public int Count { get; private set; }

        public void Add(T item)
        {
            this.EnsureCapacity();

            this.items[this.Count] = item;
            this.Count++;
        }

       
        public bool Contains(T item)
        {
            bool contains = false;

            foreach (var element in this.items)
            {
                if (element.Equals(item))
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        public int IndexOf(T item)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (this.items[i].Equals(item))
                {
                    return this.Count - 1 - i;
                }
            }
            
            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.EnsureCapacity();

            for (int i = this.Count; i >= this.Count - index; i--)
            {
                this.items[i] = this.items[i-1];
            }

            this.items[this.Count - index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            if (!this.Contains(item))
            {
                return false;
            }

            int index = this.IndexOf(item);
            this.RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (int i = this.Count - index; i < this.Count - 1; i++)
            {
                this.items[i] = this.items[i + 1];
            }

            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void EnsureCapacity()
        {
            if(this.Count == this.items.Length)
            {
                var newArray = new T[this.items.Length * 2];

                Array.Copy(this.items, newArray, this.Count);

                this.items = newArray;
            }
        }

        private void ValidateIndex(int index)
        {
            if(index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}