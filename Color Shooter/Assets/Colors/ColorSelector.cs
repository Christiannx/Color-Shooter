using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour {
    [SerializeField] Button[] buttons;

    Player player;

    void Start() {
        SelectColor(MyColor.red);
        player = FindObjectOfType<Player>();
    }

    public void SelectColor(string s) {
        MyColor color;
        switch(s) {
            case "red": color = MyColor.red; break;
            case "blue": color = MyColor.blue; break;
            case "green": color = MyColor.green; break;
            default: color = MyColor.red; break;
        }
        SelectColor(color);
    }

    public void SelectColor(MyColor color) {
        foreach (var button in buttons) {
            button.interactable = button.GetComponent<ColorButton>().color != color;
        }
        FindObjectOfType<Player>().SetColor(color);
    }
}

public enum MyColor {
    red = 0, blue = 1, green = 2
}