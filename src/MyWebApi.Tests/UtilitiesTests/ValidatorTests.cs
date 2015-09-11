﻿// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Tests.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Utilities;

    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNullReferenceShouldThrowArgumentNullExceptionWithNullObject()
        {
            Validator.CheckForNullReference(null);
        }

        [Test]
        public void CheckForNullReferenceShouldNotThrowExceptionWithNotNullObject()
        {
            Validator.CheckForNullReference(new object());
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithNullString()
        {
            Validator.CheckForNotWhiteSpaceString(null);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithEmptyString()
        {
            Validator.CheckForNotWhiteSpaceString(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithWhiteSpace()
        {
            Validator.CheckForNotWhiteSpaceString("      ");
        }

        [Test]
        public void CheckForNotEmptyStringShouldNotThrowExceptionWithNormalString()
        {
            Validator.CheckForNotWhiteSpaceString(new string('a', 10));
        }

        [Test]
        public void CheckForExceptionShouldNotThrowIfExceptionIsNull()
        {
            Validator.CheckForException(null);
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "NullReferenceException was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowIfExceptionIsNotNullWithEmptyMessage()
        {
            Validator.CheckForException(new NullReferenceException(string.Empty));
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "NullReferenceException with 'Test' message was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowIfExceptionIsNotNullWithMessage()
        {
            Validator.CheckForException(new NullReferenceException("Test"));
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "AggregateException (containing NullReferenceException with 'Null test' message, InvalidCastException with 'Cast test' message, InvalidOperationException with 'Operation test' message) was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowWithProperMessageIfExceptionIsAggregateException()
        {
            var aggregateException = new AggregateException(new List<Exception>
                    {
                        new NullReferenceException("Null test"),
                        new InvalidCastException("Cast test"), 
                        new InvalidOperationException("Operation test")
                    });

            Validator.CheckForException(aggregateException);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForClass()
        {
            object obj = TestObjectFactory.GetNullRequestModel();
            var result = Validator.CheckForDefaultValue(obj);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForStruct()
        {
            var result = Validator.CheckForDefaultValue(0);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForNullableType()
        {
            var result = Validator.CheckForDefaultValue<int?>(null);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnFalseIfValueIsNotDefaultForClass()
        {
            object obj = TestObjectFactory.GetValidRequestModel();
            var result = Validator.CheckForDefaultValue(obj);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnFalseIfValueIsNotDefaultForStruct()
        {
            var result = Validator.CheckForDefaultValue(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIfTypeCanBeNullShouldNotThrowExceptionWithClass()
        {
            Validator.CheckIfTypeCanBeNull(typeof(object));
        }

        [Test]
        public void CheckIfTypeCanBeNullShouldNotThrowExceptionWithNullableType()
        {
            Validator.CheckIfTypeCanBeNull(typeof(int?));
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "Int32 cannot be null.")]
        public void CheckIfTypeCanBeNullShouldThrowExceptionWithStruct()
        {
            Validator.CheckIfTypeCanBeNull(typeof(int));
        }
    }
}