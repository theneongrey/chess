using System.Collections.ObjectModel;

namespace MauiChessClient.ViewModel
{
    public class GameVM
    {
        public ObservableCollection<CellVM> Cells { get; } = new ();

        public GameVM()
        {
            for (var y = 7; y >= 0; y--)
            {
                for (var x = 0; x < 8; x++)
                {
                    Cells.Add(new CellVM
                    {
                        X = x,
                        Y = y,
                        Value = (y * 8 + x).ToString(),
                    }); 
                }
            }
        }
    }
}
