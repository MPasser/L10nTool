using QFramework;

namespace Ressap.L10nTool {
    public class ChangeWorkDirCmd : AbstractCommand {
        private string workDir;

        public ChangeWorkDirCmd(string workDir) {
            this.workDir = workDir;
        }

        protected override void OnExecute() {
            if (!workDir.Equals(this.GetModel<ISettingsModel>().WorkDir.Value)) {
                this.GetModel<ISettingsModel>().WorkDir.Value = workDir;
                this.SendEvent<ChangeWorkDirEvt>(new ChangeWorkDirEvt(workDir));
            }
        }
    }

    public class ChangeWorkDirEvt {
        public string WorkDir;

        public ChangeWorkDirEvt(string workDir) {
            WorkDir = workDir;
        }
    }
}