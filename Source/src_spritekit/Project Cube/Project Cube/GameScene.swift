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
    
    override func didMoveToView(view: SKView) {
        self.scaleMode = SKSceneScaleMode.ResizeFill
        self._pmr.position = CGPointMake(250,250)
        self._pmr.size = CGSize(width: 500, height: 500)
        self.addChild(_pmr)
    }
   
    override func update(currentTime: CFTimeInterval) {
        /* Called before each frame is rendered */
        _pmr.Act(Float(currentTime))
    }
}
