using QFramework;

namespace Ressap.L10nTool {
    public interface ISettingsModel : IModel {
        BindableProperty<string> WorkDir { get; }
    }


    public class DefaultSettingsModel : AbstractModel, ISettingsModel {
        public BindableProperty<string> WorkDir { get; } = new BindableProperty<string>();

        private const string save_path = Const.CONFIG_SAVE_PATH;
        private const string data_key = nameof(DefaultSettingsModel);

        private IStorageUtility storageUtility;

        protected override void OnInit() {
            storageUtility = this.GetUtility<IStorageUtility>();

            loadSettings();
            registerSaveMethods();
        }

        private void loadSettings() {
            WorkDir.Value = storageUtility.Load<string>(nameof(WorkDir), save_path);
        }

        public void registerSaveMethods() {
            WorkDir.Register((val) => {
                storageUtility.Save(nameof(WorkDir), val, save_path);
            });
        }
    }
}