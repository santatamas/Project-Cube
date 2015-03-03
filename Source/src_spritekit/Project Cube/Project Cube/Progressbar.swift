//
//  Progressbar.swift
//  Project Cube
//
//  Created by Tamas Santa on 02/03/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
import SpriteKit
class Progressbar : SKSpriteNode {
    
    var currentPercent: CGFloat = 0
    var minimumValue: CGFloat = 0
    var maximumValue: CGFloat = 100
    var borderWidth:CGFloat = 1
    
    func setCurrentPercent(value:CGFloat)
    {
        if(value >= minimumValue && value <= maximumValue)
        {
            currentPercent = value
            texture = RenderTexture()
        }
    }
    
    func RenderTexture() -> SKTexture
    {
        UIGraphicsBeginImageContext(CGSizeMake(self.frame.width, self.frame.height))
        var ctx:CGContextRef = UIGraphicsGetCurrentContext()
        
        // Render background rectangle
        AppConstants.GameForegroundColor.setFill()
        CGContextFillRect(ctx, CGRectMake(0, 0, self.frame.width, self.frame.height));
        
        // Render inverted progressbar with border
        AppConstants.GameBackgroundColor.setFill()
        var progressBarWidth:Int = Int(self.frame.width - ((self.frame.width - borderWidth * 2) * currentPercent))
        CGContextFillRect(ctx, CGRectMake(self.frame.width - CGFloat(progressBarWidth) - borderWidth, 0 + borderWidth, CGFloat(progressBarWidth), self.frame.height - borderWidth * 2));
        
        var textureImage:UIImage = UIGraphicsGetImageFromCurrentImageContext()
        UIGraphicsEndImageContext()
        
        var texture:SKTexture = SKTexture(image: textureImage)
        texture.filteringMode = SKTextureFilteringMode.Nearest
        
        return texture
    }
}