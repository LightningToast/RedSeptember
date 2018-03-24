using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubControl : MonoBehaviour {
    public float maxDiveAngle = 45f;
    public float maxHeight = 3.0f;
    public float maxEngine = 50f;
    public int dir = 1;
    public float targetAngle = 0;
    Quaternion targetRotation;
    public float targetHeight = 0;
    public float targetEngine = 0;
    public float turnSpeed = 2.0f;
    public float torpedoSpeed = 20f;
    public GameObject torpedo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float step = turnSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetEngine, targetHeight), 10f * step);

       

	}
    public void SetDive(float val) {
        targetHeight = val * maxHeight;

    }
    public void SetPitch(float val)
    {
        targetAngle = val * maxDiveAngle;
        Vector3 curRot = transform.rotation.eulerAngles;
        curRot.z = targetAngle;
        targetRotation = Quaternion.Euler(curRot.x, curRot.y, curRot.z);
    }
    public void SetEngine(float val) {
        targetEngine = val * maxEngine;
    }
    public void SetTorpedo() {
        GameObject tempTorpedo = (GameObject)Instantiate(torpedo, transform.position, transform.rotation);
        Rigidbody2D tempTorpedoRb = tempTorpedo.GetComponent<Rigidbody2D>();
        tempTorpedoRb.AddForce(transform.right * torpedoSpeed);
        Destroy(tempTorpedo, 3f);
    }
}
