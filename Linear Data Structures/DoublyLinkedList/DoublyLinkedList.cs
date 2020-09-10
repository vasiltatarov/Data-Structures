namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public DoublyLinkedList()
        {
            this.head = this.tail = null;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item);

            if (this.Count == 0)
            {
                this.head = this.tail = newNode;
            }
            else
            {
                this.head.Previous = newNode;
                this.head = newNode;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>(item);

            if (this.Count == 0)
            {
                this.head = this.tail = newNode;
            }
            else
            {
                this.tail.Next = newNode;
                this.tail = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            CheckIsNotEmpty();

            return this.head.Item;    
        }

        public T GetLast()
        {
            CheckIsNotEmpty();

            return this.tail.Item;
        }

        public T RemoveFirst()
        {
            CheckIsNotEmpty();

            var firstElement = this.head;

            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.head = this.head.Next;
                this.head.Previous = null;
            }

            this.Count--;

            return firstElement.Item;
        }

        public T RemoveLast()
        {
            CheckIsNotEmpty();

            var last = this.tail;

            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.tail = this.tail.Previous;
                this.tail.Next = null;
            }

            this.Count--;

            return last.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            CheckIsNotEmpty();

            var current = this.head;

            while (current != null)
            {
                yield return current.Item;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckIsNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Doubly Linked List is empty!");
            }
        }
    }
}