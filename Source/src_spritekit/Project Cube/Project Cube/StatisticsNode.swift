//
//  StatisticsNode.swift
//  Project Cube
//
//  Created by Tamas Santa on 02/03/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
import SpriteKit
class StatisticsNode : SKSpriteNode {
    
    var healthLabel: SKLabelNode = SKLabelNode(text: "Health")
    var healthBar: Progressbar = Progressbar(color: AppConstants.GameBackgroundColor, size: CGSize(width: 150, height: 15))
    
    var energyLabel: SKLabelNode = SKLabelNode(text: "Energy")
    var energyBar: Progressbar = Progressbar(color: AppConstants.GameBackgroundColor, size: CGSize(width: 150, height: 15))
    
    var hungerLabel: SKLabelNode = SKLabelNode(text: "Hunger")
    var hungerBar: Progressbar = Progressbar(color: AppConstants.GameBackgroundColor, size: CGSize(width: 150, height: 15))
    
    override init(texture: SKTexture!, color: UIColor!, size: CGSize) {

        healthLabel.fontName = AppConstants.FontName
        healthLabel.fontSize = 20
        healthBar.setCurrentPercent(0.2)
        
        energyLabel.fontName = AppConstants.FontName
        energyLabel.fontSize = 20
        energyBar.setCurrentPercent(0.7)
        
        hungerLabel.fontName = AppConstants.FontName
        hungerLabel.fontSize = 20
        hungerBar.setCurrentPercent(0.5)
        
        
        super.init(texture: texture, color: AppConstants.GameBackgroundColor, size: size)
    }
    
    func Initialize() {
        self.addChild(healthLabel)
        self.addChild(healthBar)
        self.addChild(energyLabel)
        self.addChild(energyBar)
        self.addChild(hungerLabel)
        self.addChild(hungerBar)
        
        healthLabel.position = CGPoint(x: 0, y: 0)
        healthBar.position = CGPoint(x: 0, y: -20)
        
        energyLabel.position = CGPoint(x: 0, y: 25)
        energyBar.position = CGPoint(x: 0, y: 5)
        
        hungerLabel.position = CGPoint(x: 0, y: 50)
        hungerBar.position = CGPoint(x: 0, y: 30)
    }

    required init?(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
}