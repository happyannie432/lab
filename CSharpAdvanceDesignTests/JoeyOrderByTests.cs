using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class KeyComparePair
    {
        public KeyComparePair(Func<Employee, string> selector, IComparer<string> comparer)
        {
            Selector = selector;
            Comparer = comparer;
        }

        public Func<Employee, string> Selector { get; private set; }
        public IComparer<string> Comparer { get; private set; }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        [Test]
        public void orderBy_lastName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };
            //transform 
            var actual = JoeyOrderByLastNameAndFirstName(
                employees, 
                new KeyComparePair(employee => employee.LastName, Comparer<string>.Default),
                new KeyComparePair(employee => employee.FirstName, Comparer<string>.Default));

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }


        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName
            (IEnumerable<Employee> employees, KeyComparePair firstPair, KeyComparePair secondPair)
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
                    if (Compare(firstPair, employee, minElement) < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                    else if (firstPair.Comparer.Compare(firstPair.Selector(employee), firstPair.Selector(minElement)) == 0)
                    {
                        if (secondPair.Comparer.Compare(secondPair.Selector(employee), secondPair.Selector(minElement)) < 0)
                        {
                            minElement = employee;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        //extract method and make method non-static chose KeycomparePair
        private static int Compare(KeyComparePair firstPair, Employee employee, Employee minElement)
        {
            return firstPair.Comparer.Compare(firstPair.Selector(employee), firstPair.Selector(minElement));
        }
    }
}