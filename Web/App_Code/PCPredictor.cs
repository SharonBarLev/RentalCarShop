using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace ByteForgeML
{
    public class PCInputData
    {
        public float Usage { get; set; }
        public float Budget { get; set; }
        public float Hours { get; set; }
        public float Storage { get; set; }
        public float Graphics { get; set; }
        public float Upgrade { get; set; }
        public float FutureApps { get; set; }
        public float Multitasking { get; set; }
        public string PCTierCode { get; set; }
    }

    public class PCPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedTierCode { get; set; }
    }

    public class MLManager
    {
        private static MLContext mlContext = new MLContext(seed: 42);
        private static ITransformer trainedModel;

        public static void TrainModel()
        {
            string dataPath = System.Web.HttpContext.Current.Server.MapPath("~/PCData.txt");

            // הגדרת המיפוי של העמודות החדשות - 8 משתנים ואינדקס 8 הוא קוד התוצאה
            var columns = new[]
            {
                new TextLoader.Column("Usage", DataKind.Single, 0),
                new TextLoader.Column("Budget", DataKind.Single, 1),
                new TextLoader.Column("Hours", DataKind.Single, 2),
                new TextLoader.Column("Storage", DataKind.Single, 3),
                new TextLoader.Column("Graphics", DataKind.Single, 4),
                new TextLoader.Column("Upgrade", DataKind.Single, 5),
                new TextLoader.Column("FutureApps", DataKind.Single, 6),
                new TextLoader.Column("Multitasking", DataKind.Single, 7),
                new TextLoader.Column("PCTierCode", DataKind.String, 8)
            };

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile(dataPath, columns, separatorChar: ',');

            // בניית ה-Pipeline המותאם ל-8 הנתונים
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "PCTierCode")
                .Append(mlContext.Transforms.Concatenate("Features",
                    "Usage", "Budget", "Hours", "Storage", "Graphics", "Upgrade", "FutureApps", "Multitasking"))
                .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            trainedModel = pipeline.Fit(trainingDataView);
        }

        public static string Predict(float usage, float budget, float hours, float storage, float graphics, float upgrade, float futureApps, float multitasking)
        {
            if (trainedModel == null)
            {
                TrainModel();
            }

            var predictionEngine = mlContext.Model.CreatePredictionEngine<PCInputData, PCPrediction>(trainedModel);

            var sample = new PCInputData
            {
                Usage = usage,
                Budget = budget,
                Hours = hours,
                Storage = storage,
                Graphics = graphics,
                Upgrade = upgrade,
                FutureApps = futureApps,
                Multitasking = multitasking
            };

            var prediction = predictionEngine.Predict(sample);

            return prediction.PredictedTierCode;
        }
    }
}