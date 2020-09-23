namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> data;

        public PriorityQueue()
        {
            this.data = new List<T>();
        }

        public int Size => this.data.Count;

        public void Add(T element)
        {
            this.data.Add(element);
            this.HeapifyUp(this.Size - 1);
        }

        public T Peek()
        {
            ValidateIsEmpty();
            return this.data[0];
        }

        public T Dequeue()
        {
            var firstElement = this.Peek();
            this.Swap(0, this.Size - 1);
            this.data.RemoveAt(this.Size - 1);
            this.HeapifyDown(0);

            return firstElement;
        }

        private void HeapifyDown(int index)
        {
            var leftChildIndex = this.GetLeftChildIndex(index);

            while (this.IsValidIndex(leftChildIndex) && this.IsLesser(index, leftChildIndex))
            {
                var toSwapWith = leftChildIndex;
                var rightChildIndex = this.GetRightChildIndex(index);

                if (this.IsValidIndex(rightChildIndex) && this.IsLesser(toSwapWith, rightChildIndex))
                {
                    toSwapWith = rightChildIndex;
                }

                this.Swap(toSwapWith, index);
                index = toSwapWith;
                leftChildIndex = this.GetLeftChildIndex(leftChildIndex);
            }
        }

        private int GetRightChildIndex(int index)
            => 2 * index + 2;

        private bool IsLesser(int index, int leftChildIndex)
            => this.data[index].CompareTo(this.data[leftChildIndex]) < 0;

        private int GetLeftChildIndex(int index)
            => 2 * index + 1;

        private void HeapifyUp(int index)
        {
            var parentIndex = this.GetParentIndex(index);

            while (this.IsValidIndex(index) && this.IsGreater(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            }
        }

        private bool IsValidIndex(int index)
            => index >= 0 && index < this.Size;

        private void Swap(int index, int parentIndex)
        {
            var temp = this.data[index];
            this.data[index] = this.data[parentIndex];
            this.data[parentIndex] = temp;
        }

        private bool IsGreater(int index, int parentIndex)
            => this.data[index].CompareTo(this.data[parentIndex]) > 0;

        private int GetParentIndex(int index)
            => (index - 1) / 2;

        private void ValidateIsEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Collection is empty!");
            }
        }
    }
}
