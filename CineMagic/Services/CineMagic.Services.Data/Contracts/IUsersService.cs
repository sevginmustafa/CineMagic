namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUsersService
    {
        bool IsEmailAvailable(string email);
    }
}
