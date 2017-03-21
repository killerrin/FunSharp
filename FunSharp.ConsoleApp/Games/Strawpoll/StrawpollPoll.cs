using System;
using System.Collections.Generic;

namespace FunSharp.Games.Strawpoll
{
    public class StrawpollPoll
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool multi { get; set; }
        public List<string> options { get; set; }

        
        public List<int> votes { get; set; } 
        public string dupcheck { get; set; }
        public bool captcha { get; set; }

        [Obsolete]
        public bool permissive { get; set; } = false;

        public StrawpollPoll()
        {
            multi = false;
            dupcheck = "normal";
            captcha = false;

            options = new List<string>();
            votes = new List<int>();
        }

        public string GetPollUrl()
        {
            return "http://www.strawpoll.me/" + id;
        }
    }
}
