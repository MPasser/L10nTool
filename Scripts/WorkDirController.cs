using Godot;
using QFramework;

namespace Ressap.L10nTool {
    public partial class WorkDirController : Node, IController {

        [Export] private Label txtWorkDir;
        [Export] private Button btnChangeWorkDir;

        public override void _Ready() {
            btnChangeWorkDir.Pressed += OnBtnChangeWorkDirClick;
            updateWorkDirText();
        }

        private void OnBtnChangeWorkDirClick() {
            FileDialog fileDialog = new FileDialog();

            fileDialog.FileMode = FileDialog.FileModeEnum.OpenDir;
            fileDialog.Access = FileDialog.AccessEnum.Filesystem;

            fileDialog.DirSelected += OnDirSelected;

            AddChild(fileDialog);

            fileDialog.PopupCentered(new Vector2I(1280, 720));
        }


        private void OnDirSelected(string dir) {
            this.SendCommand<ChangeWorkDirCmd>(new ChangeWorkDirCmd(dir));
            updateWorkDirText();
        }

        private void updateWorkDirText() {
            txtWorkDir.Text = this.GetModel<ISettingsModel>().WorkDir.Value;
        }

        public IArchitecture GetArchitecture() {
            return L10nToolApp.Interface;
        }
    }
}

