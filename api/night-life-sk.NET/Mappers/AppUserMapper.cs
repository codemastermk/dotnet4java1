using night_life_sk.Models;
using night_life_sk.Dto.User;

namespace night_life_sk.Mappers
{
    internal static class AppUserMapper
    {

        private static AppUserDto ConvertToDTO(AppUser appUser)
        {
            return new AppUserDto(appUser.Username);
        }

        internal static HashSet<AppUserDto> ConvertAllToDTO(Task<HashSet<AppUser>> appUsers)
        {
            return appUsers.Result.Select(user => ConvertToDTO(user)).ToHashSet();
        }
    }
}
