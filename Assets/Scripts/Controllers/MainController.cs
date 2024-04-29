using QFramework;
using UnityEngine;

namespace Ressap.L10nTool {
    public partial class MainController : MonoBehaviour, IController {
        private void Awake(){
            string workDir = this.GetModel<ISettingsModel>().WorkDir.Value;
            this.GetModel<ILanguageModel>().LoadDataFromDir(workDir);
        }


        public IArchitecture GetArchitecture() {
            return L10nToolApp.Interface;
        }
    }
}
