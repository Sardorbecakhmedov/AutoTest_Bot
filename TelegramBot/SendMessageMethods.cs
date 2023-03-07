using Telegram.Bot;
using AutoTest.Models.Questions;

namespace AutoTest.TelegramBot
{
    partial class Telegram1
    {
        public static async Task SendStartMenu(ITelegramBotClient bot, long chatId, CancellationToken cts, 
            string captiontext,  List<string> texts, string PhotoFilePath)
        {
        
            using ( var strem = File.OpenRead(path: PhotoFilePath) )
            {
                await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: strem!,
                    caption: captiontext,
                    replyMarkup: GetInlineButoons(texts),
                    cancellationToken: cts); 
            }
        }

        public static string GetCaptionText(string? firstName)
        {
            return $"🤗   Assalomu alekum  {firstName}! \nAuto_Test _Botga  xush kelibsiz! " +
                    "\nUshbu bot YHQ qoidalarini o'rganishingizni yanada qulayroq qilish ushun ishlab chiqildi" +
                    "\nSavollar soni 700 ta va 70 ta biletdan iborat, \nXar biletda 10 ta dan savollar mavjud! \n";
        }

        public static List<string> GetTextListForTikects()
        {
            return new List<string>()
            {
                "1 - Shablon",
                "2 - Shablon",
                "3 - Shablon",
                "4 - Shablon",
            };
        }

        public static async Task SendChooseTikectsMenu(ITelegramBotClient bot, long chatId, CancellationToken cts)
        {
            string filePath = @"C:\Users\sardo\OneDrive\Pictures\choose1.png";

            using (var strem = File.OpenRead(path: filePath))
            {
                await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: strem!,
                    caption: "⬇  Pastdagi menudan birini tanlang! ",
                    replyMarkup: GetInlineButoons( GetTextListForTikects() ),
                    cancellationToken: cts);
            }
        }

        public static async Task SendPhotoQuestion(ITelegramBotClient bot, CancellationToken cts, List<QuestionModel> questions, int index, long chatId)
        {
            string path = @$"Autotest\{questions[index].Media!.Name}.png";
            using (var strem = System.IO.File.OpenRead(path))
            {
                await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: strem!,
                    caption: $"{questions[index].Id}. {questions[index].Question}",
                    replyMarkup: GetForChoicesInlinesButton(questions, index),
                    cancellationToken: cts);
            }
        }

        public static async Task SendNoPhotoQuestion(ITelegramBotClient bot,CancellationToken cts, List<QuestionModel> questions, int index, long chatId)
        {
            string filePath = @"C:\Users\sardo\OneDrive\Pictures\nophoto4.jpeg";
            using (var strem = System.IO.File.OpenRead(filePath))
            {
                await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: strem!,
                    caption: $"{questions[index].Id}. {questions[index].Question}",
                    replyMarkup: GetForChoicesInlinesButton(questions, index),
                    cancellationToken: cts);
            }
        }
    
    }
}
