using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    public class ComboCompare : IComparer<Employee>
    {
        public ComboCompare(IComparer<Employee> firstPair, IComparer<Employee> secondPair)
        {
            FirstPair = firstPair;
            SecondPair = secondPair;
        }

        public IComparer<Employee> FirstPair { get; private set; }
        public IComparer<Employee> SecondPair { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            var finalCompareResult = FirstPair.Compare(x, y);
            if (finalCompareResult != 0)
            {
                return finalCompareResult;
            }

            return SecondPair.Compare(x, y);
        }
    }
}