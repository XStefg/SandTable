namespace SandTableEngine.Processor;

public interface IProcessInput<T>
{
  IProcessOutput<T>? Input { get; set; }
}