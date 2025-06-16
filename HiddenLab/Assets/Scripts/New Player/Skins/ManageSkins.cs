using System.Collections.Generic;
using UnityEngine;

public class ManageSkins : MonoBehaviour
{
    public GameObject[] SlimeObjects;
    public GameObject SlimeSkin;
    public GameObject SlimeAnimations;
    private Sprite SlimeSprite;
    private Animator SlimeAnimation;
    void OnEnable()
    {
        SlimeSprite = SlimeSkin.GetComponent<SpriteRenderer>().sprite;
        SlimeAnimation = SlimeAnimations.GetComponent<Animator>();
        foreach (GameObject gameObject in SlimeObjects)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = SlimeSprite;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = SlimeAnimation.runtimeAnimatorController;
        }

    }
}
