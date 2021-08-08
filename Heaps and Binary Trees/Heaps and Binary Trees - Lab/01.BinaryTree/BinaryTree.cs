namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            return this.DFSPreOrder(this, 0);
        }

        private string DFSPreOrder(IAbstractBinaryTree<T> node, int indent)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(new string(' ', indent) + node.Value);

            if (node.LeftChild != null)
            {
                result.Append(DFSPreOrder(node.LeftChild, indent + 2));
            }
            if (node.RightChild != null)
            {
                result.Append(DFSPreOrder(node.RightChild, indent + 2));
            }

            return result.ToString();
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            return this.InOrder(this, new List<IAbstractBinaryTree<T>>());
        }

        private List<IAbstractBinaryTree<T>> InOrder(IAbstractBinaryTree<T> node, List<IAbstractBinaryTree<T>> result)
        { 
            if(node.LeftChild != null)
            {
                this.InOrder(node.LeftChild, result);
            }

            result.Add(node);

            if(node.RightChild != null)
            {
                this.InOrder(node.RightChild, result);
            }

            return result;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            return this.PostOrder(this, new List<IAbstractBinaryTree<T>>());
        }

        private List<IAbstractBinaryTree<T>> PostOrder(IAbstractBinaryTree<T> node, List<IAbstractBinaryTree<T>> result)
        {
            if(node.LeftChild != null)
            {
                this.PostOrder(node.LeftChild, result);
            }

            if(node.RightChild != null)
            {
                this.PostOrder(node.RightChild, result);
            }

            result.Add(node);

            return result;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            return this.PreOrder(this, new List<IAbstractBinaryTree<T>>());
        }

        private List<IAbstractBinaryTree<T>> PreOrder(IAbstractBinaryTree<T> node, List<IAbstractBinaryTree<T>> result)
        {
            result.Add(node);

            if (node.LeftChild != null)
            {
                this.PreOrder(node.LeftChild, result);
            }

            if (node.RightChild != null)
            {
                this.PreOrder(node.RightChild, result);
            }

            return result;
        }

        public void ForEachInOrder(Action<T> action)
        {
            //var result = this.InOrder();

            //foreach (var node in result)
            //{
            //    action.Invoke(node.Value);
            //}

            if(this.LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }

            action.Invoke(this.Value);

            if(this.RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }
        }
    }
}
