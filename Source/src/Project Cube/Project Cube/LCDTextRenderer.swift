//
//  LCDTextRenderer.swift
//  Project Cube
//
//  Created by Tamas Santa on 18/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
import SpriteKit

class LCDTextRenderer: SKNode {
    
    private var _text = "*Hello-World*"
    private var _totalTime:Double = 0
    var DirectionLeftToRight = true
    var _labelNode = SKLabelNode()

    override init() {
        super.init()
        _labelNode.fontName = "Segment14"
        _labelNode.fontColor = UIColor(red: 53/255, green: 53/255, blue: 53/255, alpha: 1)
        
        var bgText1 = SKLabelNode(text: "*************")
        bgText1.fontName = "Segment14"
        bgText1.fontColor = UIColor(red:116 / 255,green:129 / 255,blue:107 / 255,alpha:0.5)
        bgText1.position = CGPointMake(0,0)
        bgText1.zPosition = 0
        self.addChild(bgText1)
        
        var bgText2 = SKLabelNode(text: "0000000000000")
        bgText2.fontName = "Segment14"
        bgText2.fontColor = UIColor(red:116 / 255,green:129 / 255,blue:107 / 255,alpha:0.5)
        bgText2.position = CGPointMake(0,0)
        bgText2.zPosition = 0
        self.addChild(bgText2)
        
        self.addChild(_labelNode)
        
        _labelNode.zPosition = 5
    }

    required init?(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    func Act(delta:Double) {
        _totalTime += delta;
        if(_totalTime > 150)
        {
            
            if(DirectionLeftToRight)
            {
                var firstChar = _text[_text.startIndex]
                var newString = _text.substringFromIndex(_text.startIndex.advancedBy(1))
                newString.append(firstChar)
                _text = newString
            }
            else
            {
                var lastChar = _text.substringFromIndex(_text.endIndex.predecessor())
                var newString = _text.substringToIndex(_text.endIndex.predecessor())
                
                _text = lastChar + newString
            }
            _labelNode.text = _text
            _totalTime = 0;
        }
    }

}