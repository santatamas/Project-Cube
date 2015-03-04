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
    var _statistics: StatisticsNode = StatisticsNode(texture: nil, color: AppConstants.GameBackgroundColor, size: CGSize(width: 0, height: 0))
    var _buttons: Array<ButtonNode> = Array<ButtonNode>()
    
    var initialized:Bool = false
    
    func Initialize() {
        _pmr.SetBackground(Background().GetCurrentFrameData())
        _pmr.AddActor(_kitty)
        
        self.addChild(_pmr)
        self.addChild(_statistics)
        
        _buttons.append(ButtonNode(buttonImageName: "feed", buttonText: "Feed"))
        _buttons.append(ButtonNode(buttonImageName: "play", buttonText: "Play"))
        _buttons.append(ButtonNode(buttonImageName: "vet", buttonText: "VET"))
        _buttons.append(ButtonNode(buttonImageName: "fight", buttonText: "Fight"))
        
        for button in _buttons {
            self.addChild(button)
            button.Initialize()
        }
        
        _statistics.Initialize()
        
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
        
        
    
        var heightForControls = view.frame.height - _pmr.size.height - navBarHeight - padding
        var statisticsHeight = heightForControls * 0.6
        if(statisticsHeight > 120)
        {
            _statistics.size = CGSize(width: view.frame.width - padding, height: statisticsHeight)
            _statistics.position = CGPointMake(0, -view.frame.width - statisticsHeight / 2 - navBarHeight)
            _statistics.ArrangeContent()
        }
        else
        {
            statisticsHeight = 0 - padding / 2
            _statistics.hidden = true
        }
        
        var buttonCnt:CGFloat = 0
        var buttonHeight = heightForControls - statisticsHeight - padding
        var buttonSize = CGSize(width: (view.frame.width - padding * 2.5) / 4, height: buttonHeight)
        var startingPositionX = -(view.frame.width / 2) + buttonSize.width / 2 + padding / 2
        var startingPositionY = _pmr.position.y - _pmr.frame.height / 2 - statisticsHeight - buttonHeight / 2 - padding
        
        for button in _buttons {
            button.size = buttonSize
            button.position = CGPointMake(startingPositionX + (buttonCnt * (buttonSize.width + padding / 2)), startingPositionY)
            button.ArrangeContent()
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
