using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsers")]

    public IEnumerable<User> GetUsers()
    {
        string sql = @"
        SELECT [UserId],
               [FirstName],
               [LastName],
               [Email],
               [Gender],
               [Active] 
        FROM TutorialAppSchema.Users";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"
        SELECT [UserId],
               [FirstName],
               [LastName],
               [Email],
               [Gender],
               [Active] 
        FROM TutorialAppSchema.Users
        WHERE UserId = " + userId.ToString();
        User user = _dapper.LoadDataSingle<User>(sql);
        return user;

    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
    UPDATE TutorialAppSchema.Users
        SET [FirstName] = '" + user.FirstName +
        "',[LastName] = '" + user.LastName +
        "',[Email] = '" + user.Email +
        "',[Gender] = '" + user.Gender +
        "',[Active] = '" + user.Active +
    "' WHERE UserId = " + user.UserId;
        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"INSERT INTO TutorialAppSchema.Users(
                    [FirstName],
                    [LastName],
                    [Email],
                    [Gender],
                    [Active]
                    ) VALUES (" +
                    " '" + user.FirstName +
                    "', '" + user.LastName +
                    "', '" + user.Email +
                    "', '" + user.Gender +
                    "', '" + user.Active + "')";
        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
        DELETE FROM TutorialAppSchema.Users
        WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Delete User");
    }

    //USER SALARY


    [HttpGet("UserSalary/{userId}")]
    public IEnumerable<UserSalary> GetUserSalary(int userId)
    {
        return _dapper.LoadData<UserSalary>(@"
        SELECT UserSalary.UserId
        , UserSalary.Salary
        FROM TutorialAppSchema.UserSalary
        WHERE UserId = " + userId);
    }

    [HttpPost("UserSalary")]
    public IActionResult PostUserSalary(UserSalary userSalaryForInsert)
    {
        string sql = @"INSERT INTO TutorialAppSchema.UserSalary(
                    [UserId]
                    ,[Salary]
                   
                    ) VALUES (" +
                    " '" + userSalaryForInsert.UserId +
                    "', '" + userSalaryForInsert.Salary + "')";
        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userSalaryForInsert);
        }
        throw new Exception("Adding User Salary Failed on save");
    }

    [HttpPut("UserSalary")]
    public IActionResult PutUserSalary(UserSalary userSalaryForUpdate)
    {
        string sql = @"
    UPDATE TutorialAppSchema.UserSalary
        SET Salary = " + userSalaryForUpdate.Salary +
    " WHERE UserId = " + userSalaryForUpdate.UserId;
        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userSalaryForUpdate);
        }
        throw new Exception("Failed to Update User Salary");
    }

    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"
        DELETE FROM TutorialAppSchema.UserSalary
        WHERE UserId = " + userId;

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Deleting User Salary Failed on save");
    }

    //USER JOB INFO

    [HttpGet("UserJobInfo/{userId}")]
    public IEnumerable<UserJobInfo> GetUserJobInfo(int userId)
    {
        return _dapper.LoadData<UserJobInfo>(@"
        SELECT UserJobInfo.UserId
        , UserJobInfo.JobTitle
        , UserJobInfo.Department
        FROM TutorialAppSchema.UserJobInfo
        WHERE UserId = " + userId.ToString());
    }

    [HttpPost("UserJobInfo")]
    public IActionResult PostUserJobInfo(UserJobInfo userJobInfoForInsert)
    {
        string sql = @"INSERT INTO TutorialAppSchema.UserJobInfo(
                    [UserId]
                    ,[JobTitle]
                    , [Department]
                    ) VALUES (" +
                    " '" + userJobInfoForInsert.UserId +
                    "', '" + userJobInfoForInsert.JobTitle +
                    "', '" + userJobInfoForInsert.Department + "')";

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userJobInfoForInsert);
        }
        throw new Exception("Adding User Job Info Failed on save");
    }

    [HttpPut("UserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfoForUpdate)
    {
        string sql = @"
    UPDATE TutorialAppSchema.UserJobInfo
        SET [JobTitle] = '" + userJobInfoForUpdate.JobTitle +
        "',[Department] = '" + userJobInfoForUpdate.Department +
    "' WHERE UserId = " + userJobInfoForUpdate.UserId;
        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userJobInfoForUpdate);
        }
        throw new Exception("Failed to Update User Job Info");
    }

        [HttpDelete("UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = @"
        DELETE FROM TutorialAppSchema.UserJobInfo
        WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Deleting User Job Info Failed on save");
    }
    
    
    
    }
    
    



