using UnityEngine;
using UnityEngine.EventSystems;
using Vmaya.Scene3D;

namespace Vmaya.Util
{
    public class MoveTo : MonoBehaviour
    {
        public float speed = 0.05f;
        private Vector3 start;
        private Vector3 moveTo;
        private Vector3 down;

        private Transform current;
        private float y;
        private float inx = 0;

        private void Awake()
        {
            y = transform.position.y;

            moveTo = start = transform.position;
            inx = 1;
        }

        private void FixedUpdate()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (VMouse.GetMouseButton(0))
                {
                    if (down.magnitude == 0) down = VMouse.mousePosition;
                }
                else if (VMouse.GetMouseButtonUp(0))
                {
                    if ((down - VMouse.mousePosition).magnitude < 5)
                    {
                        RaycastHit result = hitDetector.getNearest<Component>();
                        if (!result.point.Equals(Vector3.zero))
                        {
                            if (result.transform.GetComponent<Terrain>())
                            {
                                return;
                                //moveTo = new Vector3(result.point.x, y, result.point.z);
                            }
                            else moveTo = result.transform.position;
                            start = transform.position;
                            inx = 0;
                        }
                    }
                }
                else down = Vector3.zero;

                if (inx < 1)
                {
                    transform.position = Vector3.Lerp(start, moveTo, EasingFunction.EaseInOutCubic(0, 1, inx));
                    inx += speed * Time.fixedDeltaTime;
                }
            }
        }
    }
}
