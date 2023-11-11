//===========================================================================//
//                       FreeFlyCamera (Version 1.2)                         //
//(c) 2019 Sergey Stafeyev, 2021 modify Vadim Folov                          //
//===========================================================================//


using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Vmaya.Scene3D;
using Vmaya.Scene3D.UI;
using Vmaya.UI.Components;

namespace Vmaya.Util
{
    [RequireComponent(typeof(Camera))]
    public class FreeFlyCamera : MonoBehaviour, IPositioned
    {
        #region UI

        [Space]

        [SerializeField]
        [Tooltip("Camera rotation by mouse movement is active")]
        private bool _enableRotation = true;

        [SerializeField]
        [Tooltip("Sensitivity of mouse rotation")]
        private float _mouseSense = 4f;

        [Space]

        [SerializeField]
        [Tooltip("Camera zooming in/out by 'Mouse Scroll Wheel' is active")]
        private bool _enableTranslation = true;

        [SerializeField]
        [Tooltip("Velocity of camera zooming in/out")]
        private float _translationSpeed = 0.04f;

        [Space]

        [SerializeField]
        [Tooltip("Camera movement by 'W','A','S','D','Q','E' keys is active")]
        private bool _enableMovement = true;

        [SerializeField]
        [Tooltip("Camera movement speed")]
        private float _movementSpeed = 10f;

        [SerializeField]
        [Tooltip("Speed of the quick camera movement when holding the 'Left Shift' key")]
        private float _boostedSpeed = 10f;

        [SerializeField]
        [Tooltip("Boost speed")]
        private Key _boostSpeed = Key.LeftShift;

        [SerializeField]
        [Tooltip("Move up")]
        private Key _moveUp = Key.E;

        [SerializeField]
        [Tooltip("Move down")]
        private Key _moveDown = Key.Q;

        [Space]

        [SerializeField]
        [Tooltip("Acceleration at camera movement is active")]
        private bool _enableSpeedAcceleration = true;

        [SerializeField]
        [Tooltip("Rate which is applied during camera movement")]
        private float _speedAccelerationFactor = 1.5f;


        #endregion UI


        public bool IgnoreOverGUI;

        //private CursorLockMode _wantedMode;

        private float _currentIncrease = 1;
        private float _currentIncreaseMem = 0;

        protected Quaternion _setRotation;
        protected Vector3 _setPosition;

        [SerializeField]
        [Range(0, 1)]
        private float smooth = 0.5f;

        private bool _mouseButtonDown;
        private Transform _seekTo;
        private Vector3 _seekToStartPos;
        private Quaternion _seekToStartQuat;
        private float _seekToIndex;
        private Vector3 _lastMouse;
        protected Vector3 mouseDelta => (VMouse.mousePosition - _lastMouse) * Time.deltaTime;

        [SerializeField]
        private float _seekToSpeed;

        [SerializeField]
        private float _seekToUp;

        [SerializeField]
        private EasingFunction.Ease _seekMethod;

        public UnityEvent onChangePosition;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_boostedSpeed < _movementSpeed)
                _boostedSpeed = _movementSpeed;
        }
