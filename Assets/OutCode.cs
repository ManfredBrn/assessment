using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCode : MonoBehaviour
{
    public bool u;
    public bool d;
    public bool l;
    public bool r;

    //Three constructor
    public OutCode(float x, float y)
    {
        this.u = false;
        this.d = false;
        this.l = false;
        this.r = false;

        if (y > 1)
            this.u = true;
        else if (y < -1)
            this.d = true;

        if (x < -1)
            this.l = true;
        else if (x > 1)
            this.r = true;

    }

    public OutCode(bool u, bool d, bool l, bool r)
    {
        this.u = u;
        this.d = d;
        this.l = l;
        this.r = r;
    }

    public OutCode(bool all)
    {
        this.u = all;
        this.d = all;
        this.l = all;
        this.r = all;
    }

    //toString
    public String toString()
    {
        return this.u + " " + this.d + " " + this.l + " " + this.r;
    }

    //operator
    public static bool operator ==(OutCode a, OutCode b)
    {
        return (a.u == b.u) && (a.d == b.d) && (a.l == b.l) && (a.r == b.r);
    }

    public static bool operator !=(OutCode a, OutCode b)
    {
        return !(a == b);
    }


    //Trivially Accept/Reject
    public static bool isTriviallyAccept(OutCode firstP, OutCode secondP)
    {
        /*
        bool res = false;

        if (!(firstP.u | firstP.d | firstP.l | firstP.r | secondP.u | secondP.d | secondP.l | secondP.r))
            res = true;
        */

        return firstP == new OutCode(false) && secondP == new OutCode(false);
    }

    private static OutCode addTwoOutCode(OutCode firstP, OutCode secondP)
    {
        bool u = firstP.u && secondP.u;
        bool d = firstP.d && secondP.d;
        bool l = firstP.l && secondP.l;
        bool r = firstP.r && secondP.r;

        return new OutCode(u, d, l, r);
    }

    public static bool isTriviallyReject(OutCode firstP, OutCode secondP)
    {
        /*
        bool res = false;

        if (sum.u | sum.d | sum.l | sum.r)
            res = true;
        */

        return addTwoOutCode(firstP, secondP) != new OutCode(false);
    }

    //Line Clipping Method
    public static Vector2[] LineClipping(Vector2 point1, Vector2 point2)
    {
        OutCode firstPoint = new OutCode(point1.x, point1.y);
        OutCode secondPoint = new OutCode(point2.x, point2.y);
        bool withoutAProblem = true;

        Vector2[] twoPoint = new Vector2[2];
        twoPoint[0].x = point1.x;
        twoPoint[0].y = point1.y;
        twoPoint[1].x = point2.x;
        twoPoint[1].y = point2.y;

        // print("First Point : " + firstPoint.toString());
        // print("Second Point : " + secondPoint.toString());

        if (isTriviallyAccept(firstPoint, secondPoint))
        {
            print("Trivially Accept");

            return twoPoint;
        }
        else
        {
            print("NOT Trivially Accept");

            if (isTriviallyReject(firstPoint, secondPoint))
            {
                print("Trivially Reject");

                return twoPoint;
            }
            else
            {
                print("NOT Trivially Reject");

                float m = (point2.y - point1.y) / (point2.x - point1.x);

                // y = y1 + m(x - x1)
                // x = x1 + (y - y1)/m


                //First Point
                if (firstPoint != new OutCode(false))
                {
                    //Initialization
                    Vector2[] resTab = new Vector2[2];
                    resTab[0].x = 2;
                    resTab[0].y = 2;
                    resTab[1].x = 2;
                    resTab[1].y = 2;

                    //Where the line cross the edges ?
                    if (firstPoint.u == true)
                    {
                        float resTop = point1.x + (1 - point1.y) / m;

                        if (resTop <= 1 && resTop >= -1)
                        {
                            resTab[0].x = resTop;
                            resTab[0].y = 1;
                        }
                    }
                    else if (firstPoint.d == true)
                    {
                        float resBottom = point1.x + (-1 - point1.y) / m;

                        if (resBottom <= 1 && resBottom >= -1)
                        {
                            resTab[0].x = resBottom;
                            resTab[0].y = -1;
                        }
                    }

                    if (firstPoint.l == true)
                    {
                        float resLeft = point1.y + m * (-1 - point1.x);

                        if (resLeft <= 1 && resLeft >= -1)
                        {
                            resTab[1].x = -1;
                            resTab[1].y = resLeft;
                        }
                    }
                    else if (firstPoint.r == true)
                    {
                        float resRight = point1.y + m * (1 - point1.x);

                        if (resRight <= 1 && resRight >= -1)
                        {
                            resTab[1].x = 1;
                            resTab[1].y = resRight;
                        }
                    }

                    //What's the new point ?
                    //if we have one edge crossed
                    if (((resTab[0].x != 2) && (resTab[1].x == 2)) || ((resTab[0].x == 2) && (resTab[1].x != 2)))
                    {
                        if (resTab[0].x != 2 && (resTab[0].x != point2.x || resTab[0].y != point2.y))
                        {
                            point1.x = resTab[0].x;
                            point1.y = resTab[0].y;
                        }

                        if (resTab[1].x != 2 && (resTab[1].x != point2.x || resTab[1].y != point2.y))
                        {
                            point1.x = resTab[1].x;
                            point1.y = resTab[1].y;
                        }
                    }


                    // print(resTab[0, 0] + " , " + resTab[0, 1]);
                    // print(resTab[1, 0] + " , " + resTab[1, 1]);

                    //if we got two
                    if ((resTab[0].x != 2) && (resTab[1].x != 2))
                    {
                        //It's a bug except in one case : line pass by the angle
                        if ((resTab[0].x == resTab[1].x) && (resTab[0].y == resTab[1].y))
                        {
                            point1.x = resTab[0].x;
                            point1.y = resTab[0].y;
                        }
                        else
                        {
                            print("Got a problem : two different points crossed edges ");
                            withoutAProblem = false;
                        }

                    }

                    //if no one
                    if ((resTab[0].x == 2) && (resTab[1].x == 2))
                    {
                        //Line outside the edge, we don't keep it
                        withoutAProblem = false;
                    }
                }

                //Second Point
                if (secondPoint != new OutCode(false) && withoutAProblem)
                {
                    //Initialization
                    Vector2[] resTab2 = new Vector2[2];
                    resTab2[0].x = 2;
                    resTab2[0].y = 2;
                    resTab2[1].x = 2;
                    resTab2[1].y = 2;

                    //Where the line cross the edges ?
                    if (secondPoint.u == true)
                    {
                        float resTop = point2.x + (1 - point2.y) / m;

                        if (resTop <= 1 && resTop >= -1)
                        {
                            resTab2[0].x = resTop;
                            resTab2[0].y = 1;
                        }
                    }
                    else if (secondPoint.d == true)
                    {
                        float resBottom = point2.x + (-1 - point2.y) / m;

                        if (resBottom <= 1 && resBottom >= -1)
                        {
                            resTab2[0].x = resBottom;
                            resTab2[0].y = -1;
                        }
                    }

                    if (secondPoint.l == true)
                    {
                        float resLeft = point2.y + m * (-1 - point2.x);

                        if (resLeft <= 1 && resLeft >= -1)
                        {
                            resTab2[1].x = -1;
                            resTab2[1].y = resLeft;
                        }
                    }
                    else if (secondPoint.r == true)
                    {
                        float resRight = point2.y + m * (1 - point2.x);

                        if (resRight <= 1 && resRight >= -1)
                        {
                            resTab2[1].x = 1;
                            resTab2[1].y = resRight;
                        }
                    }

                    //What's the new point ?
                    //if we have one edge crossed
                    if (((resTab2[0].x != 2) && (resTab2[1].x == 2)) || ((resTab2[0].x == 2) && (resTab2[1].x != 2)))
                    {
                        if (resTab2[0].x != 2 && (resTab2[0].x != point1.x || resTab2[0].y != point1.y))
                        {
                            point2.x = resTab2[0].x;
                            point2.y = resTab2[0].y;
                        }

                        if (resTab2[1].x != 2 && (resTab2[1].x != point1.x || resTab2[1].y != point1.y))
                        {
                            point2.x = resTab2[1].x;
                            point2.y = resTab2[1].y;
                        }
                    }

                    //if we got two
                    if ((resTab2[0].x != 2) && (resTab2[1].x != 2))
                    {
                        //It's a bug except in one case : line pass by the angle
                        if ((resTab2[0].x == resTab2[1].x) && (resTab2[0].y == resTab2[1].y))
                        {
                            point2.x = resTab2[0].x;
                            point2.y = resTab2[0].y;
                        }
                        else
                        {
                            print("Got a problem : two different points crossed edges ");
                            withoutAProblem = false;
                        }

                    }
                }
            }
        }


        if (withoutAProblem)
        {
            twoPoint[0].x = point1.x;
            twoPoint[0].y = point1.y;
            twoPoint[1].x = point2.x;
            twoPoint[1].y = point2.y;

        }
        else
        {
            twoPoint[0].x = 2;
            twoPoint[0].y = 2;
            twoPoint[1].x = 2;
            twoPoint[1].y = 2;
        }

        return twoPoint;

    }

    
    //public Vector2[] polygonClipping(Vector2[] listPoints)
    //{
        /*
         The idea for this method is to use lineClipping on each segment.
         After that, delete all the point with coordinate greater than 1 or duplicate points
         */

      //  return 0;
    //}


    public bool isAPointOutside(Vector2[] listPoints)
    {
        bool res = false;
        for (int i = 0; i < listPoints.Length; i++)
            if ((listPoints[i].x < -1 || listPoints[i].x > 1) || (listPoints[i].y < -1 || listPoints[i].y > 1))
                res = true;

        return res;
    }


}
