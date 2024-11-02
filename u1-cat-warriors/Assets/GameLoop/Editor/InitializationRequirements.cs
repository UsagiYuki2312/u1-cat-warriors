using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLoop
{
    [InitializeOnLoad]
    public class InitializationRequirements
    {
        static InitializationRequirements()
        {
            //state machines:
            StateMachine[] stateMachines = Resources.FindObjectsOfTypeAll<StateMachine>();
            foreach (StateMachine item in stateMachines)
            {
                if (item.GetComponent<Initialization>() == null) item.gameObject.AddComponent<Initialization>();
            }

            //display object:
            DisplayObject[] displayObjects = Resources.FindObjectsOfTypeAll<DisplayObject>();
            foreach (DisplayObject item in displayObjects)
            {
                if (item.GetComponent<Initialization>() == null) item.gameObject.AddComponent<Initialization>();
            }

            //singleton (generics require some hackery to find what we need):
            foreach (GameObject item in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                foreach (Component subItem in item.GetComponents<Component>())
                {
                    //bypass this component if its currently unavailable due to a broken or missing script:
                    if (subItem == null) continue;

                    string baseType;

#if NETFX_CORE
                    baseType = subItem.GetType ().GetTypeInfo ().BaseType.ToString ();
#else
                    baseType = subItem.GetType().BaseType.ToString();
#endif

                    if (baseType.Contains("Singleton") && baseType.Contains("Pixelplacement"))
                    {
                        if (item.GetComponent<Initialization>() == null) item.gameObject.AddComponent<Initialization>();
                        continue;
                    }
                }
            }
        }
    }
}
