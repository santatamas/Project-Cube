//
//  Actor.swift
//  Project Cube
//
//  Created by Tamas Santa on 15/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation

class Actor {
    
    var Location: Point = Point(x: 0,y: 0)
    var CurrentAnimation:Animation = Animation()
    
    func Act(delta:Float) {
        CurrentAnimation.Act(delta)
    }
    
    func GetCurrentFrameData() -> [[UInt32]] {
        return CurrentAnimation.CurrentFrame.Data
    }
    
    func GetCurrentFrame() -> Frame {
        return CurrentAnimation.CurrentFrame
    }
    
    func GetCurrentWidth() -> UInt32 {
        return CurrentAnimation.CurrentFrame.Width
    }
    
    func GetCurrentHeight() -> UInt32 {
        return CurrentAnimation.CurrentFrame.Height
    }
}