using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Collections;

namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class XDocumentOperation
    {
        public List<T> ConvertFromElementsToEntities<T>(IEnumerable<XElement> elements) where T : new()
        {
            List<T> result = new List<T>();
            foreach (var element in elements)
            {
                T t = new T();
                foreach (var attribute in element.Attributes())
                {
                    string propertyName = attribute.Name.LocalName;
                    string propertyValue = attribute.Value;
                    PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(t, TypeConvert.Convert(propertyValue, propertyInfo.PropertyType), null);
                    }
                }
                result.Add(t);
            }
            return result;
        }

        public IEnumerable<XElement> ConvertFromEntitiesToElements(string elementName, IEnumerable entities)
        {
            List<XElement> result = new List<XElement>();
            foreach (var entity in entities)
            {
                XElement element = new XElement("elementName");
                foreach (var propertyInfo in entity.GetType().GetProperties())
                {
                    if (propertyInfo != null)
                    {
                        string propertyName = propertyInfo.Name;
                        string propertyValue = propertyInfo.GetValue(entity, null).ToString();
                        element.SetAttributeValue(propertyName, propertyValue);
                    }
                }
                result.Add(element);
            }
            return result;
        }

        public T GetAttributeValue<T>(XElement element, string attributeName)
        {
            XAttribute attribute = element.Attribute(attributeName);
            string value;
            if (attribute == null)
            {
                value = null;
            }
            else
            {
                value = attribute.Value;
            }
            return TypeConvert.Convert<T>(value);
        }

        public T GetElementValue<T>(XElement parentElement, string elementName)
        {
            XElement element = parentElement.Element(elementName);
            string value;
            if (element == null)
            {
                value = null;
            }
            else
            {
                value = element.Value;
            }
            return TypeConvert.Convert<T>(value);
        }

        public void SetElementValue<T>(XElement parentElement, string elementName, T value)
        {
            XElement element = parentElement.Element(elementName);
            if (element != null)
            {
                element.Value = value.ToString();
            }
        }
    }
}
