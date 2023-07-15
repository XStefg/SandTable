using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandTableEngine.Units;

[DebuggerDisplay( "{Value}m" )]
public struct Distance
{
  #region CTOR

  /// <summary>
  /// CTOR
  /// </summary>
  /// <param name="value">value in rad</param>
  private Distance( double value )
  {
    Value = value;
  }

  #endregion

  #region Public Properties

  public double Value { get; set; }

  #endregion

  #region public Methods

  public static Distance CreateFromMeter( double value ) => new( value );

  public static Distance CreateFromMilimeter( double valueInMilimeter ) => new( valueInMilimeter / 1000 );

  public override string ToString() => $"{Value:f3}m";

  #endregion

  #region Operators

  public static implicit operator Distance( double value ) => new( value );

  public static implicit operator double( Distance distance ) => distance.Value;

  #endregion
}