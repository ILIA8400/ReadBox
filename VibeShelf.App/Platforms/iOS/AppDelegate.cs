using Foundation;
using UIKit;

namespace VibeShelf.App
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UIView statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
            statusBar.BackgroundColor = UIColor.Black;
            UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);

            return base.FinishedLaunching(app, options);
        }
    }
}
