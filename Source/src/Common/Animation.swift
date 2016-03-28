//
//  Animation.swift
//  Project Cube
//
//  Created by Tamas Santa on 13/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation

class Animation {
    
    var Name = ""
    var ElapsedTime: Float = 0
    var CurrentFrameIndex = 0
    
    var CurrentFrameDuration: UInt32 {
        get
        {
            return _frames[CurrentFrameIndex].Duration;
        }
    }
    
    var CurrentFrame: Frame {
        get
        {
            return _frames[CurrentFrameIndex]
        }
    }
    
    var Frames: Array<Frame> {
        get {
            return _frames
        }
        set(frames)
        {
            _frames = frames
        }
    }
    private var _frames: Array<Frame> = []
    
    func Reset() {
        CurrentFrameIndex = 0;
        ElapsedTime = 0;
    }
    
    func Act(delta: Float) {
        ElapsedTime += delta;
        if ElapsedTime >= Float(CurrentFrameDuration * 1000)
        {
            ElapsedTime = 0
            CurrentFrameIndex = CurrentFrameIndex < (_frames.count - 1) ? (CurrentFrameIndex + 1) : 0
        }
    }
}