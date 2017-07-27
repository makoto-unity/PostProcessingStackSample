using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocalAdapter : MonoBehaviour {

    private UnityEngine.PostProcessing.DepthOfFieldModel dof_;
    private UnityEngine.PostProcessing.DepthOfFieldModel.Settings dof_settings_;

    void Start()
    {
        dof_ = GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile.depthOfField;
        dof_settings_ = dof_.settings;
    }

	void Update ()
    {
        RaycastHit hit;
        var ray = new Ray(transform.position, transform.TransformVector(Vector3.forward));
        if (Physics.Raycast(ray, out hit)) {
            // dof_settings_.focalLength = Mathf.Lerp(dof_settings_.focalLength, hit.distance, 0.01f);
            dof_settings_.focusDistance = Mathf.Lerp(dof_settings_.focusDistance, hit.distance, 0.1f);
            dof_.settings = dof_settings_;
        }
	}
}
