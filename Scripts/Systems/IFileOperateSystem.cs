using QFramework;

namespace Ressap.L10nTool {
    public interface IFileOperateSystem : ISystem {

    }

    public class DefaultOperateSystem : AbstractSystem, IFileOperateSystem {
        protected override void OnInit() {

            this.RegisterEvent<ChangeWorkDirEvt>(OnChangeWorkDir);
        }


        private void OnChangeWorkDir(ChangeWorkDirEvt evt) {

        }
    }
}