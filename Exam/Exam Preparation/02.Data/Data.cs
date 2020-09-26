namespace _02.Data
{
    using _02.Data.Interfaces;
    using _02.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Data : IRepository
    {
        private OrderedBag<IEntity> data;

        public Data()
        {
            this.data = new OrderedBag<IEntity>();
        }

        public Data(Data copy)
        {
            this.data = copy.data;
        }

        public int Size => this.data.Count;

        public void Add(IEntity entity)
        {
            this.data.Add(entity);

            var parentId = this.GetById((int)entity.ParentId);

            if (parentId != null)
            {
                parentId.Children.Add(entity);
            }
        }

        public IRepository Copy()
        {
            var data = (Data)this.MemberwiseClone();

            return new Data(data);
        }

        public IEntity DequeueMostRecent()
        {
            this.EnsureNotEmpty();
            return this.data.RemoveFirst();
        }

        public List<IEntity> GetAll()
        {
            return this.data.ToList();
        }

        public List<IEntity> GetAllByType(string type)
        {
            if (type != typeof(Invoice).Name && type != typeof(StoreClient).Name && type != typeof(User).Name)
            {
                throw new InvalidOperationException($"Invalid type: {type}");
            }

            var result = new List<IEntity>();

            for (int i = 0; i < this.Size; i++)
            {
                var current = this.data[i];

                if (current.GetType().Name == type)
                {
                    result.Add(current);
                }
            }

            return result;

            //return this.data.Where(x => x.GetType().Name == type).ToList();
        }

        public IEntity GetById(int id)
        {
            if (this.Size != 0 && this.ValidateId(id))
            {
                return this.data[this.Size - 1 - id];
            }

            return null;
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            var parentNode = this.GetById(parentId);

            if (parentNode == null)
            {
                return new List<IEntity>();
            }

            return parentNode.Children;
        }

        public IEntity PeekMostRecent()
        {
            this.EnsureNotEmpty();
            return this.data.GetFirst();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
        }

        private bool ValidateId(int id)
            => id >= 0 && id < this.Size;
    }
}
