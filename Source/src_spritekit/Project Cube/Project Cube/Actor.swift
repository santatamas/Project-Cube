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
    var Animations: Array<Animation> = Array<Animation>()
    var CurrentAnimation:Animation = Animation()
    
    func Act(delta:Float) {
        CurrentAnimation.Act(delta)
    }
    
    func GetCurrentFrameData() -> [[Int]] {
        return CurrentAnimation.GetCurrentFrame().Data
    }
    
    func GetCurrentFrame() -> Frame {
        return CurrentAnimation.GetCurrentFrame()
    }
    
    func GetCurrentWidth() -> Int {
        return CurrentAnimation.GetCurrentFrame().Width
    }
    
    func GetCurrentHeight() -> Int {
        return CurrentAnimation.GetCurrentFrame().Height
    }
}