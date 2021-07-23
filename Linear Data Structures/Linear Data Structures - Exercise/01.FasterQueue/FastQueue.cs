namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this.head;

            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.EnsureNotEmpty();

            var oldHead = this.head;
            this.head = this.head.Next;

            this.Count--;

            return oldHead.Item;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item);
            
            if(this.Count == 0)
            {
                this.head = newNode;
                this.tail = newNode;
            }
            else
            {
                var oldTail = this.tail;
                this.tail = newNode;
                oldTail.Next = tail;
            }

            this.Count++;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this.head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void EnsureNotEmpty()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty!");
            }
        }
    }
}