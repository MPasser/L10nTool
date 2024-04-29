using QFramework;

namespace Ressap.L10nTool {
    public class L10nToolApp : Architecture<L10nToolApp> {
        protected override void Init() {
            this.RegisterUtility<IStorageUtility>(new ResourceStorageUtility());

            this.RegisterModel<ISettingsModel>(new DefaultSettingsModel());
            this.RegisterModel<ILanguageModel>(new DefaultLanguageModel());
        }
    }
}