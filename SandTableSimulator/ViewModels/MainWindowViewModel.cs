using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddyfi.Core;
using SandTableEngine;

namespace SandTableSimulator.ViewModels
{
  internal class MainWindowViewModel : BaseViewModel
  {
    public MainWindowViewModel()
    {
      m_KinematicParameters = new KinematicParameters { Distance1 = 0.3, Distance2 = 0.4 };

      m_ForwardKinematic = ForwardKinematic.ComputeFor( m_KinematicParameters, m_Angle1, m_Angle2 );

      m_Subscriber = this.GetSubscriberBuilder()
                         .AddSubscription( vm => vm.Angle1, CalculateForwardKinematic )
                         .AddSubscription( vm => vm.Angle2, CalculateForwardKinematic )
                         .Subscribe();
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

    private Angle m_Angle1 = Angle.CreateFromDegree( 45 );
    private Angle m_Angle2 = Angle.CreateFromDegree( 45 );

    private readonly IDisposable m_Subscriber;
  }
}