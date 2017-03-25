using System;
using System.Collections.Generic;
using System.Text;

namespace FunSharp.Core.Games.Strawpoll
{
    public class StrawpollSettings
    {
        public string Title { get; set; }
        public bool Multi { get; set; }
        List<string> Options { get; set; }

        public StrawpollSettings()
        {
            Options = new List<string>();
        }

        public static StrawpollSettings GeneratePostSettings(string title, bool multi, List<string> options)
        {
            StrawpollSettings settings = new StrawpollSettings();
            settings.Title = title;
            settings.Multi = multi;
            settings.Options.AddRange(options);

            return settings;
        }

        #region Factory
        public StrawpollPoll CreatePoll()
        {
            StrawpollPoll poll = new StrawpollPoll();
            poll.title = Title;
            poll.multi = Multi;
            poll.options.AddRange(Options);
            return poll;
        }
        #endregion
    }
}
