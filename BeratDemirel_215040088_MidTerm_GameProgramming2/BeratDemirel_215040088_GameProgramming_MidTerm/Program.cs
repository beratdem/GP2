using System.Numerics;

namespace BeratDemirel_215040088_GameProgramming_MidTerm
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Combatant enemy = new("Gorthar the Malevolent", 100, 1, 3);
            Combatant player = new("", 100, 1, 2); // Name will be set by the player later

            Battle battle = new Battle();

            await battle.StartBattleAsync(player, enemy);
        }
    }
}