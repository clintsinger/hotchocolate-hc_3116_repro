using AutoMapper;
using Services.Core.Accounts.Data.Entities;
using Services.Core.Accounts.Interface.Schema;
using System;

namespace Services.Core.Accounts.Framework.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.AllowNullCollections = true;            
            this.AllowNullDestinationValues = true;

            this.CreateMap<CreateUserInput, UserAccountEntity>();
            
            this.CreateMap<PasswordInput, Password>();

            this.CreateMap<UserAccountEntity, UserAccount>();
        }
    }
}
