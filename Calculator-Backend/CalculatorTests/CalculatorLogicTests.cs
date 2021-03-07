using CalculatorServer.Logic;
using CalculatorServer.Models;
using Xunit;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorTests
{
    public class CalculatorLogicTests
    {
        const string filePath = "C:\\Users\\shayi\\Desktop\\tests.txt";

        [Fact]
        public void Calculate()
        {
            //Arrange
            var expect = new CalcResponse();
            expect.Result = "24";
            //Act
            var calcString = "5+3x9-8";
            var actual = CalculatorLogic.Calculate(calcString, filePath);
            //Assert
            Assert.Equal(expect.Result, actual.Result);
        }

        [Fact]
        public void GetAllResults()
        {
            //Arrange
            var mockFileSystem = new MockFileSystem();
            var mockInputFile = new MockFileData("5+sd/as*xc");
            mockFileSystem.AddFile(filePath, mockInputFile);
            //Act
            var actual = CalculatorLogic.getAllResults(filePath);
            //Assert
            Assert.NotNull(actual);
        }

    }
}
