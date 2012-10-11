using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XL.Models;

namespace XL.Client
{
    public static class CacheStrategy
    {
        public static UserModel CurUser
        {
            get;
            set;
        }
    }
}
