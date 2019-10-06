using System.Collections;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
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


            //var lastNamePair = new KeyComparePair<string>(employee => employee.LastName, Comparer<string>.Default);
            //var firstNamePair = new KeyComparePair<string>(employee => employee.FirstName, Comparer<string>.Default);
            //var agePair = new KeyComparePair<int>(employee => employee.Age, Comparer<int>.Default);

            //var comparer = new ComboCompare(lastNamePair, firstNamePair);
            //var finalComparer = new ComboCompare(comparer, agePair);

            //var actual = employees.AnnieThenBy(finalComparer);
            var actual = employees.AnnieOrderBy(e => e.LastName)
                        .AnnieThenBy(e => e.FirstName)
                        .AnnieThenBy(e => e.Age);

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