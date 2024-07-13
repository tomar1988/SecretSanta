using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SecretSanta.Services;
using SecretSanta.Services.Assigners;
using SecretSanta.Services.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Factories
{
    public class SecretSantaFactory : ISecretSantaFactory
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public SecretSantaFactory(ILogger<SecretSantaFactory> logger, IConfiguration configuration)
        {
            _logger = logger;  
             _configuration = configuration;
        }

        public IUserReader CreateUserReader(string filePath)
        {
            return new UserReader(filePath, _logger, _configuration);
        }

        public ISecretSantaAssigner CreateSecretSantaAssigner()
        {
            return new SecretSantaAssigner(_logger);
        }
        public IAssignmentsCsvWriter CreateAssignmentsCsvWriter()
        {
            return new AssignmentsCsvWriter(_logger, _configuration); // Create instance
        }
    }
}
