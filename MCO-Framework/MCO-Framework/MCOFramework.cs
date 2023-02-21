using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using HarmonyLib;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Reflection;


// 新しいオブジェクトを生成するためのフレームワーク。

//なお、このMODにて生成されたオブジェクトは既存のオブジェクトから流用する。
//

//MGT2 Custom Object Framework
namespace MCOFramework
{
    public class CustomModObject
    {

        /// <summary>
        /// Basic settings can be preconfigured.
        /// Note that if arguments are none, the default value will be default settings.
        /// </summary>
        public CustomModObject()
        {
            this.name = "Default Name";
            this.quality = 0;
            this.price = 1;
            this.uniqueObjectsTooltip_EN = "original";
            this.typeofRoom = 0;
        }

        /// <summary>
        /// Basic settings can be preconfigured.
        /// Note that if arguments are none, the default value will be default settings.
        /// </summary>
        public CustomModObject(int originalObjectNum, string name, int quality, int price, int typeofRoom, string objectTooltip)
        {
            this.prefabsInvOriginalNum = originalObjectNum;
            if (name == null) { this.name = "Default Name"; }
            else { this.name = name; }
            if (quality == 0) { this.quality = 0; }
            else { this.quality = quality; }
            if (price == 0) { this.price = 1; }
            else { this.price = price; }
            this.typeofRoom = typeofRoom;
            this.uniqueObjectsTooltip_EN = objectTooltip;
        }

        /// <summary>
        /// The unique number of the custom object, if you want call the object, try to use it.
        /// メインゲームが抱えるゲーム内プレハブアイテムリストに加えたあと、
        /// 入手することができる固有番号。これで呼び出すことができる。
        /// </summary>
        public int prefabsInvUniqueNum { get; set; }   //prefabsInventar number, Modded

        public int spritesInvUniqueNum { get; set; }
        public int objectsENInvUniqueNum { get; set; }
        public int objectsTooltipENInvUniqueNum { get; set; }
        /// <summary>
        /// The original number of the reference object from main game.
        /// </summary>
        public int prefabsInvOriginalNum { get; set; } //prefabsInventar number, Original
        /// <summary>
        /// The name of the custom object.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The quality of the custom object. Main game uses the value between 0 to 5.
        /// </summary>
        public int quality { get; set; }   //qualitaet
        /// <summary>
        /// The price of the custom object.
        /// </summary>
        public int price { get; set; }     //preis
        /// <summary>
        /// Choose the unlock object at x year what you want to.
        /// </summary>
        public int unlockYear { get; set; }
        /// <summary>
        /// The type of room id of the custom object. e.g. 0 => Corridor, 1 => Development... Check IDs list for more details.
        /// </summary>
        public int motivationRegen { get; set; }
        public int typeofRoom { get; set; }
        /// <summary>
        /// Description of the custom objects. (DEBUG)
        /// </summary>
        //public string objectTooltip { get; set; }
        public Sprite originalSprites { get; set; }
        public string originalObjects_EN { get; set; }
        /// <summary>
        /// Description of the custom objects in English.
        /// </summary>
        public string uniqueObjectsTooltip_EN { get; set; }
        // scripts of main game
        private GameObject main { get; set; }
        private mapScript mapScript { get; set; }
        private GUI_Main gui_Main { get; set; }
        private textScript textScript { get; set; }

        private Menu_BuyInventar menu_BuyInventar { get; set; }

        public void GetScripts()
        {
            main = GameObject.Find("Main");
            mapScript = main.GetComponent<mapScript>();
            gui_Main = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
            textScript = main.GetComponent<textScript>();
            menu_BuyInventar = main.GetComponent<Menu_BuyInventar>();
        }
        /// <summary>
        /// この関数を使用して、既存のオブジェクトのプレハブコンテナに追加のアイテムを加える。
        /// </summary>
        public void AddToPrefabsInv()
        {
            Array.Resize(ref mapScript.prefabsInventar, mapScript.prefabsInventar.Length + 1);    // 既存のオブジェクトのプレハブコンテナに+1する
            //this.prefabsInvUniqueNum = mapScript.prefabsInventar.Length - 1;    //固有の番号を取得
            this.prefabsInvUniqueNum = mapScript.prefabsInventar.Length - 1;    //固有の番号を取得
            Vector3 Position = new Vector3(-50f, -50000f, -50);
            mapScript.prefabsInventar[this.prefabsInvUniqueNum] = GameObject.Instantiate(mapScript.prefabsInventar[this.prefabsInvOriginalNum], Position, Quaternion.identity);
        }
        public void AddToSpritesInv()
        {
            this.spritesInvUniqueNum = gui_Main.inventarSprites.Length;
            this.originalSprites = gui_Main.inventarSprites[this.prefabsInvOriginalNum];
            Array.Resize(ref gui_Main.inventarSprites, gui_Main.inventarSprites.Length + 1);
            gui_Main.inventarSprites[this.spritesInvUniqueNum] = this.originalSprites;
        }

