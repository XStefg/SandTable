using System;
using System.Collections.Generic;

namespace SandTableEngine.Processor;

public abstract class ProcessorBase<TConfig>
  where TConfig : ProcessingConfigBase
{
  protected ProcessorBase( TConfig config )
  {
    Config = config;
  }

  #region Protected Properties

  protected TConfig Config { get; }

  #endregion
}