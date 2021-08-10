namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Root = root;
            this.LeftChild = this.Root.LeftChild;
            this.RightChild = this.Root.RightChild;

        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            return this.Contains(element, this.Root);
        }

        private bool Contains(T element, Node<T> currentNode)
        {
            if(currentNode == null)
            {
                return false;
            }
            
            if (element.CompareTo(currentNode.Value) == 0)
            {
                return true;
            }

            if (element.CompareTo(currentNode.Value) < 0)
            {
                return this.Contains(element, currentNode.LeftChild);
            }
            else
            {
                return this.Contains(element, currentNode.RightChild);
            }
        }

        public void Insert(T element)
        {
            this.Insert(element, this.Root);
        }

        private void Insert(T element, Node<T> currentNode)
        {
            if (element.Equals(currentNode))
            {
                return;
            }

            if (this.Root == null)
            {
                this.Root = new Node<T>(element, null, null);
                return;
            }

            if (element.CompareTo(currentNode.Value) < 0)
            {
                if (currentNode.LeftChild != null)
                {
                    this.Insert(element, currentNode.LeftChild);
                    return;
                }

                currentNode.LeftChild = new Node<T>(element, null, null);
            }
            else
            {
                if (currentNode.RightChild != null)
                {
                    this.Insert(element, currentNode.RightChild);
                    return;
                }

                currentNode.RightChild = new Node<T>(element, null, null);
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            Node<T> searchedNode = this.Search(element, this.Root);

            return new BinarySearchTree<T>(searchedNode);
        }

        private Node<T> Search(T element, Node<T> currentNode)
        {
            if(currentNode == null)
            {
                return null;
            }

            if (element.Equals(currentNode.Value))
            {
                return currentNode;
            }

            if (element.CompareTo(currentNode.Value) < 0)
            {
                return this.Search(element, currentNode.LeftChild);
            }
            else
            {
                return this.Search(element, currentNode.RightChild);
            }
        }
    }
}
