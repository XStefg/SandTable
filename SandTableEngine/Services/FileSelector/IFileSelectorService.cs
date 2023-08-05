using System;

namespace SandTableEngine.Services;

public interface IFileSelectorService
{
  string CurrentFile { get; }

  event EventHandler<NewValueEventArgs<string>> CurrentFileChanged;

  bool SelectNextFile();
}