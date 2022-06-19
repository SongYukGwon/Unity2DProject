using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpViewer : MonoBehaviour
{
    private EnemyHP enemyHP;
    private Slider hpSlider;

    public void Setup(EnemyHP enemyHp)
    {
        this.enemyHP = enemyHp;
        hpSlider = GetComponent<Slider>();
    }

    void Update()
    {
        hpSlider.value = enemyHP.CurrentHp / enemyHP.MaxHp;
    }
}
