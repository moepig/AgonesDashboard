﻿using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;

namespace AgonesDashboard.ViewModels.Fleet
{
    public class Detail : AbstractViewModel
    {
        public V1Fleet Fleet { get; set; }
    }
}
