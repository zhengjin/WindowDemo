#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace Stema.Windows
{
 internal static class SafeNativeMethods
 {
  // this is used in NavigationPaneExpander
  public enum WM_NC_MESSAGES
  {

  }

  [SuppressUnmanagedCodeSecurity, SuppressUnmanagedCodeSecurity]
  private static class SafeNativeMethodsPrivate
  {
   [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
   public static extern IntPtr GetCapture();
  }

  [SecurityCritical]
  public static IntPtr GetCapture()
  {
   return SafeNativeMethodsPrivate.GetCapture();
  }
 }
}
