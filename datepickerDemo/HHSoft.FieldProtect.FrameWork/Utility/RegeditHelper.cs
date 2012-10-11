using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class RegeditHelper
    {
        public bool KeyExists(RegistryKey parent, string keyName)
        {
            foreach (var item in parent.GetSubKeyNames())
            {
                if (item == keyName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValueExists(RegistryKey parent, string valueName)
        {
            foreach (var item in parent.GetValueNames())
            {
                if (item == valueName)
                {
                    return true;
                }
            }
            return false;
        }

        public T GetValue<T>(RegistryKey key, string valueName)
        {
            return TypeConvert.Convert<T>(key.GetValue(valueName));
        }

        public void SetValue(RegistryKey key, string valueName, object value)
        {
            if (value != null)
            {
                key.SetValue(valueName, value);
            }
        }
    }
}
