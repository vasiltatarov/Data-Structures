namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);

            this.HeapifyUp(this.Size - 1);
        }

        public T Peek()
        {
            ValidateIfNotEmpty();

            return this.elements[0];
        }

        private void ValidateIfNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Collection is empty!");
            }
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = this.GetParentIndex(index);

            while (this.IndexIsValid(index) && IsGreater(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            }
        }

        private bool IndexIsValid(int index) 
            => index >= 0 && index < this.Size;

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[index];
            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        private bool IsGreater(int index, int parentIndex)
            => this.elements[index].CompareTo(this.elements[parentIndex]) > 0;

        private int GetParentIndex(int index) 
            => (index - 1) / 2;
    }
}
