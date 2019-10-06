using System;
using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{

    public static class MyLinqExtensions
    {
        public static MyComparerBuilder AnnieOrderBy<TKey>(this IEnumerable<Employee> employees,
            Func<Employee, TKey> keySelector)
        {
            var comparer = new KeyComparePair<TKey>(keySelector, Comparer<TKey>.Default);
            return new MyComparerBuilder(employees, comparer);
        }

        public static IEnumerable<Employee> AnnieOrderByComboComparer(this IEnumerable<Employee> employees,
            IComparer<Employee> comparer)
        {
            return new MyComparerBuilder(employees, comparer);
            //return MyComparerBuilder.Sort(employees, comparer);
        }

        public static MyComparerBuilder AnnieThenBy<TKey>(this MyComparerBuilder myComparerBuilder,
            Func<Employee, TKey> keySelector)
        {
            var comparer = new KeyComparePair<TKey>(keySelector, Comparer<TKey>.Default);
            return myComparerBuilder.AppendComparer(comparer);
        }
    }

}