namespace EvAwareness.Modules
{
    using Utility;

    abstract class ModuleHandler
    {
        public void OnLoad()
        {
            this.CreateMenu();
            this.InitEvents();
        }

        public abstract void CreateMenu();

        public abstract void InitEvents();

        public abstract ModuleType GetModuleType();

        public abstract bool ShouldRun();

        public abstract void OnTick();
    }
}