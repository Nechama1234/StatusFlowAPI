using System;
using System.Collections.Generic;

namespace StatusFlowAPI.Models;

public partial class Status
{
   public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsInitial { get; set; }
    public bool? IsOrphan { get; set; }
    public bool? IsFinal { get; set; }


    //public virtual ICollection<Transition> TransitionFromStatuses { get; set; } = new List<Transition>();

    //public virtual ICollection<Transition> TransitionToStatuses { get; set; } = new List<Transition>();
}
