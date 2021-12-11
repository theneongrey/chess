namespace GameLogic.GameHistory
{
    internal abstract class AGameMove
    {
        internal abstract void Undo(Board field);
        internal abstract void Redo(Board field);
    }
}
