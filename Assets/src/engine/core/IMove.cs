﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core
{
    interface IMove
    {
        void MoveStep(float dTime);
    }
}
