using Services.Core.Accounts.Interface.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Asserts.Compare;

namespace Services.Core.Accounts.Tests.Asserts
{
    public static class AccountAssert
    {
        public static void Equal(UserAccount? expected, UserAccount? actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            Assert.Equal(expected!.Status, actual!.Status);
            Assert.Equal(expected!.Username, actual!.Username);
            Assert.Equal(expected!.Email, actual!.Email);
            Assert.Equal(expected!.FirstName, actual!.FirstName);
            Assert.Equal(expected!.LastName, actual!.LastName);
            Assert.Equal(expected!.Addresses, actual!.Addresses);
            Assert.Equal(expected!.Phones, actual!.Phones);
            Assert.Equal(expected!.RefLink, actual!.RefLink);
            Assert.Equal(expected!.Notes, actual!.Notes);
        }
    }
}
