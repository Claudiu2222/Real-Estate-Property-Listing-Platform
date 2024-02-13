using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using RealEstatePropertyListingPlatform.API.Utility;

namespace RealEstatePropertyListingPlatform.API.Services
{
    public class PredictPriceService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public PredictPriceService()
        {
            _mlContext = new MLContext();
            TrainModel(); // Antrenează modelul la inițializare
        }

        public void TrainModel()
        {
            // Încarcă datele din CSV
            var data = _mlContext.Data.LoadFromTextFile<ModelInput>("D:\\.NET\\Real-Estate-Property-Listing-Platform\\RealEstatePropertyListingPlatform\\RealEstatePropertyListingPlatform.API\\dataset_out.csv", separatorChar: ',');

            // Defineste caracteristicile de intrare si ieșire
            var pipeline = _mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Pret")
                .Append(_mlContext.Transforms.Concatenate("Features", "Nr_Camere", "Suprafata", "Etaj", "Total_Etaje", "City"))
                .AppendCacheCheckpoint(_mlContext)
                .Append(_mlContext.Regression.Trainers.Sdca(
                        labelColumnName: "Label",
                        featureColumnName: "Features",
                        l2Regularization: (float?)0.01));

            // Split datele în setul de antrenare și testare
            var splitData = _mlContext.Data.TrainTestSplit(data);
            IDataView trainData = splitData.TrainSet;
            IDataView testData = splitData.TestSet;

            // Antrenează modelul
            _model = pipeline.Fit(trainData);

            // Evaluează modelul
            var predictions = _model.Transform(testData);
            var metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "Label", scoreColumnName: "Score");

        }


        public float PredictPrice(ModelInput input)
        {
            // Faci predicții pe baza datelor de intrare
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_model);
            var prediction = predictionEngine.Predict(input);

            System.Diagnostics.Debug.WriteLine("Predicted price: " + prediction.Pret);

            return prediction.Pret;
        }

    }
}
