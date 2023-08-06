using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandTableEngine.File;
using SandTableEngine.Processor.ThetaRadius;

namespace SandTableEngine.Processor.FileReader
{
  public class ThetaRadiusFileReader : ProcessorBase<ThetaRadiusFileReaderConfig>, IProcessOutput<ThetaRadiusPoint>
  {
    public ThetaRadiusFileReader( ThetaRadiusFileReaderConfig config ) : base( config )
    {
    }

    public IEnumerable<ThetaRadiusPoint> ProcessSamples()
    {
      ThetaRadiusFile file = ThetaRadiusFile.CreateFromFile( Config.FileName );

      foreach ( ThetaRadiusPoint currentPoint in file.Points )
      {
        yield return currentPoint;
      }
    }
  }
}