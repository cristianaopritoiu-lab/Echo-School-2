using UnityEngine;

public class SimpleStorySwap : MonoBehaviour
{
    public GameObject[] slides;
    private int index = 0;

    void Start()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == 0);
        }
    }

    public void NextSlide()
    {
        if (index < slides.Length - 1)
        {
            slides[index].SetActive(false);
  
            index++;

            slides[index].SetActive(true);
        }
        else
        {
            Debug.Log("S-au terminat pozele!");
        }
    }
}