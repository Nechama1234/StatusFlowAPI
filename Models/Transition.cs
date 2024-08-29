using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatusFlowAPI.Models;

public partial class Transition
{
    public int TransitionId { get; set; }

    public string Name { get; set; } = null!;

    [ForeignKey("FromStatusId")]
    public int FromStatusId { get; set; }

    [ForeignKey("ToStatusId")]
    public int ToStatusId { get; set; }

    //public virtual Status FromStatus { get; set; } = null!;

    //public virtual Status ToStatus { get; set; } = null!;
}
