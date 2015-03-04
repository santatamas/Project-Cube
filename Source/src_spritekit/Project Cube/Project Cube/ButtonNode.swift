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
        
        self.image = SKSpriteNode(imageNamed: buttonImageName)

        
        self.label.text = buttonText
        self.label.fontColor = AppConstants.GameFontColor
        self.label.fontSize = AppConstants.GameFontSize
        
    }
    
    func ArrangeContent() {
        var desiredWidth = self.frame.width / 2
        var scaledHeight = self.image.frame.height / (self.image.frame.width / desiredWidth)
        self.image.size = CGSize(width: desiredWidth, height: scaledHeight)
        self.image.position = CGPoint(x: 0, y: self.image.frame.height / 3)
        
        self.label.position = CGPoint(x: 0, y: -self.frame.height / 3)
    }
    
    func Initialize()
    {
        self.addChild(image)
        self.addChild(label)
    }
}