namespace EvAwareness.UI
{
    abstract class ElementHandler
    {
        public abstract void OnLoad();

        public abstract bool ShouldDraw();

        public abstract void OnDraw();
    }
}