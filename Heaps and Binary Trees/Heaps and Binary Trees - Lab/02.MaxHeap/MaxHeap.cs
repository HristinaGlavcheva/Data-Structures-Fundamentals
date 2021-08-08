namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> heap;

        public MaxHeap()
        {
            this.heap = new List<T>();
        }

        public int Size { get { return this.heap.Count; } }

        public void Add(T element)
        {
            this.heap.Add(element);
            this.HeapifyUp(this.heap.Count - 1);
        }

        public T Peek()
        {
            if (this.heap.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.heap[0];
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;

            if (this.heap[index].CompareTo(this.heap[parentIndex]) > 0)
            {
                T temp = this.heap[parentIndex];
                this.heap[parentIndex] = this.heap[index];
                this.heap[index] = temp;

                this.HeapifyUp(parentIndex);
            }
        }
    }
}
