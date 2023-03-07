using AutoTest.EnumUserNextStep;
using User = AutoTest.Models.User;

namespace AutoTest.Services.ForUsersServices
{
    partial class UsersServices
    {
        public void UpdateUserStep(User user, E_NextStep nextstep)
        {
            user.NextStep = nextstep;
            SaveUserData();
        }

        public User GetUser(long chatId)
        {
            User? user = Users.FirstOrDefault(user => user.chatId == chatId);

            if (user == null)
            {
                user = new User();
                user.chatId = chatId;

                Users.Add(user);
                UpdateUserStep(user, E_NextStep.Start);
            }

            return user;
        }
    }
}
