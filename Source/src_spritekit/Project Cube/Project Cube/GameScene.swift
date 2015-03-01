//
//  GameScene.swift
//  Project Cube
//
//  Created by Tamas Santa on 11/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import SpriteKit

class GameScene: SKScene {
    
    var _pmr: PixelMatrixRenderer = PixelMatrixRenderer()
    var _kitty = Kitty(location: Point(x: 17, y: 0))
    var _previousframeTime: CFTimeInterval = 0
    
    var initialized:Bool = false
    
    func Initialize() {
        _pmr.SetBackground(Background().GetCurrentFrameData())
        _pmr.AddActor(_kitty)
        _pmr.position = CGPointMake(0,0)
        self.addChild(_pmr)
        initialized = true
    }
    
    override func didMoveToView(view: SKView) {
        scaleMode = SKSceneScaleMode.ResizeFill
        //backgroundColor = UIColor(red: 125/255, green: 140/255, blue: 115/255, alpha: 1)
        //backgroundColor = UIColor(red: 255/255, green: 255/255, blue: 255/255, alpha: 1)
        anchorPoint = CGPointMake(0.5,0.5)
        
        _pmr.size = CGSize(width: view.frame.width, height: view.frame.width)
    }
   
    override func update(currentTime: CFTimeInterval) {
        /* Called before each frame is rendered */
        
        var ellapsedmilliseconds = (currentTime - _previousframeTime) * 1000
        _pmr.Act(Float(ellapsedmilliseconds))
        
        _previousframeTime = currentTime
        
    }
}
