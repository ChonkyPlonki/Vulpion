using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAppearanceHandler : MonoBehaviour
{
    public bool CapeEquipped;
    private Dictionary<string, SpriteRenderer[]> bodyParts;

    [SerializeField]
    public SpriteRenderer[] HeadPattern;
    [SerializeField]
    public SpriteRenderer[] BodyPattern;
    [SerializeField]
    public SpriteRenderer[] HeadSprites;
    [SerializeField]
    public SpriteRenderer[] EyeSprites;
    [SerializeField]
    public SpriteRenderer[] BodySprites;
    [SerializeField]
    public SpriteRenderer[] LegSprites;
    [SerializeField]
    public SpriteRenderer[] ArmSprites;


    private void Start()
    {
        bodyParts = new Dictionary<string, SpriteRenderer[]>()
                                            {
                                                {"HeadPattern", HeadPattern},
                                                {"BodyPattern", BodyPattern},
                                                {"HeadSprites", HeadSprites},
                                                {"EyeSprites", EyeSprites},
                                                {"BodySprites", BodySprites},
                                                {"LegSprites", LegSprites},
                                                {"ArmSprites", ArmSprites},
                                            };
    }
}
