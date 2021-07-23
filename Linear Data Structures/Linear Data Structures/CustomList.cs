using System;

namespace Linear_Data_Structures
{
    public class CustomList<T>
    {
        private int capacity;
        private T[] array;

        public CustomList(int capacity = 4)
        {
            this.capacity = capacity;
            this.array = new T[capacity];
        }

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this.array[index];
            }
            set
            {
                this.ValidateIndex(index);
                this.array[index] = value;
            }
        }

        public void Add(T item)
        {
            this.EnsureCapacity();

            this.array[this.Count] = item;
            this.Count++;
        }

        public void Clear()
        {
            this.array = new T[this.capacity];
            this.Count = 0;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (int i = index; i < this.Count - 1; i++)
            {
                this.array[i] = this.array[i + 1];
            }

            this.array[this.Count - 1] = default;
            this.Count--;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.array[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
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
            
            //for (int i = 0; i < this.Count; i++)
            //{
            //    if (this.array[i].Equals(item))
            //    {
            //        this.RemoveAt(i);
            //        return true;
            //    }
            //}

            //return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.array[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.EnsureCapacity();
            this.Count++;

            for (int i = this.Count; i > index; i--)
            {
                this.array[i] = this.array[i - 1];
            }

            this.array[index] = item;
        }

        private void EnsureCapacity()
        {
            if (this.Count < this.array.Length)
            {
                return;
            }

            T[] doubledSizeArray = new T[this.array.Length * 2];

            for (int i = 0; i < this.array.Length; i++)
            {
                doubledSizeArray[i] = this.array[i];
            }
            //Array.Copy(this.array, doubledSizeArray, this.array.Length);

            this.array = doubledSizeArray;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
