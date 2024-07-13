using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Services.Readers
{
    public interface IUserReader
    {
        List<Users> ReadUsers();
        bool ValidateUserList(List<Users> users);
    }
}
