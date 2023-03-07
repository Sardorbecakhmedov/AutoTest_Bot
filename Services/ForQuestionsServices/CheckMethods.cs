using AutoTest.EnumUserNextStep;
using AutoTest.Models;
using AutoTest.Services.ForUsersServices;
using AutoTest.TelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace AutoTest.Services.ForQuestionsServices
{
    partial class QuestionsServices
    {
        public void SplitStartAndEndQuestion(User user, string message)
        {
            try
            {
                user.Ticket!.TikectNumber = int.Parse(message!.Split("-")[1]) / 10;

                user.Ticket.StartIndex = int.Parse(message!.Split("-")[0]) - 1;
                user.Ticket.EndIndex = int.Parse(message.Split("-")[1]);
                user.Ticket.QuestionIndex = user.Ticket!.StartIndex;
            }
            catch
            {
                return;
            }
        }

        public async Task CheckAnswer(User user, ITelegramBotClient bot, long chatId, CancellationToken cts, List<int> list)
        {
            if (Questions![list[0]].Choices![list[1]].Answer)
            {
                await bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"✅ Javob to'g'ri \n\n📃 Javob  tarifi:  {Questions![list[0]].Description}",
                    replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("➡  Keyingi savol", "next") ),
                    cancellationToken: cts);

                user.Ticket!.CorrectAnswerCount++;
            }
            else
            {
                var answer1 = Questions![list[0]].Choices!.FirstOrDefault(ch => ch.Answer == true);

                if (answer1 != null)
                {
                    await bot.SendTextMessageAsync(
                      chatId: chatId,
                      text: $"❌ Sizning javobingiz NO TO'G'RI  \n\n💹 To'g'ri javob quyidagicha:    {answer1!.Text}" +
                            $" \n\n📃  Javob  tarifi:  {Questions![list[0]].Description}",
                      replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("➡  Keyingi savol", "next")),
                      cancellationToken: cts);
                }
            }
        }

        public async Task<bool> IsCompletedQuestion(User user, ITelegramBotClient bot, long chatId, CancellationToken cts)
        {
            if (user.Ticket!.StartIndex >= user.Ticket.EndIndex)
            {
                string filePath = @"C:\Users\sardo\OneDrive\Pictures\godjoob.jpeg";

                using (var strem = File.OpenRead(filePath))
                {
                    await bot.SendPhotoAsync(
                        chatId: chatId,
                        photo: strem!,
                        caption: $"💥  Juda yaxshi! \nSiz << {user.Ticket.TikectNumber} >> - bilet  savollariga ohirigacha javob berdingiz",
                        replyMarkup: Telegram1.GetInlineButoonsTuple(new List<(string,string)>() { ("⬅  Asosiy menuga qaytish", "Asosiy menuga qaytish") }),
                        cancellationToken: cts);
                }

                var result = new Result();
                result.QuestionCount = 10;
                result.TicketNumber = $"{user.Ticket.TikectNumber}\n";
                result.CorrectAnswerCount = user.Ticket.CorrectAnswerCount;
                user.AllCorrectCount += result.CorrectAnswerCount;
                user.Results!.Add(result);
                user.Ticket.CorrectAnswerCount = 0;
                return true;
            }
            return false;
        }
    }
}
