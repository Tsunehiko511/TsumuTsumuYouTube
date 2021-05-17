using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] BallGenerator ballGenerator = default;
    bool isDragging;
    [SerializeField] List<Ball> removeBalls = new List<Ball>();
    void Start()
    {
        StartCoroutine(ballGenerator.Spawns(40));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 右クリックを押し込んだ時
            OnDragBegin();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 右クリックを離した時
            OnDragEnd();
        }
        else if (isDragging)
        {
            OnDragging();
        }
    }

    void OnDragBegin()
    {
        Debug.Log("ドラッグ開始");
        // マウスによるオブジェクトの判定
        // ・Ray
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit && hit.collider.GetComponent<Ball>())
        {
            Debug.Log("Ballにhitしたよ！");
            Ball ball = hit.collider.GetComponent<Ball>();
            AddRemoveBall(ball);
            isDragging = true;
        }
    }
    void OnDragging()
    {
        // Debug.Log("ドラッグ中");
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit && hit.collider.GetComponent<Ball>())
        {
            Debug.Log("Ballにhitしたよ！");
            Ball ball = hit.collider.GetComponent<Ball>();
            AddRemoveBall(ball);
        }
    }
    void OnDragEnd()
    {
        int removeCount = removeBalls.Count;
        for (int i = 0; i < removeCount; i++)
        {
            Destroy(removeBalls[i].gameObject);
        }
        removeBalls.Clear();
        Debug.Log("ドラッグ終了");
        isDragging = false;
    }

    void AddRemoveBall(Ball ball)
    {
        if (removeBalls.Contains(ball) == false)
        {
            removeBalls.Add(ball);
        }
    }

}
