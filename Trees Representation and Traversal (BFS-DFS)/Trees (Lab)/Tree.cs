namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
            this.IsRootDeleted = false;
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();
        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            this.CheckIsNotNull(this);

            var result = new List<T>();

            if (IsRootDeleted)
            {
                return result;
            }

            this.Bfs(this, result);

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            this.CheckIsNotNull(this);

            var result = new List<T>();

            if (IsRootDeleted)
            {
                return result;
            }

            Dfs(this, result);

            return result;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            this.CheckIsNotNull(child);

            var searchedNode = FindBfs(parentKey);

            this.CheckIsNotNull(searchedNode);

            searchedNode._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            var searchedNode = FindBfs(nodeKey);
            this.CheckIsNotNull(searchedNode);

            foreach (var child in searchedNode._children)
            {
                child.Parent = null;
            }

            searchedNode._children.Clear();

            var searchedParent = searchedNode.Parent;

            if (searchedParent == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                searchedParent._children.Remove(searchedNode);
            }

            if (IsRootDeleted)
            {
                searchedNode._children.Clear();
            }

            searchedNode.Value = default;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);

            this.CheckIsNotNull(firstNode);
            this.CheckIsNotNull(secondNode);

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent == null)
            {
                SwapRoot(secondNode);
                return;
            }
            else if (secondParent == null)
            {
                SwapRoot(firstNode);
                return;
            }

            firstNode.Parent = secondParent;    
            secondNode.Parent = firstParent;

            var indexOfFirst = firstParent._children.IndexOf(firstNode);
            var indexOfSecond = secondParent._children.IndexOf(secondNode);

            firstParent._children[indexOfFirst] = secondNode;
            secondParent._children[indexOfSecond] = firstNode;
        }

        private void CheckIsNotNull(Tree<T> tree)
        {
            if (tree == null)
            {
                throw new ArgumentNullException("Tree is null!");
            }
        }

        private void Bfs(Tree<T> tree, List<T> result)
        {
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(tree);

            while (queue.Any())
            {
                var subtree = queue.Dequeue();

                result.Add(subtree.Value);

                foreach (var child in subtree._children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private void Dfs(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree._children)
            {
                this.Dfs(child, result);
            }

            result.Add(tree.Value);
        }

        private Tree<T> FindBfs(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            this.CheckIsNotNull(this);
            queue.Enqueue(this);

            while (queue.Any())
            {
                var subtree = queue.Dequeue();

                foreach (var child in subtree._children)
                {
                    if (child.Value.Equals(parentKey))
                    {
                        return child;
                    }

                    queue.Enqueue(child);
                }
            }

            if (this.Value.Equals(parentKey))
            {
                return this;
            }

            return null;
        }

        private void SwapRoot(Tree<T> currNode)
        {
            this.Value = currNode.Value;
            this._children.Clear();

            foreach (var child in currNode._children)
            {
                this._children.Add(child);
            }
        }
    }
}