        public void AddToTextsInv()
        {
            this.objectsENInvUniqueNum = textScript.objects_EN.Length;
            //this.name = this.name;                                //コンストラクタで定義しない場合は、ここでnameを定義しなければならない。
            this.originalObjects_EN = textScript.objects_EN[this.prefabsInvOriginalNum];
            Debug.Log("originalObjects_EN :" + originalObjects_EN);
            Array.Resize(ref textScript.objects_EN, textScript.objects_EN.Length + 1);
            if (this.name == null)
            {
                textScript.objects_EN[this.objectsENInvUniqueNum] = this.originalObjects_EN;
            }
            else
            {
                textScript.objects_EN[this.objectsENInvUniqueNum] = this.name;
            }
            this.objectsTooltipENInvUniqueNum = textScript.objectsTooltip_EN.Length;
            if (this.uniqueObjectsTooltip_EN == "original" || this.uniqueObjectsTooltip_EN == "" || this.uniqueObjectsTooltip_EN == null)
            {
                this.uniqueObjectsTooltip_EN = textScript.objectsTooltip_EN[this.prefabsInvOriginalNum];
            }
            Array.Resize(ref textScript.objectsTooltip_EN, textScript.objectsTooltip_EN.Length + 1);
            textScript.objectsTooltip_EN[this.objectsTooltipENInvUniqueNum] = this.uniqueObjectsTooltip_EN;
        }

        public void AddNewFilterToInvMenu(Menu_BuyInventar __instance, string filterName, ref int room)
        {
            if (!Main.CFG_IS_ENABLED.Value) { return; }
            if (typeofRoom != room) { return; }
            var CreateFilter = AccessTools.Method(typeof(Menu_BuyInventar), "CreateFilter");
            CreateFilter.Invoke(__instance, new object[] { filterName, 0 });
        }
        public void AddToInvMenu(Menu_BuyInventar __instance, ref int room)
        {
            if (!Main.CFG_IS_ENABLED.Value) { return; }
            if (typeofRoom != room) { return; }
            var CreateInventarKaufenButton = AccessTools.Method(typeof(Menu_BuyInventar), "CreateInventarKaufenButton");
            CreateInventarKaufenButton.Invoke(__instance, new object[] { prefabsInvUniqueNum });
        }

        public void SetSettingsToPrefabsInv()
        {
            mapScript.prefabsInventar[this.prefabsInvUniqueNum].GetComponent<objectScript>().qualitaet = this.quality;
            mapScript.prefabsInventar[this.prefabsInvUniqueNum].GetComponent<objectScript>().preis = this.price;
            mapScript.prefabsInventar[this.prefabsInvUniqueNum].GetComponent<objectScript>().name = this.name;
            mapScript.prefabsInventar[this.prefabsInvUniqueNum].GetComponent<objectScript>().unlockYear = this.unlockYear;
            mapScript.prefabsInventar[this.prefabsInvUniqueNum].GetComponent<objectScript>().motivationRegen = this.motivationRegen;
        }
    }

