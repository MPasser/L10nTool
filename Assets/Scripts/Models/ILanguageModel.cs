using System.Collections.Generic;
using System.IO;
using QFramework;

namespace Ressap.L10nTool {
    public interface ILanguageModel : IModel {
        void LoadDataFromDir(string path);
    }


    public class DefaultLanguageModel : AbstractModel, ILanguageModel {
        private const string keys_filename = "Keys.txt";

        private const string txt_dir_name = "txtDir";

        private const string english_filename = "English.txt";

        private Dictionary<int, string> keyMap;
        private Dictionary<int, string> englishMap;


        protected override void OnInit() {
        }

        public void LoadDataFromDir(string dir) {
            Directory.CreateDirectory(dir);
            Directory.CreateDirectory(Path.Combine(dir, txt_dir_name));

            loadKeyMap(dir);
            loadEnglish(dir);
        }

        private void loadKeyMap(string dir) {
            string keysFilePath = Path.Combine(dir, keys_filename);
            UnityEngine.Debug.Log($"keysFilePath = {keysFilePath}");

            makeSureFileExist(keysFilePath);

            keyMap = new Dictionary<int, string>();

            loadDataMapToMap(dir, keyMap);
        }

        private void loadEnglish(string dir) {
            string englishFilePath = Path.Combine(dir, txt_dir_name, english_filename);
            UnityEngine.Debug.Log($"englishFilePath = {englishFilePath}");

            englishMap = new Dictionary<int, string>();

            if (makeSureFileExist(englishFilePath)) {
                loadDataMapToMap(englishFilePath, englishMap);

                if (englishMap.Count != keyMap.Count) {
                    UnityEngine.Debug.LogError($"{nameof(loadEnglish)}: Data dismatch, there are {keyMap.Count} keys, {englishMap.Count} english items.");
                    // correctDataMapKey(englishMap);
                }
            } else {
                correctDataMapKey(englishMap);
            }
        }

        private void correctDataMapKey(Dictionary<int, string> dataMap) {
            List<int> deleteKeys = new List<int>();
            foreach (var key in dataMap.Keys) {
                if (!keyMap.ContainsKey(key)) {
                    deleteKeys.Add(key);
                }
            }

            foreach (var dk in deleteKeys) {
                dataMap.Remove(dk);
            }

            foreach (var key in keyMap.Keys) {
                if (!dataMap.ContainsKey(key)) {
                    dataMap.Add(key, string.Empty);
                }
            }
        }

        private bool makeSureFileExist(string filePath) {
            bool isFileAlreadyExist = true;
            if (!File.Exists(filePath)) {
                using (File.Create(filePath)) { }
                isFileAlreadyExist = false;
            }

            return isFileAlreadyExist;
        }

        private void loadDataMapToMap(string filePath, Dictionary<int, string> idxItemMap) {
            int index = 0;
            try {
                foreach (string line in File.ReadLines(filePath)) {
                    if (string.IsNullOrEmpty(line) || Const.WORD_DELIMETER.Equals(line)) {
                        continue;
                    }
                    idxItemMap[index++] = line;
                    // UnityEngine.Debug.Log($"{line}");
                }
            } catch (System.Exception ex) {
                UnityEngine.Debug.LogError($"The file could not be read: {ex.Message}");
            }
        }

        private void saveDataMapToFile(Dictionary<int, string> dataMap, string filename) {
            makeSureFileExist(filename);

            try {
                using (StreamWriter writer = new StreamWriter(english_filename)) {
                    for (int i = 0; i < dataMap.Count; i++) {
                        writer.WriteLine(dataMap[i]);

                        if (i < dataMap.Count - 1) {
                            writer.WriteLine(Const.WORD_DELIMETER);
                        }
                    }
                }
            } catch (System.Exception ex) {
                UnityEngine.Debug.LogError($"Error when writing data map to file [{filename}]：" + ex.Message);
            }
        }
    }
}