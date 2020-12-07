using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AssyChargeSEHC
{
    public class ColorChangeOKNG : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MeasurementValues.Judge)
            {
                switch ((MeasurementValues.Judge)value)
                {
                    case MeasurementValues.Judge.None:
                        //return new SolidColorBrush(Color.FromRgb(102, 96, 96));
                        return null;
                    case MeasurementValues.Judge.OK:
                        return new SolidColorBrush(Color.FromRgb(53, 213, 83));
                    case MeasurementValues.Judge.NG:
                        return new SolidColorBrush(Color.FromRgb(241, 89, 55));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ColorChangeOKNGVoltage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MeasurementValues.Judge)
            {
                switch ((MeasurementValues.Judge)value)
                {
                    case MeasurementValues.Judge.None:
                        //return new SolidColorBrush(Color.FromRgb(102, 96, 96));
                        return null;
                    case MeasurementValues.Judge.OK:
                        return new SolidColorBrush(Color.FromRgb(53, 213, 83));
                    case MeasurementValues.Judge.NG:
                        return new SolidColorBrush(Color.FromRgb(241, 89, 55));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ColorChangeOKNGCurrent : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MeasurementValues.Judge)
            {
                switch ((MeasurementValues.Judge)value)
                {
                    case MeasurementValues.Judge.None:
                        //return new SolidColorBrush(Color.FromRgb(102, 96, 96));
                        return null;
                    case MeasurementValues.Judge.OK:
                        return new SolidColorBrush(Color.FromRgb(53, 213, 83));
                    case MeasurementValues.Judge.NG:
                        return new SolidColorBrush(Color.FromRgb(241, 89, 55));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
