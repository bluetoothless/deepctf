using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

public static class Runner
{ 
    private static string Id = "2022-10-12_07-56-29_id";
    private static string Environment_path = ".\\..\\.."; // \deepctf;
    private static bool Resume = true;
    private static bool graphics = false;
    private static Process process;

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
        string Configuration_path = "results\\mapoca\\newconfiguration.yaml";
        string Env_path = "SuperBuild1.0\\CTF";
        int Number_of_envs = 5;


        string resume = Resume && Check_id() ? " --resume" : "";
        string no_graphics = graphics ? "" : " --no-graphics";
        if (!Resume)
            Get_new_Id();

        return "mlagents-learn " + Configuration_path +
            " --run-id=" + Id + "" +
            " --env=" + Env_path +
            " --num-envs=" + Number_of_envs.ToString() +
            resume + no_graphics;
    }



    public static void Execute()
    {
        string command =
            CD() + " ; " +
            Buid_environment() + " ; " +
            MLagents_learn();

        CMD_Execute(command);
    }

    public static void CMD_Execute(string command)
    {
        process = new Process();
        process.StartInfo.FileName = "powershell.exe";
        process.StartInfo.Arguments = command;
        UnityEngine.Debug.Log("powershell.exe " + command);
        process.Start();
    }

    public static void Finish()
    {
        UnityEngine.Debug.Log("Finishing... process name: " + process.ProcessName 
            +  ", process ID: " + process.Id);
        //var closingProcess = new Process();
        //closingProcess.StartInfo.FileName = "taskkill";
        //closingProcess.StartInfo.Arguments = "/F /PID " + process.Id;
        //closingProcess.StartInfo.CreateNoWindow = true;
        //closingProcess.Start();
        //process.Kill();
        UnityEngine.Debug.Log("Closed");
    }
}
