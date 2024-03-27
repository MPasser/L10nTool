using QFramework;

namespace Ressap.L10nTool {
    public interface ISettingsModel : IModel {
        BindableProperty<string> WorkDir { get; }
    }

    public struct SettingsModelPoJo {
        public string WorkDir = string.Empty;
        public SettingsModelPoJo() {
        }
    }

    public class DefaultSettingsModel : AbstractModel, ISettingsModel {
        public BindableProperty<string> WorkDir { get; } = new BindableProperty<string>();

        private const string save_path = "user://Saves/Settings.json";
        private const string data_key = nameof(DefaultSettingsModel);

        private IStorageUtility storageUtility;

        private SettingsModelPoJo dataMap;
        protected override void OnInit() {
            storageUtility = this.GetUtility<IStorageUtility>();

            loadDatas();
            registerSaveMethods();
        }

        public void registerSaveMethods() {
            WorkDir.Register((val) => {
                dataMap.WorkDir = val;
                storageUtility.Save(dataMap, save_path);
            });
        }

        private void loadDatas() {
            dataMap = storageUtility.Load<SettingsModelPoJo>(save_path, new SettingsModelPoJo());
            WorkDir.Value = dataMap.WorkDir;
        }
    }
}