    /*
    /// <summary>
    /// あえて残しておきます。コメントにて色々書いています。
    /// </summary>
    public class ObjectMaker : MonoBehaviour
    {
        static CustomModObject lvSpecialDesk = new CustomModObject(53, "Test Desk", 1, 1, "", 1);

        //Modify array of prefabs(items)
        [HarmonyPrefix, HarmonyPatch(typeof(mapScript), "Start")]
        static bool AddNewObjectItems(mapScript __instance)
        {
            if (!Main.CFG_IS_ENABLED.Value){ return true; }

            //苦肉の策で色々やってます。
            //どんなことになってるかというと、Instantiateでクローンを作れるのは良いのですが、内部データ上のみでクローンを作れるわけではないようです。
            //Instantiateでクローンを作成すると、マップ上に現れ、そのデータを参照して新規オブジェクトに近いものを弄くり回せるようになる…というわけです。
            //こんな風になっている状況でも、似たような実例があり、SkyrimのDONOTDELETEの宝箱に近い状態になっています。
            //なので、遥か彼方まで飛ばして確実にクリックできないようにしてあるわけです。ちなみに、クローンをクリックしたらフリーズします。
            lvSpecialDesk.GetScripts();
            //lvSpecialDesk.prefabsInvOriginalNum = 53;
            //lvSpecialDesk.name = "Test Desk";
            lvSpecialDesk.quality = 1010;
            lvSpecialDesk.price = 5000000;
            lvSpecialDesk.AddToPrefabsInv();
            lvSpecialDesk.SetSettingsToPrefabsInv();
                //Debug.Log(lvSpecialDesk.number);
                //setCount++;

                //Modify Color of Desk
                //sharedMaterialsにするとMaterialを共有している、親元も全て変更してしまうので、materialを変更にする。
                //var originalColor = __instance.prefabsInventar[53].GetComponentInChildren<MeshRenderer>().sharedMaterials[0];
                // __instance.prefabsInventar[lvSpecialDesk.number].GetComponentInChildren<MeshRenderer>().sharedMaterials[0] = newMaterial;
                //__instance.prefabsInventar[lvSpecialDesk.number].GetComponentInChildren <MeshRenderer>().material.name = lvSpecialDesk.name;
                //__instance.prefabsInventar[lvSpecialDesk.number].GetComponentInChildren<MeshRenderer>().material.color = new Color(0, 4, 6);
                //__instance.prefabsInventar[lvSpecialDesk.number].GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
                //全部塗りつぶす場合は、childrensで一気に引き抜いてforeachでやる

                //これをやらないと、Readableにはならないので、実行する。
            Texture2D createReadableTexture2D(Texture2D texture2d)
            {
                RenderTexture renderTexture = RenderTexture.GetTemporary(
                texture2d.width,
                texture2d.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

                Graphics.Blit(texture2d, renderTexture);
                RenderTexture previous = RenderTexture.active;
                RenderTexture.active = renderTexture;
                Texture2D readableTextur2D = new Texture2D(texture2d.width, texture2d.height);
                readableTextur2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                readableTextur2D.Apply();
                RenderTexture.active = previous;
                RenderTexture.ReleaseTemporary(renderTexture);
                return readableTextur2D;
            }

            foreach (MeshRenderer children in __instance.prefabsInventar[lvSpecialDesk.prefabsInvUniqueNum].GetComponentsInChildren<MeshRenderer>()) {
                switch (children.name)
                {
                    //デスク本体
                    case "Cube.008":
                        //var clone = Texture2D.Instantiate(children.GetComponentInChildren<Texture2D>());
                        children.material.mainTexture = Instantiate(createReadableTexture2D((Texture2D)children.material.mainTexture));
                        //R G B
                        children.material.color = new Color(7, 4, 0);
                        break;
                    //デスクの取手
                    case "Cube.016":
                        children.material.color = new Color(0, 10, 30);
                        break;
                    //チェア
                    case "Cube.001":
                        children.material.color = new Color(3, 3, 3);
                        break;
                    //コントローラー
                    case "controller4_2015":
                        children.material.color = new Color(0, 1, 0);
                        break;
                    case "Cube":
                        //モニターの枠
                        if (children.material.name == "monitor_2015 (Instance)")
                        {
                            children.material.color = new Color(2, 2, 2);
                        }
                        //キーボード
                        if (children.material.name == "keyboard_2015 (Instance)")
                        {
                            children.material.color = new Color(2, 2, 2);
                        }
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        //Modify array of prefabs(pictures)
        [HarmonyPrefix, HarmonyPatch(typeof(GUI_Main), "Start")]
        static bool AddNewObjectPictures()
        {
            if (!Main.CFG_IS_ENABLED.Value) { return true; }
            lvSpecialDesk.GetScripts();
            lvSpecialDesk.AddToSpritesInv();
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(textScript), "Awake")]
        static void AddNewObjectTexts()
        {
            if (!Main.CFG_IS_ENABLED.Value) { return; }
            lvSpecialDesk.GetScripts();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
            lvSpecialDesk.AddToTextsInv();
        }

        //Inject the test mod Buy Inventory of Dev room
        [HarmonyPostfix, HarmonyPatch(typeof(Menu_BuyInventar), "BUTTON_SelectInventar")]
        static void NewObjectOnDevRoom(Menu_BuyInventar __instance, ref int room)
        {
            Debug.Log("room : " + room);
            lvSpecialDesk.GetScripts();
            lvSpecialDesk.AddToInvMenu(__instance, "Custom Desk", ref room);
        }
        /*
            //Inject object name of mods
            [HarmonyPostfix, HarmonyPatch(typeof(Item_InventarKaufen), "Start")]
            static void InjectNewObjectName(Item_InventarKaufen __instance)
            {
                if (!Main.CFG_IS_ENABLED.Value) { return; }
                Debug.Log(__instance.typ);
                if (__instance.typ == lvSpecialDesk.prefabsInvUniqueNum)
                {
                    Debug.Log("__instance.typ == lvSpecialDesk.number : " + __instance.typ);
                    __instance.uiObjects[0].GetComponent<Text>().text = lvSpecialDesk.name;
                }
            }
        }
    */
}