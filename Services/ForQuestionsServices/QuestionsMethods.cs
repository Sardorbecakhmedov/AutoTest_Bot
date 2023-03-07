using AutoTest.EnumUserNextStep;
using AutoTest.Models;
using AutoTest.Models.Questions;
using AutoTest.TelegramBot;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AutoTest.Services.ForQuestionsServices
{
    partial class QuestionsServices
    {
        public InlineKeyboardMarkup GetShablon(List<(string,string)> textForButtons)
        {
            var countRow = 6;
            var countColumn = 3;
            var index = 0;

            var inlineButtons = new List<List<InlineKeyboardButton>>();

            for (int i = 0; i < countRow; i++)
            {
                var butoon = new List<InlineKeyboardButton>();

                for (int j = 0; j < countColumn; j++)
                {
                    butoon.Add(InlineKeyboardButton.WithCallbackData(textForButtons[index].Item1, textForButtons[index].Item2));
                    index++;
                }

                inlineButtons.Add(butoon);
            }

            return new InlineKeyboardMarkup(inlineButtons);
        }  


        public async Task ChooseRangeOfQuestions(ITelegramBotClient bot, long chatId, CancellationToken cts, int index)
        {
            string filePath = @"C:\Users\sardo\OneDrive\Pictures\starttest2.jpeg";

            using (var strem = System.IO.File.OpenRead(path: filePath))
            {
                await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: strem!,
                    caption: "⚠  Etibor bering!!! \nXar bir savolga bitta javob, bir marta qabul qilinadi, " +
                    "Agarda siz qayta javob berish tugmasini bossangiz kegingi berilgan javob qabul qilinmaydi. " +
                    "\nAgarda tanlangan biletni savollariga oxirigacha javob bermasangiz, ushbu tanlangan bilet uchun natijalaringiz saqlanmaydi!  " +
                    "\n\n ⬇  Pastdagi biletlardan birini tanlang! ",
                    replyMarkup: GetShablon(GetListText(index)),
                    cancellationToken: cts); ;
            }
        }


        public async Task StartTest(ITelegramBotClient bot, long chatId, CancellationToken cts)
        {
            string filePath = @"C:\Users\sardo\OneDrive\Pictures\choose1.png";

            using (var strem = System.IO.File.OpenRead(path: filePath))
            {
                await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: strem!,
                    caption: "⬇  Pastdagi  berilgan shablonlardan birini tanlang! ",
                    replyMarkup: Telegram1.GetInlineButoons(Telegram1.GetTextListForTikects()),
                    cancellationToken: cts);
            }
        }

    }
}

