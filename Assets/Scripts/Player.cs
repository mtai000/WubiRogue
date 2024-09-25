

using UnityEngine;

class Player : MonoBehaviour
{
    public float hp = 10.0f;
    public float max_hp = 10.0f;
    public Material material;
    public void Start()
    {
        hp = max_hp;
        UpdateHPShader();
    }
    public void UpdateHPShader()
    {
        material.SetFloat("_HealthPercentage", hp/max_hp);
    }
    public void InjureCal()
    {
        hp -= 3.0f;
        UpdateHPShader();
    }

    private void Update()
    {
        if(hp <= 0.0f)
        {
            Time.timeScale = 0.0f;
            Debug.Log("Game over");
        }    
    }
}