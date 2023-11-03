using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace narrenschlag
{
    public class Camera_Move : MonoBehaviour
    {
        public Transform _target;
        Transform target;
        public float _yOff;
        float yOff;
        public Camera cam;
        [Space]
        public float speed_pivot = 10;
        public float speed_cam = 10;
        [Space]
        public float rot_right = 10;
        public float rot_up = 10;
        public float rot_max;
        public float rot_min;
        public float rot_lerp = 4;
        public float rot_lookAtLerp = 5;

        Transform child;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            child = transform.GetChild(0);

            SetTarget(_target, _yOff);
        }

        public void SetTarget(Transform t, float y)
        {
            target = t;
            yOff = y;
        }

        float rotX;
        Vector2 rotInp;
        public void Update()
        {
            float delta = Time.deltaTime;

            //
            // Rotation
            rotInp = Vector2.Lerp(rotInp, new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), delta * rot_lerp);

            float x = rotX + rotInp.y * rot_up;
            rotX = Mathf.Clamp(x, rot_min, rot_max);

            Vector3 r = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(rotX, r.y + rotInp.x * rot_right, r.z));
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            //
            // Positionating
            Vector3 tar = target.position + new Vector3(0, yOff, 0);

            //cam.transform.LookAt(tar);
            Vector3 DeltaVec = (tar - cam.transform.position);
            Quaternion Euler = Quaternion.LookRotation(DeltaVec);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(Euler.eulerAngles.x, Euler.eulerAngles.y, Euler.eulerAngles.z), delta * rot_lookAtLerp);

            transform.position = Vector3.Lerp(transform.position, tar, delta * speed_pivot);
            cam.transform.position = /*child.position; // */Vector3.Lerp(cam.transform.position, child.position, delta * speed_cam);
        }
    }
}