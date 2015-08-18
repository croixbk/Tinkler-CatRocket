using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    Rect zoneLeft = new Rect(0F, 0F, 0.5F, 1F);
    Rect zoneRight = new Rect(0.5F, 0F, 0.5F, 1F);

    Rect areaLeft;
    Rect areaRight;

    public float velocity = 10F;
    //float turnVelocity = 200F;

    void Start()
    {
        //turnVelocity = velocity * 20F;

        float screenWidth = (float)Screen.width;
        float screenHeight = (float)Screen.height;

        areaLeft = new Rect(zoneLeft.x * screenWidth, zoneLeft.y * screenHeight, zoneLeft.width * screenWidth, zoneLeft.height * screenHeight);
        areaRight = new Rect(zoneRight.x * screenWidth, zoneRight.y * screenHeight, zoneRight.width * screenWidth, zoneRight.height * screenHeight);
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - 3F, transform.position.z);

        if (Physics.Raycast(pos, -transform.up, out hit))
            transform.rotation = Quaternion.LookRotation(transform.forward, hit.normal);

        if (Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition;
            OnPress(position);
        }

        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }

    void Move(char dir)
    {
        transform.Rotate(transform.up, ((dir == 'r') ? transform.rotation.y : -transform.rotation.y) * Time.deltaTime, 0);
    }

    void OnPress(Vector3 pos)
    {
        if (areaLeft.Contains(pos))
            Move('l');

        else if (areaRight.Contains(pos))
            Move('r');
    }

    void DrawTouchArea(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    void OnGUI()
    {
        // Só pra visual
        //DrawTouchArea(areaLeft, Color.blue);
        //DrawTouchArea (areaRight, Color.red);
    }

    // NAO DELETAR ESSE SCRIPT
}
