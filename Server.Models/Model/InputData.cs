using Microsoft.ML.Data;

namespace Server.Models.Model
{
    public class InputData
    {
        [LoadColumn(1)]
        public string? Request { get; set; }

        [LoadColumn(2)]
        public string? Response { get; set; }
    }
}
