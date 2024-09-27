using Microsoft.AspNetCore.Mvc;
using Server.Models.Model;
using Server.Repository;
using Server.Services.Repository;

namespace Server.Controller
{
    [Route("/chatbot")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotRepository chatbotRepository;

        public ChatbotController(IChatbotRepository chatbotRepository)
        {
            this.chatbotRepository = chatbotRepository;
        }

        [HttpPost]
        public async Task<ChatAnswer> PostQuestion(ChatQuestion chatQuestion)
        {
            return await Task.Run(() => chatbotRepository.PostQuestion(chatQuestion));
        }
    }
}
