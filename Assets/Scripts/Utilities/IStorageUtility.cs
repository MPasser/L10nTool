using QFramework;

namespace Ressap.L10nTool {
    public interface IStorageUtility : IUtility {

        void Save<T>(string key, T val, string field);
        T Load<T>(string key, string field, T defaultVal = default);
    }

    public class ResourceStorageUtility : IStorageUtility {

        public T Load<T>(string key, string field, T defaultVal = default) {
            return ES3.Load<T>(key, field, defaultVal);
        }

        public void Save<T>(string key, T val, string field) {
            ES3.Save<T>(key, val, field);
        }
    }
}