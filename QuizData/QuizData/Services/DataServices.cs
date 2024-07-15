using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using QuizData.Models;

namespace QuizData.Services
{
    public class DataService
    {
        public List<Car> ReadXmlData(string filePath)
        {
            XDocument xdoc = XDocument.Load(filePath);
            return xdoc.Descendants("Car")
                       .Select(x => new Car
                       {
                           Name = (string)x.Element("Name"),
                           Group = (string)x.Element("Group")
                       }).ToList();
        }

        public List<Car> ReadJsonData(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Car>>(jsonString);
        }

        public List<Car> CombineXmlAndJsonData(string xmlFilePath, string jsonFilePath)
        {
            // Lire les données depuis les fichiers XML et JSON
            List<Car> xmlData = ReadXmlData(xmlFilePath);
            List<Car> jsonData = ReadJsonData(jsonFilePath);

            // Combiner les données en une seule liste
            List<Car> combinedData = xmlData.Concat(jsonData).ToList();

            return combinedData;
        }

        public List<CarGroup> GroupCarsByGroup(List<Car> cars)
        {
            // Regrouper les voitures par groupe
            var carGroups = cars.GroupBy(car => car.Group)
                                .Select(group => new CarGroup
                                {
                                    GroupName = group.Key,
                                    Cars = group.ToList()
                                }).ToList();

            return carGroups;
        }
    }
}
