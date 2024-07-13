using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Services
{
    public interface IAssignmentsCsvWriter
    {
        void WriteAssignmentsToCsv(List<(Users Giver, Users Receiver)> assignments);
        void WriteAssignmentsToCsv(List<(Users Giver, Users Receiver)> assignments, string filePath);
    }
}
