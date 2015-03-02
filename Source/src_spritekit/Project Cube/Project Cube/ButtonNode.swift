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
    
    var image: SKSpriteNode
    var label:SKLabelNode
    
    init(buttonImageName:NSString, buttonText:String)
    {
        self.image = SKSpriteNode(imageNamed: buttonImageName)
        self.label = SKLabelNode(text: buttonText)
        super.init()
    }
    
    func Initialize()
    {
        self.addChild(image)
        self.addChild(label)
    }
}