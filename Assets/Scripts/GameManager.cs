using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    Camera Cam;

    [SerializeField]
    protected Trajectory trajectory;
    [SerializeField]
    protected Joystick joystick;
    [SerializeField]
    protected Ball Ball;
    [SerializeField]
    protected GameObject target;

    [SerializeField]
    protected float pushForce = 4f;

    protected bool IsDragging { get { return (joystick && (joystick.Horizontal != 0 || joystick.Vertical != 0)); } }

    protected Vector3 startPoint = Vector3.zero;
    protected Vector3 endPoint;
    protected Vector3 direction;
    protected Vector3 force;
    protected float distance;
    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
        endPoint = Ball.transform.position;
        startPoint = new Vector3(endPoint.x, endPoint.y, 0);
        Ball.DesactivateRb();
        //OnDragStart();
    }



    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(Ball.transform.position, Vector3.forward, Color.blue);
        //Change for tactile :scream:
        if (joystick)
            {

                OnDragStart();
            }
        if (Input.GetMouseButtonUp(0))
        {
            OnDragEnd();
        }
        if (IsDragging)
        {
            OnDrag();
        }
    }
    #region Begin : Drag
    protected void OnDragStart()
    {

        Ball.DesactivateRb();
        trajectory.Show();
    }

    protected void OnDrag()
    {
        //endPoint = Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Cam.transform.position.z));
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;
        //For debug
        Debug.DrawLine(startPoint, endPoint, Color.red);
        trajectory.UpdateDots(Ball.Pos, force);
    }

    protected void OnDragEnd()
    {
        Debug.Log(direction);
        Ball.ActivateRb();
        Ball.Push(force);
        trajectory.Hide();
    }

    #endregion End : Drag
}
