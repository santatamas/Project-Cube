//
//  ButtonNode.swift
//  Project Cube
//
//  Created by Tamas Santa on 02/03/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
import SpriteKit

enum ButtonState {
    case Enabled
    case Disabled
    case Highlighted
}
class ButtonNode : SKSpriteNode {

    required init?(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    var image: SKSpriteNode = SKSpriteNode()
    var label:SKLabelNode = SKLabelNode(fontNamed: AppConstants.FontName)
    
    override init(texture: SKTexture!, color: UIColor!, size: CGSize) {
        super.init(texture: texture, color: color, size: size)
    }
    
    init(buttonImageName:NSString, buttonText:String)
    {
        super.init()
        self.color = AppConstants.GameBackgroundColor
        self.anchorPoint = CGPoint(x: 0.5, y: 0.5)
        
        self.image = SKSpriteNode(imageNamed: buttonImageName)
        //self.image.size = CGSize(width: self.image.size.width / 2, height: self.image.size.height / 2)
        
        self.image.position = CGPointMake(CGRectGetMidX(self.frame), CGRectGetMidY(self.frame) + 15)
        
        self.label.text = buttonText
        self.label.fontColor = AppConstants.GameFontColor
        self.label.fontSize = 20
        self.label.position = CGPointMake(CGRectGetMidX(self.frame), CGRectGetMidY(self.frame) - 35)
    }
    
    func ArrangeContent() {
        self.image.size = CGSize(width: self.frame.width / 2, height: self.frame.height / 2)
    }
    
    func Initialize()
    {
        self.addChild(image)
        self.addChild(label)
    }
}