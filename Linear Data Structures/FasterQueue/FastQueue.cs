namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public FastQueue()
        {
            this._head = this._tail = null;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            CheckIfNotEmpty();

            var currElement = this._head;

            while (currElement != null)
            {
                if (currElement.Item.Equals(item))
                {
                    return true;
                }

                currElement = currElement.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            CheckIfNotEmpty();

            var currElement = this._head;

            if (this.Count == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                this._head = this._head.Next;
            }

            this.Count--;

            return currElement.Item;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item);

            if (this.Count == 0)
            {
                this._head = this._tail = newNode;
            }
            else
            {
                this._tail.Next = newNode;
                this._tail = newNode;
            }

            this.Count++;
        }

        public T Peek()
        {
            CheckIfNotEmpty();

            return this._head.Item;
        }


        public IEnumerator<T> GetEnumerator()
        {
            var currNode = this._head;

            while (currNode != null)
            {
                yield return currNode.Item;
                currNode = currNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void CheckIfNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Collection is empty!");
            }
        }
    }
}