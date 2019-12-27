using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFrameWork.Membership
{
    class Membership
    {
        protected string username;
        protected string password;

        public Membership(string username, string password)
        {
            this.username = username;
            this.password = password;
        } 
        public string Username { get => username; }
        public string Password { get => password; }


    }
}
