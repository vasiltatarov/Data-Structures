namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value, IAbstractBinaryTree<T> leftChild, IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            var result = new StringBuilder();

            this.ValidateIfNotNull(this);
            this.AsIndentedPreOrderBT(this, indent, result);

            return result.ToString();
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.ValidateIfNotNull(this);
            this.InOrderBT(this, result);

            return result;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.ValidateIfNotNull(this);
            this.PostOrderBT(this, result);

            return result;
        }

        private void PostOrderBT(IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> result)
        {
            if (tree.LeftChild != null)
            {
                PostOrderBT(tree.LeftChild, result);
            }

            if (tree.RightChild != null)
            {
                PostOrderBT(tree.RightChild, result);
            }

            result.Add(tree);
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.ValidateIfNotNull(this);
            this.PreOrderBT(this, result);

            return result;
        }

        public void ForEachInOrder(Action<T> action)
        {
            var nodes = this.InOrder();

            foreach (var node in nodes)
            {
                action.Invoke(node.Value);
            }
        }

        private void ValidateIfNotNull(BinaryTree<T> binaryTree)
        {
            if (binaryTree == null)
            {
                throw new InvalidOperationException("Tree cannot be null!");
            }
        }

        private void AsIndentedPreOrderBT(IAbstractBinaryTree<T> tree, int indent, StringBuilder result)
        {
            result.AppendLine($"{ new string(' ', indent)}{tree.Value}");

            if (tree.LeftChild != null)
            {
                this.AsIndentedPreOrderBT(tree.LeftChild, indent + 2, result);
            }

            if (tree.RightChild != null)
            {
                this.AsIndentedPreOrderBT(tree.RightChild, indent + 2, result);
            }
        }

        private void PreOrderBT(IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> result)
        {
            result.Add(tree);

            if (tree.LeftChild != null)
            {
                this.PreOrderBT(tree.LeftChild, result);
            }

            if (tree.RightChild != null)
            {
                this.PreOrderBT(tree.RightChild, result);
            }
        }

        private void InOrderBT(IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> result)
        {
            if (tree.LeftChild != null)
            {
                this.InOrderBT(tree.LeftChild, result);
            }

            result.Add(tree);

            if (tree.RightChild != null)
            {
                this.InOrderBT(tree.RightChild, result);
            }
        }
    }
}
