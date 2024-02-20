using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProducts : MonoBehaviour
{
    public string product;
    public Image[] emptyIcon;
    public Sprite fillIcon;
    public int UpgradeLimit;

  public void Start()
  {
    IconsUpdate();
  }

  public void ProductUpgrade()
  {
    int ups = PlayerPrefs.GetInt("ups");
    if (ups > 0) {
      --ups;
      PlayerPrefs.SetInt("ups", ups);
      int count = PlayerPrefs.GetInt(product);
      if (count < UpgradeLimit)
      {
        count++;
        PlayerPrefs.SetInt(product, count);

        emptyIcon[count - 1].overrideSprite = fillIcon;
      }
    }
  }

    void IconsUpdate()
    {
        int count = PlayerPrefs.GetInt(product);
        for (int i = 0; i < count; i++)
        {
            emptyIcon[i].overrideSprite = fillIcon;
        }
    }
  
}