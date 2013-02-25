#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace Stema.Windows
{
	public static class Cursors
	{
		private static Cursor sizeNS;
		public static Cursor SizeNS
		{
			get
			{
				if (sizeNS == null)
				{
					Assembly a = Assembly.GetExecutingAssembly();
					using (Stream s = a.GetManifestResourceStream("NavigationPane.Resources.sizeNS.cur"))
						sizeNS = new Cursor(s);
				}
				return sizeNS;
			}
		}

  private static Cursor sizeWE;
		public static Cursor SizeWE
		{
			get
			{
				if (sizeWE == null)
				{
					Assembly a = Assembly.GetExecutingAssembly();
					using (Stream s = a.GetManifestResourceStream("NavigationPane.Resources.sizeWE.cur"))
						sizeWE = new Cursor(s);
				}
				return sizeWE;
			}
		}
 }
}
