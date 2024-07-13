using Microsoft.Extensions.Logging;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Services.Assigners
{
    public class SecretSantaAssigner : ISecretSantaAssigner
    {
        private readonly ILogger _logger;
        public SecretSantaAssigner(ILogger logger)
        {
            _logger = logger;
        }

        public List<(Users Giver, Users Receiver)> AssignSecretSanta(List<Users> users)
        {
            try
            {
                Random rand = new Random();
                List<Users> givers = new List<Users>(users);
                List<Users> receivers = new List<Users>(users);

                List<(Users Giver, Users Receiver)> assignments = new List<(Users, Users)>();

                foreach (var giver in givers)
                {
                    var availableReceivers = receivers.Where(r => r.Id != giver.Id).ToList();
                    if (availableReceivers.Count == 0)
                    {
                        throw new Exception("Not enough receivers available.");
                    }

                    var receiver = availableReceivers[rand.Next(availableReceivers.Count)];
                    assignments.Add((giver, receiver));
                    receivers.Remove(receiver);
                }

                return assignments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning Secret Santa.");
                throw;
            }
        }
    }
}
