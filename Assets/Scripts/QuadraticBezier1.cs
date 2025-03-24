using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadraticBezier : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform pointC;
    [SerializeField] float animationDuration = 2f;
    [SerializeField] AnimationCurve ease;

    void Start()
    {
        StartCoroutine(Animation());
    }

    void Update()
    {
        
    }

    IEnumerator Animation()
    {
        float elapsedTime = 0f;

        while (true)
        {

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                float normalizedTime = elapsedTime / animationDuration;

                Vector3 AC = Vector3.Lerp(pointA.position, pointC.position, ease.Evaluate(normalizedTime));
                Vector3 CB = Vector3.Lerp(pointC.position, pointB.position, ease.Evaluate(normalizedTime));
                transform.position = Vector3.Lerp(AC, CB, ease.Evaluate(normalizedTime));

                yield return new WaitForEndOfFrame();
            }

            elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                float normalizedTime = elapsedTime / animationDuration;

                Vector3 BC = Vector3.Lerp(pointB.position, pointC.position, ease.Evaluate(normalizedTime));
                Vector3 CA = Vector3.Lerp(pointC.position, pointA.position, ease.Evaluate(normalizedTime));
                transform.position = Vector3.Lerp(BC, CA, ease.Evaluate(normalizedTime));

                yield return new WaitForEndOfFrame();
            }

            elapsedTime = 0f;

            yield return null;
        }
    }
}
