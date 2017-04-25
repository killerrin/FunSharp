using FunSharp.Core.Games.Randomized;
using FunSharp.Core.Games.Strawpoll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunSharp.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //StrawpollGetPoll();
            //StrawpollPostPoll();
            //Test8Ball();
            TestDice();

            Console.ReadLine();
        }

        private static void TestDice()
        {
            Dice die = new Dice();
            while (true)
            {
                Console.Write("Enter your die string (2d20): ");
                var dieStr = Console.ReadLine();
                if (dieStr.ToLower() == "exit")
                    break;

                var result = die.RollMultiple(dieStr);
                foreach (var i in result)
                {
                    Console.WriteLine(i);
                }

                Console.WriteLine();
            }
        }

        private static void Test8Ball()
        {
            var magic8Ball = Magic8Ball.Instance;
            while (true)
            {
                Console.WriteLine(magic8Ball.RandomAll());

                var result = Console.ReadLine();
                if (result == "exit")
                    break;
            }
        }

        private static async void StrawpollGetPoll()
        {
            StrawpollService service = StrawpollService.Instance;
            StrawpollPoll poll = await service.GetPoll(1);

            if (poll != null)
            {
                Console.WriteLine($"ID: {poll.id}");
                Console.WriteLine($"Title: {poll.title}");
                Console.WriteLine($"Multi: {poll.multi}");
                Console.WriteLine($"Dupcheck: {poll.dupcheck}");
                Console.WriteLine($"Captcha: {poll.captcha}");

                for (int i = 0; i < poll.options.Count; i++)
                {
                    Console.WriteLine(string.Format("\t{0,-15}{1}", poll.votes[i] + " votes", poll.options[i]));
                }

            }
            else
            {
                Console.WriteLine("There was an unexpected error. Please try again later");
            }
        }

        private static async void StrawpollPostPoll()
        {
            StrawpollService service = StrawpollService.Instance;
            var pollSettings = StrawpollSettings.GeneratePostSettings("Will this work?", false, new List<string> { "Yes", "No", "Maybe", "Aethex is a dick" });
            StrawpollPoll poll = await service.PostPoll(pollSettings);

            if (poll != null)
            {
                Console.WriteLine($"ID: {poll.id}");
                Console.WriteLine($"Title: {poll.title}");
                Console.WriteLine($"Multi: {poll.multi}");
                Console.WriteLine($"Dupcheck: {poll.dupcheck}");
                Console.WriteLine($"Captcha: {poll.captcha}");

                for (int i = 0; i < poll.options.Count; i++)
                {
                    Console.WriteLine($"{poll.options[i]}\t\t{poll.votes[i]}");
                }
            }
            else
            {
                Console.WriteLine("There was an unexpected error. Please try again later");
            }
        }
    }
}