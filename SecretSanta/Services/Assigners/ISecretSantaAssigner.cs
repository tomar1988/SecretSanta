using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Services.Assigners
{
    public interface ISecretSantaAssigner
    {
        List<(Users Giver, Users Receiver)> AssignSecretSanta(List<Users> users);
    }
}
