namespace GameLogic.GameHistory
{
    internal abstract class AGameMove
    {
        internal abstract void Undo(Board board);
        internal abstract void Redo(Board board);
    }
}
