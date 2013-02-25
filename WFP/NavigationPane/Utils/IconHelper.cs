using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Stema.Controls.Utils
{
 //todo:  considering removing iterator definivelly
 /*
 internal static class IconHelper
 {
  internal static object GetIcon(object value, int size)
  {
   BitmapFrame bs = value as BitmapFrame;
   if (bs != null)
   {
    IconBitmapDecoder decoder = bs.Decoder as IconBitmapDecoder;
    if (decoder != null)
    {
     IEnumerable<BitmapFrame> result = decoder.Frames.Where(bf => bf.Width == size);
     if (result.Count() > 0)
      return result.First();
    }
   }
   return null;
  }
 }
  */
}
