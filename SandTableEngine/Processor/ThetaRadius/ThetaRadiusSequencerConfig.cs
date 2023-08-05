using SandTableEngine.Units;

namespace SandTableEngine.Processor.ThetaRadius;

public class ThetaRadiusSequencerConfig : ProcessingConfigBase
{
  public Distance MinimumDistance { get; init; }
}