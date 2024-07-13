using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SecretSanta.Models;
using SecretSanta.Services.Readers;
using Xunit;

namespace SecretSanta.Tests.Services.Readers
{
    public class UserReaderTests
    {
        private readonly Mock<ILogger<UserReader>> _loggerMock;
        private readonly Mock<IConfiguration> _ConfigurationMock;
       
        public UserReaderTests()
        {
            _loggerMock = new Mock<ILogger<UserReader>>();
            _ConfigurationMock = new Mock<IConfiguration>();                     
        }

        [Fact]
        public void ReadUsers_ShouldReturnListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<Users>
            {
                new Users { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new Users { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
                new Users { Id = 3, Name = "Bob Johnson", Email = "bob@example.com" },
                new Users { Id = 4, Name = "Alice Brown", Email = "alice@example.com" }
            };

            var _csvUserReader = new UserReader("Data/users.csv", _loggerMock.Object, _ConfigurationMock.Object);

            // Act
            var users = _csvUserReader.ReadUsers();

            // Assert
            Assert.Equal(expectedUsers.Count, users.Count);
            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.Equal(expectedUsers[i].Id, users[i].Id);
                Assert.Equal(expectedUsers[i].Name, users[i].Name);
                Assert.Equal(expectedUsers[i].Email, users[i].Email);
            }
        }
    }
}
