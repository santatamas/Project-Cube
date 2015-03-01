//
//  Background.swift
//  Project Cube
//
//  Created by Tamas Santa on 01/03/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
class Background : Actor {
    
    override init()
    {
        super.init()
        self.Location = Point(x: 0,y: 0)
        self.Animations.append(LoadAnimation())
        self.CurrentAnimation = self.Animations.first!
    }
    
    func LoadAnimation() -> Animation {
        var serializer = AnimationSerializer()
        var unzippedFile = serializer.ReadZip(NSBundle.mainBundle().pathForResource("night_background", ofType: "pmz", inDirectory: "Kitty/Backgrounds")!)
        
        return serializer.Deserialize(unzippedFile!)
    }
}