using System;

namespace SandTableEngine.Processor;

public class ProcessingBuffer
{
}

public class ProcessingBuffer<T> : ProcessingBuffer
{
  public T[] Buffer { get; init; } = Array.Empty<T>();

  public static ProcessingBuffer<T> Empty { get; } = new ProcessingBuffer<T>() { Buffer = Array.Empty<T>()};
}
