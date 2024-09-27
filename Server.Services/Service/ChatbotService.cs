using Server.Handler;
using Server.Models.Model;
using Server.Services.Repository;

namespace Server.Services.Service
{
    public class ChatbotService : IChatbotRepository
    {

        public async Task<ChatAnswer> PostQuestion(ChatQuestion chatQuestion)
        {
            string query = $"SELECT COUNT(*) FROM mensajes";
            string resultado = SQLiteHandler.GetScalar(query);

            // cada 10 nuevos mensajes entrena el modelo
            if (Convert.ToInt32(resultado) % 10 == 0) 
            {
                string response = PredictService.Train("chatbot", "Response", "Request");
            }

            string prediction = PredictService.Predict("chatbot", chatQuestion.question);
            
            ChatAnswer chatAnswer = new ChatAnswer();
            chatAnswer.name = chatQuestion.name;
            chatAnswer.answer = prediction;
            chatAnswer.datetime = DateTime.Now.ToString();
            return await Task.Run(() => chatAnswer);
        }
    }
}
