namespace Interface
{
    public interface IGetOwner<in T>
    {
        void Get(T owner);
    }
}