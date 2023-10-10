using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class EditorAnimDisplay : EditorWindow
    {
        #region init
        private static EditorAnimDisplay instance;
        [MenuItem("Tools/EditorAnimDisplayWindow")]
        static void PrefabWrapTool()
        {
            //获取当前窗口实例
            instance = EditorWindow.GetWindow<EditorAnimDisplay>();
            instance.Show();
            //ShowUtility() 实体窗口样式
        }
        #endregion

        public AnimationClip[] clips;
        public GameObject player;
        public string Fitter = "";

        private AnimationClip curAnimClip;
        private float Timer = 0;
        private int playCount = 0;
        private Vector2 pos = Vector2.zero;


        void OnGUI()
        {
            player = EditorGUILayout.ObjectField("player", player, typeof(GameObject), true) as GameObject;
            Fitter= EditorGUILayout.TextField("NameFitter",Fitter );
            if (player)
            {
                clips = player.GetComponent<Animator>().runtimeAnimatorController.animationClips;
                pos = GUILayout.BeginScrollView(pos, false, false);
                foreach (var item in clips)
                {

                    if (IsShow(item.name)&&GUILayout.Button(item.name))
                    {
                        PlayAnim(item);
                    }
                }
                GUILayout.EndScrollView();
            }
        }

        private bool IsShow(string clipName)
        {
            if (Fitter=="")
                return true;
            else
            {
                return clipName.ToLower().Contains(Fitter.ToLower());
            }         
        }

        private void PlayAnim(AnimationClip clip)
        {
            Timer = 0;
            playCount = 0;
            curAnimClip = clip;
            Selection.activeObject = clip;
            //DragAndDrop.objectReferences[0] = clip;
            //Debug.Log("yns  play");
        }

        private void Update()
        {
                UpdateAnim(Time.deltaTime);
        }

        private void UpdateAnim(float delta)
        {
            if (curAnimClip != null)
            {
                Timer += delta;
                if (Timer > curAnimClip.length && playCount <2)
                {
                    playCount++;
                    Timer = 0;
                }
                else
                {

                    //动画执行方法

                    curAnimClip.SampleAnimation(player, Timer);
                }
            }
        }
    }
} 
