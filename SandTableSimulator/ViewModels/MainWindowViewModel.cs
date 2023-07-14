using System;
using Eddyfi.Core;
using SandTableEngine;

namespace SandTableSimulator.ViewModels
{
  internal class MainWindowViewModel : BaseViewModel
  {
    public MainWindowViewModel()
    {
      Distance1 = 0.3;
      Distance2 = 0.4;

      Angle1 = Angle.CreateFromDegree( 45 );
      Angle2 = Angle.CreateFromDegree( 45 );

      m_KinematicParameters = new KinematicParameters { Distance1 = Distance1, Distance2 = Distance2 };
      m_ForwardKinematic    = ForwardKinematic.ComputeFor( m_KinematicParameters, Angle1, Angle2 );

      m_Subscriber = this.GetSubscriberBuilder()
                         .AddSubscription( vm => vm.Angle1,              CalculateForwardKinematic )
                         .AddSubscription( vm => vm.Angle2,              CalculateForwardKinematic )
                         .AddSubscription( vm => vm.KinematicParameters, CalculateForwardKinematic )
                         .AddSubscription( vm => vm.Distance1,           CalculateKinematicParameters )
                         .AddSubscription( vm => vm.Distance2,           CalculateKinematicParameters )
                         .SubscribeAndInvokeAll();

    }

    private void CalculateKinematicParameters()
    {
      KinematicParameters = new KinematicParameters { Distance1 = Distance1, Distance2 = Distance2 };
    }

    private void CalculateForwardKinematic()
    {
      ForwardKinematic = ForwardKinematic.ComputeFor( m_KinematicParameters, Angle1, Angle2 );
    }

    protected override void Dispose( bool disposing )
    {
      if ( disposing )
      {
        m_Subscriber.Dispose();
      }
    }

    public Angle Angle1
    {
      get => m_Angle1;
      set => SetProperty( ref m_Angle1, value );
    }

    public Angle Angle2
    {
      get => m_Angle2;
      set => SetProperty( ref m_Angle2, value );
    }

    public Distance Distance1
    {
      get => m_Distance1;
      set => SetProperty( ref m_Distance1, value );
    }

    public Distance Distance2
    {
      get => m_Distance2;
      set => SetProperty( ref m_Distance2, value );
    }

    public KinematicParameters KinematicParameters
    {
      get => m_KinematicParameters;
      set => SetProperty( ref m_KinematicParameters, value );
    }

    private KinematicParameters m_KinematicParameters;

    public ForwardKinematic ForwardKinematic
    {
      get => m_ForwardKinematic;
      set => SetProperty( ref m_ForwardKinematic, value );
    }

    private ForwardKinematic m_ForwardKinematic;

    private Angle    m_Angle1;
    private Angle    m_Angle2;
    private Distance m_Distance1;
    private Distance m_Distance2;

    private readonly IDisposable m_Subscriber;
  }
}