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
    var _icons:Array<TwoStateIcon> = Array<TwoStateIcon>()
    var _previousframeTime: CFTimeInterval = 0
    var _lcdRenderer1 = LCDTextRenderer()
    var _lcdRenderer2 = LCDTextRenderer()
    
    override func didMoveToView(view: SKView) {
        self.scaleMode = SKSceneScaleMode.ResizeFill
        self.backgroundColor = UIColor(red: 125/255, green: 140/255, blue: 115/255, alpha: 1)
        
        self._pmr.position = CGPointMake(260,250)
        self._pmr.size = CGSize(width: 500, height: 500)
        self.addChild(_pmr)
        
        for var i = 0; i < 4; i++ {
            var icon = TwoStateIcon(state1: "cake_icon_off.png", state2: "cake_icon_on.png")
            icon.position = CGPointMake(CGFloat(50 + (90 * i)),550)
            _icons.append(icon)
            self.addChild(icon)
        }
        

        _lcdRenderer1.position = CGPointMake(150,600)
        _lcdRenderer2.position = CGPointMake(150,625)
        _lcdRenderer2.DirectionLeftToRight = false
                       
        self.addChild(_lcdRenderer1)
        self.addChild(_lcdRenderer2)
    }
   
    override func update(currentTime: CFTimeInterval) {
        /* Called before each frame is rendered */
        
        var ellapsedmilliseconds = (currentTime - _previousframeTime) * 1000
        _pmr.Act(Float(ellapsedmilliseconds))
        
        for icon in _icons {
            icon.Act(ellapsedmilliseconds)
        }
        
        _lcdRenderer1.Act(ellapsedmilliseconds)
        _lcdRenderer2.Act(ellapsedmilliseconds)
        
        _previousframeTime = currentTime
        
    }
}
