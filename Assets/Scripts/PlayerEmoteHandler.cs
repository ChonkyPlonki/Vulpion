using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmoteHandler : MonoBehaviour
{
    public static string currentEmotion;
    //public Animator playerAnim;
    public int neutral = 0;
    public int sad = 1;
    public int smile = 2;

    private Dictionary<string, int> allEmotes;
    private void Start()
    {
        allEmotes = new Dictionary<string, int>();
        allEmotes.Add("Neutral", neutral);
        allEmotes.Add("Sad", sad);
        allEmotes.Add("Smile", smile);
    }

    public void SetEmotiong(string emotionName)
    {
        Animator playerAnim = PlayerRelated.Instance.playerAnim;
        playerAnim.SetInteger("Emotion", allEmotes[emotionName]);
    }
}
