using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace narrenschlag
{
    public class Motor_Player : Motor_Character
    {
        public Camera cam;

        public override void Start()
        {
            base.Start();

            cam = Camera.main;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void HandleOrientation()
        {
            if (!orientation || !cam)
                return;

            Vector3 DeltaVec = (orientation.position - cam.transform.position);
            DeltaVec.y = 0.0f; 
            Quaternion Euler = Quaternion.LookRotation(DeltaVec);
            Euler = Quaternion.Euler(Euler.eulerAngles.x, Euler.eulerAngles.y, Euler.eulerAngles.z);
            orientation.rotation = Euler;
        }

        Quaternion _rot;
        public override void HandleMovement()
        {
            if (!rb)
                return;

            float delta = Time.deltaTime;

            Vector2 inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            Vector3 vel = 
                orientation.forward * inp.y * stats.speed_move
                + orientation.right * inp.x * stats.speed_move;

            rb.velocity = Vector3.Lerp(rb.velocity, vel, delta * 4f);

            if (inp != Vector2.zero)
            {
                Vector3 rot = Quaternion.LookRotation(rb.velocity).eulerAngles;
                rot.x = 0;
                rot.z = 0;
                _rot = Quaternion.Euler(rot);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, _rot, delta * stats.speed_rotate);
        }
    }
}