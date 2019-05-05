using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace AutoDesignEnder
{
    public class AutoDesignEnderBehaviour : ModBehaviour
    {
        public override void OnActivate()
        {
            Console.WriteLine("Activating");

            timer.Elapsed += UpdateProjects;
            timer.AutoReset = true;
            timer.Start();
        }

        public override void OnDeactivate()
        {
            timer.Stop();
        }

        private readonly Timer timer = new Timer(250);

        private void UpdateProjects(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine("Updating projects");

            if (GameSettings.Instance == null
                || GameSettings.Instance.MyCompany == null
                || GameSettings.Instance.MyCompany.WorkItems == null)
            {
                return;
            }

            var workItems = GameSettings.Instance.MyCompany.WorkItems;
            var designDocuments = workItems.OfType<DesignDocument>();

            foreach (var designDocument in designDocuments)
            {
                if (designDocument.HasFinished && !designDocument.Done)
                {
                    designDocument.PromoteAction();
                }
            }
        }

    }
}
