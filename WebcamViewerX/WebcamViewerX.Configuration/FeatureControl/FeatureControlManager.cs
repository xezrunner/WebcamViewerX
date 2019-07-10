using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebcamViewerX.Configuration.FeatureControl
{
    public class FeatureControlManager
    {
        Utilities Utilities = new Utilities();

        public string GetPathToJSON()
        {
            return Utilities.GetPathForConfigurationFile("FeatureControl.json");
        }

        public string ReadJSON()
        {
            return File.ReadAllText(GetPathToJSON());
        }

        public List<FeatureGroup> GetFeatureGroups()
        {
            string file = File.ReadAllText(GetPathToJSON());
            return JsonConvert.DeserializeObject<List<FeatureGroup>>(file);
        }

        public void ChangeFeatureValue(string universeName, string featureName, object newValue)
        {
            // Read the JSON
            string json = File.ReadAllText(GetPathToJSON());

            // Deserialize JSON into a List<FeatureGroup>
            List<FeatureGroup> groups = JsonConvert.DeserializeObject<List<FeatureGroup>>(json);

            // Get the ID for the group we're targetting
            int targetGroupID = 0;
            foreach (FeatureGroup group in groups)
            {
                if (group.Universe == universeName)
                    break;
                else
                    targetGroupID++;
            }

            // Get the ID for the feature we're targetting
            int targetFeatureID = 0;
            foreach (Feature feature in groups[targetGroupID].Features)
            {
                if (feature.DevName == featureName || feature.Name == featureName)
                    break;
                else
                    targetFeatureID++;
            }

            // Change the value of the feature
            groups[targetGroupID].Features[targetFeatureID].Value = newValue;

            // Re-serialize the JSON
            using (StreamWriter file = File.CreateText(GetPathToJSON()))
            {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, groups);
            }
        }

        public void DEBUG_CreateFeatureConfig()
        {
            List<FeatureGroup> grouplist = new List<FeatureGroup>();
            FeatureGroup group = new FeatureGroup() { Universe = "TestUniverse" };
            FeatureGroup group2 = new FeatureGroup() { Universe = "UniverseTest" };
            List<Feature> featurelist = new List<Feature>();

            for (int i = 0; i <= 10; i++)
            {
                Feature newfeature = new Feature()
                {
                    Name = "Test feature #" + i,
                    DevName = "TestFeature" + i,
                    Category = Feature.Categories.Experiment,
                    Value = true
                };

                featurelist.Add(newfeature);
            }

            group.Features = featurelist;
            group2.Features = featurelist;
            grouplist.Add(group);
            grouplist.Add(group2);

            using (StreamWriter file = File.CreateText(GetPathToJSON()))
            {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, grouplist);
            }
        }
    }
}
