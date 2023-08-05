using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SandTableEngine.Services;

namespace SandTableConsole
{
  internal class Program
  {
    static async Task Main( string[] args )
    {
      IHost host = Host.CreateDefaultBuilder()
                       .ConfigureServices( ( context, services ) =>
                                           {
                                             services.AddSingleton<IFileSelectorService, FileSelectorService>();
                                             services.AddSingleton<IFileLoaderService, FileLoaderService>();

                                             services.AddOptions<FileSelectorServiceConfiguration>()
                                                     .Configure<IConfiguration>( ( settings, config ) =>
                                                                                   settings.ThetaRadiusSourceDirectory = @".\SampleThrFiles" );
                                           } )
                       .Build();

      IFileLoaderService   fileLoaderService   = host.Services.GetRequiredService<IFileLoaderService>();
      IFileSelectorService fileSelectorService = host.Services.GetRequiredService<IFileSelectorService>();

      fileSelectorService.SelectNextFile();

      await host.RunAsync();
    }
  }
}