//
//  GameScene.swift
//  Project Cube
//
//  Created by Tamas Santa on 11/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import SpriteKit

class GameScene: SKScene {
    
    override func didMoveToView(view: SKView) {
        self.scaleMode = SKSceneScaleMode.ResizeFill
    }
    
    func RenderRandomMatrixSprite() {
        UIGraphicsBeginImageContext(CGSizeMake(500, 500))
        var ctx:CGContextRef = UIGraphicsGetCurrentContext()
        
        for var i = 1; i < 50; i++ {
            for var j = 1; j < 50; j++ {
            
            UIColor(red:GetRandomFloat(),green:GetRandomFloat(),blue:GetRandomFloat(),alpha:1.0).setFill()
            CGContextFillRect(ctx, CGRectMake(CGFloat(i*7), CGFloat(j*7), 5, 5));
            
        }}

        var textureImage:UIImage = UIGraphicsGetImageFromCurrentImageContext()
        UIGraphicsEndImageContext()
        
        var texture:SKTexture = SKTexture(image: textureImage)
        texture.filteringMode = SKTextureFilteringMode.Nearest
        var bg:SKSpriteNode = SKSpriteNode(texture: texture)

        bg.position = CGPoint(x: 250, y: 250)
        self.addChild(bg)
    }
    
    func GetRandomFloat() -> CGFloat{
        let arc4randoMax:Double = 0x100000000
        let upper = 255.0
        let lower = 0.0
        return CGFloat((Double(arc4random()) / arc4randoMax) * (upper - lower) + lower) / 255.0
    }

   
    override func update(currentTime: CFTimeInterval) {
        /* Called before each frame is rendered */
        self.removeAllChildren()
        RenderRandomMatrixSprite()
    }
}
