using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SelectedNeuralNetworkScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI overTheButtonText;
    private string neuralNetworkFilePath;

    void Start()
    {
        var newestNeuralNetworkDirectory = GetLatestUpdatedDirectory();
        neuralNetworkFilePath = GetNeuralNetworkFilePath(newestNeuralNetworkDirectory);
        SetNeuralNetworkName();
        Debug.Log(neuralNetworkFilePath);
    }

    public void ChooseFromFileExplorer()
    {
        Debug.Log("choose");
        neuralNetworkFilePath = EditorUtility.OpenFilePanel("Select Neural Network", "./../../../results", "onnx");
        SetNeuralNetworkName();
    }

    public void SetNeuralNetworkName()
    {
        overTheButtonText.text = neuralNetworkFilePath;
        Debug.Log(neuralNetworkFilePath);
    }

    private string GetLatestUpdatedDirectory()
    {
        var dir = new DirectoryInfo("../../results");
        var latestUpdatedDir = dir.GetDirectories()
            .OrderByDescending(x => x.LastWriteTime)
            .Where(x => x.Name != "mapoca")
            .First();
        var path = latestUpdatedDir.FullName;
        return path;
    }

    private string GetNeuralNetworkFilePath(string path)
    {
        var dir = new DirectoryInfo(path);
        var latestUpdatedDir = dir.GetFiles()
            .Where(x => x.Name.Contains(".onnx"))
            .First();
        var neuralNetworkPath = latestUpdatedDir.FullName;
        return neuralNetworkPath;
    }

    public string GetPath()
    {
       // var neuralNetworkPath = PlayerPrefs.GetString("neuralNetworkPath");
        var nnFileName = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss_") + neuralNetworkFilePath.Substring(neuralNetworkFilePath.LastIndexOf("\\") + 1);
        var assetsNNPath = "Assets/NN/" + nnFileName;
        File.Copy(neuralNetworkFilePath, "Assets/NN/DeepCTFv2.onnx", true);
        return assetsNNPath;
    }
}
