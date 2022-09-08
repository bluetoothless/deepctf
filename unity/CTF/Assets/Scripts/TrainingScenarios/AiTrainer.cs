using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AiTrainer
{
    public static int test_id = 0; // test level form 0 to 5
    private static Transform transform;
    private static List<TestLevel> level = new List<TestLevel>() {
                                           new TestLevel0(), new TestLevel0(),
                                           new TestLevel0(), new TestLevel0(),
                                           new TestLevel0()};

    // LEVEL 0 zmieniæ na 1-5
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!!!!!


    public static void SetTransform(Transform t)
    {
        transform = t;
    }

    public static void Run()
    {
        level[0].Run();
    }
}
