using System.Collections.Generic;

namespace SandTableEngine.Processor;

public interface IProcessOutput<T>
{
  IEnumerable<T> ProcessSamples();
}