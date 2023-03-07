using AutoTest.EnumUserNextStep;
using AutoTest.Models.Questions;
using AutoTest.Services.ForQuestionsServices;
using AutoTest.Services.ForResultsServices;
using AutoTest.Services.ForUsersServices;
using AutoTest.TelegramBot;
using Newtonsoft.Json;
using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


string filePath = @"uzlotin.json";
var json = System.IO.File.ReadAllText(filePath);
var questions = JsonConvert.DeserializeObject<List<QuestionModel>>(json);


var questionServices = new QuestionsServices();
var userServices = new UsersServices();

#region [ Telegram bot basic codlari ]
const string TOKEN_BOT = "6091527971:AAGknUh6pm_13dcDEhp0A6ESydFI0NLHIIA";

var bot = new TelegramBotClient(TOKEN_BOT);

using var cts = new CancellationTokenSource();

var receicerOptions = new ReceiverOptions()
{
    AllowedUpdates = Array.Empty<UpdateType>(),
    ThrowPendingUpdates = true
};

bot.StartReceiving(
    updateHandler: UpdateHandlerAsync,
    pollingErrorHandler: Telegram1.PollingErrorHandlerAsync,
    receiverOptions: receicerOptions,
    cancellationToken: cts.Token
    );

var me = await bot.GetMeAsync();
Console.WriteLine($"{me.FirstName}:  is working! ");
Console.ReadLine();

cts.Cancel();
#endregion


async Task UpdateHandlerAsync(ITelegramBotClient bot, Update update, CancellationToken cts)
{
    var (message, chatId, firstName, isSucces) = Telegram1.GetMessage(update);

    if (!isSucces)
        return;

    Console.WriteLine($"User: {firstName},    messageText: {message}");

    AutoTest.Models.User? user = null;

    if (message == "/start")
    {
        user = userServices.GetUser(chatId);
        string filePath = @"C:\Users\sardo\OneDrive\Pictures\welcome.jpg";
        await Telegram1.SendStartMenu(bot, chatId, cts, Telegram1.GetCaptionText(firstName), new List<string>() { "Testni boshlash" }, filePath);
        userServices.UpdateUserStep(user, E_NextStep.Start);
        return;
    }
    else if (message == "/tikects" || message == "Testni boshlash")
    {
        user = userServices.GetUser(chatId);
        await questionServices.StartTest(bot, chatId, cts);
        userServices.UpdateUserStep(user, E_NextStep.Start);
        return;
    }
    else if (message == "/menu" || message == "Asosiy menuga qaytish")
    {
        user = userServices.GetUser(chatId);
        string filePath = @"C:\Users\sardo\OneDrive\Pictures\choose2.jpeg";
        await Telegram1.SendStartMenu(bot, chatId, cts, "Pastda berilgan tugmalardan birini tanlang!",
                    new List<string>() { "Testni boshlash", "Natijalarni ko'rish", }, filePath);
        userServices.UpdateUserStep(user, E_NextStep.Start);
        return;
    }
    else if (message == "/result" || message == "Natijalarni ko'rish")
    {
        user = userServices.GetUser(chatId);
        await ResultsServices.ShowResults(user, chatId, bot, update, cts);
        userServices.UpdateUserStep(user, E_NextStep.MainMenu);
        return;
    }
    else
        user = userServices.GetUser(chatId);



    switch (user.NextStep)
    {
        case E_NextStep.Start:
            {
                if (message == "Testni boshlash")
                {
                    await questionServices.StartTest(bot, chatId, cts);
                }
                else if (message == "Natijalarni ko'rish" )
                {
                    await ResultsServices.ShowResults(user, chatId, bot, update, cts);
                    userServices.UpdateUserStep(user, E_NextStep.MainMenu);

                }
                else if (message != null && Telegram1.GetTextListForTikects().Any(u => u.Contains(message)))
                {
                    try
                    {
                        user.IndexTikect = int.Parse(message.Substring(0, 1));

                        await questionServices.ChooseRangeOfQuestions(bot, chatId, cts, user.IndexTikect -1);
                        userServices.UpdateUserStep(user, E_NextStep.ShowQuestions);
                    }
                    catch
                    {
                        await bot.SendTextMessageAsync(chatId, "☢  Nomalum buyruq jo'natildi! \nIltimos berilgan tugmalardan foydalaning!...", cancellationToken: cts);
                    }
                }
                else
                    await bot.SendTextMessageAsync(chatId, "☢  Nomalum buyruq jo'natildi! \nIltimos berilgan tugmalardan foydalaning!...", cancellationToken: cts);
            }
            break;
        case E_NextStep.MainMenu:
            {
                if (message == "Asosiy menuga qaytish")
                {
                    string filePath = @"C:\Users\sardo\OneDrive\Pictures\choose2.jpeg";
                    await Telegram1.SendStartMenu(bot, chatId, cts, "Pastda berilgan tugmalardan birini tanlang!",
                                new List<string>() { "Testni boshlash", "Natijalarni ko'rish", }, filePath);
                    userServices.UpdateUserStep(user, E_NextStep.Start);
                }
                else
                    await bot.SendTextMessageAsync(chatId, "☢  Nomalum buyruq jo'natildi! \nIltimos berilgan tugmalardan foydalaning!...", cancellationToken: cts);
            }
            break;
        case E_NextStep.ShowQuestions:
            {
                int index = user.IndexTikect -1;

                if (message != null && QuestionsServices.GetListText(index).Any(u => u.Item2.Contains(message!)) || message == "next")
                {
                    questionServices.SplitStartAndEndQuestion(user, message!);

                    if (questionServices.Questions![user.Ticket!.StartIndex].Media!.Exist)
                        await Telegram1.SendPhotoQuestion(bot, cts, questionServices.Questions!, user.Ticket.StartIndex++, chatId);
                    else
                        await Telegram1.SendNoPhotoQuestion(bot, cts, questionServices.Questions!, user.Ticket.StartIndex++, chatId);

                    userServices.UpdateUserStep(user, E_NextStep.CheckAnswer);
                }
                else
                    await bot.SendTextMessageAsync(chatId, "☢  Nomalum buyruq jo'natildi! \nIltimos berilgan tugmalardan foydalaning!...", cancellationToken: cts);
            }
            break;
        case E_NextStep.CheckAnswer:
            {
                List<int> list = new List<int>();

                try
                {
                    list = message!.Split(',').Select(int.Parse).ToList();
                }
                catch
                {
                    await bot.SendTextMessageAsync(chatId, "☢  Nomalum buyruq jo'natildi! \nIltimos berilgan tugmalardan foydalaning!...", cancellationToken: cts);
                    return;
                }

                await questionServices.CheckAnswer(user, bot, chatId, cts, list);

                bool isCompleted = await questionServices.IsCompletedQuestion(user, bot, chatId, cts);

                if (isCompleted)
                    userServices.UpdateUserStep(user, E_NextStep.MainMenu);
                else
                    userServices.UpdateUserStep(user, E_NextStep.ShowQuestions);
            }
            break;
        default:
            {
                await bot.SendTextMessageAsync(chatId, "☢  Nomalum buyruq jo'natildi! \nIltimos berilgan tugmalardan foydalaning!...", cancellationToken: cts);
            }
            break;
    }
}





