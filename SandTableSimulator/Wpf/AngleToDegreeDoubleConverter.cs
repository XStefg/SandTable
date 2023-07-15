using System;
using System.Globalization;
using System.Windows.Data;
using SandTableEngine.Units;

namespace SandTableSimulator.Wpf;

public class AngleToDegreeDoubleConverter : IValueConverter
{
  public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
  {
    if ( value is Angle angle )
    {
      return angle.ValueInDegree;
    }
    else
    {
      return value;
    }
  }

  public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
  {
    if ( value is double doubleValue )
    {
      return Angle.CreateFromDegree( doubleValue );
    }
    else
    {
      return default( Angle );
    }
  }
}