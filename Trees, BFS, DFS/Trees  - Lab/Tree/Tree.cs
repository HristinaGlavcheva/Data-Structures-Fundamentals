namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;
        private bool isRootDeleted = true;

        public Tree(T value)
        {
            this.Value = value;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            this._children = children.ToList();

            if (children.Length > 0)
            {
                this.isRootDeleted = false;
            }
        }

        public T Value { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            ICollection<T> collection = new List<T>();

            if (isRootDeleted)
            {
                return collection;
            }

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                collection.Add(node.Value);

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return collection;
        }

        public ICollection<T> OrderDfs()
        {
            ICollection<T> collection = new List<T>();

            if (isRootDeleted)
            {
                return collection;
            }

            foreach (var child in this.Children)
            {
                this.Dfs(child, collection.ToList());
            }

            return collection;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> searchedNode = this.FindBfs(parentKey);
            CheckEmptyNode(searchedNode);

            searchedNode._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            var searchedNode = this.FindBfs(nodeKey);
            CheckEmptyNode(searchedNode);

            var searchedParent = searchedNode.Parent;

            if (searchedParent == null)
            {
                this.isRootDeleted = true;
            }
            else
            {
                var searchedNodeIndex = searchedParent._children.IndexOf(searchedNode);
                searchedParent._children[searchedNodeIndex] = null;
                searchedNode.Parent = null;
            }

            if (searchedNode.Children.Count == 0)
            {
                searchedParent._children.Remove(searchedNode);
                searchedNode.Parent = null;
            }

            foreach (var child in searchedNode.Children)
            {
                child.Parent = null;
            }

            searchedNode._children.Clear();
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);

            if (firstNode == null || secondNode == null)
            {
                throw new ArgumentNullException();
            }

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            var firstNodeIndex = firstParent._children.IndexOf(firstNode);
            var secondNodeIndex = secondParent._children.IndexOf(secondNode);

            firstNode.Parent = secondParent;
            secondParent._children[secondNodeIndex] = firstNode;
            //firstParent._children.Remove(firstNode);

            secondNode.Parent = firstParent;
            firstParent._children[firstNodeIndex] = secondNode;
            //secondNode._children.Remove(secondNode);
        }

        private void Dfs(Tree<T> node, List<T> collection)
        {
            foreach (var child in node.Children)
            {
                this.Dfs(child, collection);
            }

            collection.Add(this.Value);
        }

        private Tree<T> FindBfs(T key)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            Tree<T> searchedNode = null;

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                if (currentNode.Value.Equals(key))
                {
                    searchedNode = currentNode;
                    return searchedNode;
                }

                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return searchedNode;
        }

        private void CheckEmptyNode(Tree<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