#endif

        private Camera _camera => GetComponent<Camera>();

        private void Awake()
        {

        }

        private void OnEnable()
        {
            //_wantedMode = CursorLockMode.Locked;
            _setRotation = transform.rotation;
            _setPosition = transform.position;
        }

        /*// Apply requested cursor state
        private void SetCursorState()
        {
            if (VKeyboard.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = _wantedMode = CursorLockMode.None;
            }

            if (VMouse.GetMouseButtonDown(1))
                _wantedMode = CursorLockMode.Locked;

            // Apply cursor state
            Cursor.lockState = _wantedMode;
            // Hide cursor when locking
            Cursor.visible = (CursorLockMode.Locked != _wantedMode);
        }*/

        private void CalculateCurrentIncrease(bool moving)
        {
            _currentIncrease = Time.deltaTime;

            if (!_enableSpeedAcceleration || _enableSpeedAcceleration && !moving)
            {
                _currentIncreaseMem = 0;
                return;
            }

            _currentIncreaseMem += Time.deltaTime * (_speedAccelerationFactor - 1);
            _currentIncrease = Time.deltaTime + Mathf.Pow(_currentIncreaseMem, 3) * Time.deltaTime;
        }

        private void Update()
        {
            if (_seekToIndex > 0) seekToProcess();
            else
            {
                if ((!IgnoreOverGUI && hitDetector.isOverGUI()) || Curtain.isModal || (CameraManager.getCurrent() != _camera) || isFreeze)
                    _mouseButtonDown = false;

                if ((!Curtain.isModal || isFreeze) && (_camera == Camera.main) && !Scene3D.UI.InputControl.isFocus) defaultMode();
            }
            _lastMouse = VMouse.mousePosition;
        }

        private void seekToProcess()
        {
            _seekToIndex -= _seekToSpeed;
            if (_seekToIndex > 0)
                seekToSet(_seekToIndex);
            else
            {
                _seekToIndex = 0;
                seekToSet(0);
            }
        }

        private void seekToSet(float index)
        {
            float di = EasingFunction.GetEasingFunction(_seekMethod)(0, 1, index);
            transform.rotation = _setRotation = Quaternion.Lerp(_seekTo.rotation, _seekToStartQuat, di);

            setPosition(Vector3.Lerp(_seekTo.position, _seekToStartPos, di) + new Vector3(0, _seekToUp * (di < 0.5 ? di : 1 - di), 0));
            transform.position = _setPosition;
        }

        private bool isFreeze => FreezerList.instance.isFreeze();

        public float AxisVertical => getAxisVertical();

        public float AxisHorizontal => getAxisHorizontal();

        private float getAxisHorizontal()
        {
            return VKeyboard.GetKey(Key.D) ? 1 : (VKeyboard.GetKey(Key.A) ? -1 : 0);
        }

        private float getAxisVertical()
        {
            return VKeyboard.GetKey(Key.W) ? 1 : (VKeyboard.GetKey(Key.S) ? -1 : 0);
        }

        protected virtual void defaultMode()
        {

            Vector3 deltaPosition = Vector3.zero;
            Rigidbody rb = GetComponent<Rigidbody>();

            float msw = VMouse.ScrollWheel;
            if (_enableTranslation && (msw != 0) && !hitDetector.isOverGUI())
            {
                if (rb) deltaPosition += transform.forward * msw * _translationSpeed;
                else setPosition(_setPosition + transform.forward * msw * Time.deltaTime * _translationSpeed);
            }


            if (VMouse.GetMouseButtonDown(1) && !hitDetector.isOverGUI())
                _mouseButtonDown = true;
            else if (VMouse.GetMouseButtonUp(1)) _mouseButtonDown = false;

            bool Ctrl = VKeyboard.GetKey(Key.LeftCtrl);

            // Movement
            if (_enableMovement)
            {
                float currentSpeed = VKeyboard.GetKey(_boostSpeed) ? _boostedSpeed : _movementSpeed;

                if (!Ctrl)
                    deltaPosition += transform.forward * AxisVertical +
                                        transform.right * AxisHorizontal;

                if (VKeyboard.GetKey(_moveUp))
                    deltaPosition += transform.up;

                if (VKeyboard.GetKey(_moveDown))
                    deltaPosition -= transform.up;

                bool isNoZero = deltaPosition != Vector3.zero;
                // Calc acceleration
                CalculateCurrentIncrease(isNoZero);

                if (isNoZero)
                {
                    if (rb)
                    {
                        rb.velocity = deltaPosition * currentSpeed;
                        if (deltaPosition.sqrMagnitude >= 0.1f) onChangePosition.Invoke();
                    }
                    else
                    {
                        setPosition(_setPosition + deltaPosition * currentSpeed * _currentIncrease);
                        transform.position = Vector3.Lerp(transform.position, _setPosition, smooth);
                    }
                }
                else if (rb) rb.velocity = rb.velocity * smooth;
            }

            Vector3 _delta = mouseDelta;

            // Rotation
            if (_enableRotation && (_delta.sqrMagnitude > 0))
            {
                if (_mouseButtonDown)
                {
                    // Pitch
                    _setRotation *= Quaternion.AngleAxis(
                        -_delta.y * _mouseSense,
                        Vector3.right
                    );

                    // Paw
                    _setRotation = Quaternion.Euler(
                        _setRotation.eulerAngles.x,
                        _setRotation.eulerAngles.y + _delta.x * _mouseSense,
                        _setRotation.eulerAngles.z
                    );
                }
                else if (Ctrl)
                {
                    // Pitch
                    _setRotation *= Quaternion.AngleAxis(
                        -_delta.y,
                        Vector3.right
                    );

                    // Paw
                    _setRotation = Quaternion.Euler(
                        _setRotation.eulerAngles.x,
                        _setRotation.eulerAngles.y + _delta.x,
                        _setRotation.eulerAngles.z
                    );
                }


                if (rb)
                    rb.rotation = Quaternion.Lerp(rb.rotation, _setRotation, smooth);
                else transform.rotation = Quaternion.Lerp(transform.rotation, _setRotation, smooth);

            }
        }

        public void SeekTo(Transform target, bool immediately = false)
        {
            if (immediately)
                setPosition(target.position, target.rotation);
            else
            {
                _seekTo = target;
                _seekToStartPos = transform.position;
                _seekToStartQuat = transform.rotation;

                _seekToIndex = 1;
            }
        }

        public void setPosition(Vector3 value)
        {
            _setPosition = value;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb) rb.position = value;
            onChangePosition.Invoke();
        }

        public Vector3 getPosition()
        {
            return transform.position;
        }

        public Quaternion getRotate()
        {
            return transform.rotation;
        }

        public void addListener(UnityAction action)
        {
            onChangePosition.AddListener(action);
        }

        public void setPosition(Vector3 value, Quaternion rotate)
        {
            _setRotation = rotate;
            setPosition(value);
        }

        public void removeListener(UnityAction action)
        {
            onChangePosition.RemoveListener(action);
        }
    }
}
