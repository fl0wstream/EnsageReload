namespace EvAwareness.Utility.Console
{
    using System.ComponentModel;

    [DefaultValue(Medium)]
    public enum MessageClass
    {
        Warning, Error, Low, Medium, Severe
    }
}