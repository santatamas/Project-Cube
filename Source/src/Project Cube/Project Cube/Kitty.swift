//
//  Kitty.swift
//  Project Cube
//
//  Created by Tamas Santa on 15/02/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
class Kitty: Pet {
    
    init(location:Point) {
        super.init()
        
        self.Location = location
        minHealth = 0
        maxHealth = 100
        startHealth = 80
        codeName = "Kitty"
        species = "Feline"
        
        let configPath = NSBundle.mainBundle().pathForResource("kitty", ofType: "json", inDirectory: "Kitty")
        LoadAnimations(configPath!)
        
        CurrentAnimation = GetAnimation("stand")
    }
    
    var elapsedSeconds = 0
    override func Act(delta: Float) {
        super.Act(delta)
 
        if(Int(ElapsedTime / 1000) >= 1)
        {
            ElapsedTime = 0
            elapsedSeconds += 1
            PlayLifeCycleSpeedup();
            if(elapsedSeconds == 15)
            {
                CycleLifeState()
                elapsedSeconds = 0
            }
        }
    }
    
    func CycleLifeState()
    {
        if(self.lifeState == LifeState.Newborn)
        {
            self.lifeState = LifeState.Child
            CurrentAnimation = GetAnimation("stand")
            return
        }
        if(self.lifeState == LifeState.Child)
        {
            self.lifeState = LifeState.Adult
            CurrentAnimation = GetAnimation("stand")
            return
        }
        if(self.lifeState == LifeState.Adult)
        {
            self.lifeState = LifeState.Newborn
            CurrentAnimation = GetAnimation("stand")
            return
        }
    }
    
    func WalkLeft()
    {
        self.Location = Point(x: self.Location.X - 1, y: self.Location.Y)
    }
    
    func WalkRight()
    {
        self.Location = Point(x: self.Location.X + 1, y: self.Location.Y)
    }
    
    func PlayLifeCycleSpeedup()
    {
        if(elapsedSeconds == 6)//3)
        {
            CurrentAnimation = GetAnimation("sleep")
            return
        }
        
        if(elapsedSeconds == 10)//7)
        {
            CurrentAnimation = GetAnimation("walk_left")
            return
        }
        
        if(elapsedSeconds == 11 || elapsedSeconds == 12)
        {
            WalkLeft()
            return
        }
        
        if(elapsedSeconds == 13)//10)
        {
            CurrentAnimation = GetAnimation("walk_right")
            return
        }
        
        if(elapsedSeconds == 14 || elapsedSeconds == 15)
        {
            WalkRight()
            return
        }
        
        if(elapsedSeconds == 16)//13)
        {
            CurrentAnimation = GetAnimation("happy")
            return
        }
    }
}