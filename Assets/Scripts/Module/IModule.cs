namespace Module
{
    public interface IModule
    {
        ModuleCompo Owner {get;}
        
        void Init(ModuleCompo owner);
    }
}
