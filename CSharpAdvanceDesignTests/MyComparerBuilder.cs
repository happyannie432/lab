using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    public class MyComparerBuilder : IEnumerable<Employee>
    {
        private IComparer<Employee> _CurrentComparer;
        private readonly IEnumerable<Employee> _Source;

        public MyComparerBuilder(IEnumerable<Employee> source, IComparer<Employee> currentComparer)
        {
            _Source = source;
            _CurrentComparer = currentComparer;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return Sort(_Source, _CurrentComparer);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static IEnumerator<Employee> Sort(IEnumerable<Employee> employees, IComparer<Employee> comparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];
                    var finalCompareResult = comparer.Compare(employee, minElement);

                    if (finalCompareResult < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        public MyComparerBuilder AppendComparer(IComparer<Employee> nextComparer)
        {
            _CurrentComparer = new ComboCompare(_CurrentComparer, nextComparer);
            return this;
        }
    }
}