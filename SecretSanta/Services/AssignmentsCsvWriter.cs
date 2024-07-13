using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Extensions.Logging;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SecretSanta.Services
{
    public class AssignmentsCsvWriter : IAssignmentsCsvWriter
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public AssignmentsCsvWriter(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void WriteAssignmentsToCsv(List<(Users Giver, Users Receiver)> assignments)
        {
             WriteAssignmentsToCsv(assignments, _configuration["FilePaths:Assignments"]);
        }
        public void WriteAssignmentsToCsv(List<(Users Giver, Users Receiver)> assignments, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                {
                    csv.WriteField("giver_id");
                    csv.WriteField("giver_name");
                    csv.WriteField("giver_email");
                    csv.WriteField("receiver_id");
                    csv.WriteField("receiver_name");
                    csv.WriteField("receiver_email");
                    csv.NextRecord();

                    foreach (var assignment in assignments)
                    {
                        csv.WriteField(assignment.Giver.Id);
                        csv.WriteField(assignment.Giver.Name);
                        csv.WriteField(assignment.Giver.Email);
                        csv.WriteField(assignment.Receiver.Id);
                        csv.WriteField(assignment.Receiver.Name);
                        csv.WriteField(assignment.Receiver.Email);
                        csv.NextRecord();
                    }
                }

                _logger.LogInformation($"Assignments written to {filePath}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error writing assignments to CSV file: {filePath}");
                throw;
            }
        }
    }
}
