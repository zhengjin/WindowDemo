#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System;
using System.Windows.Markup;

namespace Stema.Globalization
{
 [MarkupExtensionReturnType(typeof(string))]
 public class LocalizedString : MarkupExtension
 {
  string _key;
  public LocalizedString(string key)
  {
   _key = key;
  }

  const string NotFoundError = "#LocalizedResourceNotFound#";

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
   if (string.IsNullOrEmpty(_key))
    return NotFoundError;

   return NavigationPane.Properties.NPR.ResourceManager.GetString(_key) ?? NotFoundError;
  }
 }
}
