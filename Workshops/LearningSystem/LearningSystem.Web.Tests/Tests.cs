namespace LearningSystem.Web.Tests
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using LearningSystem.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;
    using Moq;

    public static class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (testsInitialized)
            {
                return;
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            testsInitialized = true;
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var manager = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            manager.Object.UserValidators.Add(new UserValidator<TUser>());
            manager.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            return manager;
        }

        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store ??= new Mock<IUserStore<TUser>>().Object;

            var options = new Mock<IOptions<IdentityOptions>>();

            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);

            var userValidators = new List<IUserValidator<TUser>>();

            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);

            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());

            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            return userManager;
        }

        public static Mock<RoleManager<TRole>> MockRoleManager<TRole>(IRoleStore<TRole> store = null) where TRole : class
        {
            store ??= new Mock<IRoleStore<TRole>>().Object;

            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());

            return new Mock<RoleManager<TRole>>(store, 
                roles, 
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), 
                null);
        }

        public static RoleManager<TRole> TestRoleManager<TRole>(IRoleStore<TRole> store = null) where TRole : class
        {
            store ??= new Mock<IRoleStore<TRole>>().Object;

            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());

            return new RoleManager<TRole>(store, 
                roles,
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null);
        }
    }
}
