namespace EvAwareness.Utility.Console
{
    using System;

    public class ConsoleItem
    {
        public MessageClass Class { get; set; }

        public string Exception;

        public string Module;

        public ConsoleItem(string module, object exception, MessageClass @class = MessageClass.Medium)
        {
            this.Module = module;
            this.Exception = exception.ToString();
            this.Class = @class;
        }

        public string GetLoggingString()
        {
            return string.Format("({1} | {3}) {0}->{2}",
                this.Module,
                this.Class.ToString().ToUpper(),
                this.Exception,
                DateTime.Now);
        }
    }
}