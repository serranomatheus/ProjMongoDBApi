using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;
using ProjMongoDBUser.Controllers;
using ProjMongoDBUser.Services;
using ProjMongoDBUser.Utils;
using Xunit;

namespace ProjTestUser
{
    public class UnitTest1
    {
        public UserService InitializeDataBase()
        {
            var settings = new ProjMongoDBApiSettings();
            UserService userService = new(settings);
            return userService;
        }

        [Fact]
       
        public void GetUserCpf()
        {
            var userService = InitializeDataBase();

            var user = userService.GetCpf("51525879985");
            if (user == null) user = new User();
            Assert.Equal("51525879985", user.Cpf);
        }

        [Fact]
        public void GetAll()
        {
            var userService = InitializeDataBase();
            IEnumerable<User> users = userService.Get();
            Assert.Equal(3, users.Count());

        }

        [Fact]
        public void GetLoginUser()
        {
            var userService = InitializeDataBase();
            var user = userService.GetLogin("JoseADM");
            if (user == null) user = new User();
            Assert.Equal("JoseADM", user.Login);
        }

        [Fact]
        public void GetId()
        {
            var userService = InitializeDataBase();
            var user = userService.Get("624f30cbca7c6d0eb25ec3f6");
            if (user == null) user = new User();
            Assert.Equal("624f30cbca7c6d0eb25ec3f6", user.Id);
        }
        [Fact]
        public  void Create()
        {
            var userService = InitializeDataBase();
            User newUser = new User()
            {
                Name = "Jose Costa",
                LoginUser = "JoseADM",
                Login = "Costa"

            };
             userService.Create(newUser);
            var user = userService.GetLogin("Costa");
            Assert.Equal("Costa", user.Login);
        }

        [Fact]
        public void Delete()
        {
            var userService = InitializeDataBase();

            var user = userService.GetLogin("Costa");
            userService.Remove(user.Id);
            user = userService.GetLogin("Costa");
            Assert.Null(user);

        }

        [Fact]
        public void Update()
        {
            var userService = InitializeDataBase();
            User newUser = new User()
            {
               
                Name = "Jose Costa",
                LoginUser = "JoseADM",
                Login = "Costa"

            };
            
            var user = userService.GetLogin("OCosta");
            newUser.Id= user.Id;
            userService.Update(user.Id, newUser);
            var userReturn = userService.GetLogin("Costa");
            Assert.Equal("Costa",userReturn.Login);

        }
    }
}
