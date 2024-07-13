using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SecretSanta.Common;
using SecretSanta.Factories;
using SecretSanta.Models;
using SecretSanta.Services.Assigners;
using SecretSanta.Services.Readers;

namespace SecretSanta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = ServiceCollectionConfigurator.RegisterAppSetting();
            var serviceProvider = ServiceCollectionConfigurator.RegisterDependencies(configuration);        

            var loggerFactory = LoggerInitializer.Initialize();
            var logger = loggerFactory.CreateLogger<Program>();
            var factory = serviceProvider.GetService<ISecretSantaFactory>();
            if(factory != null)
            {
                string filePath = "";
                filePath = configuration["FilePaths:Users"];
                if(filePath != null)
                {
                    var userReader = factory?.CreateUserReader(filePath);
                    var secretSantaAssigner = factory?.CreateSecretSantaAssigner();

                    try
                    {
                        if(userReader != null && secretSantaAssigner != null)
                        {
                            List<Users> users = userReader?.ReadUsers();
                            if (users != null && users.Count > 0 && userReader.ValidateUserList(users))
                            {
                                var secretSantaAssignments = secretSantaAssigner.AssignSecretSanta(users);
                                PrintSecretSantaAssignments(secretSantaAssignments);
                                factory.CreateAssignmentsCsvWriter()?.WriteAssignmentsToCsv(secretSantaAssignments);
                            }

                        }                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
               

            }
            Console.ReadLine();
           
        }

        private static void PrintSecretSantaAssignments(List<(Users Giver, Users Receiver)> assignments)
        {
            Console.WriteLine("Secret Santa Assignments:");
            foreach (var assignment in assignments)
            {
                Console.WriteLine($"{assignment.Giver.Name} ({assignment.Giver.Email}) -> {assignment.Receiver.Name} ({assignment.Receiver.Email})");
            }
        }
    }
}