using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasterisation : MonoBehaviour {

    public int resX;
    public int resY;

    public Rasterisation(int x, int y)
    {
        this.resX = x;
        this.resY = y;
    }

    public static Vector2 ViewPortToPixelSpacePointCoord(Vector2 point, Rasterisation screen)
    {
        point.x = (int)(((point.x + 1) / 2) * (screen.resX-1));
        point.y = (int)(((1 - point.y) / 2) * (screen.resY-1));

        return point;
    }

    public static Vector2 pointSwap(Vector2 point)
    {
        int swapvalue = (int)point.x;
        point.x = point.y;
        point.y = swapvalue;

        return point;
    }

    public static List<Vector2> Breshenhams(Vector2 point1, Vector2 point2)
    {

        if (point1.x > point2.x)
        {
            Vector2 tempswap = point1;
            point1 = point2;
            point2 = tempswap;
        }

        float dx = point2.x - point1.x;
        float dy = point2.y - point1.y;
        Vector2 tempValue = new Vector2(0, 0);
        bool swap = false;
        bool negate = false;
        List<Vector2> result = new List<Vector2>();

        //NEGATE
        if (dy < 0)
        {
            point1.y = -point1.y;
            point2.y = -point2.y;

            negate = true;
        }

        //SWAP
        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        if (dy > dx)
        {
            point1 = pointSwap(point1);
            point2 = pointSwap(point2);

            swap = true;
        }

        tempValue = point1;
        dx = point2.x - point1.x;
        dy = point2.y - point1.y;
        /*
        float m = dy / dx;
        print("dx : " + dx);
        print("dy : " + dy);
        print("m : " + m);
        */
        float p = 0;

        //In normal case
        if(!swap)
        {
            while (tempValue.x <= point2.x)
            {
                //print("x : " + tempValue.x + " , y : " + tempValue.y + " , p :" + p);

                if (negate)
                    result.Add(new Vector2(tempValue.x, -tempValue.y));
                else
                    result.Add(tempValue);

                tempValue.x++;

                if (p < 0)
                {
                    p = p + 2 * (int)dy;
                }
                else
                {
                    p = p + 2 * (int)(dy - dx);
                    tempValue.y++;
                }
            }
        }
        else
        {
            //In case of swap
            while (tempValue.y <= point2.y)
            {
                //print("x : " + tempValue.x + " , y : " + tempValue.y + " , p :" + p);

                if (negate)
                    result.Add(new Vector2(tempValue.y, -tempValue.x));
                else
                    result.Add(new Vector2(tempValue.y, tempValue.x));

                tempValue.x++;

                if (p < 0)
                {
                    p = p + 2 * (int)dy;
                }
                else
                {
                    p = p + 2 * (int)(dy - dx);
                    tempValue.y++;
                }
            }
        }
        
        return result;
    }

	// Use this for initialization
}
