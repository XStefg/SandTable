using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Eddyfi.Core.Win32PInvoke;

namespace SandTableEngine.Processor
{

  public interface IProcessor
  {
    bool Process( ProcessingBuffer input );

    ProcessingBuffer GetOutput();
  }
}