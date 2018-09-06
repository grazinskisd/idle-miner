using System.IO;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class MineParametersLoader
    {
        private const string PATH_FORMAT = "{0}/StreamingAssets/{1}";
        private const string FILENAME_FORMAT = "{0}.json";

        public void SaveParameters(MineParametersScriptalbe parameters)
        {
            var json = JsonUtility.ToJson(parameters);
            WriteDataToFile(json, string.Format(FILENAME_FORMAT, parameters.MineName));
            //parameters.WarehouseParams.InstanceID = parameters.WarehouseParams.GetInstanceID();
            //WriteDataToFile(JsonUtility.ToJson(parameters.WarehouseParams), string.Format(FILENAME_FORMAT, "Warehouse"));

        }

        public void LoadParameters(string fileName)
        {

        }

        public static void WriteDataToFile(string jsonString, string fileName)
        {
            string path = string.Format(PATH_FORMAT, Application.dataPath, fileName);
            File.WriteAllText(path, jsonString);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}
