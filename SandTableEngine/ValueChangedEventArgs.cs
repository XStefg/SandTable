using System;

namespace SandTableEngine;

/// <summary>
///
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <seealso cref="System.EventArgs" />
public class ValueChangedEventArgs<TValue> : EventArgs
{
  #region Constructors

  /// <summary>
  /// Initializes a new instance of the <see cref="ValueChangedEventArgs{TValue}"/> class.
  /// </summary>
  /// <param name="oldValue">The old value.</param>
  /// <param name="newValue">The new value.</param>
  public ValueChangedEventArgs( TValue oldValue, TValue newValue )
    : base()
  {
    OldValue = oldValue;
    NewValue = newValue;
  }

  #endregion

  #region Public Properties

  /// <summary>
  /// Gets the old value.
  /// </summary>
  /// <value>
  /// The old value.
  /// </value>
  public TValue OldValue { get; }

  /// <summary>
  /// Gets the new value.
  /// </summary>
  /// <value>
  /// The new value.
  /// </value>
  public TValue NewValue { get; }

  #endregion
}