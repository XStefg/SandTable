using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandTableEngine
{
  public class ThetaRFile
  {
    public static ThetaRFile CreateFromString( IEnumerable<string> lines )
    {
      List<ThetaRPoint> points = new List<ThetaRPoint>();

      foreach ( string line in lines )
      {
        string[] parts = line.Split( ' ' );
        if ( parts.Length == 2 )
        {
          if ( double.TryParse( parts[0], out double theta ) && double.TryParse( parts[1], out double r ) )
          {
            points.Add( new ThetaRPoint { Angle = theta, Radius = r } );
          }
        }
      }

      return new ThetaRFile { Points = points.ToImmutableList() };
    }

    public static ThetaRFile CreateFromFile( string filename )
    {
      return CreateFromString( System.IO.File.ReadAllLines( filename ) );
    }

    public ImmutableList<ThetaRPoint> Points { get; init; } = ImmutableList<ThetaRPoint>.Empty;
  }
}