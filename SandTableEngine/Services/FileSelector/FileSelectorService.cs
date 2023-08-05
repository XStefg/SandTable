using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SandTableEngine.Services
{
  public class FileSelectorService : BaseService, IFileSelectorService
  {
    private readonly IOptions<FileSelectorServiceConfiguration> m_Configuration;

    public FileSelectorService( ILogger<FileSelectorService> logger,
                                IOptions<FileSelectorServiceConfiguration> configuration )
    {
      m_Configuration = configuration;

      m_AvailableFiles = Directory.GetFiles( m_Configuration.Value.ThetaRadiusSourceDirectory, "*.thr" ).ToList();
    }

    #region IFileSelectorService Implemenation

    public string CurrentFile
    {
      get => m_CurrentFile;
      set => SetProperty( ref m_CurrentFile, value, CurrentFileChanged );
    }

    public event EventHandler<NewValueEventArgs<string>>? CurrentFileChanged;

    public bool SelectNextFile()
    {
      if ( m_SelectedFileIndex + 1 < m_AvailableFiles.Count )
      {
        m_SelectedFileIndex++;
        CurrentFile = m_AvailableFiles[m_SelectedFileIndex];
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion

    #region Private Variables

    private string       m_CurrentFile = string.Empty;
    private List<string> m_AvailableFiles;

    private int m_SelectedFileIndex = -1;

    #endregion
  }
}