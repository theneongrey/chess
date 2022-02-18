using PropertyChanged;

namespace MauiChessClient.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class CellVM
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Size { get; set; } = 10;
        public string? Value { get; set; }
    }
}
