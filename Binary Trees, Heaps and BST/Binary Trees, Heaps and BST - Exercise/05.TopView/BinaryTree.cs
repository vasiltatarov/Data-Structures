namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            var distToValueLevel = new Dictionary<int, KeyValuePair<T, int>>();
            this.FindTopViews(this, 0, 1, distToValueLevel);

            return distToValueLevel.Values.Select(x => x.Key).ToList();
        }

        private void FindTopViews(BinaryTree<T> tree, int dist, int level, Dictionary<int, KeyValuePair<T, int>> distToValueLevel)
        {
            if (tree == null)
            {
                return;
            }

            if (!distToValueLevel.ContainsKey(dist))
            {
                distToValueLevel.Add(dist, new KeyValuePair<T, int>(tree.Value, level));
            }

            if (level < distToValueLevel[dist].Value)
            {
                distToValueLevel[dist] = new KeyValuePair<T, int>(tree.Value, level);
            }

            this.FindTopViews(tree.LeftChild, dist - 1, level + 1, distToValueLevel);
            this.FindTopViews(tree.RightChild, dist + 1, level + 1, distToValueLevel);
        }

        //public List<T> TopView()
        //{
        //    var topViews = new List<T>();
        //    var root = this;

        //    if (root != null)
        //    {
        //        topViews.Add(root.Value);
        //        this.FindLeftChildrensDfs(this, topViews);
        //        this.FindRightChildrensDfs(this, topViews);
        //    }

        //    return topViews;
        //}
        //private void FindRightChildrensDfs(BinaryTree<T> currRoot, List<T> topViews)
        //{
        //    if (currRoot.RightChild != null)
        //    {
        //        topViews.Add(currRoot.RightChild.Value);
        //        this.FindRightChildrensDfs(currRoot.RightChild, topViews);
        //    }
        //}
        //private void FindLeftChildrensDfs(BinaryTree<T> currRoot, List<T> topViews)
        //{
        //    if (currRoot.LeftChild != null)
        //    {
        //        topViews.Add(currRoot.LeftChild.Value);
        //        this.FindLeftChildrensDfs(currRoot.LeftChild, topViews);
        //    }
        //}
    }
}
