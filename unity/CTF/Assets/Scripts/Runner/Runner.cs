using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public static class Runner
{ 
    private static string Id = "xd";
    private static string Environment_path = ".\\..\\.."; // \deepctf;
    private static bool Resume = false;
    private static bool graphics = false;

    public static bool Check_id()
    {
        return Directory.Exists(Environment_path + "\\results\\" + Id);
    }

    public static void Get_new_Id()
    {
        Id = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + "_id";
    }

    public static string CD()
    {
        return "cd " + Environment_path;
    }

    public static string Buid_environment()
    {
        return ".\\deepCTFenv\\Scripts\\activate";
    }

    public static string MLagents_learn()
    {
        string Configuration_path = "results\\mapoca\\configuration.yaml";
        string Env_path = "BuildV3\\CTF";
        int Number_of_envs = 1;


        string resume = Resume && Check_id() ? " --resume" : "";
        string no_graphics = graphics ? "" : " --no-graphics";
        if (!Resume)
            Get_new_Id();

        return "mlagents-learn " + Configuration_path +
            " --run-id=" + Id + "" +
            //" --env=" + Env_path +
            " --num-envs=" + Number_of_envs.ToString() +
            resume + no_graphics;
    }



    public static void Execute()
    {
        string command =
            CD() + " && " +
            Buid_environment() + " && " +
            MLagents_learn();

        CMD_Execute(command);
    }

    public static void CMD_Execute(string command)
    {
        Debug.Log("CMD.exe /k " + command);
        System.Diagnostics.Process.Start("CMD.exe", "/k \"" + command + "\"");
    }
}
