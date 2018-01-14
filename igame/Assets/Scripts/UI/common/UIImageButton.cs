using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIImageButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    private Vector3 _scale;

    public UnityAction<GameObject> onClick;
    public UnityAction<GameObject> onDown;
    public UnityAction<GameObject> onUp;

    public bool isGray;

    public float changeScaleSize = 0.9f;

    [Header("点击之后的按钮显示")]public Sprite PressSprite;
    private Sprite normalSprite;

    private Image BtnImg
    {
        get
        {
            var img = GetComponent<Image>();
            if (img == null)
                img = gameObject.AddComponent<Image>();
            return img;
        }
    }

	private AudioSource audio
	{
		get
		{
			var m_audio = transform.GetComponent<AudioSource> ();
			if (m_audio == null)
			{
				m_audio = gameObject.AddComponent<AudioSource> ();
			}
			return m_audio;
		}
	}
    // Use this for initialization
    void Start()
    {
        _scale = transform.localScale;
        SetGray(isGray);

        var img = GetComponent<Image>();
        if(img!=null)
            normalSprite = img.sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isGray) return;
        if (onClick != null)
        {
			PlayOneShot ();
            onClick.Invoke(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isGray) return;
        transform.localScale = Vector3.one * changeScaleSize;
        if(PressSprite != null)
            BtnImg.sprite = PressSprite;
        if (onDown != null)
        {
            onDown.Invoke(gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isGray) return;
        transform.localScale = Vector3.one;
        if (normalSprite != null)
            BtnImg.sprite = normalSprite;
        if (onUp != null)
        {
            onUp.Invoke(gameObject);
        }
    }

    public void SetGray(bool isGray)
    {
        this.isGray = isGray;
        var imgs = transform.GetComponentsInChildren<Image>();
        if (imgs != null)
        {
            foreach (var imgItem in imgs)
            {
                SetImgMat(imgItem);
            }
        }
	
		var txts = transform.GetComponentsInChildren<Text>();
		if (txts != null)
		{
			foreach (var txtItem in txts)
			{
				SetTextColor(txtItem);
			}
		}
    }

    private void SetImgMat(Image img)
    {
		if (!isGray) 
		{
			img.material = null;
			return;
		}

		//var mat = ResourceManager.LoaderGrayMat ();
  //      img.material = mat;
    }

    private void SetTextColor(Text text)
    {
		if (!isGray) 
		{
			text.material = null;
			return;
		}

		//var mat = ResourceManager.LoaderGrayMat();
		//text.material = mat;

	}


	private void PlayOneShot()
	{
		//var clip = ResourceManager.LoaderAudioClip ("click");
		//if (clip != null)
		//{
		//	audio.clip = clip;
		//	audio.PlayOneShot (clip);
		//}
	}
}
