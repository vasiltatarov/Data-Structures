namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        public Queue()
        {
            this._head = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            EnsureIsNotEmprty();

            var current = this._head;

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

        public T Dequeue()
        {
            EnsureIsNotEmprty();

            var current = this._head;

            this._head = this._head.Next;
            this.Count--;

            return current.Item;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item);

            if (this._head == null)
            {
                this._head = newNode;
            }
            else
            {
                var current = this._head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
            }

            this.Count++;
        }

        public T Peek()
        {
            EnsureIsNotEmprty();

            return this._head.Item;
        }

        private void EnsureIsNotEmprty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

            while (current.Next != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}