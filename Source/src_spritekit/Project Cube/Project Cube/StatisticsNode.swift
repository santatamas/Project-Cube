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
        healthLabel.fontSize = AppConstants.GameFontSize
        
        energyLabel.fontName = AppConstants.FontName
        energyLabel.fontSize = AppConstants.GameFontSize
        
        hungerLabel.fontName = AppConstants.FontName
        hungerLabel.fontSize = AppConstants.GameFontSize
        
        super.init(texture: texture, color: AppConstants.GameBackgroundColor, size: size)
    }
    
    func Initialize() {
        self.addChild(healthLabel)
        self.addChild(healthBar)
        self.addChild(energyLabel)
        self.addChild(energyBar)
        self.addChild(hungerLabel)
        self.addChild(hungerBar)
    }
    
    func ArrangeContent()
    {
        var space:CGFloat = 12
        var barsXcoordinate =  self.frame.width * 0.06 //56%
        var barsYcoordinate = self.frame.height / 2
        var barWidth = Int(self.frame.width * 0.42)
        var barHeight = Int(self.frame.height / 10)
        
        healthLabel.position = CGPoint(x: barsXcoordinate + healthLabel.frame.width / 2, y: barsYcoordinate - 20)
        healthBar.size = CGSize(width: barWidth, height: barHeight)
        healthBar.position = CGPoint(x: barsXcoordinate + healthBar.frame.width / 2, y: healthLabel.position.y - space)
        healthBar.setCurrentPercent(0.2)
        
        energyLabel.position = CGPoint(x: barsXcoordinate + energyLabel.frame.width / 2, y: healthBar.position.y - healthBar.frame.height - space)
        energyBar.size = CGSize(width: barWidth, height: barHeight)
        energyBar.position = CGPoint(x: barsXcoordinate + energyBar.frame.width / 2, y: energyLabel.position.y - space)
        energyBar.setCurrentPercent(0.7)
        
        hungerLabel.position = CGPoint(x: barsXcoordinate + hungerLabel.frame.width / 2, y: energyBar.position.y - energyBar.frame.height - space)
        hungerBar.size = CGSize(width: barWidth, height: barHeight)
        hungerBar.position = CGPoint(x: barsXcoordinate + hungerBar.frame.width / 2, y: hungerLabel.position.y - space)
        hungerBar.setCurrentPercent(0.5)
        
    }

    required init?(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
}