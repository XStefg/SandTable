using SandTableEngine.Units;

namespace SandTableEngine.Processor;

public class ThetaRadiusSequencerConfig : ProcessingConfigBase
{
  public Distance MinimumDistance { get; init; }
}