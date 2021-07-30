namespace Tree
{
    using System.Linq;
    using System.Collections.Generic;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            foreach (var line in input)
            {
                var lineArgs = line.Split().Select(int.Parse).ToArray();
                int key = lineArgs[0];
                int newTreeRoot = lineArgs[1];

                this.CreateNodeByKey(key);
                this.CreateNodeByKey(newTreeRoot);

                this.AddEdge(key, newTreeRoot);
            }

            return this.GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            Tree<int> node = null;

            if (!this.nodesBykeys.ContainsKey(key))
            {
                node = new Tree<int>(key);
                this.nodesBykeys.Add(key, node);
            }

            return node;
        }

        public void AddEdge(int parent, int child)
        {
            Tree<int> parentNode = this.nodesBykeys[parent];
            Tree<int> childNode = this.nodesBykeys[child];

            parentNode.AddChild(childNode);
            childNode.AddParent(parentNode);
        }

        private Tree<int> GetRoot()
        {
            var root = this.nodesBykeys.FirstOrDefault().Value;

            while(root.Parent != null)
            {
                root = root.Parent;
            }

            return root;
        }
    }
}
