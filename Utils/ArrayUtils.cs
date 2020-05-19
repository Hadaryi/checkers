using System.Collections.Generic;

namespace Utils
{
    public class ArrayUtils
    {
        public static T[] convertListToArray<T>(List<T> i_List)
        {
            T[] arr = new T[i_List.Count];
            int index = 0;

            foreach (T item in i_List)
            {
                arr[index++] = item;
            }

            return arr;
        }
    }
}
