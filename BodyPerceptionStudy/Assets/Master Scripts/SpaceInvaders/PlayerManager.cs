using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10.0f;
    public float leftBound, RightBound;
    private float translation;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
       

        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        //Move Left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Vector3 temp = new Vector3((-1 * speed * Time.deltaTime), 0, 0);
            this.gameObject.transform.position += temp;
            Debug.Log("move left");

        }
        //Move Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            translation = Input.GetAxis("Vertical");
            this.gameObject.transform.position = new Vector3(speed * Time.deltaTime, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            Debug.Log("move Right");
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(Movement());
    }
}
