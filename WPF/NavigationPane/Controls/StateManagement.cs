#region Copyright and License Information
// NavigationPane Control
// http://NavigationPane.codeplex.com/
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://NavigationPane.codeplex.com/license
#endregion

using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using System.Xml;
using System;
using System.Windows.Media;
using NavigationPane.Properties;
using Stema.Controls.Utils;

namespace Stema.Controls
{
 [XmlType(AnonymousType = true)]
 public class StatesControlItemsItem
 {
  [XmlAttribute]
  public string Identifier;
  [XmlAttribute]
  public bool Excluded;
  [XmlAttribute]
  public int Order;

  public StatesControlItemsItem()
  {
  }
  internal StatesControlItemsItem(string identifier, bool excluded, int order)
  {
   Identifier = identifier;
   Excluded = excluded;
   Order = order;
  }
 }

 [XmlType(AnonymousType = true)]
 public class StatesControlItems
 {
  public StatesControlItems()
  {
   Items = new List<StatesControlItemsItem>();
  }

  [XmlAttribute]
  public int LargeItems;

  [XmlElement("Item", Order = 0)]
  public List<StatesControlItemsItem> Items;

  public StatesControlItemsItem GetItem(string id)
  {
   IEnumerable<StatesControlItemsItem> result = Items.Where(st => st.Identifier == id);
   if (result.Count() > 0)
    return result.First();

   return null;
  }
 }

 [XmlType(AnonymousType = true)]
 public class StatesControl
 {
  [XmlAttribute]
  public string Identifier;
  [XmlAttribute]
  public bool Minimized;
  [XmlAttribute]
  public double Width;
  [XmlAttribute]
  public double ActualWidth;
  [XmlAttribute]
  public double ExpandedWidth;
  [XmlAttribute]
  public double ExpandedDelta;
  [XmlAttribute]
  public Visibility Visibility;

  [XmlElement(Order = 0)]
  public StatesControlItems Items;
 }

 [XmlType(AnonymousType = true)]
 [XmlRoot("States")]
 public class States
 {
  public States()
  {
   controls = new List<StatesControl>();
  }

  private List<StatesControl> controls;
  [XmlElement("Control", Order = 0)]
  public List<StatesControl> Controls 
  {
   get { return controls; }
   set { controls = value; }
  }

  public StatesControl GetControl(string id, bool create = false) 
  {
   IEnumerable<StatesControl> result = Controls.Where(st => st.Identifier == id);
   if (result.Count() > 0)
    return result.First();

   if (create)
   {
    StatesControl control = new StatesControl();
    control.Identifier = id;
    Controls.Add(control);
    return control;
   }
   return null;
  }
 }


 internal static class NavigationPaneStateHelper
 {
  const string stateFile = "NavigationPane.dat";

  private static States GetCurrentStates(Stream stream)
  {
   States states;
   XmlSerializer s = new XmlSerializer(typeof(States));

   if ((stream.Length > 0) && s.CanDeserialize(XmlReader.Create(stream)))
   {
    // not resseting the stream to the beginning will couse a 'Root element not present error"  !!
    // the position is previously changed by s.CanDeserialize
    stream.Position = 0; 
    states = s.Deserialize(stream) as States;
   }
   else
    states = new States();

   return states;
  }

  internal static void ResetState(FrameworkElement control, Stream stream)
  {
   States states = GetCurrentStates(stream);
   StatesControl ctrl = states.GetControl(control.Name);
   if (ctrl != null)
   {
    states.Controls.Remove(ctrl);
    SaveState(control, stream);
   }
  }

  internal static void ResetState(FrameworkElement control, string fileName)
  {
   using (FileStream stream = new FileStream(fileName, FileMode.Open))
    ResetState(control, stream);  
  }

