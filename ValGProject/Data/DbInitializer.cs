using System.Diagnostics;
using ValGProject.Models;

namespace ValGProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ValGProjectContext context)
        {
            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User
                {
                    UserName = "mike",
                    Password = "12345"
                },

                new User
                {
                    UserName = "john",
                    Password = "12345"
                }
            };
            context.AddRange(users);

            var topics = new Topic[]
            {
                new Topic
                {
                    Title = "Test",
                    Description = "Teste",
                    CreatonDate = DateTime.Now,
                    Creator = "mike"
                }
            };



            context.AddRange(topics);
            context.SaveChanges();
        }
    }
}
