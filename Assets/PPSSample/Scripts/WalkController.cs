using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour {

    Rigidbody rigidbody_;
    Transform sub_transform_;
    Rigidbody sub_rigidbody_;
    bool inputed0_;
    bool inputed1_;

    public bool isInputed0()
    {
        return inputed0_;
    }

    public bool isInputed1()
    {
        return inputed1_;
    }

	void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
        sub_transform_ = transform.GetChild(0);
        sub_rigidbody_ = sub_transform_.gameObject.GetComponent<Rigidbody>();
        inputed0_ = false;
        inputed1_ = false;
	}
	
	void FixedUpdate()
    {
        // for (var i = 1; i < 10; ++i) {
        //     for (var j = 1; j < 10; ++j) {
        //         var str = string.Format("joystick {0} analog {1}", i, j);
        //         Debug.Log(string.Format("{0} : {1}", str, Input.GetAxisRaw(str)));
        //     }
        // }

        const float THRESHOLD = 0.2f;
        {
            var hori = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(hori) < THRESHOLD) {
                hori = 0f;
            }
            var vert = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(vert) < THRESHOLD) {
                vert = 0f;
            }
            inputed0_ = (hori > 0f || vert > 0f);
            var forward = new Vector3(hori * 10f, 0f, vert * 10f);
            var force = sub_transform_.TransformVector(forward);
            force.y = 0f;
            rigidbody_.AddForce(force);
        }
        {
            var hori2 = Input.GetAxis("Mouse X");
            if (Mathf.Abs(hori2) < THRESHOLD) {
                hori2 = 0f;
            }
            var vert2 = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(vert2) < THRESHOLD) {
                vert2 = 0f;
            }
            inputed1_ = (hori2 > 0f || vert2 > 0f);
            sub_rigidbody_.AddRelativeTorque(new Vector3(vert2*2f, hori2*2f, 0f)*100f);
        }
        {
            var left = sub_transform_.TransformVector(Vector3.left);
            var hori_left = new Vector3(left.x, 0f, left.z).normalized;
            var torque = Vector3.Cross(left, hori_left);
            sub_rigidbody_.AddTorque(torque*300f);
        }
        {
            var forward = sub_transform_.TransformVector(Vector3.forward);
            var hori_forward = new Vector3(forward.x, 0f, forward.z).normalized;
            var torque = Vector3.Cross(forward, hori_forward);
            sub_rigidbody_.AddTorque(torque*300f);
        }
        sub_transform_.position = transform.position + new Vector3(0f, 0.5f, 0f);
	}
}
