using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XoredTest.Chat.Domain
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        public User(string id, string name, UserType userType)
        {
            this.Id = id;
            this.Name = name;
            this.UserType = userType;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public UserType UserType { get; private set; }
    }

}
