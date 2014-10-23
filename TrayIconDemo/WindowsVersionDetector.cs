using System;

namespace BrokenEvent.Shared
{
  static class WindowsVersionDetector
  {
    /// <summary>
    /// Determines if the application is running on Vista
    /// </summary>
    public static bool RunningOnVista
    {
      get
      {
        return Environment.OSVersion.Version.Major >= 6;
      }
    }

    /// <summary>
    /// Determines if the application is running on Windows 7
    /// </summary>
    public static bool RunningOnWin7
    {
      get
      {
        // Verifies that OS version is 6.1 or greater, and the Platform is WinNT.
        return Environment.OSVersion.Platform == PlatformID.Win32NT &&
            Environment.OSVersion.Version.CompareTo(new Version(6, 1)) >= 0;
      }
    }
  }
}
