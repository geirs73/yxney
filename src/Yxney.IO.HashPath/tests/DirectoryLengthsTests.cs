using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace tests
{
    public class DirectoryLengthsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCreateAndImplicitConversionToString()
        {
            Assert.Multiple(() =>
            {
                Assert.That<string>(DirectoryLengths.Create(), Is.EqualTo("3"));
                Assert.That<string>(DirectoryLengths.Create(3), Is.EqualTo("3"));
                Assert.That<string>(DirectoryLengths.Create(3, 5), Is.EqualTo("3,5"));
                Assert.That<string>(DirectoryLengths.Create(3, 5, 7), Is.EqualTo("3,5,7"));
                Assert.That<string>(DirectoryLengths.Create(3, 5, 7, 9), Is.EqualTo("3,5,7,9"));
                Assert.That<string>(DirectoryLengths.Create(3, 5, 7, 9, 11), Is.EqualTo("3,5,7,9,11"));
                Assert.That<string>(DirectoryLengths.Create(3, 5, 7, 9, 11, 13), Is.EqualTo("3,5,7,9,11,13"));
            });
        }

        [Test]
        public void TestExplicitToString()
        {
#pragma warning disable CS8604
            Assert.That<string>(DirectoryLengths.Create(3).ToString(), Is.EqualTo("3"));
#pragma warning restore CS8604
        }

        [Test]
        public void TestEnumeration()
        {
            DirectoryLengths testData = DirectoryLengths.Create(3, 5, 1, 3);
            Assert.That(testData.TotalLength(), Is.EqualTo(12));
            int sum = 0;
            Assert.DoesNotThrow(
                () =>
                {
                    foreach (int level in testData)
                        sum += level;
                }
            );
            Assert.DoesNotThrow(() => ((IEnumerable)testData).GetEnumerator());
        }
    }
}