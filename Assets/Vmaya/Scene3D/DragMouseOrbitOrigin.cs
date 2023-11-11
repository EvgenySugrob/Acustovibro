using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Vmaya.Scene3D.UI;
using Vmaya.UI.Components;
using InputControl = Vmaya.Scene3D.UI.InputControl;

namespace Vmaya.Scene3D
{
    public class DragMouseOrbitOrigin : MonoBehaviour
    {
        //public interface I

        [SerializeField]
        private Transform target;
        public Transform Target => target;
        public float distance = 5.0f;
        public float xSpeed = 100.0f;
        public float ySpeed = 100.0f;
        public float yMinLimit = -90f;
        public float yMaxLimit = 90f;
        public float xMinLimit = 0f;
        public float xMaxLimit = 0f;
        public float distanceMin = 1f;
        public float distanceMax = 10f;
        public float smoothTime = 15f;
        public float ScrollWheel = 0.2f;
        public float backRotate = 0;
        public float mouseThreshold = 1;
        public bool useOnlyRightButton = false;
        public Vector2 startPulse = Vector2.zero;

        float rotationYAxis = 0.0f;
        float rotationXAxis = 0.0f;
        Vector2 velocity = Vector2.zero;
        int xAroundKey = 0;
        int yAroundKey = 0;
        float _distance;
        float _xdown;

        private bool _prevDown = false;
        private bool _downSpace = false;
        private int _dragState;

        private Quaternion rotation;

        [HideInInspector]
        public bool altRot;

        private Vector3 _startMouse;
        private Vector3 _lastMouse;

        void Start()
        {
            if (target == null)
                Debug.LogError("Target must be definitely");

            velocity += new Vector2(startPulse.x, startPulse.y);
            if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().freezeRotation = true;
        }

        private void OnEnable()
        {
            updateFromTarget();
        }
        private void aroundFromMouse()
        {
            if (!altRot || VKeyboard.GetKey(Key.LeftAlt) || VMouse.GetMouseButton(1))
            {
                Vector3 delta = VMouse.mousePosition - _lastMouse;
                velocity += new Vector2(xSpeed * delta.x * 0.02f, ySpeed * delta.y * 0.02f);
            }
        }

        public void setRotate(float rot)
        {
            _xdown = rot;
        }

        void getInput()
        {
            if (_downSpace && !isFocusDragable) aroundFromMouse();
            else if (_xdown != 0) velocity += new Vector2(xSpeed * _xdown * 0.02f, 0);
            else
            if (!EventSystem.current.IsPointerOverGameObject() && !Curtain.isModal)
            {
                if (VKeyboard.GetKeyDown(Key.LeftArrow)) xAroundKey = -1;
                else if (VKeyboard.GetKeyDown(Key.RightArrow)) xAroundKey = 1;
                else if (VKeyboard.GetKeyUp(Key.LeftArrow) || VKeyboard.GetKeyUp(Key.RightArrow)) xAroundKey = 0;

                if (VKeyboard.GetKeyDown(Key.UpArrow)) yAroundKey = -1;
                else if (VKeyboard.GetKeyDown(Key.DownArrow)) yAroundKey = 1;
                else if (VKeyboard.GetKeyUp(Key.UpArrow) || VKeyboard.GetKeyUp(Key.DownArrow)) yAroundKey = 0;
            }
            else
            {
                xAroundKey = 0;
                yAroundKey = 0;
            }
        }

        public void setTarget(Transform a_target)
        {
            if (enabled = (target = a_target) != null) updateFromTarget();
        }

        public void updateFromTarget()
        {
            transform.LookAt(target.position, Vector3.up);
            distance = _distance = Mathf.Clamp((target.position - transform.position).magnitude, distanceMin, distanceMax);

            Vector3 angles = transform.eulerAngles;
            rotationYAxis = angles.y;
            rotationXAxis = angles.x;

            while ((xMaxLimit != 0) && (rotationYAxis > xMaxLimit)) rotationYAxis = rotationYAxis - 360;
            while ((xMinLimit != 0) && (rotationYAxis < xMinLimit)) rotationYAxis = rotationYAxis + 360;

            while ((yMaxLimit != 0) && (rotationXAxis > yMaxLimit)) rotationXAxis = rotationXAxis - 360;
            while ((yMinLimit != 0) && (rotationXAxis < yMinLimit)) rotationXAxis = rotationXAxis + 360;

            rotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            xAroundKey = 0;
            yAroundKey = 0;

            velocity = Vector3.zero;
        }

        private bool isFocusDragable => hitDetector.Down ? hitDetector.Down.isDrag() : false;

