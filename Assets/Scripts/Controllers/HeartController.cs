using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour {

    public int startHearts = 3;
    public int curHealth;
    public Image[] healthImages;
    public Sprite[] healthSprites;

    private int maxHeartAmount = 10;
    private int maxHealth;
    private int healthPerHeart = 2;

	// Use this for initialization
	void Start () {
        curHealth = startHearts * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;
        CheckHealthAmount();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckHealthAmount()
    {
        for (int i = 0; i < maxHeartAmount; i++)
        {
            if (startHearts <= i)
            {
                healthImages[i].enabled = false;
            } else
            {
                healthImages[i].enabled = true;
            }
        }
        UpdateHearts();
    }

    void UpdateHearts()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            } else
            {
                i++;
                if (curHealth >= i * healthPerHeart)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                } else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - curHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;

                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, startHearts * healthPerHeart);
        UpdateHearts();
    }

    public void AddHeartContainer()
    {
        startHearts++;
        startHearts = Mathf.Clamp(startHearts, 0, maxHeartAmount);

        //full heal
        curHealth = startHearts * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;

        CheckHealthAmount();
    }


}
