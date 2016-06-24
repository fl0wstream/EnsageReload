namespace EvAwareness.Utility.Console
{
    using System.ComponentModel;

    [DefaultValue(Medium)]
    public enum MessageType
    {
        Warning, Error, Low, Medium, Severe
    }
}