        public void setRotation(Vector2 r)
        {
            rotationYAxis = r.x;
            rotationXAxis = r.y;
        }

        public void setRotation(Quaternion rotate)
        {
            rotationYAxis = rotate.eulerAngles.y;
            rotationXAxis = rotate.eulerAngles.x;
        }

        public Vector2 getRotation()
        {
            return new Vector2(rotationYAxis, rotationXAxis);
        }

        void LateUpdate()
        {
            if (target)
            {

                if (FreezerList.instance.isFreeze()) return;

                bool _curDown = (!useOnlyRightButton && VMouse.GetMouseButton(0)) || VMouse.GetMouseButton(1);
                if (_prevDown != _curDown)
                {
                    _downSpace = !EventSystem.current.IsPointerOverGameObject() &&
                                 !(hitDetector.Focus is IDragControl) && 
                                 _curDown;
                    if (_prevDown = _curDown)
                    {
                        _startMouse = VMouse.mousePosition;
                        _dragState = 1;
                    }
                    else _dragState = 0;
                }

                if (!InputControl.isFocus)
                {
                    if ((_dragState == 1) && ((VMouse.mousePosition - _startMouse).magnitude >= mouseThreshold))
                        _dragState = 2;
                    if (_dragState == 2) 
                        getInput();

                    if (!EventSystem.current.IsPointerOverGameObject())
                        distance = Mathf.Clamp(distance - VMouse.ScrollWheel * ScrollWheel * Time.deltaTime, distanceMin, distanceMax);
                }
                else velocity += new Vector2(backRotate, 0);


                velocity += new Vector2(xAroundKey * Settings.calcSens(xSpeed, xSpeed) * Time.deltaTime,
                                        yAroundKey * Settings.calcSens(ySpeed, ySpeed) * distance * Time.deltaTime);

                rotationYAxis += velocity.x * Time.deltaTime;
                rotationXAxis += velocity.y * Time.deltaTime;

                if ((xMinLimit != 0) && (rotationYAxis < xMinLimit))
                {
                    rotationYAxis = xMinLimit;
                    velocity.x = 0;
                }

                if ((xMaxLimit != 0) && (rotationYAxis > xMaxLimit))
                {
                    rotationYAxis = xMaxLimit;
                    velocity.x = 0;
                }

                if (rotationXAxis < yMinLimit)
                {
                    rotationXAxis = yMinLimit;
                    velocity.y = 0;
                }

                if (rotationXAxis > yMaxLimit)
                {
                    rotationXAxis = yMaxLimit;
                    velocity.y = 0;
                }

                rotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);

                float stime = smoothTime * Time.deltaTime;
                _distance += (distance - _distance) * stime;

                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -_distance) + target.position;

                transform.rotation = rotation;
                transform.position = position;

                velocity -= velocity * stime;

                Camera cam = GetComponent<Camera>();
                if (cam && cam.orthographic)
                    cam.orthographicSize = (position - target.position).magnitude * 0.3f;

                _lastMouse = VMouse.mousePosition;
            }
        }

        [System.Serializable]
        public class SavedData
        {
            public float focalLength;
            public Limit YLimit;
            public Limit XLimit;
            public Limit DistanceLimit;
            public float distance;
            public float rotationXAxis;
            public float rotationYAxis;
            public Vector3 position;
            public Vector3 targetPosition;
            public Indent targetIndent;
        }

        public SavedData savedData()
        {
            SavedData result = new SavedData();
            result.YLimit = Limit.init(yMinLimit, yMaxLimit);
            result.XLimit = Limit.init(xMinLimit, xMaxLimit);
            result.rotationXAxis = rotationXAxis;
            result.rotationYAxis = rotationYAxis;
            result.DistanceLimit = Limit.init(distanceMin, distanceMax);
            result.distance = distance;
            result.position = transform.position;
            result.targetIndent = new Indent(target);
            result.targetPosition = target.position;

            return result;
        }

        public void restoreData(SavedData data)
        {
            yMinLimit = data.YLimit.min;
            yMaxLimit = data.YLimit.max;
            xMinLimit = data.XLimit.min;
            xMaxLimit = data.XLimit.max;
            distanceMin = data.DistanceLimit.min;
            distanceMax = data.DistanceLimit.max;

            rotationXAxis = data.rotationXAxis;
            rotationYAxis = data.rotationYAxis;
            transform.position = data.position;

            distance = _distance = data.distance;

            target          = Indent.Find<Transform>(data.targetIndent);
            target.position = data.targetPosition;

            rotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            xAroundKey = 0;
            yAroundKey = 0;

            velocity = Vector3.zero;
        }
    }
}