using AutoTest.EnumUserNextStep;
using AutoTest.Models.Tickets;

namespace AutoTest.Models
{
    class User
    {
        public long chatId { get; set; }
        public int IndexTikect { get; set; }
        public int AllCorrectCount { get; set; }
        public string? UserName { get; set; }
        public E_NextStep? NextStep { get; set; }
        public List<Result>? Results { get; set; }
        public Tikect? Ticket { get; set; }

        public User()
        {
            Results = new List<Result>();
            Ticket = new Tikect();
        }
    }
}
