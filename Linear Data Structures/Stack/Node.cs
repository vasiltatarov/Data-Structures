namespace Problem02.Stack
{
    public class Node<T>
    {
        public Node(T item, Node<T> next = null)
        {
            this.Item = item;
            this.Next = next;
        }

        public T Item { get; set; }

        public Node<T> Next { get; set; }
    }
}