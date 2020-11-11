using System;
using System.Text;

namespace _01.BSTOperations
{
    public class StartUp
    {
        public static void Main()
        {
            var bst = new BinarySearchTree<int>();

            for (int i = 1; i < 10; i += 7)
            {
                bst.Insert(i);
            }

            var root = bst.Root;
            var result = new StringBuilder();

            PreOrderDfs(root, 0, result);

            Console.WriteLine(result.ToString().TrimEnd());
        }

        private static void PreOrderDfs(Node<int> root, int level, StringBuilder result)
        {
            result.AppendLine($"{new string(' ', level)}{root.Value}");

            if (root.LeftChild != null)
            {
                PreOrderDfs(root.LeftChild, level + 2, result);
            }

            if (root.RightChild != null)
            {
                PreOrderDfs(root.RightChild, level + 2, result);
            }
        }
    }
}
