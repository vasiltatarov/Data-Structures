namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var args = input[i].Split().Select(int.Parse).ToArray();
                var parentKey = args[0];
                var childKey = args[1];

                this.AddEdge(parentKey, childKey);
            }

            return this.GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            if (!this.nodesBykeys.ContainsKey(key))
            {
                this.nodesBykeys.Add(key, new Tree<int>(key));
            }

            return this.nodesBykeys[key];
        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = this.CreateNodeByKey(parent);
            var childNode = this.CreateNodeByKey(child);

            parentNode.AddChild(childNode);
            childNode.AddParent(parentNode);
        }

        private Tree<int> GetRoot()
        {
            foreach (var keyByNode in this.nodesBykeys)
            {
                if (keyByNode.Value.Parent == null)
                {
                    return keyByNode.Value;
                }
            }

            return null;
        }
    }
}
