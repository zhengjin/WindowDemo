using System;
using System.IO;
using System.Windows.Markup;
using System.Xml;
using System.Windows;
using Stema.Controls;

namespace Stema.Utils
{
 internal static class XamlHelper
 {
  internal static Object CloneUsingXaml(Object o, bool TryGetNameIfNoneWorks = false)
  {
   if (o == null)
    return null;

   if (o is DependencyObject)
   {
    string xaml = XamlWriter.Save(o);
    return XamlReader.Load(new XmlTextReader(new StringReader(xaml)));
   }

   if (TryGetNameIfNoneWorks)
   {
    XmlElement e = o as XmlElement;
    if (e != null && !string.IsNullOrEmpty(e.Name))
    {
     if(e.HasAttribute("Name"))
      return e.Attributes["Name"];
     if (e.HasAttribute("name"))
      return e.Attributes["name"];
     if (e.HasAttribute("header"))
      return e.Attributes["header"];
     if (e.HasAttribute("Header"))
      return e.Attributes["Header"];
    }
   }

   return o;
  }
 }
}
