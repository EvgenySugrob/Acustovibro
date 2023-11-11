using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileRectord 
{
    private DateTime _start;

    public ProfileRectord()
    {
        _start = DateTime.Now;
    }

    public TimeSpan timeCount()
    {
        return DateTime.Now - _start;
    }
}
