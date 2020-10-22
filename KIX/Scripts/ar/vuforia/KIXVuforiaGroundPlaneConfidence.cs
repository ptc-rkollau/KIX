/**
 *  Vuforia Ground Plane Confidence
 *  Controller to stabalize groundplance vuforia detection.
 * 
 *  @author : Robin Kollau
 *  @date   : 10-01-2020
 *  @version: 1.0.0 
 * 
 * Comment:
 *  - Possible setups:
 *  ----> Minimum of 4 points that lie at minimum 50cm apart from each other.
 *       confidence_points = 4
 *       minDelta          = 0.50f
 *       
 *  ----> Minimum of 15 points that lie at minimum 10cm apart from each other.     
 *       confidence_points = 15 
 *       minDelta          = 0.10f
 *       
 *       
 *       
  * @author : Robin Kollau
 *  @date   : 03-03-2020
 *  @version: 1.0.1
 *  
 *  Comment:
 *      Cleaned code, added comments.
 *  
 */
#if KIX_VUFORIA
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class KIXVuforiaGroundPlaneConfidence : KIXDispatcher
{
    //event to be fired.
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string ConfidentEvent = "";

    //publics.
    [Tooltip("Amount of successful points before confident.")]
    public int confidence_points = 15;

    [Tooltip("deltaXZ in cm, tested against all points")]
    public float minDelta = 0.10f;

    //list of hit results.
    private List<Vector3> hitResults;
    private bool mayProbeForHits;

    /// <summary>
    /// Start
    /// Called by Unity
    /// </summary>
    void Start()
    {
        hitResults = new List<Vector3>();
        mayProbeForHits = true;
    }

    public void EnableProbing()
    {
        hitResults.Clear();
        mayProbeForHits = true;
    }

    public void DisableProbing()
    {
        mayProbeForHits = false;
    }

    /// <summary>
    /// On Ground Plane Found While Scanning
    /// Should be called by Vuforia Automated Hittest proxy.
    /// Creates a mesh of point of minimum of X distance from each other,
    /// when satisfied it fires a KIX Event -> AREvent.GROUNDPLANE_PLANE_FOUND
    /// </summary>
    /// <param name="result">HitTestResult</param>
    public void OnGroundPlaneFoundWhileScanning(HitTestResult result)
    {
  
        if (result != null)
        {
            
            //check if we may probe.
            if (mayProbeForHits)
            {
                Debug.Log("scanning Confidence.");
                //txt_debug.text = "MAY PROBE point";
                //we need at least 1 hit point result,
                //thus if we have saved none, 
                //save first.
                if (hitResults.Count == 0)
                {
                    hitResults.Add(result.Position);
                }
                else
                {
                    //get result position.
                    Vector3 hitPosition = result.Position;
                    bool mayAddPoint = true;

                    //test again all points saved.
                    for (int i = 0; i < hitResults.Count; ++i)
                    {
                        Vector3 lastPosition = hitResults[i];
                        float deltaY = Mathf.Abs(hitPosition.y - lastPosition.y);

                        //check if height does not differ too much,
                        // this is 10cm.
                        if (deltaY < 0.1f)
                        {
                            //get length of delta x, z
                            Vector2 delta = new Vector2(hitPosition.x - lastPosition.x, hitPosition.z - lastPosition.z);
                            float length = delta.magnitude;

                            //only if length is bigger than 5.0, 
                            //we add our candidate to the list.s
                            if (length < minDelta) // probe for a delta length of 50 cm.
                            {
                                mayAddPoint = false;
                                break;
                            }
                        }
                    }

                    //if all pass, add to list.
                    if (mayAddPoint)
                    {
                        hitResults.Add(result.Position);
                    }
                }

                //check if we have 3 results in our list, 
                //if so, we are more confident... continue to ar placement.
                if (hitResults.Count >= confidence_points)
                {
                    mayProbeForHits = false;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data["pos"] = result.Position;
                    data["rot"] = result.Rotation;

         
                    if(ConfidentEvent != "")
                    {
                       
                        KIXData d = new KIXData();
                        d.Value = data;
                        KIX.Instance.FireEvent(new KIXEvent(ConfidentEvent, d));
                    }
                        

                    hitResults.Clear();
                    //this.enabled = false;

                }
            }
        }
        else
        {
            UnityEngine.Debug.Log("NO HIT");
        }
    }
}
#endif
