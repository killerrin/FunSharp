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

            Console.ReadLine();
        }

        private static async void StrawpollGetPoll()
        {
            StrawpollService service = StrawpollService.Instance;
            StrawpollPoll poll = await service.GetPoll(1);

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

        private static async void StrawpollPostPoll()
        {
            StrawpollService service = StrawpollService.Instance;
            var pollSettings = StrawpollSettings.GeneratePostSettings("Will this work?", false, new List<string> { "Yes", "No", "Maybe", "Aethex is a dick" });
            StrawpollPoll poll = await service.PostPoll(pollSettings);

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
    }
}