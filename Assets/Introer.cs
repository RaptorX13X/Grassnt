using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introer : MonoBehaviour
{

    [System.Serializable]
    class introElement
    {
        public RectTransform objTransform;
        public Vector3 desiredPos;
        public Vector3 startingPos;
        public Vector3 endPos;
        public float animEnd;
        public float endEnd;
    }
    [SerializeField] private GameUIManager guim;
    [SerializeField] private List<introElement> elements;
    private float timer;

    private void Awake()
    {
        timer = 0;
        foreach (var element in elements)
        {
            element.desiredPos = element.objTransform.anchoredPosition3D;
            element.objTransform.anchoredPosition3D = element.startingPos;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        foreach (var element in elements)
        {
            if (timer >= element.animEnd - .1f && timer <= element.animEnd + .5f)
            {
                float progress = (timer - (element.animEnd - .1f)) / .1f;
                if (progress >= 1) element.objTransform.anchoredPosition3D = element.desiredPos;
                else element.objTransform.anchoredPosition3D = Vector3.Lerp(element.startingPos, element.desiredPos,  progress);
            }

            if (timer >= element.endEnd - .1f && timer <= element.endEnd + .5f)
            {
                float progress = (timer - (element.endEnd - .1f)) / .1f;
                if (progress >= 1) element.objTransform.gameObject.SetActive(false);
                else element.objTransform.anchoredPosition3D = Vector3.Lerp(element.endPos, element.desiredPos, 1 - progress);
            }
        }
        if(timer >= elements[0].endEnd +.5f)
        {
            guim.StartPlaying();
            gameObject.SetActive(false);
        }
    }
}
