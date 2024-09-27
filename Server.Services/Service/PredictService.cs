using Microsoft.ML;
using Newtonsoft.Json;
using Server.Handler;
using Server.Models.Model;

namespace Server.Services.Service
{
    public class PredictService
    {

        public static string Predict(string modelo, string newInput)
        {
            string model = modelo + ".zip";
            var mlContext = new MLContext(seed: 0);
            ITransformer trainedModel;
            string modelDirectory = Path.Combine(Environment.CurrentDirectory, "Models");
            string modelPath = Path.Combine(modelDirectory, model);

            // Cargar modelo preentrenado
            trainedModel = mlContext.Model.Load(modelPath, out var modelSchema);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<InputData, PredictionData>(trainedModel);

            var newData = new InputData
            {
                Request = newInput,
                Response = ""
            };

            var prediction = predictionEngine.Predict(newData);
            return prediction.PredictedValue;
        }

        public static string Train(string modelo, string label, string feature)
        {
            string response = string.Empty;

            // Este es el modelo
            string model = modelo + ".zip";

            var mlContext = new MLContext(seed: 0);
            IDataView testData = null;
            ITransformer trainedModel;

            string modelDirectory = Path.Combine(Environment.CurrentDirectory, "Models");
            if (!Directory.Exists(modelDirectory))
            {
                Directory.CreateDirectory(modelDirectory);
            }

            string modelPath = Path.Combine(modelDirectory, model);
            string jsonData = SQLiteHandler.GetJson($"select * from chatbot");
            List<InputData> data = JsonConvert.DeserializeObject<List<InputData>>(jsonData);

            if (data.Count != 0)
            {
                var dataView = mlContext.Data.LoadFromEnumerable<InputData>(data);

                // Dividir los datos en conjuntos de entrenamientos y pruebas
                var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
                var trainData = trainTestSplit.TrainSet;
                testData = trainTestSplit.TestSet;

                var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", label)
                    .Append(mlContext.Transforms.Text.FeaturizeText("Features", feature))
                    .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedValue", "PredictedLabel"));

                trainedModel = pipeline.Fit(trainData);
                mlContext.Model.Save(trainedModel, dataView.Schema, modelPath);

                var predictions = trainedModel.Transform(testData);
                var metrics = mlContext.MulticlassClassification.Evaluate(predictions, "Label", "Score", "PredictedLabel");


                response += "Entrenamiento finalizado correctamente" + "\n";
                response += "--------------------------------------- \n";
                response += "LogLoss: " + metrics.LogLoss.ToString() + "\n";
                response += "PerClassLogLoss: " + string.Join(",", metrics.PerClassLogLoss) + "\n";
                response += "MicroAccuracy: " + metrics.MicroAccuracy.ToString() + "\n";
                response += "MacroAccuracy: " + metrics.MacroAccuracy.ToString() + "\n";

            }

            return response;

        }

    }
}


