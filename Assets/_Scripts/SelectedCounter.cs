using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }



    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        if (visualObjectArray != null)
        {
            foreach (var item in visualObjectArray)
            {
                item.SetActive(true);
            }
        }
    }
    private void Hide()
    {
        foreach (var item in visualObjectArray)
        {
            item.SetActive(false);
        }
    }
}
