using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    public Sprite gameLogoSprite; // assign your heart.png in the inspector
    public Font gameFont;         // assign VCR_OSD_MONO_1.001.ttf in the inspector
    public string gameplaySceneName = "Gameplay"; // replace with your scene name

    void Start()
    {
        // 1. Create Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // 2. Create full-screen Panel (background)
        GameObject panelGO = new GameObject("Panel");
        panelGO.transform.SetParent(canvasGO.transform, false);
        Image panelImage = panelGO.AddComponent<Image>();
        panelImage.color = new Color32(75, 115, 76, 255); // #4B734C
        RectTransform panelRect = panelGO.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        // 3. Add Game Logo
        GameObject logoGO = new GameObject("Logo");
        logoGO.transform.SetParent(panelGO.transform, false);
        Image logoImage = logoGO.AddComponent<Image>();
        logoImage.sprite = gameLogoSprite;
        logoImage.preserveAspect = true;

        RectTransform logoRect = logoGO.GetComponent<RectTransform>();
        logoRect.sizeDelta = new Vector2(200, 200);
        logoRect.anchoredPosition = new Vector2(0, 100);

        // 4. Add Title (NECROMANCE)
        GameObject titleGO = new GameObject("Title");
        titleGO.transform.SetParent(panelGO.transform, false);
        Text titleText = titleGO.AddComponent<Text>();
        titleText.text = "NECROMANCE";
        titleText.font = gameFont;
        titleText.fontSize = 80;
        titleText.alignment = TextAnchor.MiddleCenter;
        titleText.color = Color.black;

        RectTransform titleRect = titleGO.GetComponent<RectTransform>();
        titleRect.anchoredPosition = new Vector2(0, 20);
        titleRect.sizeDelta = new Vector2(800, 100);

        // 5. Add Subtitle / description
        GameObject descGO = new GameObject("Description");
        descGO.transform.SetParent(panelGO.transform, false);
        Text descText = descGO.AddComponent<Text>();
        descText.text = "Your partner feels like SHIT!\nCan you heal him?";
        descText.font = gameFont;
        descText.fontSize = 24;
        descText.alignment = TextAnchor.MiddleCenter;
        descText.color = Color.black;

        RectTransform descRect = descGO.GetComponent<RectTransform>();
        descRect.anchoredPosition = new Vector2(0, -50);
        descRect.sizeDelta = new Vector2(600, 100);

        // 6. Add Buttons Container
        GameObject buttonsGO = new GameObject("Buttons");
        buttonsGO.transform.SetParent(panelGO.transform, false);
        HorizontalLayoutGroup hLayout = buttonsGO.AddComponent<HorizontalLayoutGroup>();
        hLayout.spacing = 30;
        hLayout.childForceExpandWidth = true;
        hLayout.childForceExpandHeight = false;
        hLayout.childAlignment = TextAnchor.MiddleCenter;

        RectTransform buttonsRect = buttonsGO.GetComponent<RectTransform>();
        buttonsRect.sizeDelta = new Vector2(500, 60);
        buttonsRect.anchoredPosition = new Vector2(0, -150);

        // 7. Create "Ctrls" Button
        CreateButton(buttonsGO.transform, "Ctrls", gameFont, () => {
            Debug.Log("Ctrls button clicked");
        });

        // 8. Create "Start Game" Button
        CreateButton(buttonsGO.transform, "Start Game", gameFont, () => {
            SceneManager.LoadScene(gameplaySceneName);
        });

        // 9. Ensure EventSystem exists
        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<UnityEngine.EventSystems.EventSystem>();
            es.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }
    }

    void CreateButton(Transform parent, string text, Font font, UnityEngine.Events.UnityAction onClick)
    {
        GameObject btnGO = new GameObject(text + "Button");
        btnGO.transform.SetParent(parent, false);

        Button btn = btnGO.AddComponent<Button>();
        Image img = btnGO.AddComponent<Image>();
        img.color = new Color32(202, 208, 195, 255); // #CAD0C3

        RectTransform btnRect = btnGO.GetComponent<RectTransform>();
        btnRect.sizeDelta = new Vector2(200, 50);

        // Button text
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(btnGO.transform, false);
        Text btnText = textGO.AddComponent<Text>();
        btnText.text = text;
        btnText.font = font;
        btnText.fontSize = 24;
        btnText.color = Color.black;
        btnText.alignment = TextAnchor.MiddleCenter;

        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        // Add onClick listener
        btn.onClick.AddListener(onClick);
    }
}
