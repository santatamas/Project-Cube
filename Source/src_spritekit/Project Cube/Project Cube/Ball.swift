//
//  Ball.swift
//  Project Cube
//
//  Created by Tamas Santa on 15/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation

class Ball: Actor {
    var directionX = 1
    var directionY = 1
    
    init(location:Point)
    {
        super.init()
        self.Location = location
        self.Animations.append(LoadAnimation())
        self.CurrentAnimation = self.Animations.first!
    }
    
    func LoadAnimation() -> Animation {
        var serializer = AnimationSerializer()
        var unzippedFile = serializer.ReadZip(NSBundle.mainBundle().pathForResource("ball", ofType: "pmz")!)
        
        return serializer.Deserialize(unzippedFile!)
    }
}