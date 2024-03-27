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
            makeSureFile(field);
            FileAccess fa = FileAccess.Open(field, FileAccess.ModeFlags.Read);
            if (null == fa) {
                GD.Print($"File {field} not exists.");
                return default;
            }
            string jsonStr = fa.GetAsText();
            GD.Print($"Load jsonStr = {jsonStr}");
            if (string.IsNullOrEmpty(jsonStr)) {
                return defaultVal;
            }
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public void Save<T>(T val, string field) {
            string jsonStr = JsonConvert.SerializeObject(val);

            GD.Print($"Save jsonStr = {jsonStr}");

            makeSureFile(field);
            using (FileAccess fa = FileAccess.Open(field, FileAccess.ModeFlags.WriteRead)) {
                fa.StoreString(jsonStr);
            }
        }

        private void makeSureFile(string filePath) {
            if (filePath.StartsWith("user://")) {
                string userPath = ProjectSettings.GlobalizePath("user://");
                filePath = System.IO.Path.Combine(userPath, filePath.TrimStart("user://".ToCharArray()));
            } else if (filePath.StartsWith("res://")) {
                string userPath = ProjectSettings.GlobalizePath("res://");
                filePath = System.IO.Path.Combine(userPath, filePath.TrimStart("res://".ToCharArray()));
            }

            if (System.IO.Directory.Exists(filePath)) {
                GD.PrintErr($"{filePath} is a directory, can not save data here.");
            }

            string directoryPath = System.IO.Path.GetDirectoryName(filePath);
            // GD.Print($"filePath = {filePath}");
            // GD.Print($"directoryPath = {directoryPath}");
            if (!System.IO.Directory.Exists(directoryPath)) {
                System.IO.Directory.CreateDirectory(directoryPath);
            }
            if (!FileAccess.FileExists(filePath)) {
                FileAccess.Open(filePath, FileAccess.ModeFlags.WriteRead);
            }
        }
    }
}