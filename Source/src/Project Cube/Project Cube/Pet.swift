//
//  Pet.swift
//  Project Cube
//
//  Created by Tamas Santa on 15/02/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation

class Pet : Actor {
    
    var animationDictionary = Dictionary<LifeState, Dictionary<String, Animation>>()
    var lifeState = LifeState.Newborn
    var codeName:String = ""
    var species:String = ""
    var age = 0
    var minHealth = 0
    var maxHealth = 100
    
    func GetAnimation(name:String) -> Animation {
        var animationsForCurrentLifeState = animationDictionary[lifeState]!
        return animationsForCurrentLifeState[name]!
    }
    
    override func Act(delta: Float) {
        super.Act(delta)
    }
    
    func setLifeState(state:LifeState)
    {
        self.lifeState = state
        self.CurrentAnimation = GetAnimation(CurrentAnimation.Name)
    }
    
    func WalkLeft()
    {
        self.Location = Point(x: self.Location.X - 1, y: self.Location.Y)
    }
    
    func WalkRight()
    {
        self.Location = Point(x: self.Location.X + 1, y: self.Location.Y)
    }
}