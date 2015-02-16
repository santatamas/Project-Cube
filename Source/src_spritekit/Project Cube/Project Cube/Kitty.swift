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
        
        var configPath = NSBundle.mainBundle().pathForResource("kitty", ofType: "json", inDirectory: "Kitty")
        LoadAnimations(configPath!)
        
        CurrentAnimation = GetAnimation("stand")
    }
}