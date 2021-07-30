namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            throw new NotImplementedException();
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
                if(child.Children.Count > 0)
                {
                    this.GetMiddleKeys(middleKeys);

                    if(child.Parent != null)
                    {
                        middleKeys.Add(child.Key);
                    }
                }
            }

            return middleKeys;
        }

        public List<T> GetLongestPath()
        {
            throw new NotImplementedException();
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }
    }
}
