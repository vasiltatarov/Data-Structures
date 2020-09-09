namespace Problem01.FasterQueue
{
    public class Node<T>
    {
        public Node(T item)
        {
            this.Item = item;
            this.Next = null;
            this.Previous = null;
        }

        public T Item { get; set; }

        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }
    }
}