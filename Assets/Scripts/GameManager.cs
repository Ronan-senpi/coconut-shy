using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    protected Transform Cans;

    [SerializeField]
    protected Ball Ball;
    [SerializeField]
    Transform ballPosition;
    List<string> cansList = new List<string>();


    [SerializeField]
    protected StrengthBarController sbc;

    [SerializeField]
    protected float pushStrenght = 4f;
    [SerializeField]
    protected float increaseStrenght;

    [SerializeField]
    protected Animator transitionAnimator;

    protected bool IsIncrease = true;

    protected Vector3 startPoint = Vector3.zero;
    protected Vector3 endPoint;
    protected Vector3 direction;
    protected Vector3 force;

    private bool clicked;
    Vector3 lookAtPosition;
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
        if (cansList.Count == 6)
        {
            EndGame(true);
        }
        lookAtPosition = Cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Cam.farClipPlane));
        var tmp = (lookAtPosition - Ball.transform.position);
        tmp.y += 10f;
        Debug.DrawLine(Ball.transform.position, tmp, Color.blue);
        if (clicked)
        {
            StrenghtUpdate();
        }


    }

    void StrenghtUpdate()
    {
        if (pushStrenght + increaseStrenght * Time.deltaTime > sbc.MaxValue)
        {
            IsIncrease = false;
        }
        else if (pushStrenght - increaseStrenght * Time.deltaTime < sbc.MinValue )
        {
            IsIncrease = true;
        }
        if (IsIncrease)
        {
            pushStrenght += increaseStrenght * Time.deltaTime;
        }
        else
        {
            pushStrenght -= increaseStrenght * Time.deltaTime;
        }
        sbc.SetStrength(pushStrenght);
    }
    void StrenghtReset()
    {
        sbc.ResetStrength();
    }

    #region Begin : Drag
    protected void OnDragStart()
    {
        Ball.DesactivateRb();
    }

    protected void OnDragEnd()
    {
        direction = (lookAtPosition - Ball.transform.position).normalized;
        direction.y += 0.5f;
        force = direction * pushStrenght;
        Ball.ActivateRb();
        Ball.Push(force);
    }

    #endregion End : Drag

    public void Click()
    {
        OnDragStart();
        clicked = true;
    }
    public void Unclick()
    {
        StrenghtReset();
        OnDragEnd();
        StartCoroutine(ResetBallC());
        clicked = false;
    }
    IEnumerator ResetBallC()
    {
        yield return new WaitForSeconds(3f);
        ResetBall();
    }
    void ResetBall()
    {
        Ball.transform.parent = Cam.transform;
        Ball.transform.position = ballPosition.position;
        Ball.DesactivateRb();
    }
    public void OnTargetFound()
    {
        int cc = Cans.childCount;
        for (int i = 0; i < cc; i++)
        {
            Transform can = Cans.GetChild(i);
            Rigidbody rb;
            if (can.TryGetComponent<Rigidbody>(out rb))
            {
                rb.isKinematic = false;
            }
        }
    }

    public void OnTargetLost()
    {
        int cc = Cans.childCount;
        for (int i = 0; i < cc; i++)
        {
            Transform can = Cans.GetChild(i);
            Rigidbody rb;
            if (can.TryGetComponent<Rigidbody>(out rb))
            {
                rb.isKinematic = true;
            }
        }
    }

    public void AddCan(string name)
    {
        if (!cansList.Any(x => x == name))
        {
            cansList.Add(name);
        }
        if (cansList.Count == 6)
        {
            EndGame(true);
        }
    }

    private void EndGame(bool isVectory)
    {
        if (isVectory)
        {
            StartCoroutine(GoToScreenVictory());
        }
        else
        {
            //Tout le monde peut gagner a mon jeux ! Ceci est un partie pris
        }
    }
    IEnumerator GoToScreenVictory()
    {
        transitionAnimator.Play(0);
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("Victory");
    }
}
