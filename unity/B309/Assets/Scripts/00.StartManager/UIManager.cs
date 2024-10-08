using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{        
    //기본 폰트
    public TMP_FontAsset defaultFont;

    //프리팹 UI
    public GameObject popupUIPanelPrefab;
    public GameObject navBarPrefab;

    private void Start()
    {
        InitFont();
    }

    public void SetPopupCustum(string message, bool isOneButton,
        UnityAction confirmAction, UnityAction cancelAction,
        string okButtonText, string noButtonText)
    {
        NotificationUI script = null;

        if (popupUIPanelPrefab != null)
        {
           script  = popupUIPanelPrefab.GetComponent<NotificationUI>();
        }

        if (script != null)
        {
            script.Init(message, isOneButton, confirmAction, cancelAction, okButtonText, noButtonText);
        }
    }

    public void SetNavBar(bool enable)
    {
        navBarPrefab.SetActive(enable);
    }

    // 로비로 이동 여부 팝업창 (TODO여기 고쳐야됨)
    public void SetExitPopup() {
        SetPopupCustum("로비로 돌아가시겠어요?", false, PhotonManager.Inst.ReConnectTry, null, "네..", "아니요!");
    }


    //나가기 여부 팝업창
    public void SetQuitGamePopup()
    {
        SetPopupCustum("정말로 게임을 종료하시겠어요?", false, GameManager.Inst.QuitGame, null, "네..", "아니요!");
    }

    //연결 실패 팝업 창
    public void ConnctionFailedPopup(bool enable)
    {            
        if (enable)
        {
            SetPopupCustum("네트워크 연결이 원활하지 않습니다.", false, PhotonManager.Inst.ReConnectTry, GameManager.Inst.QuitGame, "재연결시도", "게임종료");
        }
    }

    public void SetInformationPopup(string text)
    {
        SetPopupCustum(text, true, null, null, "확인", null);
    }

    public void SetUnknownError()
    {
        SetPopupCustum("알 수 없는 오류", true, GameManager.Inst.QuitGame, GameManager.Inst.QuitGame, "게임종료", null);
    }

    public void SetPopup(bool enable)
    {
        popupUIPanelPrefab.SetActive(enable);
    }

    public void InitFont()
    {
        TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in allTexts)
        {
            text.font = defaultFont;
        }
    }
}
