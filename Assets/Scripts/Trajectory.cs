using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField]
    protected int dotsNumber;
    [SerializeField]
    protected GameObject dotsParent;
    [SerializeField]
    protected GameObject dotPrefab;
    [SerializeField]
    protected float dotSpacing;

    private Transform[] dotsList;

    private Vector3 pos;
    private float timeStamp;


    void Start()
    {
        //hide trajectory on start
        Hide();
        //Prepare dots 
        PrepareDots();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;
        }
    }
    public void UpdateDots(Vector3 ballPos, Vector3 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.z = (ballPos.z + forceApplied.z * timeStamp);
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics.gravity.magnitude * timeStamp * timeStamp) / 2f;
            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
