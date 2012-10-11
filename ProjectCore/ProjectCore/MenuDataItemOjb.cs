using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectCore
{
    public class MenuDataItem
    {
        private string _text;
        private string _url;
        private Guid _id;
        private Guid? _parentId;
        private string _IsSeparator;

        public MenuDataItem(Guid id, Guid? parentId, string text, string url, string isSeparator)
        {
            _id = id;
            _parentId = parentId;
            _text = text;
            _url = url;
            _IsSeparator = isSeparator;
        }

        public string IsSeparator
        {
            get
            {
                return _IsSeparator;
            }
            set
            {
                _IsSeparator = IsSeparator;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public Guid? ParentID
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
            }
        }
    }
}
