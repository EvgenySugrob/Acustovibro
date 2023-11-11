using UnityEngine;
using Vmaya.Language;
using Vmaya.Scene3D;

namespace Vmaya.Command
{
    public class DragDropCommand : BaseCommand
    {
        [SerializeField]
        private Vector3 _startPosition;
        [SerializeField]
        private Vector3 _finalPosition;
        [SerializeField]
        private Quaternion _startRotate;
        [SerializeField]
        private Quaternion _finalRotate;
        [SerializeField]
        private Indent _transformIndent;

        public IPositioned transform  => Indent.Find(_transformIndent) as IPositioned;

        public DragDropCommand()
        {
        }

        public override bool isReady()
        {
            return transform != null;
        }

        public DragDropCommand(IPositioned a_transform)
        {
            _transformIndent = Indent.New(a_transform as Component);
            _startPosition  = transform.getPosition();
            _startRotate = transform.getRotate();
        }

        public virtual void saveFinalPosition()
        {
            _finalPosition = transform.getPosition();
            _finalRotate = transform.getRotate();
        }

        public override string commandName()
        {
            return Lang.instance.get("Moving {0}", _transformIndent.Name);
        }

        public override void destroy()
        {
        }

        public override bool execute()
        {
            transform.setPosition(_finalPosition, _finalRotate);
            return true;
        }

        public override void redo()
        {
            execute();
        }

        public override void undo()
        {
            transform.setPosition(_startPosition, _startRotate);
        }
    }
}
