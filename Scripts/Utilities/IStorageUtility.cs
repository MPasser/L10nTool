using System.IO;
using Godot;
using Newtonsoft.Json;
using QFramework;

namespace Ressap.L10nTool {
    public interface IStorageUtility : IUtility {

        void Save<T>(T val, string field);
        T Load<T>(string field, T defaultVal = default);
    }

    public class ResourceStorageUtility : IStorageUtility {

        public T Load<T>(string field, T defaultVal = default) {
            string filePath = transFilePath(field);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using FileStream fs = new(filePath, FileMode.OpenOrCreate);
            using BinaryReader br = new(fs);

            string jsonStr = null;

            try {
                jsonStr = br.ReadString();
            } catch (EndOfStreamException) {
                GD.Print($"File {filePath} is empty.");
            }
            if (string.IsNullOrEmpty(jsonStr)) {
                return defaultVal;
            }
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public void Save<T>(T val, string field) {
            string filePath = transFilePath(field);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            string jsonStr = JsonConvert.SerializeObject(val);
            GD.Print($"Save jsonStr = {jsonStr}");

            using FileStream fs = new(filePath, FileMode.Create);
            using BinaryWriter bw = new(fs);

            bw.Write(jsonStr);
        }

        private string transFilePath(string filePath) {
            if (filePath.StartsWith("user://")) {
                string userPath = ProjectSettings.GlobalizePath("user://");
                return Path.Combine(userPath, filePath.TrimStart("user://".ToCharArray()));
            } else if (filePath.StartsWith("res://")) {
                string resPath = ProjectSettings.GlobalizePath("res://");
                return Path.Combine(resPath, filePath.TrimStart("res://".ToCharArray()));
            } else {
                return filePath;
            }
        }
    }
}