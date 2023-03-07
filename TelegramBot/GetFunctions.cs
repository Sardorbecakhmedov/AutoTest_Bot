using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using AutoTest.Models.Questions;
using Telegram.Bot.Types.Enums;

namespace AutoTest.TelegramBot
{
    partial class Telegram1
    {
        public static (string?, long, string?, bool) GetMessage(Update update)
        {
            if (update.Type == UpdateType.Message && update.Message is not null && update.Message.From is not null)
            {
                return new(update.Message.Text, update.Message.Chat.Id, update.Message.From.FirstName, true);
            }
            else if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery is not null && update.CallbackQuery.From is not null)
            {
                return new(update.CallbackQuery.Data, update.CallbackQuery.From.Id, update.CallbackQuery.From.FirstName, true);
            }

            return (default, default, default, default);
        }

        public static InlineKeyboardMarkup GetForChoicesInlinesButton(List<QuestionModel> questions, int index)
        {
            var inlineButton = new List<List<InlineKeyboardButton>>();

            for (int i = 0; i < questions[index].Choices!.Count; i++)
            {
                var choicesText = questions[index].Choices![i].Text;

                var button = new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData(choicesText ??= "Variat yo'q", $"{index},{i}")
                };
                inlineButton.Add(button);
            }
            return new InlineKeyboardMarkup(inlineButton);
        }
        
        public static InlineKeyboardMarkup GetInlineButoonsTuple(List<(string, string)> texts)
        {
            var inlineButoons = new List<List<InlineKeyboardButton>>();

            foreach (var text in texts)
            {
                var button = InlineKeyboardButton.WithCallbackData(text.Item1, text.Item2);

                inlineButoons.Add(new List<InlineKeyboardButton>() { button });
            }

            return new InlineKeyboardMarkup(inlineButoons);
        }

        public static InlineKeyboardMarkup GetInlineButoons(List<string> texts)
        {
            var inlineButoons = new List<List<InlineKeyboardButton>>();

            foreach (var text in texts)
            {
                var button = InlineKeyboardButton.WithCallbackData(text);

                inlineButoons.Add(new List<InlineKeyboardButton>() { button });
            }

            return new InlineKeyboardMarkup(inlineButoons);
        }

        public static Task PollingErrorHandlerAsync(ITelegramBotClient bot, Exception ex, CancellationToken cts)
        {
            var errorMessage = ex switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram Api Error:  \n[ {apiRequestException.ErrorCode} ]  \n[ {apiRequestException.Message} ]",
                _ => ex.ToString(),
            };

            Console.WriteLine(errorMessage);

            return Task.CompletedTask;
        }



    }
}
