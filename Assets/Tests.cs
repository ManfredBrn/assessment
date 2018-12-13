using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //TEST for OutCode class

        //Trivially Accept
        print("================================");
        print("NEW POINT :");

        Vector2 temp1 = new Vector2(0.5f, 0.3f);
        Vector2 temp2 = new Vector2(0.5f, -0.9f);

        Vector2[] temp_res = new Vector2[2];

        temp_res = OutCode.LineClipping(temp1, temp2);
        print("Point 1 : x = " + temp_res[0].x + " , y = " + temp_res[0].y);
        print("Point 1 : x = " + temp_res[1].x + " , y = " + temp_res[1].y);

        //Trivially Reject
        print("================================");
        print("NEW POINT :");

        temp1.x = 1.5f; temp1.y = 0.1f;
        temp2.x = 1.5f; temp2.y = 2f;

        temp_res = OutCode.LineClipping(temp1, temp2);
        print("Point 1 : x = " + temp_res[0].x + " , y = " + temp_res[0].y);
        print("Point 1 : x = " + temp_res[1].x + " , y = " + temp_res[1].y);

        //Not accept, reject and outside the screen
        print("================================");
        print("NEW POINT :");

        temp1.x = 15f; temp1.y = 0f;
        temp2.x = 0f; temp2.y = 15f;

        temp_res = OutCode.LineClipping(temp1, temp2);
        print("Point 1 : x = " + temp_res[0].x + " , y = " + temp_res[0].y);
        print("Point 1 : x = " + temp_res[1].x + " , y = " + temp_res[1].y);

        //Not accept, reject and inside the screen
        print("================================");
        print("NEW POINT :");

        temp1.x = 0f; temp1.y = 1.2f;
        temp2.x = -1.3f; temp2.y = -1.4f;

        temp_res = OutCode.LineClipping(temp1, temp2);
        print("Point 1 : x = " + temp_res[0].x + " , y = " + temp_res[0].y);
        print("Point 1 : x = " + temp_res[1].x + " , y = " + temp_res[1].y);



        //==============================
        //Test For the Rasterisation
        Rasterisation screen = new Rasterisation(64, 36);
        List<Vector2> result = new List<Vector2>();

        //Test for normal case
        print("================================");
        print("NEW RASTERISATION :");

        temp1 = new Vector2(-1f, 1f);
        temp2 = new Vector2(1f, -1f);

        temp1 = Rasterisation.ViewPortToPixelSpacePointCoord(temp1, screen);
        temp2 = Rasterisation.ViewPortToPixelSpacePointCoord(temp2, screen);

        print("x0 : " + temp1.x + " , y0 : " + temp1.y);
        print("x1 : " + temp2.x + " , y1 : " + temp2.y);

        result = Rasterisation.Breshenhams(temp1, temp2);

        for (int i = 0; i < result.Count; i++)
            print("x : " + result[i].x + " , y : " + result[i].y);

        //Test for negate case
        print("================================");
        print("NEW RASTERISATION :");

        temp1 = new Vector2(-1f, -1f);
        temp2 = new Vector2(1f, 1f);

        temp1 = Rasterisation.ViewPortToPixelSpacePointCoord(temp1, screen);
        temp2 = Rasterisation.ViewPortToPixelSpacePointCoord(temp2, screen);

        print("x0 : " + temp1.x + " , y0 : " + temp1.y);
        print("x1 : " + temp2.x + " , y1 : " + temp2.y);

        result = Rasterisation.Breshenhams(temp1, temp2);

        for (int i = 0; i < result.Count; i++)
            print("x : " + result[i].x + " , y : " + result[i].y);

        //Test for swap case
        print("================================");
        print("NEW RASTERISATION :");

        temp1 = new Vector2(0f, 1f);
        temp2 = new Vector2(1f, -1f);

        temp1 = Rasterisation.ViewPortToPixelSpacePointCoord(temp1, screen);
        temp2 = Rasterisation.ViewPortToPixelSpacePointCoord(temp2, screen);

        print("x0 : " + temp1.x + " , y0 : " + temp1.y);
        print("x1 : " + temp2.x + " , y1 : " + temp2.y);

        result = Rasterisation.Breshenhams(temp1, temp2);

        for (int i = 0; i < result.Count; i++)
            print("x : " + result[i].x + " , y : " + result[i].y);

        //Test for negate swap case
        print("================================");
        print("NEW RASTERISATION :");

        temp1 = new Vector2(0f, -1f);
        temp2 = new Vector2(1f, 1f);

        temp1 = Rasterisation.ViewPortToPixelSpacePointCoord(temp1, screen);
        temp2 = Rasterisation.ViewPortToPixelSpacePointCoord(temp2, screen);

        print("x0 : " + temp1.x + " , y0 : " + temp1.y);
        print("x1 : " + temp2.x + " , y1 : " + temp2.y);

        result = Rasterisation.Breshenhams(temp1, temp2);

        for (int i = 0; i < result.Count; i++)
            print("x : " + result[i].x + " , y : " + result[i].y);










    }
}
