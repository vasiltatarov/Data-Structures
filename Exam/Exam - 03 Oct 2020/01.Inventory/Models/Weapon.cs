﻿namespace _01.Inventory.Models
{
    using _01.Inventory.Interfaces;

    public abstract class Weapon : IWeapon
    {
        public Weapon(int id, int maxCapacity, int ammunition)
        {
            this.Id = id;
            this.MaxCapacity = maxCapacity;
            this.Ammunition = ammunition;
        }

        public int Id { get; private set; }
        public int Ammunition { get; set; }
        public int MaxCapacity { get; set; }
        public Category Category { get; set; }

        public override bool Equals(object obj)
        {
            var current = (IWeapon)obj;

            return this.Id == current.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
