using System.Collections.Generic;

namespace SandTableEngine.Processor;

public static class ProcessExtensions
{
  public static IProcessOutput<T> Wrap<T>( this IEnumerable<T> input )
  {
    return new ProcessOutputWrapper<T>() { Samples = input };
  }

  public class ProcessOutputWrapper<T> : IProcessOutput<T>
  {
    public          IEnumerable<T> ProcessSamples() => Samples;
    public required IEnumerable<T> Samples          { get; init; }
  }
}