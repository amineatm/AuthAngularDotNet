using AuthECApi.Models;
using Microsoft.AspNetCore.Identity;

public static class DbInitializer
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "Teacher", "Student" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var users = new List<UserDto>
        {
            new UserDto
            {
                Email = "admin1_m_age51@gmail.com",
                Password = "admin1_m_age51",
                FullName = "Admin 1",
                Role = "Admin",
                Gender = "Male",
                Age = 51,
            },
            new UserDto
            {
                Email = "teacher1_f_age36@gmail.com",
                Password = "teacher1_f_age36",
                FullName = "Teacher 1",
                Role = "Teacher",
                Gender = "Female",
                Age = 36,
            },
            new UserDto
            {
                Email = "teacher2_m_age40@gmail.com",
                Password = "teacher2_m_age40",
                FullName = "Teacher 2",
                Role = "Teacher",
                Gender = "Male",
                Age = 40,
            },
            new UserDto
            {
                Email = "student1_f_age21@gmail.com",
                Password = "student1_f_age21",
                FullName = "Student 1",
                Role = "Student",
                Gender = "Female",
                Age = 21,
                LibraryId = 3452345
            },
            new UserDto
            {
                Email = "student2_m_age21@gmail.com",
                Password = "student2_m_age21",
                FullName = "Student 2",
                Role = "Student",
                Gender = "Male",
                Age = 21,
            },
            new UserDto
            {
                Email = "student3_m_age9@gmail.com",
                Password = "student3_m_age9",
                FullName = "Student 3",
                Role = "Student",
                Gender = "Male",
                Age = 9,
            },
            new UserDto
            {
                Email = "student4_f_age9@gmail.com",
                Password = "student4_f_age9",
                FullName = "Student 4",
                Role = "Student",
                Gender = "Female",
                Age = 9,
                LibraryId = 555434
            }
        };

        foreach (var user in users)
        {
            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
                var newUser = new AppUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    DOB = DateOnly.FromDateTime(DateTime.Now.AddYears(-user.Age)),
                    LibraryID = user.LibraryId,
                };



                var result = await userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    var role = await roleManager.FindByNameAsync(user.Role);
                    if (role != null)
                    {
                        await userManager.AddToRoleAsync(newUser, role.Name);
                    }
                }
            }
        }
    }
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public int? LibraryId { get; set; } // Make this nullable if it's optional
    }

}
