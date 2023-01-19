using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRemaining : MonoBehaviour
{
    public int Target = 5;
    public Text textTarget;

    private void Start()
    {
        textTarget.text = Target.ToString();
    }
    public void Update()
    {
        textTarget.text = Target.ToString();
    }
    public void EnemyCounter()
    {
        Target -= 1;
        if (Target <= 0)
        {
            FindObjectOfType<GameManager>().CompleteLevel();
        }
    }
}
