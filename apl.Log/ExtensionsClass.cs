#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;

namespace ExtensionMethods
{
    using System.Reflection;

    public static class ExtensionsClass
    {
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : value.ToString()/* null*/;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string str)
        {
            StringValue = str;
        }

        public string StringValue { get; set; }
    }
}
