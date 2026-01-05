using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour
{
    public Button leftMoveBtn;
    public Button rightMoveBtn;
    public Button upMoveBtn;
    public Button downMoveBtn;

    void Awake()
    {
        // 버튼에 리스너 등록: 상태 변경 -> 그 상태의 OnMove 호출
        leftMoveBtn.onClick.RemoveAllListeners();
        leftMoveBtn.onClick.AddListener(() =>
        {
            Player player = InGameManager.Instance.player;
            if (player == null || player.controller == null) return;
            player.Move(MoveDirection.Left);
        });

        rightMoveBtn.onClick.RemoveAllListeners();
        rightMoveBtn.onClick.AddListener(() =>
        {
            Player player = InGameManager.Instance.player;
            if (player == null || player.controller == null) return;
            player.Move(MoveDirection.Right);
        });

        upMoveBtn.onClick.RemoveAllListeners();
        upMoveBtn.onClick.AddListener(() =>
        {
            Player player = InGameManager.Instance.player;
            if (player == null || player.controller == null) return;
            player.Move(MoveDirection.Up);
        });

        downMoveBtn.onClick.RemoveAllListeners();
        downMoveBtn.onClick.AddListener(() =>
        {
            Player player = InGameManager.Instance.player;
            if (player == null || player.controller == null) return;
            player.Move(MoveDirection.Down);
        });
    }
}