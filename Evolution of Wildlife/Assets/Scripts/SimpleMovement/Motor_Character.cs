using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace narrenschlag
{
    public class Motor_Character : MonoBehaviour
    {
        public Stats stats;
        public Transform orientation;

        [Header("Handled by Script")]
        public Rigidbody rb;

        public virtual void Start()
        {
            rb = GetComponent<Rigidbody>();

            SetupStats();
        }

        public void SetupStats()
        {
            stats.health_cur = stats.health_max;
        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {
            HandleMovement();
            HandleOrientation();
        }

        public virtual void HandleOrientation()
        {
            if (!orientation)
                return;
        }

        public virtual void HandleMovement()
        {
            if(!rb)
                return;
        }
    }

    [System.Serializable]
    public class Stats
    {
        public string name;
        public int health_max = 10;
        public int health_cur;
        [Space]
        public float speed_move = 10;
        public float speed_rotate = 6;
    }
}