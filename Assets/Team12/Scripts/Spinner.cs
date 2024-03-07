using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team12
{
    public class Spinner : MonoBehaviour
    {
        Rigidbody2D rb;
        public bool moving = false;
        public bool direction = false;
        float timer;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rb.MoveRotation(rb.rotation + 1);
            timer += Time.deltaTime;
            if (moving)
            {
                if (direction)
                {
                    rb.MovePosition(new Vector2(rb.position.x + -Mathf.Cos(timer * 2 / Mathf.PI) / 15, rb.position.y));

                }
                else
                {
                    rb.MovePosition(new Vector2(rb.position.x + Mathf.Cos(timer * 2 / Mathf.PI) / 15, rb.position.y));

                }
            }
        }
    }

}
