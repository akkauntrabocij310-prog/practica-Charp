using System.Windows;

namespace StudentDiaryFull
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StudentDiaryFull.Services.DataService.Initialize();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            StudentDiaryFull.Services.PipeService.Instance.Stop();
            StudentDiaryFull.Services.MmfService.Instance.Dispose();
            base.OnExit(e);
        }
    }
}
