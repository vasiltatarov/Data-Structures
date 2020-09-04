namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public Stack()
        {
            this._top = null;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            this.EnsureNotEmpty();

            var current = this._top;

            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this._top.Item;
        }

        public T Pop()
        {
            EnsureNotEmpty();

            var topNodeItem = this._top.Item;

            var newNode = this._top.Next;

            this._top.Next = null;

            this._top = newNode;
            this.Count--;

            return topNodeItem;
        }

        public void Push(T item)
        {
            var newNode = new Node<T>(item, this._top);

            this._top = newNode;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._top;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}