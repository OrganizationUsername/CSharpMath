namespace CSharpMath.Xaml.Tests.NuGet {
  using Avalonia;
  using SkiaSharp;
  using Forms;
  public class Program {
    static string File(string platform, [System.Runtime.CompilerServices.CallerFilePath] string thisDir = "") =>
      System.IO.Path.Combine(thisDir, "..", $"Test.{platform}.png");
    [Xunit.Fact]
    public void TestImage() {
      global::Avalonia.Skia.SkiaPlatform.Initialize();
      Xamarin.Forms.Device.PlatformServices = new Xamarin.Forms.Core.UnitTests.MockPlatformServices();

      using (var forms = System.IO.File.OpenWrite(File(nameof(Forms))))
        new Forms.MathView { LaTeX = "1", FontSize = 50 }.Painter.DrawAsStream()?.CopyTo(forms);
      using (var avalonia = System.IO.File.OpenWrite(File(nameof(Avalonia))))
        new Avalonia.MathView { LaTeX = "1", FontSize = 50 }.Painter.DrawAsPng(avalonia);

      using (var forms = System.IO.File.OpenRead(File(nameof(Forms))))
        Xunit.Assert.Equal(292, forms.Length);
      using (var avalonia = System.IO.File.OpenRead(File(nameof(Avalonia))))
        Xunit.Assert.Equal(292, avalonia.Length);
    }
  }
}
