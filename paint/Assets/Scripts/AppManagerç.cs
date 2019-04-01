using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManagerç : MonoBehaviour
{

    public Renderer board;
    public Renderer currentcolor;
    private Texture2D texture;

    private Color mycolor;
    private Ray rayinfo;
    private RaycastHit clickinfo;

    public Texture2D currentbrush;

    private Color erasercolor = new Color(1, 1, 1, 1);
    private bool activarGoma;



    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(2000, 2000);
        board.material.mainTexture = texture;
        currentcolor.material.color = new Color(0,0,0,0);

    }

    // Update is called once per frame
    void Update()
    {
        rayinfo = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayinfo, out clickinfo))
        {
            if (clickinfo.collider.tag == "chroma")
            {
                   if (Input.GetMouseButtonDown(0))
                   {
             
                    Texture2D chromatexture = (Texture2D) clickinfo.collider.GetComponent<Renderer>().material.mainTexture;
                    Vector2 tempcord = clickinfo.textureCoord;
                    tempcord.x *= chromatexture.width;
                    tempcord.y *= chromatexture.height;
                    Color final = chromatexture.GetPixel((int) tempcord.x,(int) tempcord.y);
                        if(final.a != 0)
                        {
                            currentcolor.material.color = final;
                            mycolor = final;

                        }

                    }
            }

            if (Input.GetMouseButton(0))
                {
                    if(clickinfo.collider.tag == "board")
                    {

                        Vector2 tempcord = clickinfo.textureCoord;
                        tempcord.x *= texture.width;
                        tempcord.y *= texture.height;
                            for(int i = 0; i < currentbrush.width; i++)
                    {
                                for(int j = 0; j < currentbrush.height; j++)
                        {
                            Color tempcolor = currentbrush.GetPixel(i, j);
                            if (tempcolor.a != 0)
                            {
                                Vector2 newcord = tempcord;
                                newcord.x += i - (currentbrush.width/2);
                                newcord.y += j - (currentbrush.height/2);
                                if (activarGoma)
                                {
                                    texture.SetPixel((int)newcord.x, (int)newcord.y, erasercolor);
                                }
                                else
                                {
                                    texture.SetPixel((int)newcord.x, (int)newcord.y, mycolor);
                                }
                            }
                        }
                    }
                    texture.Apply();
                }
               
            }
        }
    }


    public void changeBrush(Texture2D brush)
    {
        currentbrush = brush;
    }

    public void setgomaactive(Image image)
    {
        activarGoma = !activarGoma;
        if (activarGoma)
        {
            image.color = Color.green;

        }
        else
        {
            image.color = Color.white;
        }
    }
}
