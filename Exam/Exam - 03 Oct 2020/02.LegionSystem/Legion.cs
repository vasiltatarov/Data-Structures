namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> data;

        public Legion()
        {
            this.data = new OrderedSet<IEnemy>();
        }

        public int Size => this.data.Count;

        public bool Contains(IEnemy enemy)
            => this.data.Contains(enemy);

        public void Create(IEnemy enemy)
        {
            this.data.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            for (int i = 0; i < this.Size; i++)
            {
                if (this.data[i].AttackSpeed == speed)
                {
                    return this.data[i];
                }
            }

            return null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            var result = new List<IEnemy>();

            for (int i = 0; i < this.Size; i++)
            {
                if (this.data[i].AttackSpeed > speed)
                {
                    result.Add(this.data[i]);
                }
            }

            return result;
        }

        public IEnemy GetFastest()
        {
            this.EnsureNotEmpty();
            return this.data.GetFirst();
        }

        public IEnemy[] GetOrderedByHealth()
            => this.data.OrderByDescending(x => x.Health).ToArray();

        public List<IEnemy> GetSlower(int speed)
        {
            var result = new List<IEnemy>();

            for (int i = 0; i < this.Size; i++)
            {
                if (this.data[i].AttackSpeed < speed)
                {
                    result.Add(this.data[i]);
                }
            }

            return result;
        }

        public IEnemy GetSlowest()
        {
            this.EnsureNotEmpty();
            return this.data.GetLast();
        }

        public void ShootFastest()
        {
            this.EnsureNotEmpty();
            this.data.RemoveFirst();
        }

        public void ShootSlowest()
        {
            this.EnsureNotEmpty();
            this.data.RemoveLast();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }
    }
}
