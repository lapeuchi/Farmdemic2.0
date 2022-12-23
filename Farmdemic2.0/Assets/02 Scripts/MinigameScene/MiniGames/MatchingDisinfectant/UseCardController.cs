using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCardController : MonoBehaviour
{
    public string _type;
    private Vector3 destination;
    private MatchingDisinfectant root;
    private float moveSpeed = 5;

    public void Init(string type, MatchingDisinfectant main)
    {
        _type = type;
        root = main;
    }

    public void Gather(Vector3 gatherPoint)
    {
        destination = gatherPoint;

        StartCoroutine(MoveTo());
    }

    public void Shuffle(Vector3 gatherPoint)
    {
        destination = gatherPoint;

        StartCoroutine(MoveTo());
    }

    IEnumerator MoveTo()
    {
        while(Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.position = destination;
        root.AddDoneCard();
        yield break;
    }
}
