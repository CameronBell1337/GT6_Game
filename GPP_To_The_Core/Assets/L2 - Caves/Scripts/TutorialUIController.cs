using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIController : MonoBehaviour
{
    public TrackController initialSpline;

    CanvasGroup ui;
    bool destroy = false;

    private void Awake()
    {
        ui = GetComponent<CanvasGroup>();
        ui.alpha = 0;
    }

    private void Update()
    {
        if (!destroy && initialSpline.active)
        {
            float new_alpha = Mathf.Lerp(ui.alpha, 2, 2 * Time.deltaTime);

            if (new_alpha > 1.95f)
            {
                new_alpha = 2;
            }

            ui.alpha = new_alpha;
        }

        if (initialSpline.tiltDirection != 0)
        {
            destroy = true;
        }

        if (destroy)
        {
            float new_alpha = Mathf.Lerp(ui.alpha, 0, 2 * Time.deltaTime);
            ui.alpha = new_alpha;

            if (new_alpha < 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }
}
