using Godot;
using QFramework;

namespace Ressap.L10nTool {
    public partial class WorkDirController : Node, IController {

        [Export] private Label txtWorkDir;
        [Export] private Button btnChangeWorkDir;

        public string WorkDir {
            get {
                return workDir;
            }
            set {
                if (null != value && !value.Equals(workDir)) {
                    workDir = value;
                    txtWorkDir.Text = value;
                }
            }
        }

        private string workDir;

        public override void _Ready() {
            btnChangeWorkDir.Pressed += OnBtnChangeWorkDirClick;
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
            WorkDir = dir;
            GD.Print($"dir = {dir}");
        }

        public IArchitecture GetArchitecture() {
            return L10nToolApp.Interface;
        }


    }
}

