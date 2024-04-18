using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static BeratDemirel_215040088_GameProgramming_MidTerm.Enums;

namespace BeratDemirel_215040088_GameProgramming_MidTerm
{
    public class Battle
    {
        private readonly Random random = new();

        public async Task StartBattleAsync(Combatant player, Combatant enemy)
        {
            var name = "";

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please enter your name:");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty or whitespace. Please try again.");
                }
            }

            player.Name = name;
            Console.WriteLine($"Welcome, {player.Name}!");
            Console.WriteLine("Get ready to witness the most intense moments of the battle!");
            Console.WriteLine("Invincible Gorthaur the Malevolent is coming");

            Console.WriteLine("The battle begins!");
            var isRunAwayUsed = false;
            while (true)
            {
                await PlayerTurnAsync(player, enemy);
                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"{enemy.Name} has been defeated! {player.Name} wins. Congratulations!");
                    break;
                }

                await EnemyTurnAsync(enemy, player);

                if (player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name} has been defeated! {enemy.Name} wins. Congratulations!");
                    break;
                }
                if (player.Health <= 50 && !isRunAwayUsed)
                {
                    await PlayerRunAwayAsync(player);
                    isRunAwayUsed = true;
                }

                Console.WriteLine($"Current health of {player.Name}: {player.Health}");
                Console.WriteLine($"Current health of {enemy.Name}: {enemy.Health}");
                Console.WriteLine();

                await Task.Delay(1000); // Simulate some delay
            }

            Console.WriteLine("Battle ended!");
            Console.WriteLine($"Player Miss Count: {player.MissCount}");
            Console.WriteLine($"Enemy Miss Count: {enemy.MissCount}");
        }

        private async Task PlayerTurnAsync(Combatant player, Combatant enemy)
        {
            AttackType attackChoice;

            while (true)
            {
                Console.WriteLine($"Choose your attack type (Basic/Special): ");
                string? input = Console.ReadLine()?.ToLower();

                if (Enum.TryParse(input, true, out attackChoice) && Enum.IsDefined(typeof(AttackType), input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid attack type. Please enter Basic or Special.");
                }
            }

            await Task.Delay(100);
            if (random.Next(0, 10) < 2) // %20 iskalama şansı
            {
                Console.WriteLine("You missed!");
                player.IncrementMissCount();
            }
            else
            {
                int damage = CalculateDamage(player, enemy, attackChoice);
                enemy.Health -= damage;
                Console.WriteLine($"{player.Name} attacks {enemy.Name} with {attackChoice} attack and deals {damage} damage.");
            }
        }

        private async Task EnemyTurnAsync(Combatant enemy, Combatant player)
        {
            AttackType attackChoice = random.Next(0, 2) == 0 ? AttackType.basic : AttackType.special;

            await Task.Delay(100);
            if (random.Next(0, 10) < 2)
            {
                Console.WriteLine("Enemy missed!");
                enemy.IncrementMissCount();
            }
            else
            {
                int damage = CalculateDamage(enemy, player, attackChoice);
                player.Health -= damage;
                Console.WriteLine($"{enemy.Name} attacks {player.Name} with {attackChoice} attack and deals {damage} damage.");
            }
        }

        private static async Task PlayerRunAwayAsync(Combatant player)
        {
            string? choice = "";

            while (string.IsNullOrWhiteSpace(choice) || (choice != "y" && choice != "n"))
            {
                Console.WriteLine($"Do you want to run away? (Y/N)");
                choice = Console.ReadLine()?.ToLower();

                if (string.IsNullOrWhiteSpace(choice) || (choice != "y" && choice != "n"))
                {
                    Console.WriteLine("Your choice is invalid. Please try again.");
                }
            }
            await Task.Delay(100);
            if (choice == "y")
            {
                Console.WriteLine($"The merchant approached you and asked the following: 'I have a health elixir. 'Would you like to buy it?'(Y/N) (The elixir will add 10 points to your health.)");
                choice = Console.ReadLine()?.ToLower();
                if (choice == "y")
                {
                    player.Health += 10;
                }
            }
        }
        private int CalculateDamage(Combatant attacker, Combatant defender, AttackType attackType)
        {
            int baseDamage = 0;

            if (attackType == AttackType.basic)
            {
                baseDamage = random.Next(5, 16);
            }
            else if (attackType == AttackType.special)
            {
                baseDamage = random.Next(15, 26);
            }

            int damage = Math.Max(0, baseDamage - defender.Armor);

            return damage;
        }
    }
}

