using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace SandTableEngine;

public static class UltilitiesExtension
{
  /// <summary>
  /// Used to raise an event, protecting event not to propagate to all subscriber in case of exception.  It catches the exception and log it.
  /// </summary>
  /// <param name="handler"></param>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  public static void SafeRaiseEvent( this EventHandler? handler, object sender, EventArgs args )
  {
    handler?.GetInvocationList().SafeRaiseInvocations<EventHandler>( handler => handler( sender, args ), sender );
  }

  public static void SafeRaiseEvent( this PropertyChangedEventHandler? handler,
                                     object sender,
                                     PropertyChangedEventArgs e )
    => handler?.GetInvocationList()
              .SafeRaiseInvocations<PropertyChangedEventHandler>( handler => handler( sender, e ), sender );

  public static void SafeRaiseInvocations<TDelegate>( this IEnumerable<Delegate> invocationList,
                                                      Action<TDelegate> action, object sender )
    where TDelegate : Delegate
  {
    foreach ( TDelegate currentHandler in invocationList )
    {
      try
      {
        action( currentHandler );
      }
      catch ( ThreadInterruptedException )
      {
        throw;
      }
      catch ( ThreadAbortException )
      {
        throw;
      }
      catch ( Exception )
      {
        //LogManager.GetLogger( sender.GetType().ToString() ).LogAndNotifyException( "An invocation error occured.", exception );
      }
    }
  }

  /// <summary>
  /// Used to raise an event asynchronously, protecting event not to propagate to all subscriber in case of exception.  It catches the exception and log it.
  /// </summary>
  public static IAsyncResult AsyncSafeRaiseEvent( this EventHandler? handler, object sender, EventArgs args )
  {
    if ( handler is null )
    {
      return Task.CompletedTask;
    }

    Action<EventHandler?, object, EventArgs> asyncCall = SafeRaiseEvent;

#if NET6_0_OR_GREATER
    return Task.Run( () => asyncCall.Invoke( handler, sender, args ) );
#else
      return asyncCall.BeginInvoke( handler, sender, args, null, null );
#endif
  }

  /// <summary>
  /// Used to raise an event asynchronously, protecting event not to propagate to all subscriber in case of exception.  It catches the exception and log it.
  /// </summary>
  public static IAsyncResult AsyncSafeRaiseEvent<T>( this EventHandler<T> handler, object sender, T args ) where T : EventArgs
  {
    if ( handler is null )
    {
      return Task.CompletedTask;
    }

    Action<EventHandler<T>, object, T> asyncCall = SafeRaiseEvent<T>;

#if NET6_0_OR_GREATER
    return Task.Run( () => asyncCall.Invoke( handler, sender, args ) );
#else
      return asyncCall.BeginInvoke( handler, sender, args, null, null );
#endif
  }

  /// <summary>
  /// Used to raise an event, protecting event not to propagate to all subscriber in case of exception.  It catches the exception and log it.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="handler"></param>
  /// <param name="sender"></param>
  /// <param name="arg"></param>
  public static void SafeRaiseEvent<T>( this EventHandler<T>? handler, object sender, T arg ) where T : EventArgs
  {
    if ( handler != null )
    {
      handler.GetInvocationList().SafeRaiseInvocations<EventHandler<T>>( handler => handler( sender, arg ), sender );
    }
  }
}