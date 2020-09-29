namespace _04.BinarySearchTree
{
    using System;

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

        public bool Contains(T element)
        {
            var current = this.Root;

            while (current != null)
            {
                if (IsLesser(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (IsGreater(element, current.Value))
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

        public void Insert(T element)
        {
            var toInsert = new Node<T>(element);

            if (IsRootIsNull(this.Root))
            {
                this.Root = toInsert; 
            }
            else
            {
                var current = this.Root;
                Node<T> prev = null;

                while (current != null)
                {
                    prev = current;

                    if (IsLesser(element, current.Value))
                    {
                        current = current.LeftChild;
                    }
                    else if (IsGreater(element, current.Value))
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        return;
                    }
                }

                if (IsLesser(element, prev.Value))
                {
                    prev.LeftChild = toInsert;

                    if (this.LeftChild == null)
                    {
                        this.LeftChild = toInsert;
                    }
                }
                else
                {
                    prev.RightChild = toInsert;

                    if (this.RightChild == null)
                    {
                        this.RightChild = toInsert;
                    }
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;

            while (current != null && !this.AreEqual(element, current.Value))
            {
                if (IsLesser(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (IsGreater(element, current.Value))
                {
                    current = current.RightChild; 
                }
            }

            return new BinarySearchTree<T>(current);
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

        private bool AreEqual(T element, T value)
            => element.CompareTo(value) == 0;

        private bool IsGreater(T element, T value)
            => element.CompareTo(value) > 0;

        private bool IsLesser(T element, T value)
            => element.CompareTo(value) < 0;

        private bool IsRootIsNull(Node<T> root)
            => root == null;
    }
}
