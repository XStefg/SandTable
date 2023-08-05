using System;

namespace SandTableEngine;

public class NewValueEventArgs<T> : EventArgs
{
  #region Constructors

  /// <summary>
  /// Initializes a new instance of the <see cref="NewValueEventArgs{T}"/> class.
  /// </summary>
  /// <param name="newValue">The new value.</param>
  public NewValueEventArgs( T newValue ) => NewValue = newValue;

  #endregion

  #region Public Properties

  /// <summary>
  /// Gets the new value.
  /// </summary>
  /// <value>
  /// The new value.
  /// </value>
  public T NewValue { get; }

  #endregion
}