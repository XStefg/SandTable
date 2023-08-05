using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SandTableEngine.Services;

public class BaseService : INotifyPropertyChanged, IDisposable
{
  #region Public Events

  public event PropertyChangedEventHandler? PropertyChanged;

  #endregion

  #region IDispoable Implementation

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  #endregion

  protected virtual void OnPropertyChanged( PropertyChangedEventArgs eventArgs )
    => PropertyChanged.SafeRaiseEvent( this, eventArgs );

  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         [CallerMemberName] string? propertyName = null )
    => SetProperty( ref property,
                    value,
                    null,
                    null,
                    (EventHandler<ValueChangedEventArgs<T>>?)null,
                    propertyName );

  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  /// <param name="onChanged"> Action raised if the value has changed and after the value is set and before the event raised</param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         Action? onChanged,
                                         [CallerMemberName] string? propertyName = null )
    => SetProperty( ref property,
                    value,
                    null,
                    onChanged,
                    (EventHandler<ValueChangedEventArgs<T>>?)null,
                    propertyName );

  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  /// <param name="onChanging"> Action raised if the value has changed and before the value is set </param>
  /// <param name="onChanged"> Action raised if the value has changed and after the value is set and before the event raised</param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         Action? onChanging,
                                         Action? onChanged,
                                         [CallerMemberName] string? propertyName = null )
    => SetProperty( ref property,
                    value,
                    onChanging,
                    onChanged,
                    (EventHandler<ValueChangedEventArgs<T>>?)null,
                    propertyName );

  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  /// <param name="changedEventHandler"> Event to raise </param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         EventHandler<ValueChangedEventArgs<T>>? changedEventHandler,
                                         [CallerMemberName] string? propertyName = null ) =>
    SetProperty( ref property, value, null, null, changedEventHandler, propertyName );

  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  /// <param name="onChanged"> Action raised if the value has changed and after the value is set and before the event raised</param>
  /// <param name="changedEventHandler"> Event to raise </param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         Action? onChanged,
                                         EventHandler<ValueChangedEventArgs<T>>? changedEventHandler,
                                         [CallerMemberName] string? propertyName = null ) 
    => SetProperty( ref property, value, null, onChanged, changedEventHandler, propertyName );


  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  /// <param name="onChanging"> Action raised if the value has changed and before the value is set </param>
  /// <param name="onChanged"> Action raised if the value has changed and after the value is set and before the event raised</param>
  /// <param name="changedEventHandler"> Event to raise </param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         Action? onChanging,
                                         Action? onChanged,
                                         EventHandler<ValueChangedEventArgs<T>>? changedEventHandler,
                                         [CallerMemberName] string? propertyName = null )
  {
    if ( EqualityComparer<T>.Default.Equals( property, value ) )
    {
      return false;
    }

    onChanging?.Invoke();

    T oldValue = property;
    property = value;

    onChanged?.Invoke();

    OnPropertyChanged( new PropertyChangedEventArgs( propertyName ) );

    changedEventHandler.SafeRaiseEvent( this, new ValueChangedEventArgs<T>( oldValue, property ) );

    return true;
  }

  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  /// <param name="changedEventHandler"> Event to raise </param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         EventHandler<NewValueEventArgs<T>>? changedEventHandler,
                                         [CallerMemberName] string? propertyName = null ) =>
    SetProperty( ref property, value, null, null, changedEventHandler, propertyName );

  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  /// <param name="_onChanging"> Action raised if the value has changed and before the value is set </param>
  /// <param name="onChanged"> Action raised if the value has changed and after the value is set and before the event raised</param>
  /// <param name="changedEventHandler"> Event to raise </param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         Action? onChanged,
                                         EventHandler<NewValueEventArgs<T>>? changedEventHandler,
                                         [CallerMemberName] string? propertyName = null ) 
    => SetProperty( ref property, value, null, onChanged, changedEventHandler, propertyName );

  /// <summary>
  /// Used to simplify Property handling with or without associated event
  /// </summary>
  /// <param name="property"> Reference to the internal variable to set</param>
  /// <param name="value"> new value to apply </param>
  /// <param name="onChanging"> Action raised if the value has changed and before the value is set </param>
  /// <param name="onChanged"> Action raised if the value has changed and after the value is set and before the event raised</param>
  /// <param name="changedEventHandler"> Event to raise </param>
  protected virtual bool SetProperty<T>( ref T property,
                                         T value,
                                         Action? onChanging,
                                         Action? onChanged,
                                         EventHandler<NewValueEventArgs<T>>? changedEventHandler,
                                         [CallerMemberName] string? propertyName = null )
  {
    if ( EqualityComparer<T>.Default.Equals( property, value ) )
    {
      return false;
    }

    onChanging?.Invoke();

    property = value;

    onChanged?.Invoke();

    OnPropertyChanged( new PropertyChangedEventArgs( propertyName ) );

    changedEventHandler.SafeRaiseEvent( this, new NewValueEventArgs<T>( property ) );

    return true;
  }

  protected virtual void Dispose(bool disposing)
  {
    if (disposing)
    {
    }
  }
}