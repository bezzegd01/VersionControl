﻿using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;
using UnitTestExample.Services;

namespace UnitTestExample.Test
{
    class AccountControllerTestFixture
    {
        [Test,
            TestCase("abdc1234",false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.unicorvinsu.hu", false),
            TestCase("irf@asdbd.hu", true)
            
            
       ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();
            //Act
            var actualResult = accountController.ValidateEmail(email);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test,
            TestCase("Abc1",false),
            TestCase("abcdefgh",false),
            TestCase("ABCDABDC", false),
            TestCase("Abc1",false),
            TestCase("AbcdAbcd1", true)]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();
            //Act
            var actualResult = accountController.ValidatePassword(password);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test,
            TestCase("irf@uni-corvinus.hu","Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234")
            ]
        public void TestRegisterHappyPath(string email, string password)
        {
            //Arrange
            var accountController = new AccountController();
            //Act
            var actualResult = accountController.Register(email,password);

            //Assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }


        [Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234")]
        public void TestRegisterValidateException(string email, string password)
        {
              //Arrange
            var accountController = new AccountController();
            //Act
            try
            {
                var actualResult = accountController.Register(email, password);
                
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
                
            }
            //Assert
        }
    }
}
