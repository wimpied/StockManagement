using UnityEngine;
using System.Collections;

public class Bug25b : MonoBehaviour
{

    public AnimationCurve AnimCurve;
    public float CurveDuration;
    public Vector3 Position1;
    public Vector3 Position2;
    private bool moving = false;
    void Update()
    {
        if (!moving) return;
        Move();
    }

    void Moving(bool state)
    {
        moving = state;
    }

    private void Move()
    {
        StartCoroutine(Move(Position1, Position2, AnimCurve, CurveDuration));
    }

    IEnumerator Move(Vector3 pos1, Vector3 pos2, AnimationCurve ac, float time)
    {
        float timer = 0.0f;
        while (timer <= time)
        {
            transform.position = Vector3.Lerp(pos1, pos2, ac.Evaluate(timer / time));
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
