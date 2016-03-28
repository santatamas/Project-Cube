//
//  GameScene.swift
//  Project Cube
//
//  Created by Tamas Santa on 11/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import SpriteKit

class GameScene: SKScene {
    
    var _matrix: PixelMatrixRenderer = PixelMatrixRenderer()
    var _previousframeTime: CFTimeInterval = 0
    
    func Initialize() {
        let kitty = PetFactory().Create("Kitty")
        _matrix.AddActor(kitty)
        _matrix.position = CGPointMake(0,150)
        _matrix.DebugMode = true
        self.addChild(_matrix)
    }
    
    override func didMoveToView(view: SKView) {
        scaleMode = SKSceneScaleMode.ResizeFill
        backgroundColor = UIColor(red: 125/255, green: 140/255, blue: 115/255, alpha: 1)
        backgroundColor = UIColor(red: 255/255, green: 255/255, blue: 255/255, alpha: 1)
        anchorPoint = CGPointMake(0.5,0.5)
        
        _matrix.size = CGSize(width: view.frame.width, height: view.frame.width)
    }
   
    override func update(currentTime: CFTimeInterval) {
        /* Called before each frame is rendered */
        
        let ellapsedmilliseconds = (currentTime - _previousframeTime) * 1000
        _matrix.Act(Float(ellapsedmilliseconds))
        _previousframeTime = currentTime
        
    }
}
