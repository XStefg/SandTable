using System;
using System.Linq;

namespace SandTableEngine.Processor;

public abstract class ProcessingBuffer
{
  public abstract int BufferLength { get; }
}

public class ProcessingBuffer<T> : ProcessingBuffer
{
  public ProcessingBuffer<T> Append( T[] input ) => new() { Buffer = Buffer.Concat( input ).ToArray() };

  public T[] Buffer { get; init; } = Array.Empty<T>();

  public static ProcessingBuffer<T> Empty { get; } = new() { Buffer = Array.Empty<T>() };

  public override int BufferLength => Buffer.Length;
}