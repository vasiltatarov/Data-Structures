namespace Tree
{
    using System;
    using System.Collections.Generic;

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
                this._children.Add(child);
                child.Parent = this;
            }
        }

        public bool IsRootDeleted { get; private set; }
        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            this.EnsureNotNull(this);
            var result = new List<T>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            this.FindAllWithBfs(this, result);
            return result;
        }

        public ICollection<T> OrderDfs()
        {
            this.EnsureNotNull(this);
            var result = new List<T>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            this.FindAllWithDfs(this, result);
            return result;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var parentNode = this.FindNodeWithBfs(this, parentKey);
            this.EnsureNotNull(parentNode);
            parentNode._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            var nodeToRemove = this.FindNodeWithBfs(this, nodeKey);
            this.EnsureNotNull(nodeToRemove);

            //foreach (var child in searchedNode._children)
            //{
            //    child.Parent = null;
            //}

            //searchedNode._children.Clear();

            //var searchedParent = searchedNode.Parent;

            //if (searchedParent == null)
            //{
            //    this.IsRootDeleted = true;
            //}
            //else
            //{
            //    searchedParent._children.Remove(searchedNode);
            //}

            //if (IsRootDeleted)
            //{
            //    searchedNode._children.Clear();
            //}

            //searchedNode.Value = default;
            if (nodeToRemove.Children.Count == 0)
            {
                var parentNode = nodeToRemove.Parent;
                parentNode._children.Remove(nodeToRemove);
            }
            else if (nodeToRemove.Children.Count != 0 && nodeToRemove.Parent != null)
            {
                nodeToRemove._children.Clear();
                var parentNode = nodeToRemove.Parent;
                parentNode._children.Remove(nodeToRemove);
            }
            else if (nodeToRemove.Parent == null)
            {
                this.IsRootDeleted = true;
            }

            if (this.IsRootDeleted)
            {
                nodeToRemove._children.Clear();
            }

            nodeToRemove.Value = default;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstSearchedNode = this.FindNodeWithBfs(this, firstKey);
            var secondSearchedNode = this.FindNodeWithBfs(this, secondKey);
            this.EnsureNotNull(firstSearchedNode);
            this.EnsureNotNull(secondSearchedNode);

            var firstSearchedNodeParent = firstSearchedNode.Parent;
            var secondSearchedNodeParent = secondSearchedNode.Parent;

            if (firstSearchedNodeParent == null)
            {
                this.SwapRoot(secondSearchedNode);
                return;
            }
            else if (secondSearchedNodeParent == null)
            {
                this.SwapRoot(firstSearchedNode);
                return;
            }

            firstSearchedNode.Parent = secondSearchedNodeParent;
            secondSearchedNode.Parent = firstSearchedNodeParent;
            var indexOfFirst = firstSearchedNodeParent._children.IndexOf(firstSearchedNode);
            var indexOfSecond = secondSearchedNodeParent._children.IndexOf(secondSearchedNode);

            firstSearchedNodeParent._children[indexOfFirst] = secondSearchedNode;
            secondSearchedNodeParent._children[indexOfSecond] = firstSearchedNode;
        }

        private void SwapRoot(Tree<T> swapedTree)
        {
            this.Value = swapedTree.Value;
            this._children.Clear();

            foreach (var child in swapedTree.Children)
            {
                this._children.Add(child);
            }
        }

        private void FindAllWithBfs(Tree<T> tree, List<T> result)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(tree);

            while (queue.Count != 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree.Value);

                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }

        }

        private void FindAllWithDfs(Tree<T> subTree, List<T> result)
        {
            foreach (var child in subTree.Children)
            {
                this.FindAllWithDfs(child, result);
            }

            result.Add(subTree.Value);
        }


        private Tree<T> FindNodeWithBfs(Tree<T> tree, T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(tree);

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();

                if (currentNode.Value.Equals(parentKey))
                {
                    return currentNode;
                }

                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void EnsureNotNull(Tree<T> tree)
        {
            if (tree == null)
            {
                throw new ArgumentNullException("Tree cannot be null!");
            }
        }
    }
}