  internal static void ResetState(FrameworkElement control)
  {
   // fix issue proposed by buckbuchanan 
   // http://navigationpane.codeplex.com/workitem/10792
   try
   {
    using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain())
    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(stateFile, FileMode.OpenOrCreate, FileAccess.Read, storage))
     ResetState(control, stream);
   }
   catch { }
  }

  internal static bool LoadState(FrameworkElement control, Stream stream)
  {
   States states = GetCurrentStates(stream);
   string pId = control.Name;
   StatesControl ctrl = states.GetControl(pId);
   if (ctrl != null)
   {
    MinimizeHelper minimizeHelper = ((IMinimizeHelped)control).ResizeHelper;

    control.Visibility = ctrl.Visibility;

    control.Width = ctrl.Width;
    minimizeHelper.IsMinimized = ctrl.Minimized;
    minimizeHelper.ExpandedWidth = ctrl.ExpandedWidth;
    minimizeHelper.ExpandedDelta = ctrl.ExpandedDelta;
    minimizeHelper.ResetMinizedSize();

    if (control is NavigationPane)
    {
     NavigationPane pane = control as NavigationPane;

     pane.LargeItems = ctrl.Items.LargeItems;
       
     IEnumerable<NavigationPaneItem> items = pane.Items.OfType<NavigationPaneItem>();
     // if Linq were a girl, I should absolutely marry her
     var sorted = 
      from data in
      (
       from item in items
       join sItem in ctrl.Items.Items on item.Name equals sItem.Identifier into itemStates
       from itemState in itemStates.DefaultIfEmpty()
       select new { Index = itemState == null ? NavigationPanePanel.GetAbosluteIndex(item) : itemState.Order - 0.5, Exluded = itemState == null ? false : itemState.Excluded, Item = item }
      )
      orderby data.Index
      select data;

     List<NavigationPaneItem> orderdItems = new List<NavigationPaneItem>();
     foreach (var item in sorted)
     {
      NavigationPane.SetIsItemExcluded(item.Item, item.Exluded);
      orderdItems.Add(item.Item);
     }
     pane.Items.Clear();
     foreach (NavigationPaneItem item in orderdItems)
      pane.Items.Add(item);
    }
    return true;
   }
   return false;
  }

  internal static bool LoadState(FrameworkElement control, string fileName)
  {
   using(FileStream stream = new FileStream(fileName, FileMode.Open))
    return LoadState(control, stream);
  }

  internal static bool LoadState(FrameworkElement control)
  {
   // fix issue proposed by buckbuchanan 
   // http://navigationpane.codeplex.com/workitem/10792
   try
   {
    using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain())
    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(stateFile, FileMode.Open, FileAccess.Read, storage))
     return LoadState(control, stream);
   }
   catch {
    return false;
   }
  }

  public static void SaveState(FrameworkElement control, Stream stream)
  {
   if (string.IsNullOrEmpty(control.Name))
    throw new UniqueIdentierNotSet();

   States states = GetCurrentStates(stream);

   XmlSerializer s = new XmlSerializer(typeof(States));

   string pId = control.Name;
   StatesControl ctrl = states.GetControl(pId, true);

   //we should use the MinizeHelper to get the correct size if the control io minimized
   MinimizeHelper minimizeHelper = ((IMinimizeHelped)control).ResizeHelper;
   ctrl.ExpandedWidth = minimizeHelper.ExpandedWidth;
   ctrl.ExpandedDelta = minimizeHelper.ExpandedDelta;

   ctrl.ActualWidth = control.ActualWidth;
   ctrl.Width = control.Width;
   ctrl.Minimized = minimizeHelper.IsMinimized;

   ctrl.Visibility = control.Visibility;

   if (control is NavigationPane)
   {
    NavigationPane pane = control as NavigationPane;

    if (ctrl.Items == null)
     ctrl.Items = new StatesControlItems();

    ctrl.Items.LargeItems = pane.LargeItems;
    ctrl.Items.Items.Clear();
    for (int j = 0; j < pane.Items.Count; j++)
    {
     NavigationPaneItem item = (NavigationPaneItem)pane.Items[j];
     if (!string.IsNullOrEmpty(item.Name))
      ctrl.Items.Items.Add(new StatesControlItemsItem(item.Name, NavigationPane.GetIsItemExcluded(item), j));
    }
   }
   s.Serialize(stream, states);
  }

  public static void SaveState(FrameworkElement control, string fileName)
  {
   using (FileStream stream = new FileStream(fileName, FileMode.Create))
    SaveState(control, stream);
  }

  public static void SaveState(FrameworkElement control)
  {
   using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain())
   using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(stateFile, FileMode.Create, FileAccess.Write, storage))
    SaveState(control, stream);
  }

  internal static void OnAppExit(FrameworkElement control)
  {
   SaveState(control);
  }
 }

 internal class UniqueIdentierNotSet: Exception
 {
  internal UniqueIdentierNotSet(): base(NPR.Auto_State_No_Uid)
  {
  }
 }
}
