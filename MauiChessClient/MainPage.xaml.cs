using MauiChessClient.ViewModel;

namespace MauiChessClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new GameVM();
            InitializeComponent();
            SizeChanged += CollectionView_SizeChanged;
        }

        private void CollectionView_SizeChanged(object sender, EventArgs e)
        {
            if (BindingContext is GameVM game)
            {
                var firstCell = game.Cells.FirstOrDefault();
                if (firstCell is not null)
                {
                    firstCell.Size = Board.Width / 8;
                }
            }
        }
    }
}