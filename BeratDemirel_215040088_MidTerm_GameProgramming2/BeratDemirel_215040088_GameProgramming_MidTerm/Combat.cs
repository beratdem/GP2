using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeratDemirel_215040088_GameProgramming_MidTerm
{
    public class Combatant : Entity
    {
        public int MissCount { get; private set; } = 0;

        public Combatant(string name, int health, int level, int armor)
            : base(name, health, level, armor)
        {
        }

        public void IncrementMissCount()
        {
            MissCount++;
        }
    }
}