using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

namespace AutoDesignEnder
{
    public class AutoDesignEnderBehaviour : ModBehaviour
    {
        public override void OnActivate()
        {
            isActive = true;
        }

        public override void OnDeactivate()
        {
            isActive = false;
        }

        volatile bool isActive;

        void Start()
        {
            StartCoroutine(UpdateProjects());
        }

        IEnumerator UpdateProjects()
        {
            while (true)
            {
                if (isActive
                    && GameSettings.Instance != null
                    && GameSettings.Instance.MyCompany != null
                    && GameSettings.Instance.MyCompany.WorkItems != null)
                {
                    var designDocuments = GameSettings.Instance.MyCompany.WorkItems.OfType<DesignDocument>().ToArray();

                    foreach (var designDocument in designDocuments)
                    {
                        if (designDocument.HasFinished && !designDocument.Done)
                        {
                            designDocument.PromoteAction();
                        }
                    }
                }

                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
