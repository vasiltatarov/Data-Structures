namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public SinglyLinkedList()
        {
            this._head = null;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item, this._head);

            this._head = newNode;
            this.Count++;
        }

        public void AddLast(T item) 
        {
            var newNode = new Node<T>(item);
            var current = this._head;

            if (current == null)
            {
                this._head = newNode;
            }
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            CheckIfIsEmpty();

            return this._head.Item;
        }

        public T GetLast()
        {
            CheckIfIsEmpty();

            var current = this._head;
             
            while (current.Next != null)
            {
                current = current.Next;
            }

            return current.Item;
        }

        public T RemoveFirst()  
        {
            CheckIfIsEmpty();
                 
            var current = this._head;

            this._head = this._head.Next;
            this.Count--;

            return current.Item;
        }

        public T RemoveLast()
        {
            CheckIfIsEmpty();

            if (this.Count == 1)
            {
                var value = this._head.Item;

                this._head = null;

                this.Count--;

                return value;
            }
            else
            {
                var preLast = this._head;
                var toRemove = this._head.Next;

                while (toRemove.Next != null)
                {
                    preLast = toRemove;
                    toRemove = toRemove.Next;
                }

                preLast.Next = null;

                this.Count--;

                return toRemove.Item;
            }
        }

        public IEnumerator<T> GetEnumerator()   
        {
            var current = this._head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void CheckIfIsEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Collection is empty");
            }
        }
    }
}