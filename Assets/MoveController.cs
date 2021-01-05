using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    protected Joystick joystick;
    [SerializeField]
    protected float minX = -0.3f;
    [SerializeField]
    protected float maxX = 0.3f;
    [SerializeField]
    protected float minY = -0.15f;
    [SerializeField]
    protected float maxY = 0.4f;
    [SerializeField]
    [Range(0.1f, 1f)]
    protected float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogError((transform.position.y + joystick.Vertical) * speed * Time.deltaTime);
        if (joystick && (joystick.Horizontal != 0 || joystick.Vertical != 0))
        {
            Vector3 v = new Vector3(Mathf.Clamp((transform.position.x + joystick.Horizontal) * speed, minX, maxX),
                                             Mathf.Clamp((transform.position.y + joystick.Vertical) * speed,minY, maxY),
                                             transform.position.z);
            transform.position = v;
        }
    }
}
