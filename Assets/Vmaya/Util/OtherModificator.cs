using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Util
{
    [ExecuteInEditMode]
    public class OtherModificator : MonoBehaviour
    {
        public interface IFollower
        {
            float[] getLimits();
            void setOMValue(float value);
        }

        [SerializeField]
        private List<Component> follower;
        public IFollower Follower(int index) {
            return follower[index].GetComponent<IFollower>();
        }

        [SerializeField]
        private float index;
        private float _index;

        public bool takeOffLimitFixation;
        public bool takeOff;

        private List<float[]> _limits;

        private void OnValidate()
        {
            for (int i=0; i<follower.Count; i++)
                follower[i] = Follower(i) as Component;

            if (follower.Count == 0)
            {
                IFollower af = GetComponent<IFollower>();
                if (af != null) follower.Add(af as Component);
            }

            _limits = new List<float[]>();
            for (int i = 0; i < follower.Count; i++) _limits.Add(null);
        }

        private void Awake()
        {
            OnValidate();
        }

        virtual protected void Update()
        {
            for (int i = 0; i<follower.Count; i++)
            {
                if (!takeOff && takeOffLimitFixation) _limits[i] = Follower(i).getLimits();
                else if (takeOff && (_limits[i] != null))
                {
                    takeOffLimitFixation = false;
                    Follower(i).setOMValue(_limits[i][0] + (_limits[i][1] - _limits[i][0]) * Mathf.Clamp(index, 0, 1));
                }
            }
        }
    }
}