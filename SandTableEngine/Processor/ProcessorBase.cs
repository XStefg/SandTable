using System;

namespace SandTableEngine.Processor;

public abstract class ProcessorBase<TInput, TOutput, TConfig> : IProcessor
  where TConfig : ProcessingConfigBase
{
  protected ProcessorBase( TConfig config )
  {
    Config = config;
  }

  #region IProcessor implementation

  bool IProcessor.Process( ProcessingBuffer input )
  {
    if ( input is not ProcessingBuffer<TInput> )
    {
      throw new ArgumentException( $"Expected {nameof(ProcessingBuffer<TInput>)} but got {input.GetType().Name}" );
    }

    return Process( (ProcessingBuffer<TInput>)input );
  }

  ProcessingBuffer IProcessor.GetOutput()
  {
    return GetOutput();
  }

  #endregion

  #region Abstract Methods

  public abstract bool Process( ProcessingBuffer<TInput> input );

  public abstract ProcessingBuffer<TOutput> GetOutput();

  #endregion

  #region Protected Properties

  protected TConfig Config { get; }

  #endregion
}