namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> currentNode = this._head;

            while (currentNode != null)
            {
                if(currentNode.Value.Equals(item))
                {
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.EnsureNotEmpty();

            Node<T> oldHead = this._head;

            if (this._head.Next == null)
            {
                this._head = null;
            }
            else
            {
                Node<T> newHead = this._head.Next;
                this._head = newHead;
            }

            this.Count--;

            return oldHead.Value;
        }

        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>(item);

            if (this._head == null)
            {
                this._head = newNode;
            }
            else
            {
                Node<T> current = this._head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
            }

            this.Count++;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this._head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = this._head;

            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if(this._head == null)
            {
                throw new InvalidOperationException("The quere is empty!");
            }
        }
    }
}