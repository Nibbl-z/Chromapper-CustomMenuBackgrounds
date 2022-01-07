using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

namespace Chromapper_CustomMenuBackgrounds
{
    [Plugin("Custom Menu Backgrounds")]
    public class Plugin
    {
        List<string> bgsList = new List<string>();
        public static readonly List<string> ImageExtensions = new List<string> {".JPG", ".JPEG", ".PNG"};

        [Init]
        private void Init()
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }
        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/CustomBackgrounds");

            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + "/CustomBackgrounds", "*.*", SearchOption.AllDirectories);
            

            Debug.Log(allFiles[0]);
            
            foreach(string file in allFiles)
            {
                Debug.Log(file);
                if(ImageExtensions.Contains(Path.GetExtension(file).ToUpperInvariant()))
                {
                    Debug.Log("holy cow");
                    bgsList.Add(file);
                }
            }

            if (arg0.buildIndex == 1)
            {
                GameObject background = GameObject.Find("SongSelectorCanvas").transform.Find("BGImageCanvas").Find("Image").gameObject;
                background.GetComponent<RandomImage>().enabled = false;
                Sprite bgSprite = chooseBGImage();

                background.GetComponent<Image>().sprite = bgSprite;
            }
            if (arg0.buildIndex == 2)
            {
                GameObject background = GameObject.Find("Canvas").transform.Find("BGImageCanvas").Find("Image").gameObject;
                background.GetComponent<RandomImage>().enabled = false;
                Sprite bgSprite = chooseBGImage();

                background.GetComponent<Image>().sprite = bgSprite;
            }
        }

        public Sprite chooseBGImage()
        {
            string[] bgs = bgsList.ToArray();
            int bgToPick = new System.Random().Next(bgs.Length);
            Debug.Log(bgToPick);
            Debug.Log(bgs.Length);
            Debug.Log(bgToPick - 1);
            string file = bgs[bgToPick];
            var bytes = File.ReadAllBytes(file);

            var texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            return sprite;
        }
    }
}
