using System;
using System.Globalization;
using System.Windows.Data;
using Eddyfi.Core;
using SandTableEngine;

namespace SandTableSimulator.Wpf;

public class DistanceToStringConverter : IValueConverter
{
  public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
  {
    if ( value is Distance distance )
    {
      return distance.Value.ToString( "F3" );
    }
    else
    {
      return value.ToString() ?? "";
    }
  }

  public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
  {
    if ( value is string stringValue && stringValue.IsDouble() )
    {
      return (Distance) double.Parse( stringValue );
    }
    else
    {
      return default( Distance );
    }
  }
}