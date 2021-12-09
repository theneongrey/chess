namespace GameLogic.GameHistory
{
    internal abstract class AGameMove
    {
        internal abstract void Undo(Field field);
        internal abstract void Redo(Field field);
    }
}
