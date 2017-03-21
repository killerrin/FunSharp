using FunSharp.Games.Strawpoll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunSharp.ConsoleApp
{
    class Program
    {
        static async void Main(string[] args)
        {
            StrawpollService service = StrawpollService.Instance;
            var poll = await service.GetPoll(1);

            Console.WriteLine($"ID: {poll.id}");
            Console.WriteLine($"Title: {poll.title}");
            Console.WriteLine($"Multi: {poll.multi}");
            Console.WriteLine($"Dupcheck: {poll.dupcheck}");
            Console.WriteLine($"Captcha: {poll.captcha}");

            for (int i = 0; i < poll.options.Count; i++)
            {
                Console.WriteLine($"{poll.options[i]}\t\t{poll.votes[i]}");
            }

            Console.ReadLine();
        }
    }
}
