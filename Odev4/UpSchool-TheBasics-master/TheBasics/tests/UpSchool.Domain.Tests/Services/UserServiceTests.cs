using FakeItEasy;
using UpSchool.Domain.Data;
using UpSchool.Domain.Entities;
using UpSchool.Domain.Services;

namespace UpSchool.Domain.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUser_ShouldGetUserWithCorrectId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var cancellationSource = new CancellationTokenSource();

            var expectedUser = new User()
            {
                Id = userId
            };

            A.CallTo(() =>  userRepositoryMock.GetByIdAsync(userId, cancellationSource.Token))
                .Returns(Task.FromResult(expectedUser));

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.GetByIdAsync(userId, cancellationSource.Token);

            Assert.Equal(expectedUser, user);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenEmailIsEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            var emptyEmailUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = ""
            };
            var nullEmailUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = null
            };

            A.CallTo(() => userRepositoryMock.AddAsync(emptyEmailUser, cancellationSource.Token));
            A.CallTo(() => userRepositoryMock.AddAsync(nullEmailUser, cancellationSource.Token));

            IUserService userService = new UserManager(userRepositoryMock);

            //var user = await userService.AddAsync(emptyEmailUser.FirstName, emptyEmailUser.LastName, emptyEmailUser.Age, emptyEmailUser.Email, cancellationSource.Token);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.AddAsync(emptyEmailUser.FirstName, emptyEmailUser.LastName, emptyEmailUser.Age, emptyEmailUser.Email, cancellationSource.Token);
            });

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.AddAsync(nullEmailUser.FirstName, nullEmailUser.LastName, nullEmailUser.Age, nullEmailUser.Email, cancellationSource.Token);
            });

        }

        [Fact]
        public async Task AddAsync_ShouldReturn_CorrectUserId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var userToAdd = new User()
            {
                Id = userId,
                FirstName = "Büşra",
                LastName = "Akay",
                Age = 24,
                Email = "busraakay@example.com"
            };

            A.CallTo(() => userRepositoryMock.AddAsync(userToAdd, cancellationSource.Token));

            IUserService userService = new UserManager(userRepositoryMock);


            var addedUserId = await userService.GetByIdAsync(userToAdd.Id, cancellationSource.Token);

            Assert.Equal(userId, addedUserId.Id);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenUserExists()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            var userId = Guid.NewGuid();
            A.CallTo(() => userRepositoryMock.DeleteAsync(x => x.Id == userId, cancellationSource.Token));

            IUserService userService = new UserManager(userRepositoryMock);

            var result = await userService.DeleteAsync(userId, cancellationSource.Token);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenUserDoesntExists()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            var userId = Guid.NewGuid();
            A.CallTo(() => userRepositoryMock.DeleteAsync(x => x.Id == userId, cancellationSource.Token));

            IUserService userService = new UserManager(userRepositoryMock);

            var result = await userService.DeleteAsync(userId, cancellationSource.Token);

            Assert.True(result);
        }



        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserIdIsEmpty()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            var emptyEmailUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = ""
            };

            A.CallTo(() => userRepositoryMock.UpdateAsync(emptyEmailUser, cancellationSource.Token));

            IUserService userService = new UserManager(userRepositoryMock);

            //var user = await userService.AddAsync(emptyEmailUser.FirstName, emptyEmailUser.LastName, emptyEmailUser.Age, emptyEmailUser.Email, cancellationSource.Token);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.AddAsync(emptyEmailUser.FirstName, emptyEmailUser.LastName, emptyEmailUser.Age, emptyEmailUser.Email, cancellationSource.Token);
            });


        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserEmailEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            var emptyEmailUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = ""
            };
            var nullEmailUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = null
            };

            A.CallTo(() => userRepositoryMock.UpdateAsync(emptyEmailUser, cancellationSource.Token));
            A.CallTo(() => userRepositoryMock.UpdateAsync(nullEmailUser, cancellationSource.Token));

            IUserService userService = new UserManager(userRepositoryMock);

            //var user = await userService.AddAsync(emptyEmailUser.FirstName, emptyEmailUser.LastName, emptyEmailUser.Age, emptyEmailUser.Email, cancellationSource.Token);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.AddAsync(emptyEmailUser.FirstName, emptyEmailUser.LastName, emptyEmailUser.Age, emptyEmailUser.Email, cancellationSource.Token);
            });

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.AddAsync(nullEmailUser.FirstName, nullEmailUser.LastName, nullEmailUser.Age, nullEmailUser.Email, cancellationSource.Token);
            });

        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn_UserListWithAtLeastTwoRecords()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            var userList = new List<User>()
            {
                new User() { Id = Guid.NewGuid(), FirstName = "Busra", LastName = "Akay", Age = 24, Email = "busarakay@gmail.com" },
                new User() { Id = Guid.NewGuid(), FirstName = "Songul", LastName = "Bayer", Age = 22, Email = "songulbayer@gmail.com" },
            };

            A.CallTo(() => userRepositoryMock.GetAllAsync(cancellationSource.Token)).Returns(userList);

            IUserService userService = new UserManager(userRepositoryMock);

            var users = await userService.GetAllAsync(cancellationSource.Token);

            Assert.NotNull(users);
            Assert.True(users.Count >= 2);
        }
    }
}
