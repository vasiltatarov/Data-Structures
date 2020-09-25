namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.EnsureNorEmpty();
            var element = this.Peek();
            this.Swap(0, this.Size - 1);
            this._elements.RemoveAt(this.Size - 1);
            this.HeapifyDown(0);

            return element;
        }

        public void Add(T element)
        {
            this._elements.Add(element);
            this.HeapifyUp(this.Size - 1);
        }

        public T Peek()
        {
            this.EnsureNorEmpty();
            return this._elements[0];
        }

        private void EnsureNorEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Heap is mepty!");
            }
        }

        private void Swap(int index, int lastElement)
        {
            var temp = this._elements[index];
            this._elements[index] = this._elements[lastElement];
            this._elements[lastElement] = temp;
        }

        private void HeapifyDown(int index)
        {
            var leftChildIndex = this.GetLeftChildIndex(index);

            while (leftChildIndex < this.Size && this.IsGreater(index, leftChildIndex))
            {
                var toSwapWith = leftChildIndex;
                var rightChildIndex = this.GetRightChildIndex(index);

                if (rightChildIndex < this.Size && this.IsGreater(toSwapWith, rightChildIndex))
                {
                    toSwapWith = rightChildIndex;
                }

                this.Swap(toSwapWith, index);
                index = toSwapWith;
                leftChildIndex = this.GetLeftChildIndex(index);
            }
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = this.GetParentIndex(index);

            while (index > 0 && this.IsLesser(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            } 
        }

        private bool IsGreater(int index, int leftChildIndex)
            => this._elements[index].CompareTo(this._elements[leftChildIndex]) > 0;

        private bool IsLesser(int index, int parentIndex)
            => this._elements[index].CompareTo(this._elements[parentIndex]) < 0;

        private bool IsValidIndex(int index)
            => index >= 0 && index < this.Size;

        private int GetRightChildIndex(int index)
            => 2 * index + 2;

        private int GetParentIndex(int index)
            => (index - 1) / 2;

        private int GetLeftChildIndex(int index)
            => 2 * index + 1;
    }
}
