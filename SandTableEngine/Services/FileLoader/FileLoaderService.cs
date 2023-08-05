using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandTableEngine.File;

namespace SandTableEngine.Services
{
  public class FileLoaderService : BaseService, IFileLoaderService
  {
    private readonly IFileSelectorService m_FileSelectorService;

    public FileLoaderService( IFileSelectorService fileSelectorService )
    {
      m_FileSelectorService = fileSelectorService;

      m_FileSelectorService.CurrentFileChanged += FileSelectorService_CurrentFileChanged;
    }

    protected override void Dispose( bool disposing )
    {
      if ( disposing )
      {
        m_FileSelectorService.CurrentFileChanged -= FileSelectorService_CurrentFileChanged;
      }

      base.Dispose( disposing );
    }

    private void FileSelectorService_CurrentFileChanged( object? sender, NewValueEventArgs<string> e )
    {
      Task.Run( LoadFile );
    }

    private void LoadFile()
    {
      ThetaRadiusFile currentFile = ThetaRadiusFile.CreateFromFile( m_FileSelectorService.CurrentFile );
    }
  }

  public interface IFileLoaderService
  {
  }
}