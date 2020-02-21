using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WWOF.Application.Data.Repositories;
using WWOF.Application.Models;

namespace WWOF.Application.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void Insert_OkUser_ReturnOne()
        {
            // Arrange
            UserRepository userRepository = new UserRepository();

            var user = new User {
                UserName = "edufabian",
                Password = "fabian1995",
                Name = "Fabian",
                Surname = "Guanco",
                Email = "fabian@gmail.com"
            };

            // Act
            var result = userRepository.Insert(user);

            // Assert
            Assert.AreEqual(-1, result);
        }
    }
}
