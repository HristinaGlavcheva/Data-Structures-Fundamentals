namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.AddParent(this);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            return this.GetAsString(0).TrimEnd();
        }

        private string GetAsString(int indentation = 0)
        {
            string result = new string(' ', indentation) + this.Key + Environment.NewLine;

            foreach (var child in this.Children)
            {
                result += child.GetAsString(indentation + 2);
            }

            return result;
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var leafNodes = this.GetLeafNodes();

            int maxLevel = 1;
            var deepestNode = this;

            for (int i = leafNodes.Count - 1; i >= 0; i--)
            {
                var currentNode = leafNodes[i];
                var currentLevel = 1;

                while (leafNodes[i].Parent != null)
                {
                    leafNodes[i] = leafNodes[i].Parent;
                    currentLevel++;
                }

                if (currentLevel >= maxLevel)
                {
                    deepestNode = currentNode;
                    maxLevel = currentLevel;
                }
            }

            return deepestNode;
        }

        private List<Tree<T>> GetLeafNodes(List<Tree<T>> leafs)
        {
            foreach (var child in this.Children)
            {
                if (child.Children.Count == 0)
                {
                    leafs.Add(child);
                }

                child.GetLeafNodes(leafs);
            }

            return leafs;
        }

        public List<Tree<T>> GetLeafNodes()
        {
            List<Tree<T>> leafs = new List<Tree<T>>();

            this.GetLeafNodes(leafs);

            return leafs;
        }

        public List<T> GetLeafKeys()
        {
            List<T> leafs = new List<T>();

            //DFS solution:
            this.GetLeafKeys(leafs);

            //BFS solution:
            //foreach (var child in this.Children)
            //{
            //    if (child.Children.Count == 0)
            //    {
            //        leafs.Add(child.Key);
            //    }

            //    child.GetLeafKeys();
            //}
            //Queue<Tree<T>> queue = new Queue<Tree<T>>();
            //queue.Enqueue(this);

            //while (queue.Count > 0)
            //{
            //    if (queue.Peek().Children.Count == 0)
            //    {
            //        leafs.Add(queue.Dequeue().Key);
            //    }
            //    else
            //    {
            //        foreach (var child in queue.Dequeue().Children)
            //        {
            //            queue.Enqueue(child);
            //        }
            //    }
            //}

            return leafs;
        }

        private List<T> GetLeafKeys(List<T> leafs)
        {
            foreach (var child in this.Children)
            {
                if (child.Children.Count == 0)
                {
                    leafs.Add(child.Key);
                }

                child.GetLeafKeys(leafs);
            }

            return leafs;
        }

        public List<T> GetMiddleKeys()
        {
            List<T> middleKeys = new List<T>();

            //DFS Solution:
            this.GetMiddleKeys(middleKeys);

            //BFS Solution:
            //Queue<Tree<T>> queue = new Queue<Tree<T>>();

            //queue.Enqueue(this);

            //while (queue.Count > 0)
            //{
            //    if (queue.Peek().Children.Count > 0)
            //    {
            //        if (queue.Peek().Parent != null)
            //        {
            //            middleKeys.Add(queue.Peek().Key);
            //        }

            //        foreach (var child in queue.Dequeue().Children)
            //        {
            //            queue.Enqueue(child);
            //        }
            //    }
            //    else
            //    {
            //        queue.Dequeue();
            //    }
            //}

            return middleKeys;
        }

        private List<T> GetMiddleKeys(List<T> middleKeys)
        {
            foreach (var child in this.Children)
            {
                if (child.Children.Count > 0)
                {
                    this.GetMiddleKeys(middleKeys);

                    if (child.Parent != null)
                    {
                        middleKeys.Add(child.Key);
                    }
                }
            }

            return middleKeys;
        }

        public List<T> GetLongestPath()
        {
            var deepestNode = this.GetDeepestLeftomostNode();
            var parentsList = new List<T>();
            parentsList.Add(deepestNode.Key);

            while (deepestNode.Parent != null)
            {
                parentsList.Add(deepestNode.Parent.Key);
                deepestNode = deepestNode.Parent;
            }

            parentsList.Reverse();

            return parentsList;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var leafNodes = this.GetLeafNodes();
            var pathsWithGivenSum = new List<List<T>>();

            foreach (var leaf in leafNodes)
            {
                var currentNode = leaf;
                int currentSum = 0;
                var currentPath = new List<T>();

                while (currentNode != null)
                {
                    int.TryParse(currentNode.Key.ToString(), out int currentNodeValue);
                    currentSum += currentNodeValue;
                    currentPath.Add(currentNode.Key);
                    currentNode = currentNode.Parent;
                }

                if (currentSum == sum)
                {
                    currentPath.Reverse();
                    pathsWithGivenSum.Add(currentPath);
                }
            }

            return pathsWithGivenSum;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var roots = new List<Tree<T>>();
            this.SubTreeSumDFS(sum, this, roots);

            return roots;
        }

        private int SubTreeSumDFS(int sum, Tree<T> node, List<Tree<T>> roots)
        {
            int currentSum = Convert.ToInt32(node.Key);

            foreach (var child in node.Children)
            {
                currentSum += SubTreeSumDFS(sum, child, roots);
            }

            if (currentSum == sum)
            {
                roots.Add(node);
            }

            return currentSum;
        }
    }
}
