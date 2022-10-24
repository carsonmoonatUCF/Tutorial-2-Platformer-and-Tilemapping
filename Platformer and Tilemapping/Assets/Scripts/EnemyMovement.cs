using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float moveDistance;
    public Vector2 point1;
    public Vector2 point2;

    private void Start() {
        point1 = transform.position;
        point2 = transform.position + new Vector3(moveDistance, 0, 0);

        StartCoroutine(LerpToPosition2());
    }

    public IEnumerator LerpToPosition2(){
        float time = 0;

        while(time < moveSpeed){
            transform.position = Vector2.Lerp(point1, point2, time / moveSpeed);
            time += Time.deltaTime;
            yield return null;
        }

        //transform.position = point2;

        StartCoroutine(LerpToPosition1());
    }

    public IEnumerator LerpToPosition1(){
        float time = 0;

        while(time < moveSpeed){
            transform.position = Vector2.Lerp(point2, point1, time / moveSpeed);
            time += Time.deltaTime;
            yield return null;
        }

        //transform.position = point1;

        StartCoroutine(LerpToPosition2());
    } 

}
