using Projekt.Models;

namespace Projekt
{
    public static class UserSession
    {
        public static User CurrentUser { get; private set; }
        public static bool LoggedIn => CurrentUser != null;
        public static bool IsAdmin => CurrentUser?.UserType == UserType.Admin;

        public static void Login(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
