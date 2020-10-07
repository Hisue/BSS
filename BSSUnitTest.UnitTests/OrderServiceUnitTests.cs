using System;
using Autofac.Extras.Moq;
using BSSUnitTest.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BSSUnitTest.UnitTests
{
    [TestClass]
    public class OrderServiceUnitTests
    {
        [TestMethod]
        public void GetAvailableIdAsync_OrderFoundInRepository_ShouldReturnIsAvailableFalseAndSuggestCorrectValue()
        {
            //Arrange
            const string testOrderId = "AZ005";
            const string orderStartsWith = "A";
            var expected = new AvailableId
            {
                IsAvailable = false,
                Suggest = "A007"
            };
            var sampleOrder = GetCorrectSampleOrder();
            var sampleOrders = GetCorrectSampleOrders();

            //Act
            var orderService = CreateMockOrderService(sampleOrder, sampleOrders, testOrderId, orderStartsWith);
            var actual = orderService.GetAvailableIdAsync(testOrderId).Result;

            //Assert
            Assert.IsNotNull(actual, "");
            Assert.AreEqual(expected.IsAvailable, actual.IsAvailable, "");
            Assert.AreEqual(expected.Suggest, actual.Suggest, "");
        }

        [TestMethod]
        public void GetAvailableIdAsync_NoOrderFoundWithSimilarId_ShouldThrowInvalidAggregateException()
        {
            //Arrange
            const string badTestOrderId = "AZ005";
            var sampleOrder = GetCorrectSampleOrder();
            var emptySampleOrders = new List<Order>();
            //Act
            try
            {
                var orderService = CreateMockOrderService(sampleOrder, emptySampleOrders, badTestOrderId);
                var unused = orderService.GetAvailableIdAsync(badTestOrderId).Result;
            //Assert
                Assert.IsTrue(false, "Did not catch an exception during execution");
            }
            catch (Exception exception)
            {
                var actualExceptionType = exception.GetType();
                var expectedExceptionType = typeof(AggregateException);

                Assert.AreEqual(expectedExceptionType, actualExceptionType, $"Exception is not an {expectedExceptionType}");
            }
        }

        [TestMethod]
        public void GetAvailableIdAsync_WrongPatternProvided_ShouldThrowInvalidAggregateException()
        {
            //Arrange
            const string orderIdNotMatchingPattern = ",,,,";
            var sampleOrder = GetCorrectSampleOrder();
            var emptySampleOrders = new List<Order>();
            //Act
            try
            {
                var orderService = CreateMockOrderService(sampleOrder, emptySampleOrders, orderIdNotMatchingPattern);
                var unused = orderService.GetAvailableIdAsync(orderIdNotMatchingPattern).Result;
            //Assert
                Assert.IsTrue(false, "Did not catch an exception during execution");
            }
            catch (Exception exception)
            {
                var actualExceptionType = exception.GetType();
                var expectedExceptionType = typeof(AggregateException);

                Assert.AreEqual(expectedExceptionType, actualExceptionType, $"Exception is not an {expectedExceptionType}");
            }
        }

        [TestMethod]
        public void GetAvailableIdAsync_OrderNotFoundInRepository_ShouldReturnIsAvailableTrueAndSuggestNull()
        {
            //Arrange
            const string badTestOrderId = "AZ999";
            const Order sampleOrder = null;
            var sampleOrders = GetCorrectSampleOrders();
            var expected = new AvailableId
            {
                IsAvailable = true
            };
            //Act
            var orderService = CreateMockOrderService(sampleOrder, sampleOrders, badTestOrderId);
            var actual = orderService.GetAvailableIdAsync(badTestOrderId).Result;

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.IsAvailable, actual.IsAvailable);
            Assert.AreEqual(expected.Suggest, actual.Suggest);
        }

        private static OrderService CreateMockOrderService(Order sampleOrderReturnValue, IEnumerable<Order> sampleOrdersReturnValue, string testOrderId, string orderStartsWith = "")
        {
            using var mock = AutoMock.GetLoose();
            mock.Mock<IOrderRepository>().Setup(x => x.GetOrderAsync(testOrderId)).ReturnsAsync(sampleOrderReturnValue);
            mock.Mock<IOrderRepository>().Setup(x => x.FindOrdersWithSimilarIdAsync(orderStartsWith)).ReturnsAsync(sampleOrdersReturnValue.ToList());

            return mock.Create<OrderService>();
        }

        private static Order GetCorrectSampleOrder()
        {
            var sampleOrder = new Order("AZ005");

            return sampleOrder;
        }

        private static IEnumerable<Order> GetCorrectSampleOrders()
        {
            var sampleOrder = new List<Order>
            {
                new Order("AZ005"),
                new Order("AZ006")
            };

            return sampleOrder;
        }
    }
}