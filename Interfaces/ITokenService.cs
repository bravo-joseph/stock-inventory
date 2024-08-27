using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resful_project.models;

namespace resful_project.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appuser);
    }
}