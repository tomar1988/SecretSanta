using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using SecretSanta.Models;
using SecretSanta.Services.Assigners;
using Xunit;

namespace SecretSanta.Tests.Services.Assigners
{
    public class SecretSantaAssignerTests
    {
        private readonly Mock<ILogger<SecretSantaAssigner>> _loggerMock;
        private readonly SecretSantaAssigner _assigner;

        public  SecretSantaAssignerTests()
        {
            _loggerMock = new Mock<ILogger<SecretSantaAssigner>>();
            _assigner = new SecretSantaAssigner(_loggerMock.Object);
        }

        [Fact]
        public void AssignSecretSanta_ShouldAssignUniqueChildAndParentToEachUser()
        {
            // Arrange
            var users = new List<Users>
            {
                new Users { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new Users { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
                new Users { Id = 3, Name = "Bob Johnson", Email = "bob@example.com" },
                new Users { Id = 4, Name = "Alice Brown", Email = "alice@example.com" }
            };

            // Act
            var assignments = _assigner.AssignSecretSanta(users);

            // Assert
            Assert.Equal(users.Count, assignments.Count);
            var assignedGivers = new HashSet<int>();
            var assignedReceivers = new HashSet<int>();

            foreach (var assignment in assignments)
            {
                Assert.False(assignedGivers.Contains(assignment.Giver.Id), "Each user should give a gift to exactly one other user.");
                Assert.False(assignedReceivers.Contains(assignment.Receiver.Id), "Each user should receive a gift from exactly one other user.");
                Assert.NotEqual(assignment.Giver.Id, assignment.Receiver.Id);

                assignedGivers.Add(assignment.Giver.Id);
                assignedReceivers.Add(assignment.Receiver.Id);
            }
        }
    }
}