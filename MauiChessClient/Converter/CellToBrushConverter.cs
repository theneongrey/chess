using MauiChessClient.ViewModel;
using System.Globalization;

namespace MauiChessClient.Converter
{
    public class CellToBrushConverter : IValueConverter
    {
        public Brush BrightBrush { get; set; } = new SolidColorBrush(Colors.LightGray);
        public Brush DarkBrush { get; set; } = new SolidColorBrush(Colors.DarkGray);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CellVM cell)
            {
                if (cell.Y % 2 != 0)
                {
                    return cell.X % 2 == 0 ? BrightBrush : DarkBrush;
                }
                else
                {
                    return cell.X % 2 == 0 ? DarkBrush : BrightBrush;
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
