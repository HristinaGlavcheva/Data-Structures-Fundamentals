using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Linear_Data_Structures
{
    public class CustomStack<T> : IEnumerable<T>
    {
        private T[] array;
        private int capacity;

        public CustomStack(int capacity = 4)
        {
            this.capacity = capacity;
            this.array = new T[capacity];
        }

        public int Count { get; private set; }

        public void Push(T item)
        {
            this.EnsureCapacity();
            
            this.array[this.Count] = item;
            this.Count++;
        }

        public T Pop()
        {
            this.EnsureStackNotEmpty();

            var lastItem = this.array[this.Count - 1];
            this.Count--;

            return lastItem;
        }

        public T Peek()
        {
            this.EnsureStackNotEmpty();

            var lastItem = this.array[this.Count - 1];

            return lastItem;
        }

        public void Clear()
        {
            this.array = new T[this.capacity];
            this.Count = 0;
        }

        private void EnsureCapacity()
        {
            if(this.Count < this.array.Length)
            {
                return;
            }

            T[] doubledSizeArray = new T[this.array.Length * 2];

            for (int i = 0; i < this.Count; i++)
            {
                doubledSizeArray[i] = this.array[i];
            }

            this.array = doubledSizeArray;
        }

        private void EnsureStackNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty!");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
