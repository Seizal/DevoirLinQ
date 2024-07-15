using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using QuizData.Models;
using QuizData.Services;

namespace QuizData
{
    internal class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            // Chemins relatifs pour les fichiers de données
            string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataProjet", "Cars.xml");
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataProjet", "Cars.json");

            Console.WriteLine($"XML file path: {xmlFilePath}");
            Console.WriteLine($"JSON file path: {jsonFilePath}");

            if (!File.Exists(xmlFilePath))
            {
                Console.WriteLine($"Le fichier XML '{xmlFilePath}' n'existe pas.");
                return;
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"Le fichier JSON '{jsonFilePath}' n'existe pas.");
                return;
            }

            DataService dataService = new DataService();

            // Charger les données depuis les fichiers XML et JSON
            List<Car> xmlData = dataService.ReadXmlData(xmlFilePath);
            List<Car> jsonData = dataService.ReadJsonData(jsonFilePath);

            // Combiner toutes les données
            List<Car> combinedData = xmlData.Concat(jsonData).ToList();

            // Regrouper les données par groupe
            var carGroups = combinedData.GroupBy(car => car.Group)
                                        .Select(group => new CarGroup
                                        {
                                            GroupName = group.Key,
                                            Cars = group.ToList()
                                        }).ToList();

            // Initialiser le quiz
            StartQuiz(carGroups);
        }

        static void StartQuiz(List<CarGroup> carGroups)
        {
            Console.WriteLine("Bienvenue au quiz sur les voitures !");
            Console.WriteLine("Veuillez répondre aux questions suivantes :\n");

            bool continueQuiz = true;

            while (continueQuiz)
            {
                // Sélectionner une question aléatoire
                var randomGroup = carGroups[random.Next(carGroups.Count)];
                var randomCar = randomGroup.Cars[random.Next(randomGroup.Cars.Count)];

                Console.WriteLine($"Quel est le groupe de la voiture : {randomCar.Name} ?");

                string userAnswer = Console.ReadLine();

                if (userAnswer.Equals(randomGroup.GroupName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Correct !");
                }
                else
                {
                    Console.WriteLine($"Incorrect ! La bonne réponse est : {randomGroup.GroupName}");
                }

                Console.WriteLine();

                // Demander à l'utilisateur s'il souhaite continuer
                Console.WriteLine("Souhaitez-vous continuer à jouer ? (oui/non)");
                string continueInput = Console.ReadLine().ToLower();

                if (continueInput != "oui")
                {
                    continueQuiz = false;
                }

                Console.WriteLine();
            }

            Console.WriteLine("Merci d'avoir joué au quiz !");
        }
    }
}
