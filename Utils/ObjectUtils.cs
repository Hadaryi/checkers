using System.Collections.Generic;
using System.Reflection;

namespace Utils
{
    public class ObjectUtils
    {
        /**
         * This method receives an object and returns all the properties of the object using a key=>value dictionary.
         */
        public static Dictionary<string, object> GetObjectProperties(object i_Object)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            PropertyInfo[] reflectionProperties = i_Object.GetType().GetProperties();

            foreach (PropertyInfo reflectionProperty in reflectionProperties)
            {
                properties[reflectionProperty.Name] = reflectionProperty.GetValue(i_Object, null);
            }

            return properties;
        }
    }
}
