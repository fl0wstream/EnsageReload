namespace EvAwareness.Utility.Console
{
    using System;

    public class ConsoleItem
    {
        public MessageType Type { get; set; }

        public string Exception;

        public string Module;

        public ConsoleItem(string module, object exception, MessageType type = MessageType.Medium)
        {
            this.Module = module;
            this.Exception = exception.ToString();
            this.Type = type;
        }

        public string GetLoggingString()
        {
            return string.Format("({1} | {3}) {0}->{2}",
                this.Module,
                this.Type.ToString().ToUpper(),
                this.Exception,
                DateTime.Now);
        }
    }
}