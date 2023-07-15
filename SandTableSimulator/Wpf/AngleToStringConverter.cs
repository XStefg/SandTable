using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Eddyfi.Core;
using SandTableEngine.Units;

namespace SandTableSimulator.Wpf
{
  public class AngleToStringConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if ( value is Angle angle )
      {
        return angle.ValueInDegree.ToString( "F1" );
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
        return Angle.CreateFromDegree( double.Parse( stringValue ) );
      }
      else
      {
        return default( Angle );
      }
    }
  }
}