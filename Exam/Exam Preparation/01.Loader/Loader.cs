namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> data;

        public Loader()
        {
            this.data = new List<IEntity>();
        }

        public int EntitiesCount => this.data.Count;

        public void Add(IEntity entity)
        {
            this.data.Add(entity);
        }

        public void Clear()
        {
            this.data.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this.GetById(entity.Id) != null;
        }

        public IEntity Extract(int id)
        {
            var found = this.GetById(id);

            if (found != null)
            {
                this.data.Remove(found);
                return found;
            }

            return null;
        }

        public IEntity Find(IEntity entity)
        {
            return this.GetById(entity.Id);
        }

        public List<IEntity> GetAll()
        {
            return this.data;
        }

        public void RemoveSold()
        {
            var status = BaseEntityStatus.Sold;

            for (int i = 0; i < this.data.Count; i++)
            {
                if (this.data[i].Status == status)
                {
                    this.data.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            if (!this.Contains(oldEntity))
            {
                throw new InvalidOperationException("Entity not found");
            }

            var indexOfOldEntity = this.data.IndexOf(oldEntity);

            this.data[indexOfOldEntity] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            var result = new List<IEntity>();
            var firstValueEntityStatus = (int)lowerBound;
            var secondValueEntityStatus = (int)upperBound;

            for (int i = 0; i < this.data.Count; i++)
            {
                var statusNum = (int)this.data[i].Status;

                if (statusNum >= firstValueEntityStatus && statusNum <= secondValueEntityStatus)
                {
                    result.Add(this.data[i]);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            var indexOfFirstEntity = this.data.IndexOf(first);
            var indexOfSecondEntity = this.data.IndexOf(second);

            if (indexOfFirstEntity == -1 || indexOfSecondEntity == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            var temp = this.data[indexOfFirstEntity];
            this.data[indexOfFirstEntity] = this.data[indexOfSecondEntity];
            this.data[indexOfSecondEntity] = temp;
        }

        public IEntity[] ToArray()
        {
            return this.data.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            for (int i = 0; i < this.data.Count; i++)
            {
                if (this.data[i].Status == oldStatus)
                {
                    this.data[i].Status = newStatus;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        public IEnumerator<IEntity> GetEnumerator()
        {
            for (int i = 0; i < this.data.Count; i++)
            {
                yield return this.data[i];
            }
        }

        private IEntity GetById(int id)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var current = this.data[i];

                if (current.Id == id)
                {
                    return current;
                }
            }

            return null;
        }
    }
}
