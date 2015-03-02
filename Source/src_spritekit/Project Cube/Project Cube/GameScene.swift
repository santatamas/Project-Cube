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
    var _statistics: StatisticsNode = StatisticsNode()
    var _buttons: [ButtonNode] = [ButtonNode](count: 4, repeatedValue: ButtonNode())
    
    var initialized:Bool = false
    
    func Initialize() {
        _pmr.SetBackground(Background().GetCurrentFrameData())
        _pmr.AddActor(_kitty)
        
        self.addChild(_pmr)
        self.addChild(_statistics)
        
        _buttons[0] = ButtonNode()
        _buttons[1] = ButtonNode()
        _buttons[2] = ButtonNode()
        _buttons[3] = ButtonNode()
        
        for button in _buttons {
            self.addChild(button)
        }
        
        initialized = true
    }
    
    override func didMoveToView(view: SKView) {
        scaleMode = SKSceneScaleMode.ResizeFill
        //backgroundColor = UIColor(red: 125/255, green: 140/255, blue: 115/255, alpha: 1)
        backgroundColor = UIColor(red: 255/255, green: 255/255, blue: 255/255, alpha: 1)
        anchorPoint = CGPointMake(0.5,1)
        
        var padding:CGFloat = 10
        var navBarHeight:CGFloat = 45
        
        _pmr.size = CGSize(width: view.frame.width - padding, height: view.frame.width - padding)
        _pmr.position = CGPointMake(0, -view.frame.width / 2 - navBarHeight)
        _pmr.Initialize()
        
        var statisticsHeight = view.frame.height - padding - view.frame.width - navBarHeight / 0.3
        _statistics.size = CGSize(width: view.frame.width - padding, height: statisticsHeight)
        _statistics.position = CGPointMake(0, -view.frame.width - statisticsHeight / 2 - 45)
        _statistics.color = UIColor.greenColor()
        
        var buttonCnt:CGFloat = 0
        var buttonHeight = view.frame.height - _pmr.size.height - statisticsHeight - padding * 6.5
        var buttonSize = CGSize(width: (view.frame.width - padding * 2.5) / 4, height: buttonHeight)
        var startingPositionX = -(view.frame.width / 2) + buttonSize.width / 2 + padding / 2
        var startingPositionY = _statistics.position.y - _statistics.size.height + padding * 1
        
        for button in _buttons {
            button.size = buttonSize
            button.position = CGPointMake(startingPositionX + (buttonCnt * (buttonSize.width + padding / 2)), startingPositionY)
            button.color = UIColor.redColor()
            buttonCnt++
        }
    }
   
    override func update(currentTime: CFTimeInterval) {
        /* Called before each frame is rendered */
        
        var ellapsedmilliseconds = (currentTime - _previousframeTime) * 1000
        _pmr.Act(Float(ellapsedmilliseconds))
        
        _previousframeTime = currentTime
        
    }
}
