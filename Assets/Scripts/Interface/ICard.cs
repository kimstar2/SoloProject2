using SO;
using Vector2 = UnityEngine.Vector2;

namespace Interface
{
    public interface ICard
    {
        CardDefinitionSo CardDefinition { get; }
        bool IsDragging { get; }
        bool IsInDeck { get; }

        void SetLayout(Vector2 pos, float zRot);
    }
}
