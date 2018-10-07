using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject _livesDisplay;
    [SerializeField]
    private Sprite[] _livesImages;

    [SerializeField]
    private Text _scoreDisplay;

    [SerializeField]
    private GameObject _titleScreen;

    private int _score = 0;

    public void UpdateLives(int lives)
    {
        _livesDisplay.GetComponent<Image>().sprite = _livesImages[lives];
    }

    public void UpdateScore()
    {
        _score += 10;
        _scoreDisplay.text = "Score: " + _score;
    }

    public void SetInitialScore()
    {
        _score = 0;
        _scoreDisplay.text = "Score: " + _score;
    }

    public void SetTitleScreen(bool set)
    {
        _titleScreen.SetActive(set);
    }

}
