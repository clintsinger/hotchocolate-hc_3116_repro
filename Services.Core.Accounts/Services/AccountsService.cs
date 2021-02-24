using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Core.Accounts.Data;
using Services.Core.Accounts.Framework;
using Services.Core.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Services.Core.Accounts.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly GarrisonContext context;
        private readonly ILogger<AccountsService> logger;

        public AccountsService(GarrisonContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.logger = loggerFactory.CreateLogger<AccountsService>();
        }

        public async Task<AccountEntity?> GetAccountAsync(Guid id)
        {
            return await this.context.Accounts
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public IAsyncEnumerable<AccountEntity> GetAccountsAsync(List<Guid>? include)
        {
            return this.context.Accounts
                .Where(p => p.DeletedUtc == null)
                .AsAsyncEnumerable();
        }

        public async Task<OrganizationEntity?> GetOrganizationAccountAsync(Guid id)
        {
            //return await this.context.Organizations
            //    .Where(p => p.Id == id)
            //    .FirstOrDefaultAsync();

            throw new NotImplementedException();
        }

        public IAsyncEnumerable<OrganizationEntity> GetOrganizationAccountsAsync(List<Guid>? include)
        {
            //return this.context.Organizations
            //    .Where(p => p.DeletedUtc == null)
            //    .AsAsyncEnumerable();

            throw new NotImplementedException();
        }

        public async Task<OrganizationEntity> CreateOrganizationAsync(OrganizationEntity account)
        {
            //account.Id = Guid.NewGuid();
            //account.CreatedUtc = DateTime.UtcNow;
            //account.Status = AccountStatus.Active;

            //var response = this.context.Organizations.Add(account);
            //await this.context.SaveChangesAsync();

            //return response.Entity;

            throw new ArgumentNullException();
        }

        public async Task<OrganizationEntity> UpdateOrganizationAsync(OrganizationEntity account)
        {
            //if (account.Id == Guid.Empty)
            //{
            //    throw new Exception("Unable to update. Id not supplied.");
            //}

            //var existing = await this.GetOrganizationAccountAsync(account.Id);
            //if (existing == null)
            //{
            //    throw new Exception("Unable to update. Existing account not found.");
            //}

            //existing.UpdatedUtc = DateTime.UtcNow;

            //existing.ParentOrganization = account.ParentOrganization ?? existing.ParentOrganization;
            //existing.Name = account.Name ?? existing.Name;
            //existing.Phones = account.Phones ?? existing.Phones;
            //existing.Addresses = account.Addresses ?? existing.Addresses;
            //existing.Status = account.Status ?? existing.Status;
            //existing.RefLink = account.RefLink ?? existing.RefLink;
            //existing.Industries = account.Industries ?? existing.Industries;
            //existing.Website = account.Website ?? existing.Website;

            //var response = this.context.Organizations.Update(existing);
            //await this.context.SaveChangesAsync();

            //return response.Entity;
            throw new ArgumentNullException();
        }

        public async Task<IEnumerable<Guid>> DeleteOrganizationAsync(Guid id)
        {
            //var parentOrganization = await this.GetOrganizationAccountAsync(id);
            //if (parentOrganization == null)
            //{
            //    throw new Exception("Unable to delete Organization. Account not found.");
            //}

            //var deletedTimestamp = DateTime.UtcNow;

            //parentOrganization.DeletedUtc = deletedTimestamp;
            //this.context.Organizations.Update(parentOrganization);

            //var deletedAccountIds = new List<Guid>()
            //{
            //    parentOrganization.Id
            //};

            //// Delete all of the child accounts.
            //async Task deleteChildrenAsync(Account parent)
            //{
            //    var children = this.context.Accounts
            //        .Where(p => p.ParentOrganization == parent.Id)
            //        .AsAsyncEnumerable();

            //    await foreach (var child in children)
            //    {
            //        this.logger.LogInformation($"Deleting child account (parent={parent.Id}, id={child.Id}, name={child.Name})");
            //        child.DeletedUtc = deletedTimestamp;
            //        this.context.Accounts.Update(child);

            //        deletedAccountIds.Add(child.Id);

            //        await deleteChildrenAsync(child);
            //    }
            //}

            //await deleteChildrenAsync(parentOrganization);

            //await this.context.SaveChangesAsync();

            //return deletedAccountIds;
            throw new ArgumentNullException();
        }

        public async Task<UserEntity?> GetUserAccountAsync(UserQueryType queryType, string parameter)
        {
            try
            {
                UserEntity? response = null;

                if (queryType == UserQueryType.Id)
                {
                    var id = Guid.Parse(parameter);
                    response = await this.context.Users
                        .Where(p => p.Id == id)
                        .FirstOrDefaultAsync();
                }
                else if (queryType == UserQueryType.Username)
                {
                    response = await this.context.Users
                        .Where(p => p.Username == parameter)
                        .FirstOrDefaultAsync();
                }
                else if (queryType == UserQueryType.Email)
                {
                    response = await this.context.Users
                        .Where(p => p.Email == parameter)
                        .FirstOrDefaultAsync();
                }
                else if (queryType == UserQueryType.ExternalProviderId)
                {
                    var results = from p in this.context.Users
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                                   where p.Provider.SubjectId == parameter
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                                   select p;

                    response = results.FirstOrDefault();
                }
                else if (queryType == UserQueryType.RefLink)
                {
                    response = await this.context.Users
                        .Where(p => p.RefLink == parameter)
                        .FirstOrDefaultAsync();
                }

                if (response != null)
                {
                    this.logger.LogDebug($"Got user account {response.Id}");
                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Unable to get user account [{parameter}]");
                throw ex;
            }
        }

        public IAsyncEnumerable<UserEntity> GetUserAccountsAsync(List<Guid>? include)
        {
            return this.context.Users
                .Where(p => p.DeletedUtc == null)
                .AsAsyncEnumerable(); ;
        }

        public async Task<(UserEntity, CreatePasswordResultEntity)> CreateUserAsync(UserEntity account, CreatePasswordResultEntity? createPassword)
        {
            if (account.Id != Guid.Empty)
            {
                throw new Exception("User already exists");
            }

            if (string.IsNullOrEmpty(account.Username))
            {
                throw new Exception("Username must be provided to create a new user.");
            }

            // Throw an error if the username is already taken.
            var existingUser = await this.GetUserAccountAsync(UserQueryType.Username, account.Username);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            account.Id = Guid.NewGuid();
            account.CreatedUtc = DateTime.UtcNow;
            
            account.Status = AccountStatusEntity.Active;

            var salt = PasswordHelper.GeneratePasswordSalt();

            if (account.Provider == null)
            {
                if (string.IsNullOrEmpty(createPassword?.ClearText))
                {
                    // Auto-generate a password for the user because one wasn't supplied.
                    if (createPassword == null)
                    {
                        createPassword = new CreatePasswordResultEntity();
                    }

                    createPassword.ClearText = PasswordHelper.GenerateRandomPassword(8);
                }

                var password = new PasswordEntity()
                {
                    IsTemporary = createPassword.IsTemporary,
                    Hash = PasswordHelper.GeneratedPasswordHash(createPassword.ClearText, salt),
                    Salt = Convert.ToBase64String(salt)
                };

                account.Password = password;

                if (createPassword.EmailReceipients != null
                    && createPassword.EmailReceipients.Any())
                {
                    // TODO: Send password notification to email recipients.
                    System.Diagnostics.Debug.WriteLine($"Notify people of the new password.");
                }
            }
            else
            {
                var p = account.Provider;
                account.Provider = new ProviderEntity()
                {
                    Name = p.Name,
                    SubjectId = p.SubjectId,
                    ExternalIdToken = p.ExternalIdToken
                };

                // A user that is being provisioned via an external provider is logging in at the same time.
                if (account.Login == null)
                {
                    account.Login = new LoginEntity();
                }

                account.Login.LastLoginUtc = DateTime.UtcNow;
            }

            this.context.Users.Add(account);
            await this.context.SaveChangesAsync();

            return (account, createPassword);
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity account)
        {
            if (account.Id == Guid.Empty)
            {
                throw new Exception("Unable to update. Id not supplied.");
            }

            var existing = await this.GetUserAccountAsync(UserQueryType.Id, account.Id.ToString());
            if (existing == null)
            {
                throw new Exception("Unable to update. Existing account not found.");
            }

            existing.UpdatedUtc = DateTime.UtcNow;

            existing.ParentOrganization = account.ParentOrganization ?? existing.ParentOrganization;
            existing.Name = account.Name ?? existing.Name;
            existing.Phones = account.Phones ?? existing.Phones;
            existing.Addresses = account.Addresses ?? existing.Addresses;
            existing.Status = account.Status ?? existing.Status;
            existing.RefLink = account.RefLink ?? existing.RefLink;

            existing.FirstName = account.FirstName ?? existing.FirstName;
            existing.LastName = account.LastName ?? existing.LastName;
            existing.Login = account.Login ?? existing.Login;
            existing.Email = account.Email ?? existing.Email;
            existing.Provider = account.Provider ?? existing.Provider;
            existing.Username = account.Username ?? existing.Username;

            var response = this.context.Users.Update(existing);
            await this.context.SaveChangesAsync();

            return response.Entity;
        }

        public async Task<UserEntity?> DeleteUserAsync(Guid id)
        {
            var existing = await this.GetUserAccountAsync(UserQueryType.Id, id.ToString());
            if (existing == null)
            {
                return null;
            }

            existing.DeletedUtc = DateTime.UtcNow;

            var response = this.context.Users.Update(existing);

            await this.context.SaveChangesAsync();
            return response.Entity;
        }

        public async Task UpdateUserExternalIdTokenAsync(string id, string idToken)
        {
            var existing = await this.GetUserAccountAsync(UserQueryType.Id, id);
            if (existing == null)
            {
                throw new RepositoryException();
            }

            existing.UpdatedUtc = DateTime.UtcNow;

            if (existing.Provider == null)
            {
                existing.Provider = new ProviderEntity();
            }

            existing.Provider.ExternalIdToken = idToken;

            this.context.Users.Update(existing);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            bool valid = false;

            var existing = await this.GetUserAccountAsync(UserQueryType.Username, username);
            if (existing == null)
            {
                throw new Exception("User account was not found.");
            }

            if (existing.Id == Guid.Empty)
            {
                throw new Exception("User account does not have an Id");
            }

            if (existing.Password?.Hash == null || existing.Password?.Salt == null)
            {
                throw new Exception("Unable to validate account due to Password not being set.");
            }

            var hashedPassword = PasswordHelper.GeneratedPasswordHash(password, Convert.FromBase64String(existing.Password.Salt));

            if (existing.Login == null)
            {
                existing.Login = new LoginEntity();
            }

            existing.Login.LastLoginUtc = DateTime.UtcNow;
            existing.Login.LoginAttempts++;

            if (existing.Password.Hash == hashedPassword)
            {
                valid = true;
                existing.Login.LoginAttempts = 0;
            }

            this.context.Users.Update(existing);
            await this.context.SaveChangesAsync();

            return valid;
        }
    }
}
