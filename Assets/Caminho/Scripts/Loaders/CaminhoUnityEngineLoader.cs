﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caminho;
using Caminho.Loaders;
using System.IO;
using System.Text;

public class CaminhoUnityEngineLoader : ICaminhoEngineLoader
{
    bool ICaminhoEngineLoader.Exists(string file)
    {
        return true;
    }

    Stream ICaminhoEngineLoader.LoadFile(string file)
    {
        var assetName = string.Format("Engine/{0}", file);

        var textAsset =
            Resources.Load(assetName, typeof(TextAsset)) as TextAsset;

        if (textAsset != null)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(textAsset.text);
            return new MemoryStream(byteArray);
        }

        return null;
    }

}
