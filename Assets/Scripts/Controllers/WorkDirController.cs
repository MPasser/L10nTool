using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Ressap.L10nTool {
    public partial class MainPanel : MonoBehaviour, IController {

        [SerializeField] private Text txtWorkDir;
        [SerializeField] private Button btnChangeWorkDir;

        private void Awake() {
            btnChangeWorkDir.onClick.AddListener(OnBtnChangeWorkDirClick);
            updateWorkDirText();
        }

        private void OnBtnChangeWorkDirClick() {
            // FileDialog fileDialog = new FileDialog();

            // fileDialog.FileMode = FileDialog.FileModeEnum.OpenDir;
            // fileDialog.Access = FileDialog.AccessEnum.Filesystem;

            // fileDialog.DirSelected += OnDirSelected;

            // AddChild(fileDialog);

            // fileDialog.PopupCentered(new Vector2I(1280, 720));
        }


        private void OnDirSelected(string dir) {
            this.SendCommand<ChangeWorkDirCmd>(new ChangeWorkDirCmd(dir));
            updateWorkDirText();
        }

        private void updateWorkDirText() {
            txtWorkDir.text = this.GetModel<ISettingsModel>().WorkDir.Value;
        }

        public IArchitecture GetArchitecture() {
            return L10nToolApp.Interface;
        }
    }
}

