using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class MouseFollowImage : MonoBehaviour
{
    public Tilemap tilemap;
    public Image followImage; // ���콺�� ����ٴ� �̹���
    private RectTransform followImageRect; // Image�� RectTransform
    private bool isFollowing = false; // �̹����� ����ٴϴ� ����
    public GameObject nowClick;
    void Start()
    {
        followImageRect = followImage.GetComponent<RectTransform>();
        followImage.gameObject.SetActive(false); // ó������ ��Ȱ��ȭ
    }

    void Update()
    {
        if (isFollowing)
        {
            // ���콺 ��ġ�� ����ٴϰ� ����
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                followImage.canvas.transform as RectTransform,
                Input.mousePosition,
                followImage.canvas.worldCamera,
                out mousePosition
            );

            followImageRect.anchoredPosition = mousePosition;
        }
        if (Input.GetMouseButtonDown(0)&& isFollowing) // ���콺 ���� Ŭ��
        {
            // ���콺 Ŭ�� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0; // 2D ȯ�濡���� Z ��ǥ�� 0���� ����

            // ���� ��ǥ�� Ÿ�ϸ� �� ��ǥ�� ��ȯ
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPosition);

            // ���� �߽� ���� ��ǥ ���
            Vector3 cellWorldPosition = tilemap.GetCellCenterWorld(cellPosition);

            Debug.Log($"Cell Position: {cellPosition}, World Position: {cellWorldPosition}");

            // ������Ʈ ����
            SpawnObjectAt(cellWorldPosition);
        }
    }

    public void StartFollow(Button button)
    {
        // ��ư�� �̹����� ������ ����ٴϴ� �̹����� ����
        Image buttonImage = button.transform.GetChild(0).GetComponent<Image>();
        int nown = button.GetComponent<CanvasGetInfo>().thisInfo.GetComponent<GoodSetting>().toothinfo.coin;
        Debug.Log(nown+" "+ PlayerSetting.playerScore);
        if (0 > PlayerSetting.playerScore-nown) return;
        if (buttonImage != null)
        {
            nowClick = button.GetComponent<CanvasGetInfo>().thisInfo;
            followImage.sprite = buttonImage.sprite; // ��ư�� Sprite�� ��������
            followImage.gameObject.SetActive(true); // �̹��� Ȱ��ȭ
            isFollowing = true;
            PlayerSetting.Instance.SubScore(nown);
        }
    }

    public void StopFollow()
    {
        followImage.gameObject.SetActive(false); // �̹��� ��Ȱ��ȭ
        isFollowing = false;
    }
    void SpawnObjectAt(Vector3 position)
    {
        // �������� �ش� ��ġ�� ����
        Instantiate(nowClick, position, Quaternion.identity);
        StopFollow();
    }
}
