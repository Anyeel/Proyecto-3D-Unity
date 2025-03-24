using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetList : MonoBehaviour
{
    [SerializeField] MeshRenderer objectMesh;
    [SerializeField] Transform[] points;
    [SerializeField] Color[] colors;
    [SerializeField] float animationDuration = 2;
    [SerializeField] AnimationCurve generalEase;
    [SerializeField] AnimationCurve positionEase;

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
        int fromPosition = 0;
        int toPosition = 1;
        
        while (true)
        {
            if (animationDuration == 0) yield return null;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                float normalizedTime = elapsedTime / animationDuration;

                transform.position = Vector3.LerpUnclamped(points[fromPosition].position, points[toPosition].position, positionEase.Evaluate(normalizedTime));
                transform.localScale = Vector3.Lerp(points[fromPosition].localScale, points[toPosition].localScale, generalEase.Evaluate(normalizedTime));
                transform.rotation = Quaternion.Lerp(points[fromPosition].rotation, points[toPosition].rotation, generalEase.Evaluate(normalizedTime));
                objectMesh.material.color = Color.Lerp(colors[fromPosition], colors[toPosition], generalEase.Evaluate(normalizedTime));

                yield return new WaitForEndOfFrame();
            }

            fromPosition = toPosition;
            toPosition = (toPosition + 1) % points.Length;
            
            elapsedTime = 0f;
        }
    }
}
