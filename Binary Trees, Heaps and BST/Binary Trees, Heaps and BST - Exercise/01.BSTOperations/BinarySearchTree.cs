namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Copy(root);
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => this.Root.Count;

        public bool Contains(T element)
        {
            var current = this.Root;

            while (current != null)
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(T element) // Recursive Traversal
        {
            var toInsert = new Node<T>(element);

            if (this.IsRootIsNull(this.Root))
            {
                this.Root = toInsert;
            }
            else
            {
                this.InsertElementDfs(this.Root, null, toInsert);
            }
        }

        //public void Insert(T element) // Iterative Traversal
        //{
        //    var toInsert = new Node<T>(element);

        //    if (this.IsRootIsNull(this.Root))
        //    {
        //        this.Root = toInsert;
        //    }
        //    else
        //    {
        //        var current = this.Root;
        //        Node<T> prev = null;

        //        while (current != null)
        //        {
        //            prev = current;

        //            if (this.IsLess(element, current.Value))
        //            {
        //                current = current.LeftChild;
        //            }
        //            else if (this.IsGreater(element, current.Value))
        //            {
        //                current = current.RightChild;
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }

        //        if (this.IsLess(element, prev.Value))
        //        {
        //            prev.LeftChild = toInsert;

        //            if (this.LeftChild == null)
        //            {
        //                this.LeftChild = toInsert;
        //            }
        //        }
        //        else
        //        {
        //            prev.RightChild = toInsert;

        //            if (this.RightChild == null)
        //            {
        //                this.RightChild = toInsert;
        //            }
        //        }
        //    }

        //    this.Count++;
        //}

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;

            while (current != null && !this.AreEqual(element, current.Value))
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsLess(element, current.Value))
                {
                    current = current.RightChild;
                }
            }

            return new BinarySearchTree<T>(current);
        }

        public void EachInOrder(Action<T> action)
        {
            this.ValidateIfNotEmpty();
            this.EachInOrderDfs(this.Root, action);
        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();
            this.ValidateIfNotEmpty();
            this.OrderInRangeDfs(this.Root, result, lower, upper);

            return result;
        }

        public void DeleteMin()
        {
            this.ValidateIfNotEmpty();
            var current = this.Root;
            Node<T> prev = null;

            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
            }
            else
            {
                while (current.LeftChild != null)
                {
                    current.Count--;
                    prev = current;
                    current = current.LeftChild;
                }

                prev.LeftChild = current.RightChild;
            }
        }

        public void DeleteMax()
        {
            this.ValidateIfNotEmpty();
            var current = this.Root;
            Node<T> prev = null;

            if (this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
            }
            else
            {
                while (current.RightChild != null)
                {
                    current.Count--;
                    prev = current;
                    current = current.RightChild;
                }

                prev.RightChild = current.LeftChild;
            }
        }

        public int GetRank(T element)
        {
            return this.GetRankDfs(this.Root, element);
        }

        private int GetRankDfs(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }

            if (this.IsLess(element, current.Value))
            {
                return this.GetRankDfs(current.LeftChild, element);
            }
            else if (this.AreEqual(element, current.Value))
            {
                return this.GetNodeCount(current);
            }

            return this.GetNodeCount(current.LeftChild) + 1 + this.GetRankDfs(current.RightChild, element);
        }

        private int GetNodeCount(Node<T> current)
            => current == null ? 0 : current.Count;

        private bool IsLess(T element, T value)
            => element.CompareTo(value) < 0;

        private bool IsGreater(T element, T value)
            => element.CompareTo(value) > 0;

        private bool AreEqual(T element, T value)
            => element.CompareTo(value) == 0;

        private bool IsRootIsNull(Node<T> root)
            => root == null;

        private void ValidateIfNotEmpty()
        {
            if (this.IsRootIsNull(this.Root))
            {
                throw new InvalidOperationException("Tree is empty!");
            }
        }

        private void Copy(Node<T> root)
        {
            if (IsRootIsNull(root))
            {
                return;
            }

            this.Insert(root.Value);
            this.Copy(root.LeftChild);
            this.Copy(root.RightChild);
        }

        private void InsertElementDfs(Node<T> current, Node<T> prev, Node<T> toInsert)
        {
            if (current == null && this.IsLess(toInsert.Value, prev.Value))
            {
                prev.LeftChild = toInsert;

                if (this.LeftChild == null)
                {
                    this.LeftChild = toInsert;
                }

                return;
            }

            if (current == null && this.IsGreater(toInsert.Value, prev.Value))
            {
                prev.RightChild = toInsert;

                if (this.RightChild == null)
                {
                    this.RightChild = toInsert;
                }

                return;
            }

            if (this.IsLess(toInsert.Value, current.Value))
            {
                this.InsertElementDfs(current.LeftChild, current, toInsert);
                current.Count++;
            }
            else if (this.IsGreater(toInsert.Value, current.Value))
            {
                this.InsertElementDfs(current.RightChild, current, toInsert);
                current.Count++;
            }
        }

        private void EachInOrderDfs(Node<T> current, Action<T> action)
        {
            if (current.LeftChild != null)
            {
                this.EachInOrderDfs(current.LeftChild, action);
            }

            action.Invoke(current.Value);

            if (current.RightChild != null)
            {
                this.EachInOrderDfs(current.RightChild, action);
            }
        }

        private void OrderInRangeDfs(Node<T> root, List<T> result, T lower, T upper)
        {

            if (root.LeftChild != null)
            {
                this.OrderInRangeDfs(root.LeftChild, result, lower, upper);
            }

            if (this.IsLessOrEqual(root.Value, upper) && this.IsGreaterOrEqual(root.Value, lower))
            {
                result.Add(root.Value);
            }

            if (root.RightChild != null)
            {
                this.OrderInRangeDfs(root.RightChild, result, lower, upper);
            }
        }

        private bool IsGreaterOrEqual(T value, T lower)
            => value.CompareTo(lower) >= 0;

        private bool IsLessOrEqual(T value, T upper)
            => value.CompareTo(upper) <= 0;
    }
}
