namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Transactions;

    public class Inventory : IHolder
    {
        private List<IWeapon> data;

        public Inventory()
        {
            this.data = new List<IWeapon>();
        }

        public int Capacity => this.data.Count;

        public void Add(IWeapon weapon)
        {
            this.data.Add(weapon);
        }

        public void Clear()
        {
            this.data.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            var founded = this.GetById(weapon.Id);

            if (founded != null)
            {
                return true;
            }

            return false;
        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                var current = this.data[i].Category;

                if (current == category)
                {
                    this.data[i].Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            if (!this.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (ammunition > weapon.Ammunition)
            {
                return false;
            }

            weapon.Ammunition -= ammunition;
            return true;
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                var current = this.data[i];

                if (current.Id == id)
                {
                    return current;
                }
            }

            return null;
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            if (!this.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            var capacity = weapon.MaxCapacity;
            weapon.Ammunition += ammunition;

            if (weapon.Ammunition > capacity)
            {
                weapon.Ammunition = capacity;
            }

            return weapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            var founded = this.GetById(id);

            if (founded != null)
            {
                this.data.Remove(founded);
                return founded;
            }

            throw new InvalidOperationException("Weapon does not exist in inventory!");
        }

        public int RemoveHeavy()
        {
            var counter = 0;

            for (int i = 0; i < this.Capacity; i++)
            {
                var current = this.data[i].Category;

                if (current == Category.Heavy)
                {
                    this.data.RemoveAt(i);
                    i--;
                    counter++;
                }
            }

            return counter;
        }

        public List<IWeapon> RetrieveAll()
            => this.data;

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            var result = new List<IWeapon>();
            var firstWeaponCategoryBound = (int)lower;
            var secondWeaponCategoryBound = (int)upper;

            for (int i = 0; i < this.data.Count; i++)
            {
                var statusNum = (int)this.data[i].Category;

                if (statusNum >= firstWeaponCategoryBound && statusNum <= secondWeaponCategoryBound)
                {
                    result.Add(this.data[i]);
                }
            }

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            var first = this.data.IndexOf(firstWeapon);
            var second = this.data.IndexOf(secondWeapon);

            if (first == -1 || second == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (firstWeapon.Category == secondWeapon.Category)
            {
                var temp = this.data[first];
                this.data[first] = this.data[second];
                this.data[second] = temp;
            }
        }

        public IEnumerator GetEnumerator()
            => this.data.GetEnumerator();
    }
}
