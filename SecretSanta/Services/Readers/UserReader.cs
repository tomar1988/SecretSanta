using CsvHelper.Configuration;
using CsvHelper;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace SecretSanta.Services.Readers
{
    public class UserReader : IUserReader
    {
        private readonly string _filePath;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public UserReader(string filePath, ILogger logger, IConfiguration configuration)
        {
            _filePath = filePath;
            _logger = logger;
            _configuration = configuration;
        }

        public List<Users> ReadUsers()
        {
            try
            {
                _logger.LogInformation("Reding the user data......");
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    return csv.GetRecords<Users>().ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading users from CSV file.");
                throw;
            }
        }

        public bool ValidateUserList(List<Users> users)
        {
            int requiredUserCount = _configuration.GetValue<int>("MinimumUsersRequired");
            if (users.Count < requiredUserCount)
            {
                _logger.LogError("At least two users are required for Secret Santa.");
                return false;
            }
            return true;
        }
    }
}
