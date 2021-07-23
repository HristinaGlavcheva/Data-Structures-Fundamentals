namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> newHead = new Node<T>(item);

            if (this._head == null)
            {
                this._head = newHead;
            }
            else
            {
                newHead.Next = this._head;
                this._head = newHead;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            Node<T> newNode = new Node<T>(item);

            if (this._head == null)
            {
                this._head = newNode;
            }
            else
            {
                Node<T> currentNode = this._head;

                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }

                currentNode.Next = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();

            return this._head.Value;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();

            Node<T> currentNode = this._head;

            while(currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            return currentNode.Value;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();

            Node<T> oldHead = this._head;
            Node<T> newHead = this._head.Next;
            this._head = newHead;
            this.Count--;

            return oldHead.Value;
        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();

            if(this._head.Next == null)
            {
                return this.RemoveFirst();
            }

            Node<T> currentNode = this._head;

            while (currentNode.Next.Next != null)
            {
                currentNode = currentNode.Next;
            }

            T valueToReturn = currentNode.Next.Value;
            currentNode.Next = null;
            this.Count--;

            return valueToReturn;
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
            if (this.Count == 0)
            {
                throw new InvalidOperationException("List is empty!");
            }
        }
    }
}