using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public static class MyLinqExtensions
    {
        public static IEnumerable<Employee> AnnieOrderBy
            (this IEnumerable<Employee> employees, ComboCompare comboCompare)
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
                    var finalCompareResult = comboCompare.Compare(employee, minElement);

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
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        [Test]
        public void orderBy_lastName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };

           
            var lastNamePair = new KeyComparePair<string>(employee => employee.LastName, Comparer<string>.Default);
            var firstNamePair = new KeyComparePair<string>(employee => employee.FirstName, Comparer<string>.Default);
            var agePair = new KeyComparePair<int>(employee => employee.Age, Comparer<int>.Default);

            var comparer = new ComboCompare(lastNamePair, firstNamePair);
            var finalComparer = new ComboCompare(comparer, agePair);

            var actual = employees.AnnieOrderBy(finalComparer);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        //class + change to static and change to extensions
    }
}