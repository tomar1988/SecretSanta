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
    public interface ISecretSantaFactory
    {
        IUserReader CreateUserReader(string filePath);
        ISecretSantaAssigner CreateSecretSantaAssigner();
        IAssignmentsCsvWriter CreateAssignmentsCsvWriter();
    }
}
