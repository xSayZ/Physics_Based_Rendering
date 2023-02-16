// Copyright 2021-2023 Emil Forslund
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS {
    public class MovementController : MonoBehaviour {
    
        private CharacterController body;
        private float verticalSpeed;
        private bool grounded;
    
        [SerializeField]
        private float movementSpeed = 1.0f;
    
        [SerializeField]
        private float jumpHeight = 1.0f;
    
        [SerializeField]
        private float gravityConstant = 9.0f;

        [SerializeField]
        [Tooltip("If 0, then all momentum will be lost if the player jumps and hits the ceiling.")]
        [Range(0.0f, 1.0f)]
        private float ceilingBounce = 0.5f;
    
        void Start() {
            body = transform.GetComponent<CharacterController>();
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = grounded ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }

        void Update() {
            var t = transform;
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");
            var amount = Mathf.Max(Mathf.Abs(hor), Mathf.Abs(ver));
            grounded = body.isGrounded;

            if (grounded && verticalSpeed > 0.0f) {
                verticalSpeed = 0.0f;
            }
        
            if (amount > float.Epsilon) {
                var dir = t.right * hor + t.forward * ver;
                dir.y = 0.0f;
                dir.Normalize();
                dir *= amount * movementSpeed * Time.deltaTime;
                body.Move(dir);
            }
        
            if (grounded && Input.GetButton("Jump")) {
                verticalSpeed = -Mathf.Sqrt(jumpHeight * 3.0f * gravityConstant);
            }
        
            verticalSpeed += gravityConstant * Time.deltaTime;

            var collisionFlags = body.Move(Vector3.down * (verticalSpeed * Time.deltaTime));
            if ((collisionFlags & CollisionFlags.Above) != 0) {
                verticalSpeed *= -ceilingBounce; // Bounce against the ceiling
            }
        }
    }
}