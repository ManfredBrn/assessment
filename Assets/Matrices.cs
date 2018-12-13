using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3[] cube = new Vector3[8];
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);
        cube[2] = new Vector3(-1, -1, 1);
        cube[3] = new Vector3(1, -1, 1);
        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        printVertices("start", cube);

        //////////////////////
        //ROTATION

        Vector3 startingAxis = new Vector3(15, -1, -1);
        startingAxis.Normalize();
        Quaternion rotation = Quaternion.AngleAxis(-35, startingAxis);
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(new Vector3(0, 0, 0),
                            rotation,
                            Vector3.one);

        print("rotation Matrix \n" + rotationMatrix.ToString());

        Vector3[] imageAfterRotation = transformVertices(cube, rotationMatrix);
        printVertices("after rotation", imageAfterRotation);

        ///////////////
        //SCALE

        Vector3 scale = new Vector3(15, 1, 1);
        Matrix4x4 scaleMatrix =
            Matrix4x4.TRS(new Vector3(0, 0, 0),
                            Quaternion.identity,
                            scale);

        print("scale Matrix \n" + scaleMatrix.ToString());

        Vector3[] imageAfterScaling = transformVertices(imageAfterRotation, scaleMatrix);
        printVertices("after scaling", imageAfterScaling);

        ///////////////////
        //TRANSLATION

        Vector3 trans = new Vector3(0, -4, 0);
        Matrix4x4 transMatrix =
            Matrix4x4.TRS(trans,
                            Quaternion.identity,
                            Vector3.one);

        print("trans Matrix \n" + transMatrix.ToString());

        Vector3[] imageAfterTranslation = transformVertices(imageAfterScaling, transMatrix);
        printVertices("after translation", imageAfterTranslation);

        ///////////////////
        //TRS

        Matrix4x4 tucMatrix = transMatrix * scaleMatrix * rotationMatrix;



        print("trs Matrix \n" + tucMatrix.ToString());

        Vector3[] imageAfterTuc = transformVertices(cube, tucMatrix);
        printVertices("after trs", imageAfterTuc);

        ///////////////////
        //Camera

        Vector3 position = new Vector3(17, 2, 49);
        Vector3 lookat = new Vector3(-1, 15, 1);
        Vector3 up = new Vector3(0, -1, 15);

        Vector3 forward = lookat - position;

        forward.Normalize();
        up.Normalize();

        //print("forward \n" + forward.ToString());
        //print("up \n" + up.ToString());


        Quaternion cameraRotation = Quaternion.LookRotation(forward, up);

        Matrix4x4 viewMatrix =
            Matrix4x4.TRS(-position,
                    cameraRotation,
                    Vector3.one);

        print("Viewing Matrix \n" + viewMatrix.ToString());

        Vector3[] imageAfterViewing = transformVertices(imageAfterTuc, viewMatrix);
        printVertices("after view", imageAfterViewing);

        /////////////////
        //Projection

        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(110, 16 / 9, 1, 1000);

        print("Projection Matrix\n" + projectionMatrix);

        Vector3[] imageAfterProjection = transformVertices(imageAfterViewing, projectionMatrix);
        printVertices("after projection", imageAfterProjection);

        /////////////////
        //Projection by hand

        //Matrix4x4 projectionMatrixByHand = new Matrix4x4(
        //    new Vector4(1, 0, 0, 0),
        //    new Vector4(0, 1, 0, 0),
        //    new Vector4(0, 0, 1, -1),
        //    new Vector4(0, 0, 0, 0));

        //print("Projection Matrix by Hand\n" + projectionMatrixByHand);
        
        //Vector3[] imageAfterProjectionByHand = transformVertices(imageAfterViewing, projectionMatrixByHand);
        //printVertices("after projection by hand", imageAfterProjectionByHand);

        ///////////////////
        //TRSVP

        Matrix4x4 trsvpMatrix = projectionMatrix * viewMatrix * transMatrix * scaleMatrix * rotationMatrix;

        print("trsvp Matrix \n" + trsvpMatrix.ToString());

        Vector3[] imageAfterTrsvp = transformVertices(cube, trsvpMatrix);
        printVertices("after trs", imageAfterTrsvp);

    }

    private void printVertices(String msg, Vector3[] vertices)
    {
        String output = string.Copy(msg + ": \n");
        for (int i = 0; i < vertices.Length; i++)
        {
            output += " " + vertices[i].x + "  ,  " + vertices[i].y + "  ,  " + vertices[i].z + "\n";

        }
        print(output);
    }

    private Vector3[] transformVertices(Vector3[] vertices, Matrix4x4 transformMatrix)
    {
        Vector3[] output = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector4 vertHomog = new Vector4(vertices[i].x, vertices[i].y, vertices[i].z, 1);
            Vector4 imageHomog = transformMatrix * vertHomog;
            output[i] = new Vector3(imageHomog.x, imageHomog.y, imageHomog.z);
        }
        return output;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

