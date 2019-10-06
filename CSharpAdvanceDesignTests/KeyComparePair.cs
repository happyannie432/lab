using System;
using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    public class KeyComparePair<TKey> : IComparer<Employee>
    {
        public KeyComparePair(Func<Employee, TKey> selector, IComparer<TKey> comparer)
        {
            Selector = selector;
            Comparer = comparer;
        }

        private Func<Employee, TKey> Selector { get; set; }
        private IComparer<TKey> Comparer { get; set; }

        public int Compare(Employee x, Employee y)
        {
            return Comparer.Compare(Selector(x), Selector(y));
        }
    }
}