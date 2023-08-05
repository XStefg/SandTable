using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandTableEngine.Processor
{

  public interface IProcessor
  {
    bool Process( ProcessingBuffer input );

    ProcessingBuffer GetOutput();
  }
}