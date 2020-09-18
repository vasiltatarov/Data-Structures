namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.Parent = null;
            this._children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                this.AddParent(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            var sb = new StringBuilder();

            this.OrderAsStringByDfs(this, 0, sb);

            return sb.ToString().TrimEnd();
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var leafnodes = new List<Tree<T>>();
            this.FindLeafNodes(this, leafnodes);
            var deepestPath = 0;
            Tree<T> deepestNode = null;

            foreach (var node in leafnodes)
            {
                var currNodePath = FindNodePath(node);

                if (currNodePath > deepestPath)
                {
                    deepestPath = currNodePath;
                    deepestNode = node;
                }
            }

            return deepestNode;
        }

        public List<T> GetLeafKeys()
        {
            var leafNodes = new List<T>();

            this.FindLeafKeysWithDfs(this, leafNodes);

            return leafNodes;
        }

        public List<T> GetMiddleKeys()
        {
            var middleNodes = new List<T>();

            this.FindMiddlesNodesOrderByDfs(this, middleNodes);

            return middleNodes;
        }

        public List<T> GetLongestPath()
        {
            var longestPath = new List<T>();
            var deepestNode = this.GetDeepestLeftomostNode();

            while (deepestNode.Parent != null)
            {
                longestPath.Insert(0, deepestNode.Key);
                deepestNode = deepestNode.Parent;
            }

            if (deepestNode.Key != null)
            {
                longestPath.Insert(0, deepestNode.Key);
            }


            return longestPath;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var wantedPaths = new List<List<T>>();
            var currPath = new List<T>();
            currPath.Add(this.Key);
            var currSum = Convert.ToInt32(this.Key);

            this.GetPathWithSum(this, wantedPaths, currPath, ref currSum, sum);

            return wantedPaths;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum) //HERE
        {
            var allNodes = new List<Tree<T>>();
            this.OrderByDfs(this, allNodes);
            var nodesWithGivenSum = new List<Tree<T>>();

            foreach (var node in allNodes)
            {
                var currSum = 0;
                currSum += Convert.ToInt32(node.Key);
                this.FindSubTreeSumDfs(node, ref currSum);

                if (currSum == sum)
                {
                    nodesWithGivenSum.Add(node);
                }
            }

            return nodesWithGivenSum;
        }

        private void FindSubTreeSumDfs(Tree<T> node, ref int currSum)
        {
            foreach (var child in node._children)
            {
                currSum += Convert.ToInt32(child.Key);

                this.FindSubTreeSumDfs(child, ref currSum);
            }
        }

        private void OrderAsStringByDfs(Tree<T> tree, int spaces, StringBuilder sb)
        {
            sb.AppendLine($"{new string(' ', spaces)}{tree.Key}");

            foreach (var child in tree._children)
            {
                this.OrderAsStringByDfs(child, spaces + 2, sb);
            }
        }

        private void FindLeafKeysWithDfs(Tree<T> tree, List<T> leafNodes)
        {
            if (IsLeafNode(tree))
            {
                leafNodes.Add(tree.Key);
            }

            foreach (var child in tree._children)
            {
                this.FindLeafKeysWithDfs(child, leafNodes);
            }
        }

        private void FindMiddlesNodesOrderByDfs(Tree<T> tree, List<T> middleNodes)
        {
            if ((!IsLeafNode(tree)) && IsHaveParent(tree))
            {
                middleNodes.Add(tree.Key);
            }

            foreach (var child in tree._children)
            {
                this.FindMiddlesNodesOrderByDfs(child, middleNodes);
            }
        }

        private Tree<T> FindDeepestLeftmostNode(Tree<T> tree)
        {
            if (IsLeafNode(tree))
            {
                return tree;
            }
            foreach (var child in tree._children)
            {

                this.FindDeepestLeftmostNode(child);
            }

            return null;
        }

        private bool IsLeafNode(Tree<T> tree)
            => tree._children.Count == 0;

        private bool IsHaveParent(Tree<T> tree)
            => tree.Parent != null;

        private void FindLeafNodes(Tree<T> tree, List<Tree<T>> leafnodes)
        {
            if (IsLeafNode(tree))
            {
                leafnodes.Add(tree);
            }

            foreach (var child in tree._children)
            {
                this.FindLeafNodes(child, leafnodes);
            }
        }

        private int FindNodePath(Tree<T> node)
        {
            var path = 0;
            var currNode = node;

            while (currNode.Parent != null)
            {
                path++;
                currNode = currNode.Parent;
            }

            return path;
        }

        private void GetPathWithSum(Tree<T> tree, List<List<T>> wantedPaths, List<T> currPath, ref int currSum, int sum)
        {
            foreach (var child in tree._children)
            {
                currSum += Convert.ToInt32(child.Key);
                currPath.Add(child.Key);

                this.GetPathWithSum(child, wantedPaths, currPath, ref currSum, sum);
            }

            if (currSum == sum)
            {
                wantedPaths.Add(new List<T>(currPath));
            }

            currSum -= Convert.ToInt32(tree.Key);
            currPath.Remove(tree.Key);
        }

        private void OrderByDfs(Tree<T> tree, List<Tree<T>> allNodes)
        {
            foreach (var child in tree._children)
            {
                this.OrderByDfs(child, allNodes);
            }

            allNodes.Add(tree);
        }
    }
}
