using UnityEngine;

namespace Vmaya.Command
{
    public class RotateCommand : BaseCommand
    {
        [SerializeField]
        private Transform transform;
        [SerializeField]
        private Quaternion backRotate;
        [SerializeField]
        private Quaternion applyRotate;

        public RotateCommand()
        {

        }

        public RotateCommand(Transform a_trans, Quaternion a_backRotate, Quaternion a_newRotate)
        {
            transform = a_trans;
            backRotate = a_backRotate;
            applyRotate = a_newRotate;
        }

        public RotateCommand(Transform a_trans, Quaternion rotate)
        {
            transform = a_trans;
            backRotate = transform.localRotation;
            applyRotate = rotate;
        }

        public override string commandName()
        {
            return "Вращение";
        }

        public override void destroy()
        {
            transform = null;
        }

        public override bool execute()
        {
            transform.localRotation = applyRotate;
            return true;
        }

        public override bool isReady()
        {
            return true;
        }

        public override void redo()
        {
            transform.localRotation = applyRotate;
        }

        public override void undo()
        {
            transform.localRotation = backRotate;
        }
    }
}