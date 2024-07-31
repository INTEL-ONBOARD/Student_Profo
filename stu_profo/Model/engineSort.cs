using System;
using System.Collections.Generic;

namespace stu_profo.Model
{
    public class engineSort
    {
        public static List<dataModel> BubbleSortAscending(List<dataModel> list, string propertyName)
        {
            return SortList(new List<dataModel>(list), propertyName, true);
        }

        public static List<dataModel> BubbleSortDescending(List<dataModel> list, string propertyName)
        {
            return SortList(new List<dataModel>(list), propertyName, false);
        }

        private static List<dataModel> SortList(List<dataModel> list, string propertyName, bool ascending)
        {
            var propertyInfo = typeof(dataModel).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException("Property not found", nameof(propertyName));
            }

            int count = list.Count;

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    var value1 = propertyInfo.GetValue(list[j]);
                    var value2 = propertyInfo.GetValue(list[j + 1]);

                    bool condition;
                    if (value1 is IComparable && value2 is IComparable)
                    {
                        condition = ascending
                            ? ((IComparable)value1).CompareTo(value2) > 0
                            : ((IComparable)value1).CompareTo(value2) < 0;
                    }
                    else
                    {
                        throw new ArgumentException("Property values must implement IComparable");
                    }

                    if (condition)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }

            return list;
        }
    }
}