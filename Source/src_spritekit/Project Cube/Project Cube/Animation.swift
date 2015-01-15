//
//  Animation.swift
//  Project Cube
//
//  Created by Tamas Santa on 13/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation

class Animation {
    
    var Depth: ColorDepth = ColorDepth.GrayScale
    var ElapsedTime: Float = 0
    var CurrentFrameIndex: Int = 0
    var CurrentFrameDuration: Int = 0
    
    var Frames: Array<Frame> {
        get {
            return _frames
        }
        set(frames)
        {
            _frames = frames
            if (_frames.first != nil)
            {
                CurrentFrameDuration = _frames.first!.Duration
            }
        }
    }
    private var _frames: Array<Frame> = []
    
    func Reset() {
        CurrentFrameIndex = 0;
        ElapsedTime = 0;
        
        if (_frames.first != nil)
        {
            CurrentFrameDuration = _frames.first!.Duration
        }
    }
    
    func Act(delta: Float) {
        ElapsedTime += delta;
        if ElapsedTime >= Float(CurrentFrameDuration * 500)
        {
            ElapsedTime = 0
            CurrentFrameIndex = CurrentFrameIndex < (_frames.count - 1) ? (CurrentFrameIndex + 1) : 0
            CurrentFrameDuration = _frames[CurrentFrameIndex].Duration
        }
    }
    
    func GetCurrentFrame() -> Frame {
        return _frames[CurrentFrameIndex]
    }
    
}