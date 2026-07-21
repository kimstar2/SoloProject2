namespace Interface
{
    public interface IDraggable
    {
        bool CanDrag { get; }
        bool IsDragging { get; }
        bool DropSucceeded { get; }

        void MarkDropSucceeded();
        void MarkDropFailed();
    }
}
