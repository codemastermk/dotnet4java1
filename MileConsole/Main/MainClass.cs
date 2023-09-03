using Newtonsoft.Json;
using night_life_sk.Dto.Event;
using night_life_sk.Mappers;
using night_life_sk.Models;

namespace Main
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            string jsonFilePath = "demo.json";
            string jsonString = File.ReadAllText(jsonFilePath);

            AppUser? appUser = JsonConvert.DeserializeObject<AppUser>(jsonString);
            if (appUser == null )
            {
                Console.WriteLine("its null");
            }
            else
            {
                
                IEnumerable<AppUser> filteredUsers = 
                    GetUsers(appUser)
                    .Where(u => u.Id < 3);
                foreach (AppUser user in filteredUsers)
                {
                    Console.WriteLine(user);
                }
            }
        }

        private static IEnumerable<AppUser> GetUsers(AppUser appUser)
        {
            List<AppUser> users = new List<AppUser>();

            for (int i = 0; i < 10; i++)
            {
                yield return CreateNewUser(appUser, i);              
            }
        }

        private static AppUser CreateNewUser(AppUser appUser, int i)
        {
            AppUser user = new AppUser();
            user.Id = i;
            user.Username = appUser.Username + i;
            user.Password = appUser.Password + i;
            user.PartyPlaces = new HashSet<PartyPlace>();
            return user;
        }
    }
}
