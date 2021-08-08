namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> queue;

        public PriorityQueue()
        {
            this.queue = new List<T>();
        }

        public int Size { get { return this.queue.Count; } }

        public T Dequeue()
        {
            if (this.queue.Count == 0)
            {
                throw new InvalidOperationException();
            }

            T removedElement = this.queue[0];
            this.queue[0] = this.queue[this.queue.Count - 1];
            this.queue.RemoveAt(this.queue.Count - 1);

            this.HeapifyDown(0);

            return removedElement;
        }

        private void HeapifyDown(int index)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int maxChildIndex = leftChildIndex;

            if (leftChildIndex >= this.queue.Count)
            {
                return; 
            }

            if (rightChildIndex < this.queue.Count && this.queue[leftChildIndex].CompareTo(this.queue[rightChildIndex]) < 0)
            {
                maxChildIndex = rightChildIndex;
            }

            if (this.queue[index].CompareTo(this.queue[maxChildIndex]) < 0)
            {
                T temp = this.queue[index];
                this.queue[index] = this.queue[maxChildIndex];
                this.queue[maxChildIndex] = temp;

                this.HeapifyDown(maxChildIndex);
            }
        }

        public void Add(T element)
        {
            this.queue.Add(element);
            this.HeapifyUp(this.queue.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;

            if (this.queue[index].CompareTo(this.queue[parentIndex]) > 0)
            {
                T temp = this.queue[index];
                this.queue[index] = this.queue[parentIndex];
                this.queue[parentIndex] = temp;

                this.HeapifyUp(parentIndex);
            }
        }

        public T Peek()
        {
            if (this.queue.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.queue[0];
        }
    }
}
