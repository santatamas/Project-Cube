﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    public class StoppedEvent : CompositePresentationEvent<int>
    {
    }
}
