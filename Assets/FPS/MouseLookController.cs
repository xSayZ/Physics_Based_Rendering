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

using UnityEngine;

namespace FPS {
    public class MouseLookController : MonoBehaviour {
    
        private Transform head;
        private Transform body;
    
        [SerializeField]
        private float mouseSpeed = 1.0f;
    
        [SerializeField, Range(0, 180)]
        private float verticalAngle = 160;
        
        void Start() {
            head = GetComponentInChildren<Camera>().transform;
            body = transform;
        }
    
        void Update() {
            var hor = Input.GetAxis("Mouse X");
            var ver = Input.GetAxis("Mouse Y");
    
            if (Mathf.Abs(hor) > float.Epsilon) {
                body.Rotate(Vector3.up, hor * mouseSpeed);
            }
    
            if (Mathf.Abs(ver) > float.Epsilon) {
                var rot = head.localRotation;
                var aim = Quaternion.AngleAxis(-0.5f * verticalAngle * Mathf.Sign(ver), Vector3.right);
                var delta = Quaternion.RotateTowards(rot, aim, Mathf.Sign(ver) * ver * mouseSpeed);
                head.localRotation = delta;
            }
        }
    }
}
