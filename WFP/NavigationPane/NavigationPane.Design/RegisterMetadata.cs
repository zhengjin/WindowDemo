using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Windows.Design;
using Microsoft.Windows.Design.Features;
using Microsoft.Windows.Design.Metadata;

using Stema.Controls;
using System.ComponentModel;
using Microsoft.Windows.Design.Model;
using System.Windows.Controls;
using System.Windows.Media;

// The ProvideMetadata assembly-level attribute indicates to designers
// that this assembly contains a class that provides an attribute table. 
[assembly: ProvideMetadata(typeof(Stema.Controls.Design.RegisterMetadata))]
namespace Stema.Controls.Design
{
	internal class RegisterMetadata : IProvideAttributeTable
	{
		public void Register()
		{
		}

		// Called by the designer to register any design-time metadata. 
		public AttributeTable AttributeTable
		{
			get
			{
				AttributeTableBuilder builder = new AttributeTableBuilder();

				builder.AddCustomAttributes(typeof(NavigationPanePanel), ToolboxBrowsableAttribute.No);
				builder.AddCustomAttributes(typeof(NavigationPaneSplitter), ToolboxBrowsableAttribute.No);

    // typeof(NavigationPaneExpander)
    builder.AddCustomAttributes(typeof(NavigationPaneExpander), "ExpanderName", new CategoryAttribute("Common Properties"));
				builder.AddCustomAttributes(typeof(NavigationPaneExpander), "IsExpanded", new CategoryAttribute("Common Properties"));
				builder.AddCustomAttributes(typeof(NavigationPaneExpander), new FeatureAttribute(typeof(NavgationPaneExpanderDefaults)));

    // typeof(NavigationPaneItem)
    builder.AddCustomAttributes(typeof(NavigationPaneItem), new FeatureAttribute(typeof(NavigationPaneItemDefaults)));
    builder.AddCustomAttributes(typeof(NavigationPane), "SubItems", new CategoryAttribute("Common Properties"));

    // typeof(NavigationPane)
    builder.AddCustomAttributes(typeof(NavigationPane), "LargeItems", new CategoryAttribute("Common Properties"));
				builder.AddCustomAttributes(typeof(NavigationPane), new FeatureAttribute(typeof(NavigationPaneDefaults)));

				return builder.CreateTable();
			}
		}
	}

	internal class NavgationPaneExpanderDefaults : DefaultInitializer
	{
		public override void InitializeDefaults(ModelItem item)
		{
			Grid grid = new Grid();
   //grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE5E5E5"));
			item.Content.SetValue(grid);
		}
	}

	internal class NavigationPaneItemDefaults : DefaultInitializer
	{
  public override void InitializeDefaults(ModelItem item, EditingContext context)
  {

   base.InitializeDefaults(item, context);
  }

		public override void InitializeDefaults(ModelItem item)
		{
			using (ModelEditingScope batchedChange = item.BeginEdit("Creates Default Items"))
			{
				// vs editor defaults some properties values in the control
				// we must clear those for the NavigationPaneDefaults to work correctly
				// and because WE DON'T NEED THOSE property values and the template 
				// already put correct values to have a correct diplay of items inside te control 
				item.Properties["Name"].ClearValue();
				item.Properties["Height"].ClearValue();
				item.Properties["HorizontalAlignment"].ClearValue();
				item.Properties["VerticalAlignment"].ClearValue();
				item.Properties["Width"].ClearValue();

				item.Properties["Header"].SetValue("NavigationPaneItem");
				Grid grid = new Grid();
				grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE5E5E5"));
				item.Content.SetValue(grid);

				batchedChange.Complete();
			}
		}
	}

	internal class NavigationPaneDefaults : DefaultInitializer
	{
		public override void InitializeDefaults(ModelItem item)
		{
			using (ModelEditingScope batchedChange = item.BeginEdit("Creates Default Items"))
			{
				for (int j = 0; j < 2; j++)
				{
					ModelItem i = ModelFactory.CreateItem(item.Context, typeof(NavigationPaneItem), CreateOptions.InitializeDefaults, null);
					item.Properties["Items"].Collection.Add(i);
				}
				batchedChange.Complete();
			}
		}
	}
}
