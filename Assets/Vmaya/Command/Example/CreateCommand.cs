
using UnityEngine;

namespace Vmaya.Command {
    public class CreateCommand : ICommand
    {
        private Transform _object;
        private CommandExample _source;
        private Vector3 _position;
        private Vector3 _direct;
        public CreateCommand(CommandExample a_source, Vector3 a_position, Vector2 a_direct)
        {
            _source = a_source;
            _position = a_position;
            _direct = a_direct;
        }

        public string commandName()
        {
            return "Create object";
        }

        public void destroy()
        {
            if (_object) GameObject.Destroy(_object.gameObject);
        }

        public bool execute()
        {
            _object = _source.createObject();
            _object.position = _position;
            _object.rotation = Quaternion.LookRotation(_direct);
            return true;
        }

        public float executeTime()
        {
            return 0;
        }

        public bool isReady()
        {
            return true;
        }

        public void redo()
        {
            execute();
        }

        public void undo()
        {
            GameObject.Destroy(_object.gameObject);
            _object = null;
        }
    }
}