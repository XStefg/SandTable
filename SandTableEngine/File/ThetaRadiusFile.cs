using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandTableEngine.File
{
  public class ThetaRadiusFile
  {
    public static ThetaRadiusFile CreateFromString( IEnumerable<string> lines )
    {
      List<ThetaRadiusPoint> points = new List<ThetaRadiusPoint>();

      foreach ( string line in lines )
      {
        string[] parts = line.Split( ' ' );
        if ( parts.Length == 2 )
        {
          if ( double.TryParse( parts[0], out double theta ) && double.TryParse( parts[1], out double r ) )
          {
            points.Add( new ThetaRadiusPoint { Angle = theta, Radius = r } );
          }
        }
      }

      return new ThetaRadiusFile { Points = points.ToImmutableList() };
    }

    public static ThetaRadiusFile CreateFromFile( string filename )
    {
      return CreateFromString( System.IO.File.ReadAllLines( filename ) );
    }

    public ImmutableList<ThetaRadiusPoint> Points { get; init; } = ImmutableList<ThetaRadiusPoint>.Empty;
  }
}