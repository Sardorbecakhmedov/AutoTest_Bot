using AutoTest.Models;
using AutoTest.TelegramBot;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AutoTest.Services.ForResultsServices
{
    partial class ResultsServices
    {
        public static async Task ShowResults(Models.User user, long chatId, ITelegramBotClient bot, Update update, CancellationToken cts)
        {
            var inlineButtons = new List<List<InlineKeyboardButton>>();

            var buttons1 = new List<InlineKeyboardButton>()
            {
                  InlineKeyboardButton.WithCallbackData( $"Bilet raqami" ),
                  InlineKeyboardButton.WithCallbackData( $"To'g'ri  javob" ),
                  InlineKeyboardButton.WithCallbackData( $"Savollar soni" )
            };

            inlineButtons.Add(buttons1);


            foreach (var result in user.Results!)
            {

                var buttons2 = new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData( $"{result.TicketNumber}" ),
                    InlineKeyboardButton.WithCallbackData( $"✅  {result.CorrectAnswerCount}" ),
                    InlineKeyboardButton.WithCallbackData( $"{result.QuestionCount}" )
                };

                inlineButtons.Add(buttons2);
            }


            var buttons3 = new List<InlineKeyboardButton>()
            {
                  InlineKeyboardButton.WithCallbackData( $"Jami to'g'ri javoblar" ),
                  InlineKeyboardButton.WithCallbackData( $"Jami savollar soni" )
            };
            inlineButtons.Add(buttons3);

            var buttons33 = new List<InlineKeyboardButton>()
            {
                  InlineKeyboardButton.WithCallbackData(  user.AllCorrectCount.ToString() ),
                  InlineKeyboardButton.WithCallbackData( $"700" )
            };
            inlineButtons.Add(buttons33);

            var buttons4 = new List<InlineKeyboardButton>()
            {
               InlineKeyboardButton.WithCallbackData( "⬅  Asosiy menuga qaytish ", $"Asosiy menuga qaytish" )
            };

            inlineButtons.Add(buttons4);

            string filePath = @"C:\Users\sardo\OneDrive\Pictures\results.jpg";
            using (var strem = System.IO.File.OpenRead(filePath))
            {
                await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: strem!,
                    caption: $"Foydalanuvchi Natijalari: ",
                    replyMarkup: new InlineKeyboardMarkup(inlineButtons),
                    cancellationToken: cts);
            }
        }


    }
}
