using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeratDemirel_215040088_GameProgramming_MidTerm
{
    public class Entity
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int Armor { get; set; }

        public Entity(string name, int health, int level, int armor)
        {
            Name = name;
            Health = health;
            Level = level;
            Armor = armor;
        }
    }
}

