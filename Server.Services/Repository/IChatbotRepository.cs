using Server.Models.Model;

namespace Server.Services.Repository
{
    public interface IChatbotRepository
    {
        public Task<ChatAnswer> PostQuestion(ChatQuestion chatQuestion);
    }
}
