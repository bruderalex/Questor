using System;

namespace Questor.Web.Models
{
    [Serializable]
    public class SelectedEngineVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}