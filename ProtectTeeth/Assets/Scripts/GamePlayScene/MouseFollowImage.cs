using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class MouseFollowImage : MonoBehaviour
{
    public Tilemap tilemap;
    public Image followImage; // 마우스를 따라다닐 이미지
    private RectTransform followImageRect; // Image의 RectTransform
    private bool isFollowing = false; // 이미지가 따라다니는 상태
    public GameObject nowClick;
    void Start()
    {
        followImageRect = followImage.GetComponent<RectTransform>();
        followImage.gameObject.SetActive(false); // 처음에는 비활성화
    }

    void Update()
    {
        if (isFollowing)
        {
            // 마우스 위치를 따라다니게 설정
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                followImage.canvas.transform as RectTransform,
                Input.mousePosition,
                followImage.canvas.worldCamera,
                out mousePosition
            );

            followImageRect.anchoredPosition = mousePosition;
        }
        if (Input.GetMouseButtonDown(0)&& isFollowing) // 마우스 왼쪽 클릭
        {
            // 마우스 클릭 위치를 월드 좌표로 변환
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0; // 2D 환경에서는 Z 좌표를 0으로 고정

            // 월드 좌표를 타일맵 셀 좌표로 변환
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPosition);

            // 셀의 중심 월드 좌표 계산
            Vector3 cellWorldPosition = tilemap.GetCellCenterWorld(cellPosition);

            Debug.Log($"Cell Position: {cellPosition}, World Position: {cellWorldPosition}");

            // 오브젝트 생성
            SpawnObjectAt(cellWorldPosition);
        }
    }

    public void StartFollow(Button button)
    {
        // 버튼의 이미지를 가져와 따라다니는 이미지로 설정
        Image buttonImage = button.transform.GetChild(0).GetComponent<Image>();
        int nown = button.GetComponent<CanvasGetInfo>().thisInfo.GetComponent<GoodSetting>().toothinfo.coin;
        Debug.Log(nown+" "+ PlayerSetting.playerScore);
        if (0 > PlayerSetting.playerScore-nown) return;
        if (buttonImage != null)
        {
            nowClick = button.GetComponent<CanvasGetInfo>().thisInfo;
            followImage.sprite = buttonImage.sprite; // 버튼의 Sprite를 가져오기
            followImage.gameObject.SetActive(true); // 이미지 활성화
            isFollowing = true;
            PlayerSetting.Instance.SubScore(nown);
        }
    }

    public void StopFollow()
    {
        followImage.gameObject.SetActive(false); // 이미지 비활성화
        isFollowing = false;
    }
    void SpawnObjectAt(Vector3 position)
    {
        // 프리팹을 해당 위치에 생성
        Instantiate(nowClick, position, Quaternion.identity);
        StopFollow();
    }
}
